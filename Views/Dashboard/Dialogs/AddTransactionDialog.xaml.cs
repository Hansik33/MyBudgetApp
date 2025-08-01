using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MyBudgetApp.Views.Dashboard.Dialogs
{
    public sealed partial class AddTransactionDialog : ContentDialog
    {
        public AddTransactionDialog() => InitializeComponent();

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            AmountTextBox.Focus(FocusState.Programmatic);
            AmountTextBox.SelectionStart = AmountTextBox.Text.Length;
            AmountTextBox.SelectionLength = 0;
        }
    }
}