using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.ViewModels.Dashboard;
using System.Windows.Input;

namespace MyBudgetApp.Controls.Dashboard
{
    public sealed partial class CategoryCard : UserControl
    {
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(nameof(DeleteCommand),
                                        typeof(ICommand),
                                        typeof(CategoryCard),
                                        new PropertyMetadata(null));

        public static readonly DependencyProperty CategoryProperty =
            DependencyProperty.Register(nameof(Category),
                                        typeof(CategoryViewModel),
                                        typeof(CategoryCard),
                                        new PropertyMetadata(null));

        public CategoryCard() => InitializeComponent();

        public CategoryViewModel Category
        {
            get => (CategoryViewModel)GetValue(CategoryProperty);
            set => SetValue(CategoryProperty, value);
        }

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }
    }
}