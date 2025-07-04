using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.Interfaces;

namespace MyBudgetApp.Views
{
    public sealed partial class RegisterView : UserControl
    {
        private readonly IDialogService _dialogService;

        public RegisterView()
        {
            InitializeComponent();
            _dialogService = App.ServiceProvider!.GetRequiredService<IDialogService>();
            Loaded += RegisterView_Loaded;
        }

        private void RegisterView_Loaded(object sender, RoutedEventArgs e) => _dialogService.SetXamlRoot(XamlRoot);
    }
}