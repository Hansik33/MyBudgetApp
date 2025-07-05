using MyBudgetApp.Interfaces;

namespace MyBudgetApp.Services
{
    public class UserContextService : IUserContext
    {
        public int UserId { get; set; } = 0;
        public string Username { get; set; } = string.Empty;
        public void Clear()
        {
            UserId = 0;
            Username = string.Empty;
        }
    }
}