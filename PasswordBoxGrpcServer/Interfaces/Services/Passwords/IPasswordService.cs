namespace PasswordBoxGrpcServer.Interfaces.Services.Passwords
{
    public interface IPasswordService : IPasswordAdder, IPasswordCreator, IPasswordDeleter, IPasswordUpdater, IPasswordRetriever
    {
    }
}
