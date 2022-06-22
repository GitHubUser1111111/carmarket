using IdentityModel.Client;

namespace Carmarket.Infrastructure.Authorization.IdentityServer
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
