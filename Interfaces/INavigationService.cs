using Microsoft.UI.Xaml.Controls;

namespace MyBudgetApp.Interfaces
{
    public interface INavigationService
    {
        void Initialize(ContentControl host);
        void NavigateTo<TViewModel>() where TViewModel : class;
        void GoToLogin();
        void GoToRegister();
        void GoToDashboard();
    }
}