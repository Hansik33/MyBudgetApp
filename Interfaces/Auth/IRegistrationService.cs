using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Auth
{
    public interface IRegistrationService
    {
        Task<bool> RegisterAsync(string username, string password, string confirmPassword);
    }
}