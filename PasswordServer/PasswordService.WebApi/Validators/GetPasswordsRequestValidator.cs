using FluentValidation;
using GrpcPasswordService;

namespace PasswordService.WebApi.Validators
{
    public class GetPasswordsRequestValidator : AbstractValidator<GetPasswordsRequest>
    {
        public GetPasswordsRequestValidator()
        {
            
        }
    }
}
