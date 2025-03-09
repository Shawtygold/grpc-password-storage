using PasswordService.Application.CQRS.Commands.CreatePassword;
using PasswordService.Application.CQRS.Commands.UpdatePassword;
using PasswordService.Application.CQRS.DTO;
using PasswordService.Domain.Entities;

namespace PasswordService.Application.Abstractions.Mappers
{
    public interface IPasswordMapper
    {
        public Task<Password> ToPassword(CreatePasswordCommand command);
        public Task<Password> ToPassword(UpdatePasswordCommand command);
        public Task<PasswordResponse> ToPasswordResponse(Password password);
    }
}
