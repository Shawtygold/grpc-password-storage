using MediatR;
using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.CQRS.DTO;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Repositories;

namespace PasswordService.Application.CQRS.Commands.CreatePassword
{
    public sealed class CreatePasswordCommandHandler : IRequestHandler<CreatePasswordCommand, PasswordResponse>
    {
        private readonly IPasswordRepository _passwordRepository;
        private readonly IPasswordMapper _passwordMapper;

        public CreatePasswordCommandHandler(IPasswordRepository passwordRepository, IPasswordMapper passwordMapper)
        {
            _passwordRepository = passwordRepository;
            _passwordMapper = passwordMapper;
        }

        public async Task<PasswordResponse> Handle(CreatePasswordCommand command, CancellationToken cancellationToken)
        {
            Password password = await _passwordMapper.ToPassword(command);
            
            await _passwordRepository.AddAsync(password);

            return await _passwordMapper.ToPasswordResponse(password);
        }
    }
}
