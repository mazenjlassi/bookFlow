using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using bookFlow.Models;
using bookFlow.Repositories.Interfaces;
using bookFlow.Data;
using bookFlow.Enum;

namespace bookFlow.Repositories.Implementations
{
    public class LoanRepository : ILoanRepository
    {
        private readonly UserDbContext _context;

        public LoanRepository(UserDbContext context)
        {
            _context = context;
        }



        public async Task AddAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
        }

        public void Delete(Loan entity)
        {
            _context.Loans.Remove(entity);
        }

        public async Task DeleteAsync(Loan loan)
        {
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteLoanIfEnCoursAsync(Guid loanId)
        {
            var loan = await _context.Loans
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.Id == loanId);

            if (loan == null || loan.Status != LoanStatus.EN_COURS)
                return false;

            // Set book back to available
            if (loan.Book != null)
                loan.Book.IsAvailable = true;

            _context.Loans.Remove(loan);

            // Tell EF explicitly that the book entity has changed
            _context.Books.Update(loan.Book);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Loan>> FindAsync(Expression<Func<Loan, bool>> predicate)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .ToListAsync();
        }

        public async Task<Loan?> GetByIdAsync(Guid id)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Loan?> GetByIsbnAndUserAsync(string isbn, Guid userId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.Book.ISBN == isbn && l.UserId == userId && l.Status == LoanStatus.EN_COURS);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Loan entity)
        {
            _context.Loans.Update(entity);
        }

        // Additional useful methods you might want to add:

        public async Task<IEnumerable<Loan>> GetLoansByUserIdAsync(Guid userId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByBookIdAsync(Guid bookId)
        {
            return await _context.Loans
                .Include(l => l.Book)   // optional: load book details
                .Include(l => l.User)   // optional: load user who borrowed
                .Where(l => l.BookId == bookId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByStatusAsync(LoanStatus status)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(l => l.Status == status)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Loans.AnyAsync(l => l.Id == id);
        }


        public async Task<Loan> ApproveLoanAsync(Guid loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null || loan.Status != Enum.LoanStatus.EN_COURS)
                return null;

            loan.Status = Enum.LoanStatus.APPROVED;
            await _context.SaveChangesAsync();
            return loan;
        }
    }
}