using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using MyBudgetApp.Interfaces;

namespace MyBudgetApp
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private async void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            var dialogService = App.ServiceProvider!.GetService<IDialogService>();
            if (dialogService != null)
                await dialogService.ShowInfoAsync();
        }
    }
}