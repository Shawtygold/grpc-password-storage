using AuthService.Application.Abstractions.Mappers;
using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Abstractions.Security;
using AuthService.Application.Abstractions.Services;
using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.Application.Mappers;
using AuthService.Domain.Entities;
using AuthService.Domain.Events;
using AuthService.Infrastructure;
using AuthService.Infrastructure.Abstractions.Providers;
using AuthService.Infrastructure.Projections;
using AuthService.Infrastructure.Providers;
using AuthService.Infrastructure.Repositories;
using AuthService.Infrastructure.Security;
using AuthService.Infrastructure.Services;
using AuthService.WebApi.Validators;
using FluentValidation;
using GrpcAuthService;
using Marten;
using Weasel.Core;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

string connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection")
    ?? throw new InvalidOperationException("PostgreSQL connection not found in configuration");

// Marten
builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
    options.UseSystemTextJsonForSerialization();
    options.Events.AddEventType<UserRegistered>();
    options.Projections.Add<UserProjection>(Marten.Events.Projections.ProjectionLifecycle.Inline);

    if (builder.Environment.IsDevelopment())
        options.AutoCreateSchemaObjects = AutoCreate.All;
});

// Wolverine
builder.Host.UseWolverine(options =>
{
    options.UseFluentValidation();
    options.Discovery.IncludeAssembly(typeof(RegisterUserCommand).Assembly);
});

// Repositories
builder.Services.AddScoped<IEventSourcingRepository<UserAggregate>, EventSourcingRepository>();
builder.Services.AddScoped<IProjectionRepository<UserView>, ProjectionRepository>();

// Providers
builder.Services.AddScoped<IJWTProvider, JWTProvider>();

// Mappers
builder.Services.AddScoped<ICommandEventMapper, CommandEventMapper>();
builder.Services.AddScoped<IUserViewMapper, UserViewMapper>();

// Services
builder.Services.AddScoped<IUserAuthenticator, UserAuthenticator>();

// Validators
builder.Services.AddScoped<IValidator<AuthenticateUserRequest>, AuthenticateUserRequestValidator>();

// Hash
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// Settings
builder.Services.Configure<JWTSettings>(builder.Configuration.GetRequiredSection(JWTSettings.SectionName));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AuthService.WebApi.Services.AuthService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
