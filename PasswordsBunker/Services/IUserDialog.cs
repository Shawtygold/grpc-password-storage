using PasswordService.Model.Entities;

namespace PasswordBoxClient.Services
{
    internal interface IUserDialog
    {
        string OpenFileDialog(string filters);
        void OpenCreatePasswordWindow();
        void OpenEditPasswordWindow(Password password);
        void ShowMessageBox(string message);
    }
}
