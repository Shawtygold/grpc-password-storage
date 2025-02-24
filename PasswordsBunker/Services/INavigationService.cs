using PasswordBoxClient.Core;

namespace PasswordBoxClient.Services
{
    internal interface INavigationService
    {
        ViewModel CurrentView { get; }
        void NavigateTo<T>() where T : ViewModel;
    }
}
