using Microsoft.UI.Xaml;

namespace MyBudgetApp.Services
{
    public class AppStartupService
    {
        private Window? _window;

        public void Start()
        {
            var dbService = new DatabaseService();
            var connected = dbService.TryConnect();

            _window = new MainWindow();
            _window.Activate();

            if (_window is MainWindow mw)
            {
                NavigationService.Initialize(mw.MainContent);
                NavigationService.GoToLogin();
            }
        }
    }
}
