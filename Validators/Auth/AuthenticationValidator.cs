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
        if (username.Length < 3)
            return AuthenticationValidationResult.UserTooShort;
        if (username.Length > 20)
            return AuthenticationValidationResult.UserTooLong;
        if (string.IsNullOrWhiteSpace(password))
            return AuthenticationValidationResult.PasswordEmpty;
        if (password.Length < 4)
            return AuthenticationValidationResult.PasswordTooShort;
        if (password.Length > 32)
            return AuthenticationValidationResult.PasswordTooLong;
        if (password != confirmPassword)
            return AuthenticationValidationResult.PasswordMismatch;
        return AuthenticationValidationResult.Success;
    }
}