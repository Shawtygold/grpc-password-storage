using PasswordBoxGrpcServer.Interfaces.Cryptographers;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Interfaces.Services.Passwords;
using PasswordBoxGrpcServer.Interfaces.Services.Users;
using PasswordBoxGrpcServer.Model.AppContext;
using PasswordBoxGrpcServer.Model.Cryptographers;
using PasswordBoxGrpcServer.Model.Repositories;
using PasswordBoxGrpcServer.Services.Main;
using PasswordBoxGrpcServer.Services.Passwords;
using PasswordBoxGrpcServer.Services.TempUnnecessary;
using PasswordBoxGrpcServer.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Repositories
builder.Services.AddSingleton<ApplicationContext>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPasswordRepository, PasswordRepository>();

// User Service
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRegistration, UserRegistration>();
builder.Services.AddTransient<IUserAuthenticator, UserAuthenticator>();
builder.Services.AddTransient<IUserCreator, UserCreator>();

// Password Service
builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddTransient<IPasswordCreator, PasswordCreator>();

// Encryptor
builder.Services.AddSingleton<IEncryptor, AESEncryptor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PasswordBoxService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
