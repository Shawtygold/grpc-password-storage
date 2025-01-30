using PasswordsBunker.Core;
using PasswordsBunker.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace PasswordsBunker.MVVM.ViewModel.FormsViewModel
{
    internal abstract class PasswordWindowViewModel : ObservableObject
    {
        //private protected readonly IMessageBus _messageBus;
        //private protected readonly IDisposable _subscription;
        private protected readonly IUserDialog _userDialog;
        private const string _imageDefaultPath = "pack://application:,,,/Resources/IconPassword.png";

        public PasswordWindowViewModel(/*IMessageBus messageBus,*/ IUserDialog userDialog)
        {
            //_messageBus = messageBus;
            _userDialog = userDialog;
            //_subscription = _messageBus.RegisterHandler<TMessage>(OnReceiveMessage);

            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);
            AddImageCommand = new RelayCommand(AddImage);
            AcceptCommand = new RelayCommand(AcceptAsync);
        }

        //private protected abstract void OnReceiveMessage(TMessage message);

        #region Properties

        private protected int Id { get; set; }
        private protected int UserId { get; set; }

        private protected string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        private protected string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(); }
        }

        private protected string _passwordValue;
        public string PasswordValue
        {
            get { return _passwordValue; }
            set { _passwordValue = value; OnPropertyChanged(); }
        }

        private protected string _image = _imageDefaultPath;
        public string Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }

        private protected string _commentary;
        public string Commentary
        {
            get { return _commentary; }
            set { _commentary = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public ICommand CloseCommand { get; set; }
        private protected void Close(object obj)
        {
            if (obj is not Window wnd)
                throw new ArgumentException($"{nameof(obj)} is not a Window");

            wnd.Close();
        }

        public ICommand MinimizeCommand { get; set; }
        private protected void Minimize(object obj)
        {
            if (obj is not Window wnd)
                throw new ArgumentException($"{nameof(obj)} is not a Window");

            wnd.WindowState = WindowState.Minimized;
        }

        public ICommand AddImageCommand { get; set; }
        private protected virtual void AddImage(object obj)
        {
            Image = _userDialog.OpenFileDialog("Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*");
        }

        public ICommand AcceptCommand { get; set; }
        private protected abstract void AcceptAsync(object obj);

        #endregion
    }
}
