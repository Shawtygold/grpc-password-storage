using AESEncryptionLib;
using FluentValidation;
using MediatR;
using PasswordService.Application.Abstractions.Encryption;
using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.CQRS.Behaviors;
using PasswordService.Application.CQRS.Commands.CreatePassword;
using PasswordService.Application.CQRS.Commands.DeletePassword;
using PasswordService.Application.CQRS.Commands.UpdatePassword;
using PasswordService.Application.CQRS.Queries.GetPasswordByID;
using PasswordService.Application.CQRS.Queries.GetPasswordsByUserID;
using PasswordService.Application.Mappers;
using PasswordService.Application.Validators.Commands;
using PasswordService.Application.Validators.Queries;
using PasswordService.Domain.Repositories;
using PasswordService.Infrastructure.Abstractions;
using PasswordService.Infrastructure.AppContext;
using PasswordService.Infrastructure.Repositories;
using PasswordService.Infrastructure.Security;
using PasswordService.Infrastructure.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Repositories
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();

// Mappers
builder.Services.AddScoped<IPasswordMapper, PasswordMapper>();

// Validators
builder.Services.AddScoped<IValidator<CreatePasswordCommand>, CreatePasswordCommandValidator>();
builder.Services.AddScoped<IValidator<UpdatePasswordCommand>, UpdatePasswordCommandValidator>();
builder.Services.AddScoped<IValidator<DeletePasswordCommand>, DeletePasswordCommandValidator>();
builder.Services.AddScoped<IValidator<GetPasswordByIDQuery>, GetPasswordByIDQueryValidator>();
builder.Services.AddScoped<IValidator<GetPasswordsByUserIDQuery>, GetPasswordByUserIDQueryValidator>();
builder.Services.AddScoped<IValidator<AesEncryptionConfig>, AesEncryptionConfigValidator>();

// Encryption
builder.Services.AddScoped<IAesConfigProvider, AesConfigProvider>();
builder.Services.AddScoped<IAesConfig>(provider => provider.GetRequiredService<IAesConfigProvider>().GetAesConfig()); 
builder.Services.AddScoped<IAes, AES>();
builder.Services.AddScoped<IEncryptor, AesEncryptor>();

// Settings
builder.Services.Configure<AesSettings>(builder.Configuration.GetRequiredSection(AesSettings.SectionName));
builder.Services.Configure<AppContextSettings>(builder.Configuration.GetRequiredSection(AppContextSettings.SectionName));

// MediatR
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(PasswordService.Application.AssemblyReference.Assembly);
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PasswordService.Presentation.GRPC.Implementation.PasswordService>();

app.Run();
