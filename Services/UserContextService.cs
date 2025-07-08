using MyBudgetApp.Interfaces;

namespace MyBudgetApp.Services
{
    public class UserContextService : IUserContext
    {
        public int UserId { get; set; } = 0;
        public required string Username { get; set; }
        public void Clear()
        {
            UserId = 0;
            Username = string.Empty;
        }
    }
}