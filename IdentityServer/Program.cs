using IdentityServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentityServer()
        .AddInMemoryApiResources(Config.ApiResources)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryClients(Config.Clients)
        .AddDeveloperSigningCredential(persistKey: true); // For demo purposes only; use a proper signing credential in production

var app = builder.Build();

app.UseIdentityServer();
app.Run();