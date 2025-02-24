using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordService.Model.Entities
{
    public class Password
    {
        public Password(int userId, string title, string login, byte[] encryptedPassword, string iconPath, string note, DateTime createdAt, DateTime updatedAt) 
            : this(0, userId, title, login, encryptedPassword, iconPath, note, createdAt, updatedAt) { }

        public Password(int id, int userId, string title, string login, byte[] encryptedPassword, string iconPath, string note, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            UserId = userId;
            Title = title;
            Login = login;
            EncryptedPassword = encryptedPassword;
            Note = note;
            IconPath = iconPath;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // ID
        [Required]
        public int UserId { get; set; } // User who created the password
        [Required]
        public string Title { get; set; } // Title of password
        [Required]
        public string Login { get; set; } // Login
        [Required]
        public byte[] EncryptedPassword { get; set; } // Encrypted password
        [Required]
        public string IconPath { get; set; } // Icon path
        [Required]
        public DateTime CreatedAt { get; set; } // Time when the password was created
        public DateTime UpdatedAt { get; set; } // Time when the password was updated
        public string Note { get; set; } // Password note
    }
}
