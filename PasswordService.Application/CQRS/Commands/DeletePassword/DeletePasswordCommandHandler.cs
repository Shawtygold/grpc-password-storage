using MediatR;
using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.CQRS.DTO;
using PasswordService.Application.CQRS.Queries.GetPasswordByID;
using PasswordService.Application.Exceptions;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Repositories;

namespace PasswordService.Application.CQRS.Commands.DeletePassword
{
    public class DeleteCommandHandler : IRequestHandler<DeletePasswordCommand, PasswordResponse>
    {
        private readonly IPasswordRepository _passwordRepository;
        private readonly IPasswordMapper _passwordMapper;
        private readonly IMediator _mediator;

        public DeleteCommandHandler(IMediator mediator, IPasswordRepository passwordRepository, IPasswordMapper passwordMapper)
        {
            _passwordRepository = passwordRepository;
            _passwordMapper = passwordMapper;
            _mediator = mediator;
        }

        public async Task<PasswordResponse> Handle(DeletePasswordCommand command, CancellationToken cancellationToken)
        {
            Password password = await _mediator.Send(new GetPasswordByIDQuery(command.PasswordId), cancellationToken)
                ?? throw new PasswordNotFoundException(command.PasswordId);

            await _passwordRepository.DeleteAsync(command.PasswordId);

            return await _passwordMapper.ToPasswordResponse(password);
        }
    }
}
