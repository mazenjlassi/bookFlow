using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using bookFlow.Models;
using bookFlow.Repositories.Interfaces;
using System;
using bookFlow.Data;

namespace bookFlow.Repositories.Implimentations
{
    public class LoanRepository : ILoanRepository
    {
        private readonly UserDbContext _context;

        public LoanRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Loan entity)
        {
            await _context.Loans.AddAsync(entity);
        }

        public void Delete(Loan entity)
        {
            _context.Loans.Remove(entity);
        }

        public async Task<IEnumerable<Loan>> FindAsync(Expression<Func<Loan, bool>> predicate)
        {
            return await _context.Loans.Include(l => l.Book).Include(l => l.User).Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loans.Include(l => l.Book).Include(l => l.User).ToListAsync();
        }

        public async Task<Loan?> GetByIdAsync(Guid id)
        {
            return await _context.Loans.Include(l => l.Book).Include(l => l.User).FirstOrDefaultAsync(l => l.Id== id);
        }

        public async Task<Loan?> GetByIsbnAndUserAsync(string isbn, Guid userId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.Book.ISBN == isbn && l.UserId == userId && l.Status == Enum.LoanStatus.EN_COURS);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Loan entity)
        {
            _context.Loans.Update(entity);
        }
    }
}
