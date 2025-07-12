using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.ViewModels.Dashboard;
using System.Windows.Input;

namespace MyBudgetApp.Controls.Dashboard
{
    public sealed partial class SavingCard : UserControl
    {
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(nameof(DeleteCommand),
                                typeof(ICommand),
                                typeof(SavingCard),
                                new PropertyMetadata(null));

        public static readonly DependencyProperty SavingProperty =
            DependencyProperty.Register(nameof(Saving),
                                        typeof(SavingViewModel),
                                        typeof(SavingCard),
                                        new PropertyMetadata(null));

        public SavingCard() => InitializeComponent();

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public SavingViewModel Saving
        {
            get => (SavingViewModel)GetValue(SavingProperty);
            set => SetValue(SavingProperty, value);
        }
    }
}