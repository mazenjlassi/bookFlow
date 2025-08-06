using bookFlow.Enum;
using bookFlow.Models;
using bookFlow.Repositories.Interfaces;
using bookFlow.Services.Interfaces;

namespace bookFlow.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;

        public LoanService(ILoanRepository loanRepo, IBookRepository bookRepo)
        {
            _loanRepository = loanRepo;
            _bookRepository = bookRepo;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _loanRepository.GetAllAsync();
        }

        public async Task<Loan?> GetByIdAsync(Guid id)
        {
            return await _loanRepository.GetByIdAsync(id);
        }

        public async Task<Loan?> CreateLoanAsync(string isbn, Guid userId)
        {
            var book = await _bookRepository.GetByIsbnAsync(isbn);
            if (book == null || !book.IsAvailable) return null;

            var newLoan = new Loan
            {
                Id = Guid.NewGuid(),
                BookId = book.Id,
                UserId = userId,
                StartDate = DateTime.UtcNow,
                Status = LoanStatus.EN_COURS
            };

            await _loanRepository.AddAsync(newLoan);
            await _loanRepository.SaveChangesAsync();
            return newLoan;
        }

        public async Task<bool> UpdateLoanStatusAsync(Guid loanId, Guid userId, LoanStatus newStatus, bool isAdmin)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null) return false;

            if (!isAdmin && loan.UserId != userId) return false;

            // Users can only cancel loans if the current status is EN_COURS
            if (!isAdmin)
            {
                if (loan.Status != LoanStatus.EN_COURS || newStatus != LoanStatus.ANNULE)
                    return false;
            }

            loan.Status = newStatus;
            _loanRepository.Update(loan);
            return await _loanRepository.SaveChangesAsync();
        }
    }
}
