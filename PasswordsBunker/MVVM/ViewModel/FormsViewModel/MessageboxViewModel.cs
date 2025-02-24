using PasswordBoxClient.Core;
using System.Windows;
using System.Windows.Input;

namespace PasswordBoxClient.MVVM.ViewModel.FormsViewModel
{
    class MessageboxViewModel : Core.ViewModel
    {
        public MessageboxViewModel()
        {
            //Message = message;

            OkCommand = new RelayCommand(Ok);
        }

        #region Properties

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public ICommand OkCommand { get; set; }
        private void Ok(object obj)
        {
            if (obj is not Window wnd)
                return;

            wnd.Close();
        }

        #endregion
    }
}
