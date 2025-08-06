using bookFlow.Enum;
using bookFlow.Models;

namespace bookFlow.Services.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(Guid id);
        Task<Loan?> CreateLoanAsync(string isbn, Guid userId);
        Task<bool> UpdateLoanStatusAsync(Guid loanId, Guid userId, LoanStatus newStatus, bool isAdmin);
    }
}
