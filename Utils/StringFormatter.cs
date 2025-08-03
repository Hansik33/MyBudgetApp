using System.Text.RegularExpressions;

namespace MyBudgetApp.Utils
{
    public static partial class StringFormatter
    {
        [GeneratedRegex(@"\s+")]
        private static partial Regex WhiteSpaceRegex();

        public static string Format(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            input = input.Trim();
            input = WhiteSpaceRegex().Replace(input, " ");
            return char.ToUpper(input[0]) + input[1..];
        }
    }
}