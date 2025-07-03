using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.Interfaces;
using MyBudgetApp.ViewModels;

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

        private LoginViewModel ViewModel => (LoginViewModel)DataContext;

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
                ViewModel.Password = passwordBox.Password;
        }
    }
}
