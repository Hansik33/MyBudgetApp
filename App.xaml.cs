using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Services;
using MyBudgetApp.Services.Dashboard;
using MyBudgetApp.ViewModels.Auth;
using MyBudgetApp.ViewModels.Dashboard;
using System;

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
            services.AddSingleton<IBudgetService, BudgetService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<ISavingService, SavingService>();
            services.AddSingleton<ISavingGoalService, SavingGoalService>();

            services.AddSingleton<IUserContext, UserContextService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDatabaseService, DatabaseService>();
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false).Build());
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IPasswordHashService, PasswordHashService>();
            services.AddSingleton<AppStartupService>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<DashboardViewModel>();
        }
    }
}