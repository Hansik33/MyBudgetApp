using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.ViewModels.Dashboard;
using System.Windows.Input;

namespace MyBudgetApp.Controls.Dashboard
{
    public sealed partial class TransactionCard : UserControl
    {
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(nameof(DeleteCommand),
                                typeof(ICommand),
                                typeof(TransactionCard),
                                new PropertyMetadata(null));

        public static readonly DependencyProperty TransactionProperty =
            DependencyProperty.Register(nameof(Transaction),
                                        typeof(TransactionViewModel),
                                        typeof(TransactionCard),
                                        new PropertyMetadata(null));

        public TransactionCard() => InitializeComponent();

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public TransactionViewModel Transaction
        {
            get => (TransactionViewModel)GetValue(TransactionProperty);
            set => SetValue(TransactionProperty, value);
        }
    }
}