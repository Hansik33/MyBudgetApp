using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Auth
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(string username, string password);
        Task LogoutAsync();
    }
}
