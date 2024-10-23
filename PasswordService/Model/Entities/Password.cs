using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using PasswordService.Model.Validators;

namespace PasswordService.Model.Entities
{
    public class Password
    {
        private readonly IValidator<Password> _validator;

        public Password(string userLogin, string title, string login, string passwordValue, string commentary, string image) : this(0, userLogin, title, login, passwordValue, commentary, image)
        { }

        public Password(int id, string userLogin, string title, string login, string passwordValue, string commentary, string image)
        {
            Id = id;
            UserLogin = userLogin;
            Title = title;
            Login = login;
            PasswordValue = passwordValue;
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
        public string Login { get; set; }
        [Required]
        public string PasswordValue { get; set; }
        public string Commentary { get; set; } = null!;
    }
}
