using GrpcPasswordService;
using PasswordService.Model.CQRS.Commands;
using PasswordService.Model.Entities;

namespace PasswordService.Model.Mappers
{
    public interface IPasswordMapper
    {
        public Task<Password> ToPassword(CreatePasswordCommand command);
        public Task<Password> ToPassword(UpdatePasswordCommand command);
        public Task<PasswordModel> ToPasswordModel(Password password);
    }
}
