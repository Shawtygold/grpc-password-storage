using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using PasswordsBunker.MVVM.View.Forms;
using PasswordsBunker.MVVM.ViewModel.FormsViewModel;
using System;

namespace PasswordsBunker.Services.Implementation
{
    internal class UserDialogService : IUserDialog
    {
        private readonly IServiceProvider _serviceProvider;

        public UserDialogService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OpenPasswordActionWindow()
        {
            var window = _serviceProvider.GetRequiredService<PasswordActionForm>();
            window.ShowDialog();
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

        public void ShowMessageBox(string message)
        {
            var vm = _serviceProvider.GetRequiredService<MessageboxViewModel>();
            vm.Message = message;
            var msgBox = _serviceProvider.GetRequiredService<Messagebox>();
            msgBox.DataContext = vm;
            msgBox.ShowDialog();
        }
    }
}
