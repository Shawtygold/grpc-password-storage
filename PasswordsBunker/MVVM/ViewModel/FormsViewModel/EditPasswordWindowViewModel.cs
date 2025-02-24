using Google.Rpc;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc.Routing;
using PasswordBoxClient.MVVM.Model.Entities.BusMessages;
using PasswordBoxClient.Services;
using PasswordClient;
using PasswordService.Model.Entities;
using System;
using System.Net.NetworkInformation;
using System.Text;

namespace PasswordBoxClient.MVVM.ViewModel.FormsViewModel
{
    internal class EditPasswordWindowViewModel : PasswordWindowViewModel, IDisposable
    {
        private PasswordProtoService.PasswordProtoServiceClient _passwordClient;
        private readonly IMessageBus _messageBus;
        private readonly IDisposable _subscription;

        public EditPasswordWindowViewModel(PasswordProtoService.PasswordProtoServiceClient client, IMessageBus messageBus, IUserDialog userDialog) : base(userDialog)
        {
            _passwordClient = client;
            _messageBus = messageBus;
            _subscription = _messageBus.RegisterHandler<UpdatePasswordMessage>(OnReceiveMessage);
        }

        private void OnReceiveMessage(UpdatePasswordMessage message)
        {
            Id = message.Password.Id;
            UserId = message.Password.UserId;
            Title = message.Password.Title;
            Login = message.Password.Login;
            PasswordValue = message.Password.PasswordValue;
            Image = message.Password.Image;
            Commentary = message.Password.Commentary;
        }

        private protected async override void AcceptAsync(object obj)
        {
            PasswordModel reply;
            try
            {
                reply = await _passwordClient.UpdatePasswordAsync(new UpdatePasswordRequest()
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
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.InvalidArgument)
            {
                StringBuilder sb = new();
               
                var rpcStatus = ex.GetRpcStatus();
                if(rpcStatus != null)
                {
                    foreach(var msg in rpcStatus.UnpackDetailMessages())
                    {
                        switch (msg)
                        {
                            case BadRequest badRequest:
                                foreach (var fv in badRequest.FieldViolations)
                                    sb.Append(fv.Description);
                                break;
                        }        
                    }
                }

                _userDialog.ShowMessageBox(sb.ToString());          
                return;
            }
            catch (RpcException ex)
            {

                return;
            }

            //Message to PasswordsViewModel
            _messageBus.Send(new PasswordUpdatedMessage(new Password(
                reply.Id,
                reply.UserId,
                reply.Title,
                reply.Login,
                reply.PasswordValue,
                reply.Commentary,
                reply.Image)));

            Close(obj);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}
