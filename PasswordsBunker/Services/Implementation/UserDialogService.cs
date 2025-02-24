using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using PasswordBoxClient.MVVM.ViewModel.FormsViewModel;
using PasswordBoxClient.MVVM.View.Forms;
using System;
using PasswordService.Model.Entities;
using PasswordBoxClient.MVVM.Model.Entities.BusMessages;

namespace PasswordBoxClient.Services.Implementation
{
    internal class UserDialogService : IUserDialog
    {
        private readonly IServiceProvider _serviceProvider;

        public UserDialogService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string OpenFileDialog(string filters)
        {
            OpenFileDialog openFileDialog = new();

            openFileDialog.Filter = filters;

            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileName;

            throw new Exception("Failed to show OpenFileDialog");
        }

        public void OpenCreatePasswordWindow()
        {
            var window = _serviceProvider.GetRequiredService<CreatePasswordWindow>();
            window.ShowDialog();
        }

        public void OpenEditPasswordWindow(Password password)
        {
            var window = _serviceProvider.GetRequiredService<EditPasswordWindow>();
            var viewModel = _serviceProvider.GetRequiredService<EditPasswordWindowViewModel>();
            IMessageBus? messageBus = _serviceProvider.GetService<IMessageBus>() ?? throw new Exception($"{nameof(messageBus)} is null");
            messageBus.Send(new UpdatePasswordMessage(password));
            window.DataContext = viewModel;
            window.ShowDialog();
        }

        public void ShowMessageBox(string message)
        {
            var vm = _serviceProvider.GetRequiredService<MessageboxViewModel>();
            vm.Message = message;
            var window = _serviceProvider.GetRequiredService<Messagebox>();
            window.DataContext = vm;
            window.ShowDialog();
        }
    }
}
