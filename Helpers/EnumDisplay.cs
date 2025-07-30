namespace MyBudgetApp.Helpers
{
    public class EnumDisplay<T>(T value, string displayName)
    {
        public T Value { get; } = value;
        public string DisplayName { get; } = displayName;

        public override string ToString() => DisplayName;
    }
}