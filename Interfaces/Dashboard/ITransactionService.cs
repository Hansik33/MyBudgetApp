using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface ITransactionService
    {
        Task DeleteTransactionAsync(int transactionId);
    }
}
