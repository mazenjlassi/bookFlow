using bookFlow.Models;

namespace bookFlow.Repositories.Interfaces
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<Loan?> GetByIsbnAndUserAsync(string isbn, Guid userId);
    }
}
