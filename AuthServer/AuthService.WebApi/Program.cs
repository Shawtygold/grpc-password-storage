using AESEncryptionLib;
using AuthService.Application.Abstractions.Encryption;
using AuthService.Application.Abstractions.Mappers;
using AuthService.Application.Abstractions.Services;
using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.Application.Mappers;
using AuthService.Domain.Events;
using AuthService.Infrastructure;
using AuthService.Infrastructure.Abstractions.Providers;
using AuthService.Infrastructure.Abstractions.Security;
using AuthService.Infrastructure.Projections;
using AuthService.Infrastructure.Providers;
using AuthService.Infrastructure.Security;
using AuthService.Infrastructure.Services;
using AuthService.Infrastructure.Validators;
using FluentValidation;
using GrpcAuthService;
using Marten;
using Weasel.Core;
using WebApi.Validators;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

// Marten
builder.Services.AddMarten(options =>
{
    options.Connection("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=CyfQRh0SGG5g4");
    options.UseSystemTextJsonForSerialization();
    options.Events.AddEventType<UserRegistered>();
    options.Projections.Add<UserProjection>(Marten.Events.Projections.ProjectionLifecycle.Inline);

    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
});

// Wolverine
builder.Host.UseWolverine(options =>
{
    options.UseFluentValidation();
    options.Discovery.IncludeAssembly(typeof(RegisterUserCommand).Assembly);
});

// Providers
builder.Services.AddScoped<IJWTProvider, JWTProvider>();

// Mappers
builder.Services.AddScoped<IUserMapper, UserMapper>();
builder.Services.AddScoped<ICommandMapper, CommandMapper>();

// Auth services
builder.Services.AddScoped<IUserRegistration, UserRegistration>();
builder.Services.AddScoped<IUserAuthenticator, UserAuthenticator>();

// Validators
builder.Services.AddScoped<IValidator<AesEncryptionConfig>, AesEncryptionConfigValidator>();
builder.Services.AddScoped<IValidator<AuthenticateUserRequest>, AuthenticateUserRequestValidator>();

// Encryption
builder.Services.AddScoped<IAesConfigProvider, AesConfigProvider>();
builder.Services.AddScoped<IAesConfig>(provider => provider.GetRequiredService<IAesConfigProvider>().GetAesConfig());
builder.Services.AddScoped<IAes, AES>();
builder.Services.AddScoped<IEncryptor, AesEncryptor>();

// Settings
builder.Services.Configure<AesSettings>(builder.Configuration.GetRequiredSection(AesSettings.SectionName));
builder.Services.Configure<JWTSettings>(builder.Configuration.GetRequiredSection(JWTSettings.SectionName));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<WebApi.Services.AuthService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
