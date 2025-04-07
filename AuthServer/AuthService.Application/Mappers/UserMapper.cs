using AuthService.Application.Abstractions.Encryption;
using AuthService.Application.Abstractions.Mappers;
using AuthService.Application.DTO;
using AuthService.Domain.Entities;

namespace AuthService.Application.Mappers
{
    public class UserMapper : IUserMapper
    {
        private readonly IEncryptor _encryptor;

        public UserMapper(IEncryptor encryptor)
        {
            _encryptor = encryptor; 
        }

        public async Task<UserDTO> ToUserDTO(User user)
        {
            return new UserDTO(
                user.Id,
                user.Login,
                user.Email,
                await _encryptor.DecryptAsync(user.EncryptedPassword),
                user.CreatedAt);
        }
    }
}
