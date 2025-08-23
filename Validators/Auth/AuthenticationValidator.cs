using MyBudgetApp.Enums.ValidationResults;

namespace MyBudgetApp.Validators.Auth;

public static class AuthenticationValidator
{
    public static AuthenticationValidationResult Validate(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            return AuthenticationValidationResult.UserEmpty;
        if (string.IsNullOrWhiteSpace(password))
            return AuthenticationValidationResult.PasswordEmpty;
        return AuthenticationValidationResult.Success;
    }

    public static AuthenticationValidationResult Validate(string username, string password, string confirmPassword)
    {
        if (string.IsNullOrWhiteSpace(username))
            return AuthenticationValidationResult.UserEmpty;
        if (string.IsNullOrWhiteSpace(password))
            return AuthenticationValidationResult.PasswordEmpty;
        if (password != confirmPassword)
            return AuthenticationValidationResult.PasswordMismatch;
        return AuthenticationValidationResult.Success;
    }
}