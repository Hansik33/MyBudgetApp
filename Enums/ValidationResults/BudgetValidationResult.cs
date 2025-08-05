namespace MyBudgetApp.Enums.ValidationResults
{
    public enum BudgetValidationResult
    {
        Success,
        Empty,
        NotANumber,
        Negative,
        Zero,
        TooLarge,
        NotUnique,
        CategoryNotSelected
    }
}