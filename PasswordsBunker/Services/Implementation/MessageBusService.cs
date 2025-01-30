using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PasswordsBunker.Services.Implementation
{
    internal class MessageBusService : IMessageBus
    {
        private class Subscription<T> : IDisposable
        {
            private readonly WeakReference<MessageBusService> _Bus;

            public Action<T> Handler { get; }

            public Subscription(MessageBusService Bus, Action<T> Handler)
            {
                this.Handler = Handler;
                _Bus = new(Bus);
            }

            public void Dispose()
            {
                if (!_Bus.TryGetTarget(out var bus))
                    return;

                var @lock = bus._Lock;
                @lock.EnterWriteLock();
                var messageType = typeof(T);
                try
                {
                    if (!bus._Subscriptions.TryGetValue(messageType, out var refs))
                        return;

                    var updatedRefs = refs.Where(r => r.IsAlive).ToList();

                    WeakReference? currentRef = null;
                    foreach (var @ref in updatedRefs)
                        if (ReferenceEquals(@ref.Target, this))
                        {
                            currentRef = @ref;
                            break;
                        }

                    if (currentRef is null)
                        return;

                    updatedRefs.Remove(currentRef);

                    bus._Subscriptions[messageType] = updatedRefs;
                }
                finally
                {
                    @lock.ExitWriteLock();
                }
            }
        }

        private readonly Dictionary<Type, IEnumerable<WeakReference>> _Subscriptions = new();
        private readonly ReaderWriterLockSlim _Lock = new();

        public IDisposable RegisterHandler<T>(Action<T> Handler)
        {
            var subscription = new Subscription<T>(this, Handler);

            _Lock.EnterWriteLock();
            try
            {
                var subscriptionRef = new WeakReference(subscription);
                var messageType = typeof(T);
                _Subscriptions[messageType] = _Subscriptions.TryGetValue(messageType, out var subscriptions)
                    ? subscriptions.Append(subscriptionRef)
                    : new[] { subscriptionRef };
            }
            finally
            {
                _Lock.ExitWriteLock();
            }

            return subscription;
        }

        private IEnumerable<Action<T>>? GetHandlers<T>()
        {
            var handlers = new List<Action<T>>();
            var messageType = typeof(T);
            var existDieRefs = false;

            _Lock.EnterReadLock();
            try
            {
                if (!_Subscriptions.TryGetValue(messageType, out var refs))
                    return null;

                foreach (var @ref in refs)
                    if (@ref.Target is Subscription<T> { Handler: var handler })
                        handlers.Add(handler);
                    else
                        existDieRefs = true;
            }
            finally
            {
                _Lock.ExitReadLock();
            }

            if (!existDieRefs) return handlers;

            _Lock.EnterWriteLock();
            try
            {
                if (_Subscriptions.TryGetValue(messageType, out var refs))
                    if (refs.Where(r => r.IsAlive).ToArray() is { Length: > 0 } new_refs)
                        _Subscriptions[messageType] = new_refs;
                    else
                        _Subscriptions.Remove(messageType);
            }
            finally
            {
                _Lock.ExitWriteLock();
            }

            return handlers;
        }

        public void Send<T>(T message)
        {
            if (GetHandlers<T>() is not { } handlers)
                return;

            foreach (var handler in handlers)
                handler(message);
        }
    }
}
