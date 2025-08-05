using System.Collections.Generic;

namespace MyBudgetApp.Validators.Auth;

public static class AuthenticationValidator
{
    public static IEnumerable<string> Validate(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            yield return "UsernameEmpty";

        if (string.IsNullOrWhiteSpace(password))
            yield return "PasswordEmpty";
    }

    public static IEnumerable<string> Validate(string username, string password, string confirmPassword)
    {
        if (string.IsNullOrWhiteSpace(username))
            yield return "UsernameEmpty";

        if (string.IsNullOrWhiteSpace(password))
            yield return "PasswordEmpty";

        if (password != confirmPassword)
            yield return "PasswordMismatch";
    }
}
