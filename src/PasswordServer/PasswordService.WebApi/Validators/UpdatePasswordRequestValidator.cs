using FluentValidation;
using GrpcPasswordService;

namespace PasswordService.WebApi.Validators
{
    public class UpdatePasswordRequestValidator : AbstractValidator<UpdatePasswordRequest>
    {
        public UpdatePasswordRequestValidator()
        {
            RuleFor(c => c.Password.Id).NotEmpty();
            RuleFor(c => c.Password.Title).NotEmpty();
            RuleFor(c => c.Password.Login).NotEmpty();
            RuleFor(c => c.Password.EncryptedPassword).NotEmpty();
            RuleFor(c => c.Password.IconPath).NotEmpty();
        }
    }
}
