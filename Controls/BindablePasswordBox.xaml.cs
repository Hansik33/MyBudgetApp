using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MyBudgetApp.Controls
{
    public sealed partial class BindablePasswordBox : UserControl
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(
                nameof(Password),
                typeof(string),
                typeof(BindablePasswordBox),
                new PropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        public BindablePasswordBox() => InitializeComponent();

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        private static void OnPasswordPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is BindablePasswordBox control && control.PART_PasswordBox.Password != (string)e.NewValue)
            {
                control.PART_PasswordBox.PasswordChanged -= control.OnPasswordChanged;
                control.PART_PasswordBox.Password = (string)e.NewValue;
                control.PART_PasswordBox.PasswordChanged += control.OnPasswordChanged;
            }
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password != PART_PasswordBox.Password)
                Password = PART_PasswordBox.Password;
        }
    }
}