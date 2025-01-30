namespace PasswordsBunker.Services
{
    internal interface IUserDialog
    {
        string OpenFileDialog(string filters);
        void OpenCreatePasswordWindow();
        void ShowMessageBox(string message);

    }
}
