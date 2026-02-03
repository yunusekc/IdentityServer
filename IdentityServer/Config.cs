using Duende.IdentityServer.Models;
namespace IdentityServer
{
    public static class Config
    {
        // API Kaynaklarını Tanımlıyoruz
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("api1.read", "Read access to API 1"),
                new ApiScope("api1.write", "Write access to API 1"),
                new ApiScope("api2.read", "Read access to API 2"),
                new ApiScope("api2.write", "Write access to API 2")
            };
        public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("api1", "My API 1") 
            {
                Scopes = { "api1.read", "api1.write" }
            },
            new ApiResource("api2", "My API 2")
            {
                Scopes = { "api2.read", "api2.write" }
            } 
        };

        // API için Kullanıcı Yetkilendirmesi
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name = "employee_info",
                    UserClaims = new List<string> { "employment_start", "seniority","contractor"}
                }
            };

        //İstemcileri Tanımlıyoruz
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client1",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("client1-secret".Sha256())
                    },
                    AllowedScopes = { "api1.read", "api2.read" }
                },
                new Client
                {
                    ClientId = "client2",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("client2-secret".Sha256())
                    },
                    AllowedScopes = { "api1.write", "api2.write" }
                }
            };
    }
}
