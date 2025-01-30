using PasswordsBunker.Services;

namespace PasswordsBunker.MVVM.ViewModel
{
    class LoadingScreenViewModel : Core.ViewModel
    {
        public LoadingScreenViewModel(INavigationService navigation)
        {
            Navigation = navigation;
        }

        #region Properties

        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }

        #endregion
    }
}
