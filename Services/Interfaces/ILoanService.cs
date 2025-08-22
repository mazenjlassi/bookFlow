using bookFlow.Dto;
using bookFlow.Enum;
using bookFlow.Models;

namespace bookFlow.Services.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan?> GetLoanByIdAsync(Guid id);

        Task<LoanDto?> CreateLoanAsync(Guid bookId, Guid userId);
        Task<bool> UpdateLoanStatusAsync(Guid loanId, Guid userId, LoanStatus newStatus, bool isAdmin);

        Task<bool> DeleteLoanIfEnCoursAsync(Guid loanId);

        Task<IEnumerable<LoanDto>> GetAllLoansByUserIdAsync(Guid userId);

    }
}
