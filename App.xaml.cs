using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Services;
using MyBudgetApp.ViewModels;
using System;

namespace MyBudgetApp
{
    public partial class App : Application
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var startupService = ServiceProvider.GetRequiredService<AppStartupService>();
            startupService.Start();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDatabaseService, DatabaseService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<AppStartupService>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
        }
    }
}