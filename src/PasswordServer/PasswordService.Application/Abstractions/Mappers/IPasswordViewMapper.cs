using PasswordService.Application.DTO;
using PasswordService.Domain.Entities;

namespace PasswordService.Application.Abstractions.Mappers
{
    public interface IPasswordViewMapper
    {
        PasswordDTO Map(PasswordView passwordView);
    }
}
