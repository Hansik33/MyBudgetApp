namespace MyBudgetApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password_hash { get; set; } = string.Empty;
    }
}
