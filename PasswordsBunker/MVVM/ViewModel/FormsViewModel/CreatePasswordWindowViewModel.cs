using Grpc.Core;
using PasswordClient;
using PasswordsBunker.MVVM.Model.Entities;
using PasswordsBunker.MVVM.Model.Entities.BusMessages;
using PasswordsBunker.Services;

namespace PasswordsBunker.MVVM.ViewModel.FormsViewModel
{
    internal class CreatePasswordWindowViewModel : PasswordWindowViewModel
    {
        private PasswordProtoService.PasswordProtoServiceClient _client { get; set; }
        private readonly IMessageBus _messageBus;

        public CreatePasswordWindowViewModel(PasswordProtoService.PasswordProtoServiceClient client, IMessageBus messageBus, IUserDialog userDialog) : base(userDialog)
        {
            _client = client;
            _messageBus = messageBus;
        }

        private protected override async void AcceptAsync(object obj)
        {
            try
            {
                var reply = await _client.CreatePasswordAsync(new CreatePasswordRequest() { UserId = 2, Title = Title, Login = Login, PasswordValue = PasswordValue, Image = Image, Commentary = Commentary });               
                _messageBus.Send(new PasswordCreatedMessage(new Password(reply.Id, reply.Title, reply.Login, reply.PasswordValue, reply.Commentary, reply.Image)));
            }
            catch (RpcException ex) 
            {

            }

            Close(obj);
        }
    }
}
