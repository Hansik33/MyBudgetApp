using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using MyBudgetApp.Interfaces;
using MyBudgetApp.ViewModels;
using Windows.System;

namespace MyBudgetApp.Views
{
    public sealed partial class RegisterView : UserControl
    {
        private readonly IDialogService _dialogService;

        public RegisterView()
        {
            InitializeComponent();
            _dialogService = App.ServiceProvider!.GetRequiredService<IDialogService>();
            Loaded += RegisterView_Loaded;
        }

        private void RegisterView_Loaded(object sender, RoutedEventArgs e) => _dialogService.SetXamlRoot(XamlRoot);

        private RegisterViewModel ViewModel => (RegisterViewModel)DataContext;

        private void Enter_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && DataContext is RegisterViewModel vm)
            {
                if (sender is TextBox textBox)
                    textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

                if (sender is PasswordBox passwordBox)
                {
                    switch (passwordBox.Name)
                    {
                        case "PasswordBox":
                            vm.Password = passwordBox.Password;
                            break;
                        case "ConfirmPasswordBox":
                            vm.ConfirmPassword = passwordBox.Password;
                            break;
                    }
                }

                if (vm.RegisterCommand.CanExecute(null))
                    vm.RegisterCommand.Execute(null);
            }
        }

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