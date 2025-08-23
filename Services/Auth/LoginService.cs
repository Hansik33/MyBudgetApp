using MyBudgetApp.Enums;
using MyBudgetApp.Enums.ValidationResults;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Auth;
using MyBudgetApp.Resources;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Auth
{
    public class LoginService(IUserContext userContext, IUserService userService, IDialogService dialogService) : ILoginService
    {
        public async Task<bool> LoginAsync(string username, string password)
        {
            if (await dialogService.ShowAuthenticationValidationDialog(username, password)
                == AuthenticationValidationResult.Success)
            {
                var user = userService.GetUserByCredentials(username, password);

                if (user != null)
                {
                    userContext.Username = user.Username;
                    userContext.UserId = user.Id;

                    await dialogService.ShowMessageAsync(string.Format(AppStrings.Dialogs.Auth.LoginSuccess, username),
                                                         DialogType.Info);

                    return true;
                }
                else
                    await dialogService.ShowMessageAsync(AppStrings.Dialogs.Auth.UserNotFound, DialogType.Error);
            }
            return false;
        }
    }
}