using MyBudgetApp.Enums;

namespace MyBudgetApp.Validators
{
    public static class BudgetValidator
    {
        public static BudgetLimitValidationResult ValidateLimit(string limitAmount)
        {
            if (string.IsNullOrEmpty(limitAmount))
                return BudgetLimitValidationResult.Empty;
            if (!decimal.TryParse(limitAmount, out var amount))
                return BudgetLimitValidationResult.NotANumber;
            if (amount < 0)
                return BudgetLimitValidationResult.Negative;
            if (amount == 0)
                return BudgetLimitValidationResult.Zero;
            if (amount > 1000000)
                return BudgetLimitValidationResult.TooLarge;
            return BudgetLimitValidationResult.Success;
        }
    }
}