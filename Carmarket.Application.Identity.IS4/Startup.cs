// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Carmarket.Application.Identity.IS4.Data;
using Carmarket.Application.Identity.IS4.Services;
using Carmarket.Domain.Identity;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Carmarket.Application.Identity.IS4
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback += CertificateHandler;

            // reverse proxy
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
                options.RequireHeaderSymmetry = false;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            // view
            services.AddControllersWithViews();

            // Identity Server
            services.AddTransient<IProfileService, ProfileService>();
            var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = Configuration.GetConnectionString("CarmarketIdentityDb");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly)));

            services.AddIdentity<CarmarketIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // IS4 SSL
            var certificate = FindCertificate();

            var builder = services.AddIdentityServer(options =>
            {
                //options.EmitStaticAudienceClaim = true;
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.IssuerUri = "http://carmarket.identity";
            })
                .AddSigningCredential(certificate)
                .AddJwtBearerClientAuthentication()
                .AddConfigurationStore(opt =>
                {
                    opt.ConfigureDbContext = c => c.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore(opt =>
                {
                    opt.ConfigureDbContext = o => o.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
                    opt.EnableTokenCleanup = true;
                })
                .AddAspNetIdentity<CarmarketIdentityUser>();

            services.AddLocalApiAuthentication();
        }

        public void Configure(IApplicationBuilder app)
        {
            // reverse proxy
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
           
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private X509Certificate2 FindCertificate()
        {
            var certificatePath = "/https/carmarket.identity.pfx";
            if (!File.Exists(certificatePath))
            {
                Log.Error($"FindCertificate(). Certificate not exists. Path: {certificatePath}");
                return null;
            }

            Log.Information("FindCertificate().Certificate file exists. Load certificate");
            var certificate = new X509Certificate2(certificatePath, "mypass123");
            Log.Information("FindCertificate().Certificate file loaded");
            return certificate;
        }

        private bool CertificateHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            Log.Error("SSL certificate error");
            Log.Error($"SSL certificate error sender: {sender}");
            Log.Error($"SSL certificate error certificate: {certificate}");
            Log.Error($"SSL certificate error chain: {chain}");
            Log.Error($"SSL certificate error policies: {sslPolicyErrors}");
            return true;
        }
    }
}
