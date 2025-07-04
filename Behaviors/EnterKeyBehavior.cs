using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.System;

namespace MyBudgetApp.Behaviors
{
    public static class EnterKeyBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(EnterKeyBehavior),
                new PropertyMetadata(null, OnCommandChanged));

        public static void SetCommand(DependencyObject dependencyObject, ICommand value) =>
            dependencyObject.SetValue(CommandProperty, value);

        public static ICommand GetCommand(DependencyObject dependencyObject) =>
            (ICommand)dependencyObject.GetValue(CommandProperty);

        private static void OnCommandChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is UIElement element)
            {
                element.KeyUp -= OnKeyUp;
                element.KeyUp += OnKeyUp;
            }
        }

        private static void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Enter)
                return;

            if (sender is not FrameworkElement container || GetCommand(container) is not ICommand command)
                return;

            var viewModel = container.DataContext;

            foreach (var textBox in FindVisualChildren<TextBox>(container))
                textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

            foreach (var custom in FindVisualChildren<UserControl>(container))
            {
                if (custom.GetType().Name == "BindablePasswordBox")
                {
                    var passwordProperty = custom.GetType().GetProperty("Password");
                    var passwordValue = passwordProperty?.GetValue(custom);

                    var viewModelProperty = viewModel?.GetType().GetProperty(custom.Name);
                    if (viewModelProperty != null && passwordValue != null)
                        viewModelProperty.SetValue(viewModel, passwordValue);
                }
            }

            if (command.CanExecute(null))
                command.Execute(null);
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject root) where T : DependencyObject
        {
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int count = VisualTreeHelper.GetChildrenCount(current);

                for (int i = 0; i < count; i++)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    if (child is T tChild)
                        yield return tChild;

                    queue.Enqueue(child);
                }
            }
        }
    }
}
