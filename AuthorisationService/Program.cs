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

// Repositories
builder.Services.AddSingleton<ApplicationContext>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

// Authorisation services
builder.Services.AddSingleton<IUserRegistration, UserRegistration>();
builder.Services.AddSingleton<IUserAuthenticator, UserAuthenticator>();

// Encryption
builder.Services.AddSingleton<IAesConfig, AesConfig>(); // Aes algorithm config
builder.Services.AddSingleton<IAes, AES>(); // Base aes algorithm
builder.Services.AddSingleton<IEncryptor, AesEncryptor>(); // Class of encryptor using the Aes algorithm and configuration

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AuthorisationService.Services.AuthorisationService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
