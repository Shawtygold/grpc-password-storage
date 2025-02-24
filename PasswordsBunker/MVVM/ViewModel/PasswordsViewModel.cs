using PasswordBoxClient.Core;
using PasswordBoxClient.MVVM.Model.Entities.BusMessages;
using PasswordBoxClient.Services;
using PasswordService.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PasswordBoxClient.MVVM.ViewModel
{
    class PasswordsViewModel : Core.ViewModel, IDisposable
    {
        private readonly IDisposable _passwordCreatedSubscription;
        private readonly IDisposable _passwordUpdatedSubscription;
        private readonly IMessageBus _messageBus;
        private readonly IUserDialog _userDialog;

        public PasswordsViewModel(INavigationService navigation, IMessageBus messageBus, IUserDialog userDialog)
        {
            Navigation = navigation;

            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);

            _messageBus = messageBus;
            _userDialog = userDialog;
            _passwordCreatedSubscription = _messageBus.RegisterHandler<PasswordCreatedMessage>(OnReceiveMessage);
            _passwordUpdatedSubscription = _messageBus.RegisterHandler<PasswordUpdatedMessage>(OnReceiveMessage);
        }

        void OnReceiveMessage(PasswordMessage message)
        {
            switch (message)
            {
                case PasswordCreatedMessage cpMsg: _passwords.Add(cpMsg.Password); break;
                case PasswordUpdatedMessage upMsg:
                    {
                        var oldPassword = _passwords.FirstOrDefault(p => p.Id == message.Password.Id) ?? throw new Exception("Password with this ID does not exist");
                        int oldPasswordIndex = _passwords.IndexOf(oldPassword);
                        _passwords.Remove(oldPassword);
                        _passwords.Insert(oldPasswordIndex, message.Password);
                        break;
                    }
            }
        }

        #region Properties

        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }

        private readonly ObservableCollection<Password> _passwords = new();
        public IEnumerable<Password> Passwords
        {
            get { return _passwords; }
        }

        #region SelectedItem

        private Password? _selectedItem; 
        public Password? SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        private void Add(object obj)
        {
            _userDialog.OpenCreatePasswordWindow();
        }
        private void Edit(object obj)
        {
            if (SelectedItem != null)
                _userDialog.OpenEditPasswordWindow(SelectedItem);
            else
                _userDialog.ShowMessageBox("Password is not selected");
        }
        private async void Delete(object obj)
        {
            if (SelectedItem != null)
            {

            }
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            _passwordCreatedSubscription?.Dispose();
            _passwordUpdatedSubscription?.Dispose();
        }

        #endregion
    }
}
