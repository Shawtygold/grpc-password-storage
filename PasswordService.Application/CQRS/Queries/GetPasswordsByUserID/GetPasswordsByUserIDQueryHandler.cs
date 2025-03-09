using MediatR;
using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.CQRS.DTO;
using PasswordService.Domain.Repositories;

namespace PasswordService.Application.CQRS.Queries.GetPasswordsByUserID
{
    public class GetPasswordsByUserIDQueryHandler : IRequestHandler<GetPasswordsByUserIDQuery, IEnumerable<PasswordResponse>>
    {
        private readonly IPasswordRepository _passwordRepository;
        private readonly IPasswordMapper _passwordMapper;

        public GetPasswordsByUserIDQueryHandler(IPasswordRepository passwordRepository, IPasswordMapper passwordMapper)
        {
            _passwordRepository = passwordRepository;
            _passwordMapper = passwordMapper;
        }

        public async Task<IEnumerable<PasswordResponse>> Handle(GetPasswordsByUserIDQuery command, CancellationToken cancellationToken)
        {
            List<PasswordResponse> response = [];

            var passwords = await _passwordRepository.GetCollectionByAsync(p => p.UserId == command.UserID);

            foreach (var password in passwords)
                response.Add(await _passwordMapper.ToPasswordResponse(password));

            return response;
        }
    }
}
