using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;

namespace MyBudgetApp.Controls.Dashboard
{
    public sealed partial class DeleteIconButton : UserControl
    {
        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command),
                                    typeof(ICommand),
                                    typeof(DeleteIconButton),
                                    new PropertyMetadata(null));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter),
                                        typeof(object),
                                        typeof(DeleteIconButton),
                                        new PropertyMetadata(null));

        public DeleteIconButton() => InitializeComponent();

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
    }
}