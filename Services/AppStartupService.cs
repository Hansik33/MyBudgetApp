using Microsoft.UI.Xaml;
using MyBudgetApp.Interfaces;

namespace MyBudgetApp.Services
{
    public class AppStartupService
    {
        private readonly INavigationService _navigationService;
        private readonly IDatabaseService _databaseService;

        private Window? _window;

        public AppStartupService(INavigationService navigationService, IDatabaseService databaseService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
        }

        public void Start()
        {
            _databaseService.TryConnect();

            _window = new MainWindow();
            _window.Activate();

            if (_window is MainWindow mainWindow)
            {
                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(_window);
                var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
                var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

                if (appWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter presenter)
                {
                    presenter.IsResizable = false;       
                    presenter.IsMaximizable = false;      
                    presenter.IsMinimizable = true;       
                }

                var displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(windowId, 
                    Microsoft.UI.Windowing.DisplayAreaFallback.Primary);
                var workArea = displayArea.WorkArea;
                appWindow.MoveAndResize(workArea);

                _navigationService.Initialize(mainWindow.MainContent);
                _navigationService.GoToLogin();
            }


        }
    }
}