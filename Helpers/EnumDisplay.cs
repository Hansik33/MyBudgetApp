namespace MyBudgetApp.Helpers
{
    public class EnumDisplay<T>
    {
        public T Value { get; }
        public string DisplayName { get; }

        public EnumDisplay(T value, string displayName)
        {
            Value = value;
            DisplayName = displayName;
        }

        public override string ToString() => DisplayName;
    }
}