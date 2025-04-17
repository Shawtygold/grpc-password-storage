using FluentValidation;
using GrpcPasswordService;

namespace PasswordService.WebApi.Validators
{
    public class DeletePasswordRequestValidator : AbstractValidator<DeletePasswordRequest>
    {
        public DeletePasswordRequestValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}
