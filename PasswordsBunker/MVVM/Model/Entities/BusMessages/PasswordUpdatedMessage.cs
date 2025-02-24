using PasswordService.Model.Entities;

namespace PasswordBoxClient.MVVM.Model.Entities.BusMessages
{
    internal class PasswordUpdatedMessage : PasswordMessage
    {
        public PasswordUpdatedMessage(Password password) : base(password) { }
    }
}
