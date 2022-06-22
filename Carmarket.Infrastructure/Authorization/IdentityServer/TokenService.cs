using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Carmarket.Infrastructure.Authorization.IdentityServer
{
    public class TokenService : ITokenService
    {
        public readonly IOptions<IdentityServerSettings> IdentityServerSettings;
        public readonly DiscoveryDocumentResponse DiscoveryDocumentResponse;
        private readonly HttpClient httpClient;

        public TokenService(IOptions<IdentityServerSettings> identityServerSettings)
        {
            IdentityServerSettings = identityServerSettings;
            httpClient = new HttpClient();
            DiscoveryDocumentResponse = httpClient.GetDiscoveryDocumentAsync(identityServerSettings.Value.Url).Result;

            if (DiscoveryDocumentResponse.IsError)
            {
                throw new Exception("");
            }
        }

        public async Task<TokenResponse> GetToken(string scope)
        {
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = DiscoveryDocumentResponse.TokenEndpoint,
                ClientId = IdentityServerSettings.Value.ClientName,
                ClientSecret = IdentityServerSettings.Value.ClientPassword,
                Scope = scope
            });

            if(tokenResponse.IsError) throw new System.Exception("Unable to get token", tokenResponse.Exception);

            return tokenResponse;
        }
    }
}
