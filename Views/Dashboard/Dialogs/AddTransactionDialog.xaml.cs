using Microsoft.UI.Xaml.Controls;

namespace MyBudgetApp.Views.Dashboard.Dialogs
{
    public sealed partial class AddTransactionDialog : ContentDialog
    {
        public AddTransactionDialog()
        {
            InitializeComponent();
            Loaded += AddTransactionDialog_Loaded;
        }

        private void AddTransactionDialog_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (DataContext is ViewModels.Dashboard.Dialogs.AddTransactionDialogViewModel viewModel)
            {
                if (DatePickerControl.SelectedDate != viewModel.SelectedDate)
                    DatePickerControl.SelectedDate = viewModel.SelectedDate;
            }
        }
    }
}