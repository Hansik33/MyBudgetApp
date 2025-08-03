using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MyBudgetApp.Views.Dashboard.Dialogs
{
    public sealed partial class AddSavingGoalDialog : ContentDialog
    {
        public AddSavingGoalDialog() => InitializeComponent();

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            TargetAmountTextBox.Focus(FocusState.Programmatic);
            TargetAmountTextBox.SelectionStart = TargetAmountTextBox.Text.Length;
            TargetAmountTextBox.SelectionLength = 0;
        }
    }
}