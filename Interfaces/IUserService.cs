using MyBudgetApp.Models;

namespace MyBudgetApp.Interfaces
{
    public interface IUserService
    {
        bool AddUser(string username, string password);
        User? GetUserByCredentials(string username, string password);
    }
}

