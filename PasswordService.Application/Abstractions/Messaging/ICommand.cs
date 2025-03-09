using MediatR;

namespace PasswordService.Application.Abstractions.Messaging
{
    interface ICommand<TResponse> : IRequest<TResponse> { }

    interface ICommand : IRequest { }
}
