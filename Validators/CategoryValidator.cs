using MyBudgetApp.ViewModels.Dashboard;
using System.Collections.Generic;
using System.Linq;

namespace MyBudgetApp.Validators
{
    public static class CategoryValidator
    {
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