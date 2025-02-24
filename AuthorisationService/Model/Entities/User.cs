using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AuthService.Model.Validators;

namespace AuthService.Model.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        public User(string login, string password) : this(0, login, password) { }

        public User(int id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;

            IValidator<User> userVaidator = (IValidator<User>)new UserValidator();
            userVaidator.ValidateAndThrow(this);
        }
    }
}
