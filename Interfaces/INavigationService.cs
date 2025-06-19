using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces
{
    public interface INavigationService
    {
        void NavigateTo(UserControl view);
        void GoToLogin();
        void GoToRegister();
    }
}
