using MyBudgetApp.Enums;
using MyBudgetApp.Enums.ValidationResults;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Auth;
using MyBudgetApp.Resources;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Auth
{
    public class RegistrationService(IUserService userService, IDialogService dialogService) : IRegistrationService
    {
        public async Task<bool> RegisterAsync(string username, string password, string confirmPassword)
        {
            if (await dialogService.ShowAuthenticationValidationDialog(username, password, confirmPassword)
                == AuthenticationValidationResult.Success)
            {
                if (userService.AddUser(username, password))
                {
                    await dialogService.ShowMessageAsync(AppStrings.Dialogs.Auth.RegisterSuccess, DialogType.Success);
                    return true;
                }
                else
                    await dialogService.ShowMessageAsync(AppStrings.Dialogs.Auth.UserExists, DialogType.Error);
            }
            return false;
        }
    }
}