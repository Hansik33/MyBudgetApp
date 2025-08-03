using MyBudgetApp.Utils;

namespace MyBudgetApp.ViewModels.Dashboard.Dialogs
{
    public partial class AddCategoryDialogViewModel : BaseViewModel
    {
        private string _categoryName = string.Empty;
        public string CategoryName
        {
            get => _categoryName;
            set => SetProperty(ref _categoryName, StringFormatter.Format(value));
        }
    }
}