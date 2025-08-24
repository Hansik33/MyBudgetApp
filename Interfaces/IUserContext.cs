namespace MyBudgetApp.Interfaces
{
    public interface IUserContext
    {
        int Id { get; set; }
        string Username { get; set; }

        void Clear();
    }
}