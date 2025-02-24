using GrpcPasswordService;
using MediatR;
using PasswordService.Model.CQRS.Queries;
using PasswordService.Model.Entities;
using PasswordService.Model.Exceptions;
using PasswordService.Model.Mappers;
using PasswordService.Model.Repositories;

namespace PasswordService.Model.CQRS.Commands
{
    public record DeletePasswordCommand(int PasswordId) : IRequest<DeletePasswordResponse>;

    public class DeleteCommandHandler : IRequestHandler<DeletePasswordCommand, DeletePasswordResponse>
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

        public async Task<DeletePasswordResponse> Handle(DeletePasswordCommand command, CancellationToken cancellationToken)
        {
            Password password = await _mediator.Send(new GetPasswordByIDQuery(command.PasswordId), cancellationToken) 
                ?? throw new PasswordNotFoundException(command.PasswordId);

            await _passwordRepository.DeleteAsync(command.PasswordId);

            return new DeletePasswordResponse()
            {
                Password = await _passwordMapper.ToPasswordModel(password)
            };
        }
    }
}
