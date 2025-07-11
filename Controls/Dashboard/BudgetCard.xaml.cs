using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.ViewModels.Dashboard;
using System.Windows.Input;

namespace MyBudgetApp.Controls.Dashboard
{
    public sealed partial class BudgetCard : UserControl
    {
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(nameof(DeleteCommand),
                                        typeof(ICommand),
                                        typeof(BudgetCard),
                                        new PropertyMetadata(null));

        public static readonly DependencyProperty BudgetProperty =
            DependencyProperty.Register(nameof(Budget),
                                        typeof(BudgetViewModel),
                                        typeof(BudgetCard),
                                        new PropertyMetadata(null));

        public BudgetCard() => InitializeComponent();

        public BudgetViewModel Budget
        {
            get => (BudgetViewModel)GetValue(BudgetProperty);
            set => SetValue(BudgetProperty, value);
        }

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }
    }
}