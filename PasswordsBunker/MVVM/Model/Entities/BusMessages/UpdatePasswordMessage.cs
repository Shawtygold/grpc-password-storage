using PasswordService.Model.Entities;

namespace PasswordBoxClient.MVVM.Model.Entities.BusMessages
{
    internal class UpdatePasswordMessage : PasswordMessage
    {
        public UpdatePasswordMessage(Password password) : base(password) { }
    }
}
