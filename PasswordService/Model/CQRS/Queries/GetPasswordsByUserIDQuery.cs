using GrpcPasswordService;
using MediatR;
using PasswordService.Model.Mappers;
using PasswordService.Model.Repositories;

namespace PasswordService.Model.CQRS.Queries
{
    public record GetPasswordsByUserIDQuery(int UserID) : IRequest<GetPasswordsResponse>;

    public class GetPasswordsByUserIDQueryHandler : IRequestHandler<GetPasswordsByUserIDQuery, GetPasswordsResponse>
    {
        private readonly IPasswordRepository _passwordRepository;
        private readonly IPasswordMapper _passwordMapper;

        public GetPasswordsByUserIDQueryHandler(IPasswordRepository passwordRepository, IPasswordMapper passwordMapper)
        {
            _passwordRepository = passwordRepository;
            _passwordMapper = passwordMapper;
        }

        public async Task<GetPasswordsResponse> Handle(GetPasswordsByUserIDQuery command, CancellationToken cancellationToken)
        {
            GetPasswordsResponse response = new();
           
            var passwords = await _passwordRepository.GetCollectionByAsync(p => p.UserId == command.UserID);

            foreach(var password in passwords)
                response.Passwords.Add(await _passwordMapper.ToPasswordModel(password));

            return response;
        }
    }
}
