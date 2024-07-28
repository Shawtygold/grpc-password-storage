using FluentValidation;
using PasswordBoxGrpcServer.Model.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordBoxGrpcServer.Model.Entities
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

            IValidator<User> userVaidator = new UserValidator();
            userVaidator.ValidateAndThrow(this);
        }
    }
}
