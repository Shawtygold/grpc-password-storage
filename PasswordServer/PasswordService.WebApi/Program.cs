using Marten;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Application.CQRS.Commands.CreatePassword;
using PasswordService.Application.Mappers;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Events;
using PasswordService.Infrastructure;
using PasswordService.Infrastructure.Projections;
using PasswordService.Infrastructure.Repositories;
using PasswordService.WebApi.Abstractions;
using PasswordService.WebApi.Mappers;
using System.Text;
using Weasel.Core;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Connection string
string connectionString = builder.Configuration.GetConnectionString("PosgreSQLConnection")
    ?? throw new InvalidOperationException("PostgreSQL connection not found in configuration");

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
builder.Services.AddScoped<IEventSourcingRepository<PasswordAggregate>, EventSourcingRepository>();
builder.Services.AddScoped<IProjectionRepository<PasswordView>, ProjectionRepository>();

// Mappers
builder.Services.AddScoped<ICommandEventMapper, CommandEventMapper>();
builder.Services.AddScoped<IPasswordViewMapper, PasswordViewMapper>();
builder.Services.AddScoped<IGrpcExceptionMapper, GrpcExceptionMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PasswordService.WebApi.Services.PasswordService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
