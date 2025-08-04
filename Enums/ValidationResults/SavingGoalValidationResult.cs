namespace MyBudgetApp.Enums.ValidationResults
{
    public enum SavingGoalValidationResult
    {
        Success,
        NameEmpty,
        NameTooShort,
        NameTooLong,
        NameNotUnique,
        TargetAmountEmpty,
        TargetAmountNotANumber,
        TargetAmountNegative,
        TargetAmountZero,
        TargetAmountTooLarge,
        DeadlineInvalid
    }
}