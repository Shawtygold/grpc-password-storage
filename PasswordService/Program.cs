using AESEncryptionLib;
using FluentValidation;
using MediatR;
using PasswordService.Model.AppContext;
using PasswordService.Model.Configs;
using PasswordService.Model.CQRS.Behaviors;
using PasswordService.Model.CQRS.Commands;
using PasswordService.Model.Encryption;
using PasswordService.Model.Encryption.Implementation;
using PasswordService.Model.Mappers;
using PasswordService.Model.Mappers.Implementation;
using PasswordService.Model.Repositories;
using PasswordService.Model.Repositories.Implementation;
using PasswordService.Model.Validators.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Repositories
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();

//Mappers
builder.Services.AddTransient<IPasswordMapper, PasswordMapper>();

// Validators
builder.Services.AddScoped<IValidator<CreatePasswordCommand>, CreatePasswordCommandValidator>();
builder.Services.AddScoped<IValidator<UpdatePasswordCommand>, UpdatePasswordCommandValidator>();
builder.Services.AddScoped<IValidator<DeletePasswordCommand>, DeletePasswordCommandValidator>();

// Encryption
builder.Services.AddSingleton<IAesConfig, AesConfig>(); // Aes algorithm config
builder.Services.AddScoped<IAes, AES>(); // Base aes algorithm
builder.Services.AddScoped<IEncryptor, AesEncryptor>(); // A class of encryptor using the Aes algorithm and configuration

// MediatR
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingAndValidationPipelineBehavior<,>));   
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PasswordService.Services.Implementation.PasswordService>();

app.Run();
