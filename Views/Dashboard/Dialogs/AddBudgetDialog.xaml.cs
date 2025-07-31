using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MyBudgetApp.Views.Dashboard.Dialogs
{
    public sealed partial class AddBudgetDialog : ContentDialog
    {
        public AddBudgetDialog() => InitializeComponent();

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            LimitAmountTextBox.Focus(FocusState.Programmatic);
            LimitAmountTextBox.SelectionStart = LimitAmountTextBox.Text.Length;
            LimitAmountTextBox.SelectionLength = 0;
        }
    }
}