using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Carmarket.Application.Identity.IS4
{
    public static class InMemoryConfig
    {
        internal static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "blazor",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "https://localhost:5001" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api1",
                        "roles"
                    },
                    RedirectUris = { "https://localhost:5001/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:5001/" },
                    Enabled = true
                },

                new Client
                {
                    ClientId = "microservice.users",
                    ClientSecrets = { new Secret("microservice.users".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "api1",
                        "roles"
                    }
                },
                new Client
                {
                    ClientId = "IdentityClient",
                    ClientSecrets = { new Secret("IdentityClientSecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,           
                    Enabled = true,
                    AllowedScopes = { IdentityServerConstants.LocalApi.ScopeName },
                }
            };
        }

        internal static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
                new IdentityResource
                {
                    Name = "roles",
                    DisplayName = "Your role",
                    UserClaims = { JwtClaimTypes.Role }
                }
            };
        }

        internal static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("api1", "api1", new[] 
                { 
                    JwtClaimTypes.Audience,
                    JwtClaimTypes.Issuer,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.PhoneNumber,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.NickName
                }),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName, "Local IS4 API scope", new[] 
                {
                    JwtClaimTypes.Audience,
                    JwtClaimTypes.Issuer,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.PhoneNumber,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.NickName
                }),
            };
        }

        internal static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "api1", new[] 
                { 
                    JwtClaimTypes.Audience,
                    JwtClaimTypes.Issuer,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.PhoneNumber,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.NickName
                }),
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName, "Local IS4 API resource", new[] 
                {
                    JwtClaimTypes.Audience,
                    JwtClaimTypes.Issuer,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.PhoneNumber,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.NickName
                }),
            };
        }
    }
}