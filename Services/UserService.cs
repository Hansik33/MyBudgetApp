using MyBudgetApp.Interfaces;
using MyBudgetApp.Models;

namespace MyBudgetApp.Services
{
    public class UserService(IDatabaseService databaseService) : IUserService
    {
        public bool AddUser(string username, string password) =>
            databaseService.AddUser(username, password);
        public User? GetUserByCredentials(string username, string password) =>
            databaseService.GetUserByCredentials(username, password);
    }
}