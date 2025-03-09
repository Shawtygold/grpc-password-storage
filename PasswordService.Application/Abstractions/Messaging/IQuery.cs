using MediatR;

namespace PasswordService.Application.Abstractions.Messaging
{
    interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
