using FluentValidation;
using GrpcPasswordService;

namespace PasswordService.WebApi.Validators
{
    public class CreatePasswordRequestValidator : AbstractValidator<CreatePasswordRequest>
    {
        public CreatePasswordRequestValidator()
        {
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.Login).NotEmpty();
            RuleFor(c => c.EncryptedPassword).NotEmpty();
            RuleFor(c => c.IconPath).NotEmpty();
        }
    }
}
