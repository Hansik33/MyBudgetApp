using MyBudgetApp.Models;

namespace MyBudgetApp.Interfaces
{
    public interface IUserService
    {
        bool InsertUser(string username, string plainPassword);
        User? GetUserByCredentials(string username, string plainPassword);
    }
}

