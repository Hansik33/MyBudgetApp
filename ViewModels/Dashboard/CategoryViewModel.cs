using MyBudgetApp.Models;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public partial class CategoryViewModel(Category category)
    {
        public Category Model => category;

        public int Id => category.Id;
        public string Name => category.Name;
        public int UserId => category.UserId;
    }
}