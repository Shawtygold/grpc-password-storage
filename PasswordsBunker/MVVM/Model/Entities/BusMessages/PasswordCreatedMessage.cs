namespace PasswordsBunker.MVVM.Model.Entities.BusMessages
{
    internal class PasswordCreatedMessage : PasswordMessage
    {
        public PasswordCreatedMessage(Password password) : base(password) { }
    }
}
