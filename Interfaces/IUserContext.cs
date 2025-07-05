namespace MyBudgetApp.Interfaces
{
    public interface IUserContext
    {
        int UserId { get; set; }
        string Username { get; set; }
        void Clear();
    }
}