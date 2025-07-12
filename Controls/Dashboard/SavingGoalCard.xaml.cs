using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.ViewModels.Dashboard;
using System.Windows.Input;

namespace MyBudgetApp.Controls.Dashboard
{
    public sealed partial class SavingGoalCard : UserControl
    {
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(nameof(DeleteCommand),
                                typeof(ICommand),
                                typeof(SavingGoalCard),
                                new PropertyMetadata(null));

        public static readonly DependencyProperty SavingGoalProperty =
            DependencyProperty.Register(nameof(SavingGoal),
                                        typeof(SavingGoalViewModel),
                                        typeof(SavingGoalCard),
                                        new PropertyMetadata(null));

        public SavingGoalCard() => InitializeComponent();

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public SavingGoalViewModel SavingGoal
        {
            get => (SavingGoalViewModel)GetValue(SavingGoalProperty);
            set => SetValue(SavingGoalProperty, value);
        }
    }
}