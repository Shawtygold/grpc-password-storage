using PasswordService.Model.Entities;

namespace PasswordBoxClient.MVVM.Model.Entities.BusMessages
{
    internal class PasswordMessage
    {
        public Password Password { get; set; }

        public PasswordMessage(Password password)
        {
            Password = password;
        }
    }
}
