using PasswordBoxClient.Core;
using System;

namespace PasswordBoxClient.Services.Implementation
{
    class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, ViewModel> _viewModelFactory;

        private ViewModel _currentView;

        public ViewModel CurrentView
        {
            get { return _currentView; }
            private set { _currentView = value; OnPropertyChanged(); }
        }

        public NavigationService(Func<Type, ViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }
    }
}
