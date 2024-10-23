using AESEncryptionLib.Interfaces;
using AESEncryptionLib.Model;
using PasswordBoxGrpcServer.Interfaces.Cryptographers;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Interfaces.Services.Passwords;
using PasswordBoxGrpcServer.Model.AppContext;
using PasswordBoxGrpcServer.Model.Cryptographers;
using PasswordBoxGrpcServer.Model.Cryptographers.Configs;
using PasswordBoxGrpcServer.Model.Repositories;
using PasswordBoxGrpcServer.Services.Main;
using PasswordBoxGrpcServer.Services.Passwords;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Repositories
builder.Services.AddSingleton<ApplicationContext>();
builder.Services.AddSingleton<IPasswordRepository, PasswordRepository>();

// Password Service
builder.Services.AddSingleton<IPasswordService, PasswordService>();

// Encryption
builder.Services.AddSingleton<IAesConfig, AesConfig>(); // Aes algorithm config
builder.Services.AddSingleton<IAes, AES>(); // Base aes algorithm
builder.Services.AddSingleton<IEncryptor, AesEncryptor>(); // A class of encryptor using the Aes algorithm and configuration

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PasswordBoxService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
