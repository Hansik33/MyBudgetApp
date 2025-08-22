namespace MyBudgetApp.Enums.ValidationResults
{
    public enum SavingValidationResult
    {
        Success,
        AmountEmpty,
        AmountNotANumber,
        AmountNegative,
        AmountZero,
        AmountTooLarge,
        SavingGoalNotSelected
    }
}