using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Auth;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Services;
using MyBudgetApp.Services.Auth;
using MyBudgetApp.Services.Dashboard;
using MyBudgetApp.ViewModels.Auth;
using MyBudgetApp.ViewModels.Dashboard;
using System;
using System.IO;

namespace MyBudgetApp
{
    public partial class App : Application
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            var startup = ServiceProvider.GetRequiredService<AppStartupService>();
            startup.Start();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoginService, LoginService>();
            services.AddSingleton<IRegistrationService, RegistrationService>();

            services.AddSingleton<IBudgetService, BudgetService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<ISavingService, SavingService>();
            services.AddSingleton<ISavingGoalService, SavingGoalService>();

            services.AddSingleton<IUserContext, UserContextService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDatabaseService, DatabaseService>();

            var exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName;
            var exeDir = exePath != null ? Path.GetDirectoryName(exePath) : AppContext.BaseDirectory;
            var configPath = Path.Combine(exeDir ?? "", "appsettings.json");
            services.AddSingleton<IConfiguration>(
                new ConfigurationBuilder()
                    .AddJsonFile(configPath, optional: true, reloadOnChange: true)
                    .Build()
            );

            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IPasswordHashService, PasswordHashService>();
            services.AddSingleton<AppStartupService>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<DashboardViewModel>();
        }
    }
}