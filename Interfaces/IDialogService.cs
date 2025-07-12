using Microsoft.UI.Xaml;
using MyBudgetApp.Enums;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces
{
    public interface IDialogService
    {
        void SetXamlRoot(XamlRoot root);
        Task ShowMessageAsync(string message, DialogType dialogType = DialogType.Info);
        Task<bool> ShowConfirmationAsync(string message);
    }
}
