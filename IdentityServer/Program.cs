using Duende.IdentityServer.Validation;
using IdentityServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<IResourceOwnerPasswordValidator, CustomResourceOwnerPasswordValidator>();
builder.Services.AddIdentityServer()
        .AddInMemoryApiResources(Config.ApiResources)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryClients(Config.Clients)
        .AddDeveloperSigningCredential(persistKey: true)
        .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();

var app = builder.Build();

app.UseIdentityServer();
app.Run();