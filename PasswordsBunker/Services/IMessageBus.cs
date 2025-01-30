using System;

namespace PasswordsBunker.Services
{
    internal interface IMessageBus
    {
        IDisposable RegisterHandler<T>(Action<T> handler);
        void Send<T>(T message);
    }
}
