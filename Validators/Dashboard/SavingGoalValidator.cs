using MyBudgetApp.Enums.ValidationResults;
using MyBudgetApp.ViewModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBudgetApp.Validators.Dashboard
{
    public static class SavingGoalValidator
    {
        public static SavingGoalValidationResult Validate(string name,
                                                          string targetAmount,
                                                          DateTime deadline,
                                                          IEnumerable<SavingGoalViewModel> savingGoals)
        {
            if (string.IsNullOrEmpty(name))
                return SavingGoalValidationResult.NameEmpty;
            if (name.Length < 3)
                return SavingGoalValidationResult.NameTooShort;
            if (name.Length > 30)
                return SavingGoalValidationResult.NameTooLong;
            if (savingGoals.Any(savingGoal => savingGoal.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                return SavingGoalValidationResult.NameNotUnique;
            if (string.IsNullOrEmpty(targetAmount))
                return SavingGoalValidationResult.TargetAmountEmpty;
            if (!decimal.TryParse(targetAmount, out var amount))
                return SavingGoalValidationResult.TargetAmountNotANumber;
            if (amount < 0)
                return SavingGoalValidationResult.TargetAmountNegative;
            if (amount == 0)
                return SavingGoalValidationResult.TargetAmountZero;
            if (amount > 1000000)
                return SavingGoalValidationResult.TargetAmountTooLarge;
            if (!IsDeadlineValid(deadline))
                return SavingGoalValidationResult.DeadlineInvalid;
            return SavingGoalValidationResult.Success;
        }

        public static bool IsDeadlineValid(DateTime deadline)
        {
            var minDate = DateTime.Today.AddMonths(1);
            return deadline.Date >= minDate;
        }
    }
}