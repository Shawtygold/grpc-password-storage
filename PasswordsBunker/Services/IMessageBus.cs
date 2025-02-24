using System;

namespace PasswordBoxClient.Services
{
    internal interface IMessageBus
    {
        IDisposable RegisterHandler<T>(Action<T> handler);
        void Send<T>(T message);
    }
}
