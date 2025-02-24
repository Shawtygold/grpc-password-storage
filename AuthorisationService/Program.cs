using AESEncryptionLib;
using AuthService.Model.AppContext;
using AuthService.Model.Configs;
using AuthService.Model.Cryptographers;
using AuthService.Model.Cryptographers.Implementation;
using AuthService.Model.Repositories;
using AuthService.Model.Repositories.Implementation;
using AuthService.Services;
using AuthService.Services.Implementation;

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
app.MapGrpcService<AuthService.Services.Implementation.AuthService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
