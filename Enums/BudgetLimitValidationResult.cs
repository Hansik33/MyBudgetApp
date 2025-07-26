namespace MyBudgetApp.Enums
{
    public enum BudgetLimitValidationResult
    {
        Success,
        Empty,
        NotANumber,
        Negative,
        Zero,
        TooLarge
    }
}
