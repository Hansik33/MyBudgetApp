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
            var startup = new AppStartupService();
            startup.Start();
        }
    }
}