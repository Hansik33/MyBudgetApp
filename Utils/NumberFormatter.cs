namespace MyBudgetApp.Utils
{
    public static class NumberFormatter
    {
        public static string FormatThousands(decimal value)
            => value.ToString("N0", new System.Globalization.CultureInfo("pl-PL"));
    }
}
