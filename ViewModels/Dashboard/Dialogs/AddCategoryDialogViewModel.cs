namespace MyBudgetApp.ViewModels.Dashboard.Dialogs
{
    public partial class AddCategoryDialogViewModel : BaseViewModel
    {
        private string _categoryName = string.Empty;
        private static string FormatCategoryName(string input)
        {
            input = input.Trim();
            input = System.Text.RegularExpressions.Regex.Replace(input, @"\s+", " ");
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        public string CategoryName
        {
            get => _categoryName;
            set => SetProperty(ref _categoryName, FormatCategoryName(value));
        }
    }
}