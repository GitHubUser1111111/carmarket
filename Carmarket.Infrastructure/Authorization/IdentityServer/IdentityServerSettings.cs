namespace Carmarket.Infrastructure.Authorization.IdentityServer
{
    public class IdentityServerSettings
    {
        public string Url { get; set; }

        public string ClientName { get; set; }

        public string ClientPassword { get; set; }

        public bool UseHttps { get; set; }
    }
}
