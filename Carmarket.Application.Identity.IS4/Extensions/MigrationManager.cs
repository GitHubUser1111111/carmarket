using Carmarket.Application.Identity.IS4.Data;
using Carmarket.Domain.Identity;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;

namespace Carmarket.Application.Identity.IS4.Extensions
{
    public static class MigrationManager
    {
        public static void MigrateDatabase(IServiceScope scope)
        {
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
            scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
            
            using var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            try
            {
                context.Database.Migrate();

                if (!context.Clients.Any())
                {
                    foreach (var client in InMemoryConfig.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in InMemoryConfig.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var apiScope in InMemoryConfig.GetApiScopes())
                    {
                        context.ApiScopes.Add(apiScope.ToEntity());
                    }

                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in InMemoryConfig.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Log errors or do anything you think it's needed
                throw;
            }

            // add admin role
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var adminRoleExists = roleManager.RoleExistsAsync("admin").Result;

            if (!adminRoleExists)
            {
                var adminRole = new IdentityRole("admin");
                var adminRoleResult = roleManager.CreateAsync(adminRole).Result;
                if(!adminRoleResult.Succeeded)
                    throw new Exception($"Create admin role exception. {adminRoleResult.Errors.First().Description}");

                var adminClaimsResult = roleManager.AddClaimAsync(adminRole, new Claim(JwtClaimTypes.Role, "admin")).Result;
            }


            // add admin user
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CarmarketIdentityUser>>();
            var admin = userManager.FindByNameAsync("admin").Result;
            if (admin == null)
            {
                admin = new CarmarketIdentityUser()
                {
                    UserName = "admin",
                    PhoneNumber = "+1235542332",
                    Email = "admin@carmarket.com",
                    EmailConfirmed = true,
                    SubjectId = "admin"
                };

                var adminResult = userManager.CreateAsync(admin, "Admin123!").Result;
                if (!adminResult.Succeeded)
                    throw new Exception($"Create admin user exception. {adminResult.Errors.First().Description}");

                var adminRoleResult = userManager.AddToRoleAsync(admin, "admin").Result;

                if (!adminRoleResult.Succeeded)
                    throw new Exception($"Assign role to admin exception. {adminRoleResult.Errors.First().Description}");

                var adminClaimsResult = userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Role, "admin"),
                    new Claim(JwtClaimTypes.Gender, "male"),
                    new Claim(JwtClaimTypes.NickName, "carmarketmaster"),
                }).Result;

                if (!adminClaimsResult.Succeeded)
                    throw new Exception($"Assign claims to admin exception. {adminClaimsResult.Errors.First().Description}");
            }
        }
    }
}
