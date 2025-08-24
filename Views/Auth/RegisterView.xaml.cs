using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MyBudgetApp.Views.Auth
{
    public sealed partial class RegisterView : UserControl
    {
        public RegisterView() => InitializeComponent();

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            UsernameTextBox.Focus(FocusState.Programmatic);
            UsernameTextBox.SelectionStart = UsernameTextBox.Text.Length;
            UsernameTextBox.SelectionLength = 0;
        }
    }
}