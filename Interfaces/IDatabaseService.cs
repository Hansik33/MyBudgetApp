namespace MyBudgetApp.Interfaces
{
    public interface IDatabaseService
    {
        bool TryConnect();
        bool InsertUser(string username, string passwordHash);
    }
}
