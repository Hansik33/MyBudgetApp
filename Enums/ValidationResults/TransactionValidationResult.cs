namespace MyBudgetApp.Enums.ValidationResults
{
    public enum TransactionValidationResult
    {
        Success,
        AmountEmpty,
        AmountNotANumber,
        AmountNegative,
        AmountZero,
        AmountTooLarge,
        DescriptionTooShort,
        DescriptionTooLong,
        DateInvalid,
        CategoryNotSelected,
        AdditionNotAllowed
    }
}
