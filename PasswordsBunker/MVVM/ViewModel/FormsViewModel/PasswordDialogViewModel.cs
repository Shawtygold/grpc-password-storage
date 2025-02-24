using PasswordBoxClient.Core;
using System.Windows;
using System.Windows.Input;

namespace PasswordBoxClient.MVVM.ViewModel.FormsViewModel
{
    class PasswordDialogViewModel : Core.ViewModel
    {
        public PasswordDialogViewModel(string login, string password, string notes)
        {
            Login = login;
            Password = password;
            Notes = notes;

            //отображение звездочек вместо реального пароля
            for(int i = 0; i < password.Length; i++)
            {
                StarPassword += "*";
            }

            CopyCommand = new RelayCommand(Copy);
            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);
        }

        #region Properties

        #region Login

        private string _login = null!;
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(); }
        }

        #endregion

        #region Password

        private string _password = null!;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        #endregion

        #region Notes

        private string _notes = null!;
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; OnPropertyChanged(); }
        }

        #endregion

        #region StarPassword

        private string _starPassword = null!;
        public string StarPassword
        {
            get { return _starPassword; }
            set { _starPassword = value; OnPropertyChanged(); }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand CopyCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }

        private void Copy(object obj)
        {
            if (obj is string text)
                //копирование текста в буфер обмена
                Clipboard.SetText(text);
        }
        private void Close(object obj)
        {
            if (obj is not Window wnd)
                return;

            wnd.Close();
        }
        private void Minimize(object obj)
        {
            if(obj is Window wnd)
                wnd.WindowState = WindowState.Minimized;
        }

        #endregion
    }
}
