using System.Security.Cryptography.X509Certificates;

namespace Carmarket.Infrastructure.Certificate
{
    public class CertificateValidationService
    {
        public bool ValidateCertificate(string validCertificatePath, string validCertificatePassword, X509Certificate2 certificate)
        {
            if (!File.Exists(validCertificatePath))
            {
                throw new ArgumentException(validCertificatePath);
            }

            var validCertificate = new X509Certificate2(validCertificatePath, validCertificatePassword);
            return certificate.Thumbprint == validCertificate.Thumbprint;
        }
    }
}
