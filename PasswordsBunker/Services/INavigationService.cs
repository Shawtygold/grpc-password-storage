using PasswordsBunker.Core;

namespace PasswordsBunker.Services
{
    internal interface INavigationService
    {
        ViewModel CurrentView { get; }
        void NavigateTo<T>() where T : ViewModel;
    }
}
