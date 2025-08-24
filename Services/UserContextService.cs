using MyBudgetApp.Interfaces;

namespace MyBudgetApp.Services
{
    public class UserContextService : IUserContext
    {
        public required int Id { get; set; } = 0;
        public required string Username { get; set; }

        public void Clear()
        {
            Id = 0;
            Username = string.Empty;
        }
    }
}