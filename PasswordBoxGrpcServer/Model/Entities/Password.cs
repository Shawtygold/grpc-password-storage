using FluentValidation;
using PasswordBoxGrpcServer.Model.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordBoxGrpcServer.Model.Entities
{
    public class Password
    {
        private readonly IValidator<Password> _validator;

        public Password(string userLogin, string title, string login, string passwordHash, string commentary, string image) : this(0, userLogin, title, login, passwordHash, commentary, image)
        { }

        public Password(int id, string userLogin, string title, string loginHash, string passwordHash, string commentary, string image)
        {
            Id = id;
            UserLogin = userLogin;
            Title = title;
            LoginHash = loginHash;
            PasswordHash = passwordHash;
            Commentary = commentary;
            Image = image;

            _validator = new PasswordValidator();
            _validator.ValidateAndThrow(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserLogin { get; set; } // содержит информацию о пользователе, который создал пароль
        [Required]
        public string Title { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string LoginHash { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;
        public string Commentary { get; set; } = null!;
    }
}
