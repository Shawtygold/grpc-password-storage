using AESEncryptionLib;
using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PasswordService.Application.Abstractions.Encryption;
using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Application.CQRS.Commands.CreatePassword;
using PasswordService.Application.CQRS.Commands.DeletePassword;
using PasswordService.Application.CQRS.Commands.UpdatePassword;
using PasswordService.Application.CQRS.Queries.GetPasswordByID;
using PasswordService.Application.CQRS.Queries.GetPasswordsByUserID;
using PasswordService.Application.Mappers;
using PasswordService.Application.Validators.Commands;
using PasswordService.Application.Validators.Queries;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Events;
using PasswordService.Infrastructure;
using PasswordService.Infrastructure.Abstractions;
using PasswordService.Infrastructure.Projections;
using PasswordService.Infrastructure.Repositories;
using PasswordService.Infrastructure.Security;
using PasswordService.Infrastructure.Validators;
using System.Text;
using Weasel.Core;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Connection string
string connectionString = builder.Configuration.GetConnectionString("PosgreSQLConnection")
    ?? throw new InvalidOperationException("PostgreSQL connection is required");

// Marten
builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
    options.UseSystemTextJsonForSerialization();
    options.Events.AddEventType<PasswordCreated>();
    options.Events.AddEventType<PasswordUpdated>();
    options.Events.AddEventType<PasswordDeleted>();
    options.Projections.Add<PasswordProjection>(Marten.Events.Projections.ProjectionLifecycle.Inline);

    if (builder.Environment.IsDevelopment())
        options.AutoCreateSchemaObjects = AutoCreate.All;
});

// Wolverine
builder.Host.UseWolverine(options =>
{
    options.UseFluentValidation();
    options.Discovery.IncludeAssembly(typeof(CreatePasswordCommand).Assembly);
});

// JWT Settings
JWTStructure jwtStructure = builder.Configuration.GetRequiredSection("JWTSettings").Get<JWTStructure>() ?? throw new ArgumentNullException("JWTSettings");
byte[] secretKey = Encoding.Default.GetBytes(jwtStructure.SecretKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters()
{
    ValidAudience = jwtStructure.ValidAudence,
    ValidateAudience = true,
    ValidIssuer = jwtStructure.ValidIssuer,
    ValidateIssuer = true,
    ValidateLifetime = true,
    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
});
builder.Services.AddAuthorization();

// Repositories
builder.Services.AddScoped<IEventSourcingRepository<Password>, EventSourcingRepository>();
builder.Services.AddScoped<IProjectionRepository<PasswordView>, ProjectionRepository>();

// Mappers
builder.Services.AddScoped<ICommandEventMapper, CommandEventMapper>();

// Validators
//builder.Services.AddScoped<IValidator<CreatePasswordCommand>, CreatePasswordCommandValidator>();
//builder.Services.AddScoped<IValidator<UpdatePasswordCommand>, UpdatePasswordCommandValidator>();
//builder.Services.AddScoped<IValidator<DeletePasswordCommand>, DeletePasswordCommandValidator>();
//builder.Services.AddScoped<IValidator<GetPasswordByIDQuery>, GetPasswordByIDQueryValidator>();
//builder.Services.AddScoped<IValidator<GetPasswordsByUserIDQuery>, GetPasswordByUserIDQueryValidator>();
builder.Services.AddScoped<IValidator<AesEncryptionConfig>, AesEncryptionConfigValidator>();

// Encryption
//builder.Services.AddScoped<IAesConfigProvider, AesConfigProvider>();
//builder.Services.AddScoped<IAesConfig>(provider => provider.GetRequiredService<IAesConfigProvider>().GetAesConfig());
//builder.Services.AddScoped<IAes, AES>();
//builder.Services.AddScoped<IEncryptor, AesEncryptor>();

// Settings
//builder.Services.Configure<AesSettings>(builder.Configuration.GetRequiredSection(AesSettings.SectionName));
//builder.Services.Configure<AppContextSettings>(builder.Configuration.GetRequiredSection(AppContextSettings.SectionName));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PasswordService.WebApi.Services.PasswordService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
