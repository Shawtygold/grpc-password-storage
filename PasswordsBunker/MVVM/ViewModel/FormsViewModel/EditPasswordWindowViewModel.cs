using PasswordClient;
using PasswordsBunker.Enums;
using PasswordsBunker.MVVM.Model.Entities;
using PasswordsBunker.MVVM.Model.Entities.BusMessages;
using PasswordsBunker.Services;
using System;

namespace PasswordsBunker.MVVM.ViewModel.FormsViewModel
{
    internal class EditPasswordWindowViewModel : PasswordWindowViewModel, IDisposable
    {
        private PasswordProtoService.PasswordProtoServiceClient _passwordClient;
        private readonly IDisposable _subscription;

        public EditPasswordWindowViewModel(PasswordProtoService.PasswordProtoServiceClient client,IMessageBus messageBus, IUserDialog userDialog) : base(userDialog)
        {
            _passwordClient = client;
            //_subscription = _messageBus.RegisterHandler<EditPasswordMessage>(OnReceiveMessage);
        }

        //private void OnReceiveMessage(EditPasswordMessage message)
        //{
        //    Id = message.Password.Id;
        //    UserId = message.Password.UserId;
        //    Title = message.Password.Title;
        //    Login = message.Password.Login;
        //    PasswordValue = message.Password.PasswordValue;
        //    Image = message.Password.Image;
        //    Commentary = message.Password.Commentary;
        //}

        private protected async override void AcceptAsync(object obj)
        {
            var reply = await _passwordClient.UpdatePasswordAsync(new UpdatePasswordRequest()
            {
                Password = new PasswordModel()
                {
                    Id = Id,
                    UserId = UserId,
                    Title = Title,
                    Login = Login,
                    PasswordValue = PasswordValue,
                    Image = Image,
                    Commentary = Commentary
                }
            });

            // Message to PasswordsViewModel
            //_messageBus.Send(new PasswordResponseMessage(new Password(
            //    reply.Id,
            //    reply.UserId,
            //    reply.Title,
            //    reply.Login,
            //    reply.PasswordValue,
            //    reply.Image,
            //    reply.Commentary), PasswordAction.Update));

            Close(obj);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}
