using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.Interfaces;

namespace MyBudgetApp.Views
{
    public sealed partial class LoginView : UserControl
    {
        private readonly IDialogService _dialogService;

        public LoginView()
        {
            InitializeComponent();
            _dialogService = App.ServiceProvider!.GetRequiredService<IDialogService>();
            Loaded += LoginView_Loaded;
        }

        private void LoginView_Loaded(object sender, RoutedEventArgs e) => _dialogService.SetXamlRoot(XamlRoot);
    }
}
