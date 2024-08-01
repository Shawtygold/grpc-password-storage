using PasswordBoxGrpcServer.Interfaces;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Model.AppContext;
using PasswordBoxGrpcServer.Model.Repositories;
using PasswordBoxGrpcServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddTransient<ApplicationContext>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddTransient<IUserCreatorService, UserCreatorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PasswordBoxService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
