using PasswordService.Model.Entities;

namespace PasswordBoxClient.MVVM.Model.Entities.BusMessages
{
    internal class PasswordCreatedMessage : PasswordMessage
    {
        public PasswordCreatedMessage(Password password) : base(password) { }
    }
}
