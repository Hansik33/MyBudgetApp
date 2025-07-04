using Microsoft.UI.Xaml;
using MyBudgetApp.Interfaces;

namespace MyBudgetApp.Helpers;

public static class DialogServiceXamlRootSetter
{
    public static readonly DependencyProperty AttachProperty =
        DependencyProperty.RegisterAttached(
            "Attach",
            typeof(bool),
            typeof(DialogServiceXamlRootSetter),
            new PropertyMetadata(false, OnAttachChanged));

    public static void SetAttach(DependencyObject dependencyObject, bool value) =>
        dependencyObject.SetValue(AttachProperty, value);

    public static bool GetAttach(DependencyObject dependencyObject) =>
        (bool)dependencyObject.GetValue(AttachProperty);

    private static void OnAttachChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        if (dependencyObject is FrameworkElement element && (bool)e.NewValue)
        {
            element.Loaded += (_, _) =>
            {
                if (App.ServiceProvider?.GetService(typeof(IDialogService)) is IDialogService dialogService)
                    dialogService.SetXamlRoot(element.XamlRoot);
            };
        }
    }
}
