using MyBudgetApp.Utils;

namespace MyBudgetApp.ViewModels.Dashboard.Dialogs
{
    public partial class AddCategoryDialogViewModel : BaseViewModel
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, StringFormatter.Format(value));
        }
    }
}