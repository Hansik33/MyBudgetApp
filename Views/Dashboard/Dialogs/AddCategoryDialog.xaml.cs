using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MyBudgetApp.Views.Dashboard.Dialogs
{
    public sealed partial class AddCategoryDialog : ContentDialog
    {
        public AddCategoryDialog() => InitializeComponent();

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            CategoryNameTextBox.Focus(FocusState.Programmatic);
            CategoryNameTextBox.SelectionStart = CategoryNameTextBox.Text.Length;
            CategoryNameTextBox.SelectionLength = 0;
        }
    }
}