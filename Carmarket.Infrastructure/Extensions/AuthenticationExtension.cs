using Carmarket.Infrastructure.Certificate;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;

namespace Carmarket.Infrastructure.Extensions
{
    public static class AuthenticationExtension
    {
        public static void ConfigureAuthetication(this IServiceCollection services, string validCertificatePath, string validCertificatePassword)
        {
            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate(options =>
                {
                    options.RevocationMode = X509RevocationMode.NoCheck;
                    options.AllowedCertificateTypes = CertificateTypes.All;
                    options.Events = new CertificateAuthenticationEvents
                    {
                        OnCertificateValidated = context =>
                        {
                            var validationService = context.HttpContext.RequestServices.GetService<CertificateValidationService>();
                            if (validationService != null && validationService.ValidateCertificate(validCertificatePath, validCertificatePassword, context.ClientCertificate))
                            {
                                Console.WriteLine("Success");
                                context.Success();
                            }
                            else
                            {
                                Console.WriteLine("invalid cert");
                                context.Fail("invalid cert");
                            }

                            return System.Threading.Tasks.Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();
        }
    }
}
