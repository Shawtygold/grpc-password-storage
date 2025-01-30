namespace PasswordsBunker.MVVM.Model.Entities
{
    internal class Password
    {
        //private readonly IValidator<Password> _validator;

        public Password(int userId, string title, string login, string passwordValue, string commentary, string image) : this(0, userId, title, login, passwordValue, commentary, image)
        { }

        public Password(int id, int userId, string title, string login, string passwordValue, string commentary, string image)
        {
            Id = id;
            UserId = userId;
            Title = title;
            Login = login;
            PasswordValue = passwordValue;
            Commentary = commentary;
            Image = image;

            //_validator = new PasswordValidator();
            //_validator.ValidateAndThrow(this);
        }

        public int Id { get; set; }
        public int UserId { get; set; } // содержит информацию о пользователе, который создал пароль
        public string Title { get; set; }
        public string Image { get; set; }
        public string Login { get; set; }
        public string PasswordValue { get; set; }
        public string Commentary { get; set; } = null!;
    }
}
