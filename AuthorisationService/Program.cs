using AESEncryptionLib.Interfaces;
using AESEncryptionLib.Model;
using AuthorisationService.Interfaces.Cryptographers;
using AuthorisationService.Interfaces.Repositories;
using AuthorisationService.Interfaces.Services;
using AuthorisationService.Model.AppContext;
using AuthorisationService.Model.Cryptographers;
using AuthorisationService.Model.Cryptographers.Configs;
using AuthorisationService.Model.Repositories;
using AuthorisationService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Database and Repositories
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Authorisation services
builder.Services.AddScoped<IUserRegistration, UserRegistration>();
builder.Services.AddScoped<IUserAuthenticator, UserAuthenticator>();

// Encryption
builder.Services.AddSingleton<IAesConfig, AesConfig>(); // Aes algorithm config
builder.Services.AddScoped<IAes, AES>(); // Base aes algorithm
builder.Services.AddScoped<IEncryptor, AesEncryptor>(); // Class of encryptor using the Aes algorithm and configuration
builder.Services.AddScoped<IEncryptionHelper, EncryptionHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AuthorisationService.Services.AuthorisationService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
