using MyBudgetApp.Enums.ValidationResults;
using MyBudgetApp.ViewModels.Dashboard;
using System.Collections.Generic;
using System.Linq;

namespace MyBudgetApp.Validators
{
    public static class BudgetValidator
    {
        public static BudgetValidationResult Validate(string limitAmount, IEnumerable<CategoryViewModel> categories)
        {
            if (string.IsNullOrEmpty(limitAmount))
                return BudgetValidationResult.Empty;
            if (!decimal.TryParse(limitAmount, out var amount))
                return BudgetValidationResult.NotANumber;
            if (amount < 0)
                return BudgetValidationResult.Negative;
            if (amount == 0)
                return BudgetValidationResult.Zero;
            if (amount > 1000000)
                return BudgetValidationResult.TooLarge;
            if (!categories.Any())
                return BudgetValidationResult.CategoryNotSelected;
            return BudgetValidationResult.Success;
        }
    }
}