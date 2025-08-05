using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MyBudgetApp.Views.Dashboard.Dialogs
{
    public sealed partial class AddSavingGoalDialog : ContentDialog
    {
        public AddSavingGoalDialog() => InitializeComponent();

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            NameTextBox.Focus(FocusState.Programmatic);
            NameTextBox.SelectionStart = NameTextBox.Text.Length;
            NameTextBox.SelectionLength = 0;
        }
    }
}