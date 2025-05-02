using AuthService.Application;
using AuthService.Application.Abstractions.Mappers;
using AuthService.Application.Abstractions.Providers;
using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Abstractions.Security;
using AuthService.Application.Abstractions.Services;
using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.Application.Mappers;
using AuthService.Application.Services;
using AuthService.Domain.Entities;
using AuthService.Domain.Events;
using AuthService.Infrastructure.AppContext;
using AuthService.Infrastructure.Projections;
using AuthService.Infrastructure.Providers;
using AuthService.Infrastructure.Repositories;
using AuthService.Infrastructure.Security;
using AuthService.Infrastructure.Settings;
using AuthService.WebApi.Abstractions;
using AuthService.WebApi.Interceptors;
using AuthService.WebApi.Mappers;
using Marten;
using Weasel.Core;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(opt =>
{
    opt.Interceptors.Add<LoggingInterceptor>();
    opt.Interceptors.Add<ValidationInterceptor>();
    opt.Interceptors.Add<ExceptionHandlingInterceptor>();
});

string connectionString = builder.Configuration.GetConnectionString("UsersPostgreSQLConnection")
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

// Db context
builder.Services.AddDbContext<ApplicationContext>();

// Repositories
builder.Services.AddScoped<IEventSourcingRepository<UserAggregate>, EventSourcingRepository>();
builder.Services.AddScoped<IProjectionRepository<UserView>, ProjectionRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

// Providers
builder.Services.AddScoped<IRefreshTokenProvider, RefreshTokenProvider>();

// Mappers
builder.Services.AddScoped<ICommandEventMapper, CommandEventMapper>();
builder.Services.AddScoped<IUserViewMapper, UserViewMapper>();
builder.Services.AddScoped<IGrpcExceptionMapper, GrpcExceptionMapper>();

// Services
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

// Hash
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// Settings
builder.Services.Configure<JWTSettings>(builder.Configuration.GetRequiredSection(JWTSettings.SectionName));
builder.Services.Configure<RefreshTokenDbSettings>(builder.Configuration.GetRequiredSection(RefreshTokenDbSettings.SectionName));
builder.Services.Configure<RefreshTokenSettings>(builder.Configuration.GetRequiredSection(RefreshTokenSettings.SectionName));

var app = builder.Build();

app.UseRequestLocalization(opt =>
{
    opt.DefaultRequestCulture = new("en");
});

// Configure the HTTP request pipeline.
app.MapGrpcService<AuthService.WebApi.Services.AuthService>();

app.Run();
