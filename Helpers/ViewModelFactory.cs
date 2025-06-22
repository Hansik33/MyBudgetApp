using MyBudgetApp.Services;
using MyBudgetApp.ViewModels;

namespace MyBudgetApp.Helpers
{
    public static class ViewModelFactory
    {
        public static RegisterViewModel CreateRegisterViewModel() => new RegisterViewModel();

        public static LoginViewModel CreateLoginViewModel() => new LoginViewModel();
    }
}
