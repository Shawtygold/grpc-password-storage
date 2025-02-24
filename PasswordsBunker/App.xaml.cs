using Microsoft.Extensions.DependencyInjection;
using PasswordBoxClient.Core;
using PasswordBoxClient.MVVM.ViewModel;
using PasswordBoxClient.MVVM.ViewModel.FormsViewModel;
using PasswordBoxClient.Services;
using PasswordBoxClient.Services.Implementation;
using PasswordClient;
using PasswordBoxClient.MVVM.View.Forms;
using System;
using System.Windows;
using PasswordBoxClient;

namespace PasswordsBunker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            // View Models
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<PasswordsViewModel>();
            services.AddSingleton<LoadingScreenViewModel>();
            services.AddTransient<CreatePasswordWindowViewModel>();
            services.AddTransient<EditPasswordWindowViewModel>();
            services.AddTransient<MessageboxViewModel>();

            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            // Windows
            services.AddTransient<Messagebox>();
            services.AddTransient<EditPasswordWindow>();
            services.AddTransient(provider =>
            {
                var model = provider.GetRequiredService<CreatePasswordWindowViewModel>();
                var window = new CreatePasswordWindow() { DataContext = model };
                return window;
            });

            // Client Factory
            services.AddGrpcClient<PasswordProtoService.PasswordProtoServiceClient>(o =>
            {
                o.Address = new Uri("http://localhost:5159");
            });

            // Services
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IMessageBus, MessageBusService>();
            services.AddSingleton<IUserDialog, UserDialogService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
