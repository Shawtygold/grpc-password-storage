using GrpcPasswordService;
using MediatR;
using PasswordService.Model.Entities;
using PasswordService.Model.Mappers;
using PasswordService.Model.Repositories;

namespace PasswordService.Model.CQRS.Commands
{
    public record CreatePasswordCommand(int UserID, string Title, string Login, string Password, string IconPath, string Note) : IRequest<CreatePasswordResponse>;

    public sealed class CreatePasswordCommandHandler : IRequestHandler<CreatePasswordCommand, CreatePasswordResponse>
    {
        private readonly IPasswordRepository _passwordRepository;
        private readonly IPasswordMapper _passwordMapper;

        public CreatePasswordCommandHandler(IPasswordRepository passwordRepository, IPasswordMapper passwordMapper)
        {
            _passwordRepository = passwordRepository;
            _passwordMapper = passwordMapper;
        }

        public async Task<CreatePasswordResponse> Handle(CreatePasswordCommand command, CancellationToken cancellationToken)
        {
            Password password = await _passwordMapper.ToPassword(command);

            await _passwordRepository.AddAsync(password);

            return new CreatePasswordResponse()
            {
                Password = await _passwordMapper.ToPasswordModel(password),
            };
        }
    }
}
