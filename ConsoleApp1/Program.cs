// See https://aka.ms/new-console-template for more information
using CertificateManager;
using ConsoleApp1;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Hello, World!");
var sp = new ServiceCollection()
       .AddCertificateManager()
       .BuildServiceProvider();
Class1._cc = sp.GetService<CreateCertificates>();

var domains = new Dictionary<string, string>();
domains.Add("carmarket.identity", "mypass123");
domains.Add("carmarket.users", "mypass123");
foreach (var domainItem in domains)
{
    var domain = domainItem.Key;
    var password = domainItem.Value;
    var rsaCert = Class1.CreateRsaCertificate($"{domain}", 10);
    var iec = sp.GetService<ImportExportCertificate>();
    var rsaCertPfxBytes =
        iec.ExportSelfSignedCertificatePfx(password, rsaCert);
    File.WriteAllBytes($"C:\\Personal\\Projects\\carmarket\\Carmarket\\Carmarket.Application.Proxy\\ssl\\{domain}.pfx", rsaCertPfxBytes);
}


