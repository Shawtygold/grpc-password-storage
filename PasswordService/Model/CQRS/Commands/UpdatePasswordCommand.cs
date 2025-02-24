using GrpcPasswordService;
using MediatR;
using PasswordService.Model.Entities;
using PasswordService.Model.Exceptions;
using PasswordService.Model.Mappers;
using PasswordService.Model.Repositories;

namespace PasswordService.Model.CQRS.Commands
{
    public record UpdatePasswordCommand(
        int ID,
        int UserID,
        string Title,
        string Login,
        string Password,
        string IconPath,
        string Note,
        DateTime CreatedAt,
        DateTime UpdatedAt) : IRequest<UpdatePasswordResponse>;

    public sealed class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UpdatePasswordResponse>
    {
        private readonly IPasswordRepository _passwordRepository;
        private readonly IPasswordMapper _passwordMapper;

        public UpdatePasswordCommandHandler(IPasswordRepository passwordRepository, IPasswordMapper passwordMapper)
        {
            _passwordRepository = passwordRepository;
            _passwordMapper = passwordMapper;
        }

        public async Task<UpdatePasswordResponse> Handle(UpdatePasswordCommand command, CancellationToken cancellationToken)
        {
            if (await _passwordRepository.GetByIDAsync(command.ID) == null)
                throw new PasswordNotFoundException(command.ID);

            Password password = await _passwordMapper.ToPassword(command);

            await _passwordRepository.UpdateAsync(password);

            return new UpdatePasswordResponse()
            {
                Password = await _passwordMapper.ToPasswordModel(password)
            };
        }
    }
}
