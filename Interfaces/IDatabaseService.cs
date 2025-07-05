using MyBudgetApp.Models;

namespace MyBudgetApp.Interfaces
{
    public interface IDatabaseService
    {
        bool TryConnect();
        bool InsertUser(string username, string passwordHash);
        User? GetUserByCredentials(string username, string plainPassword);
    }
}
