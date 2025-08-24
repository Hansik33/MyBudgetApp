namespace MyBudgetApp.Enums.ValidationResults
{
    public enum AuthenticationValidationResult
    {
        Success,
        UserEmpty,
        UserTooShort,
        UserTooLong,
        PasswordEmpty,
        PasswordTooShort,
        PasswordTooLong,
        PasswordMismatch
    }
}