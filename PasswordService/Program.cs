using AESEncryptionLib.Interfaces;
using AESEncryptionLib.Model;
using PasswordService.Interfaces.Cryptographers;
using PasswordService.Interfaces.Repositories;
using PasswordService.Model.AppContext;
using PasswordService.Model.Cryptographers;
using PasswordService.Model.Cryptographers.Configs;
using PasswordService.Model.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Repositories
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();

// Encryption
builder.Services.AddSingleton<IAesConfig, AesConfig>(); // Aes algorithm config
builder.Services.AddScoped<IAes, AES>(); // Base aes algorithm
builder.Services.AddScoped<IEncryptor, AesEncryptor>(); // A class of encryptor using the Aes algorithm and configuration
builder.Services.AddScoped<IEncryptionHelper, EncryptionHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PasswordService.Services.PasswordService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
