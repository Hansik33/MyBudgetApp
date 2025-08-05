using MyBudgetApp.Enums.ValidationResults;
using MyBudgetApp.ViewModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBudgetApp.Validators.Dashboard
{
    public static class CategoryValidator
    {
        public static CategoryNameValidationResult ValidateName(string categoryName, IEnumerable<CategoryViewModel> categories)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return CategoryNameValidationResult.Empty;
            if (categoryName.Length < 2)
                return CategoryNameValidationResult.TooShort;
            if (categoryName.Length > 30)
                return CategoryNameValidationResult.TooLong;
            if (categories.Any(category => category.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase)))
                return CategoryNameValidationResult.NotUnique;
            return CategoryNameValidationResult.Success;
        }

        public static bool IsDeletionAllowed(CategoryViewModel category,
                                              IEnumerable<BudgetViewModel> budgets,
                                              IEnumerable<TransactionViewModel> transactions)
        {
            bool usedInBudgets = budgets.Any(budget => budget.CategoryId == category.Id);
            bool usedInTransactions = transactions.Any(transaction => transaction.CategoryId == category.Id);

            return !(usedInBudgets || usedInTransactions);
        }
    }
}