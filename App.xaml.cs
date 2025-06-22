using Microsoft.UI.Xaml;
using MyBudgetApp.Services;

namespace MyBudgetApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            var dbService = new DatabaseService();
            var connected = dbService.TryConnect();

            var mainWindow = new MainWindow();
            mainWindow.Activate();

            NavigationService.Initialize(mainWindow.MainContent);
            NavigationService.GoToLogin();
        }
    }
}