using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.Services;
using MyBudgetApp.ViewModels;

namespace MyBudgetApp.Views
{
    public sealed partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();            
            Loaded += RegisterView_Loaded;
        }

        private void RegisterView_Loaded(object sender, RoutedEventArgs e)
        {
            var dialogService = new DialogService();
            dialogService.SetXamlRoot(this.XamlRoot);
            DataContext = new RegisterViewModel(dialogService);
        }

        private RegisterViewModel ViewModel => (RegisterViewModel)DataContext;

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
                ViewModel.Password = passwordBox.Password;
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
                ViewModel.ConfirmPassword = passwordBox.Password;
        }
    }
}