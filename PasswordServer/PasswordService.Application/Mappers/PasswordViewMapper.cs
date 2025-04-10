using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.DTO;
using PasswordService.Domain.Entities;

namespace PasswordService.Application.Mappers
{
    public class PasswordViewMapper : IPasswordViewMapper
    {
        public PasswordDTO Map(PasswordView passwordView)
        {
            return new PasswordDTO(
                passwordView.Id,
                passwordView.UserId,
                passwordView.Title,
                passwordView.Login,
                passwordView.EncryptedPassword,
                passwordView.IconPath,
                passwordView.Note,
                passwordView.CreatedAt,
                passwordView.UpdatedAt);
        }
    }
}
