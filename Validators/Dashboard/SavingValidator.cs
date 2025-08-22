using MyBudgetApp.Enums.ValidationResults;
using MyBudgetApp.ViewModels.Dashboard;
using System.Collections.Generic;
using System.Linq;

namespace MyBudgetApp.Validators.Dashboard
{
    public static class SavingValidator
    {
        public static SavingValidationResult Validate(string savingAmount,
                                                      IEnumerable<SavingGoalViewModel> savingGoals,
                                                      decimal currentBalance)
        {
            if (string.IsNullOrEmpty(savingAmount))
                return SavingValidationResult.AmountEmpty;
            if (!decimal.TryParse(savingAmount, out var amount))
                return SavingValidationResult.AmountNotANumber;
            if (amount < 0)
                return SavingValidationResult.AmountNegative;
            if (amount == 0)
                return SavingValidationResult.AmountZero;
            if (amount > currentBalance)
                return SavingValidationResult.AmountTooLarge;
            if (!savingGoals.Any())
                return SavingValidationResult.SavingGoalNotSelected;
            return SavingValidationResult.Success;
        }
    }
}