using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Todo.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new []
        {
            new ApiScope("todo-api", "Todo API")
        };

    public static IEnumerable<Client> Clients =>
        new []
        {
            new Client
            {
                AccessTokenType = AccessTokenType.Jwt,
                AllowedGrantTypes = GrantTypes.DeviceFlow,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile,
                    "todo-api"
                },
                ClientId = "todo-cli",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                ClientName = "Todo CLI",
                RequireClientSecret = true
            },
            
            new Client
            {
                AccessTokenType = AccessTokenType.Jwt,
                AllowedCorsOrigins = new []
                {
                    "https://localhost:9000"
                },
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes =
                {
                    "todo-api"
                },
                ClientId = "todo-api-swagger",
                ClientName = "Todo API Swagger",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                PostLogoutRedirectUris = new []
                {
                    "https://localhost:9000/swagger/signout-callback-oidc"
                },
                RedirectUris = new []
                {
                    "https://localhost:9000/swagger/oauth2-redirect.html"
                }
            }
        };
}