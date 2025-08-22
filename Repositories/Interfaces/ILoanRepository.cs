using bookFlow.Models;

namespace bookFlow.Repositories.Interfaces
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<Loan?> GetByIsbnAndUserAsync(string isbn, Guid userId);
        Task AddAsync(Loan loan);
        Task<bool> DeleteLoanIfEnCoursAsync(Guid loanId);

        Task<Loan?> GetByIdAsync(Guid id);

        Task<IEnumerable<Loan>> GetLoansByUserIdAsync(Guid userId);

    }
}
