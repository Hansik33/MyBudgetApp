using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using MyBudgetApp.Interfaces;
using MyBudgetApp.ViewModels;
using Windows.System;

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

        private void Enter_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && DataContext is LoginViewModel vm)
            {
                if (sender is TextBox textBox)
                {
                    var bindingExpr = textBox.GetBindingExpression(TextBox.TextProperty);
                    bindingExpr?.UpdateSource();
                }

                if (sender is PasswordBox passwordBox)
                    vm.Password = passwordBox.Password;

                if (vm.LoginCommand.CanExecute(null))
                    vm.LoginCommand.Execute(null);
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
                ViewModel.Password = passwordBox.Password;
        }
    }
}
