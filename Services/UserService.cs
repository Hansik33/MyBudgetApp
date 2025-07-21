using MyBudgetApp.Interfaces;
using MyBudgetApp.Models;

namespace MyBudgetApp.Services
{
    public class UserService(IDatabaseService databaseService) : IUserService
    {
        public bool AddUser(string username, string plainPassword) =>
            databaseService.AddUser(username, plainPassword);
        public User? GetUserByCredentials(string username, string plainPassword) =>
            databaseService.GetUserByCredentials(username, plainPassword);
    }
}