using PasswordService.Application.DTO;
using PasswordService.Domain.Entities;

namespace PasswordService.Application.Extensions
{
    public static class PasswordViewExtension
    {
        public static PasswordDTO ToPasswordDTO(this PasswordView passwordView)
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
