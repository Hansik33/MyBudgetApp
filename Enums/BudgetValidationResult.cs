namespace MyBudgetApp.Enums
{
    public enum BudgetValidationResult
    {
        Success,
        Empty,
        NotANumber,
        Negative,
        Zero,
        TooLarge,
        CategoryNotSelected
    }
}
