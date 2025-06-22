using Microsoft.UI.Xaml.Controls;

namespace MyBudgetApp.Interfaces
{
    public interface INavigationService
    {
        void Initialize(ContentControl contentControl);
        void NavigateTo<TView, TViewModel>()
            where TView : UserControl, new()
            where TViewModel : class;
        void GoToLogin();
        void GoToRegister();
    }
}