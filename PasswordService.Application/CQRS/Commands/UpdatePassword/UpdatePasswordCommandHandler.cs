using MediatR;
using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.CQRS.DTO;
using PasswordService.Application.Exceptions;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Repositories;

namespace PasswordService.Application.CQRS.Commands.UpdatePassword
{
    public sealed class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, PasswordResponse>
    {
        private readonly IPasswordRepository _passwordRepository;
        private readonly IPasswordMapper _passwordMapper;

        public UpdatePasswordCommandHandler(IPasswordRepository passwordRepository, IPasswordMapper passwordMapper)
        {
            _passwordRepository = passwordRepository;
            _passwordMapper = passwordMapper;
        }

        public async Task<PasswordResponse> Handle(UpdatePasswordCommand command, CancellationToken cancellationToken)
        {
            if (await _passwordRepository.GetByIDAsync(command.ID) == null)
                throw new PasswordNotFoundException(command.ID);

            Password password = await _passwordMapper.ToPassword(command);

            await _passwordRepository.UpdateAsync(password);

            return await _passwordMapper.ToPasswordResponse(password);
        }
    }
}
