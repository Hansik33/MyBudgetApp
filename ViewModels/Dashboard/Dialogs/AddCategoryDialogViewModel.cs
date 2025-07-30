namespace MyBudgetApp.ViewModels.Dashboard.Dialogs
{
    public partial class AddCategoryDialogViewModel : BaseViewModel
    {
        private string _categoryName = string.Empty;
        private static string FormatCategoryName(string input)
        {
            input = input.Trim();
            input = MyRegex().Replace(input, " ");
            return char.ToUpper(input[0]) + input[1..];
        }

        public string CategoryName
        {
            get => _categoryName;
            set => SetProperty(ref _categoryName, FormatCategoryName(value));
        }

        [System.Text.RegularExpressions.GeneratedRegex(@"\s+")]
        private static partial System.Text.RegularExpressions.Regex MyRegex();
    }
}