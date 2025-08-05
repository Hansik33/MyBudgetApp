using MyBudgetApp.Enums.ValidationResults;
using MyBudgetApp.ViewModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBudgetApp.Validators.Dashboard
{
    public static class CategoryValidator
    {
        public static CategoryValidationResult ValidateName(string name, IEnumerable<CategoryViewModel> categories)
        {
            if (string.IsNullOrWhiteSpace(name))
                return CategoryValidationResult.Empty;
            if (name.Length < 2)
                return CategoryValidationResult.TooShort;
            if (name.Length > 30)
                return CategoryValidationResult.TooLong;
            if (categories.Any(category => category.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                return CategoryValidationResult.NotUnique;
            return CategoryValidationResult.Success;
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