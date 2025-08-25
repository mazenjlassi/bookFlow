using System;
using bookFlow.Data;
using bookFlow.Dto;
using bookFlow.Enum;
using bookFlow.Models;
using bookFlow.Repositories.Interfaces;
using bookFlow.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bookFlow.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly UserDbContext _context;

        public LoanService(ILoanRepository loanRepo, IBookRepository bookRepo,UserDbContext context)
        {
            _loanRepository = loanRepo;
            _bookRepository = bookRepo;
            _context = context;

        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _loanRepository.GetAllAsync();
        }

        public async Task<Loan?> GetLoanByIdAsync(Guid id)
        {
            return await _loanRepository.GetByIdAsync(id);
        }


        public async Task<LoanDto?> CreateLoanAsync(Guid bookId, Guid userId)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (book == null || user == null || !book.IsAvailable)
                return null;
            // Calculate dates
            var startDate = DateTime.UtcNow;
            var returnDate = startDate.AddDays(15);

            // create loan
            var loan = new Loan
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                UserId = userId,
                StartDate = DateTime.UtcNow,
                ReturnDate = DateTime.UtcNow.AddDays(14),
                Status = LoanStatus.EN_COURS

            };

            // mark book as unavailable
            book.IsAvailable = false;

            await _loanRepository.AddAsync(loan);
            await _loanRepository.SaveChangesAsync();
           
            // Map to DTO
            return new LoanDto
            {
                Id = loan.Id,
                BookId = loan.BookId,
                BookTitle = book.Title,
                UserId = loan.UserId,
                UserName = user.Username,
                StartDate = loan.StartDate,
                ReturnDate = loan.ReturnDate,
                Status = loan.Status.ToString()
            };
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
        public async Task<bool> DeleteLoanIfEnCoursAsync(Guid loanId)
        {
            return await _loanRepository.DeleteLoanIfEnCoursAsync(loanId);
        }

        public async Task<IEnumerable<LoanDto>> GetAllLoansByUserIdAsync(Guid userId)
        {
            var loans = await _loanRepository.FindAsync(l => l.UserId == userId);

            return loans.Select(l => new LoanDto
            {
                Id = l.Id,
                BookId = l.BookId,
                BookTitle = l.Book?.Title ?? "N/A",
                UserId = l.UserId,
                UserName = l.User?.Username ?? "N/A",
                StartDate = l.StartDate,
                ReturnDate = l.ReturnDate,
                Status = l.Status.ToString()
            });
        }


        public async Task<IEnumerable<Loan>> GetLoansByBookIdAsync(Guid bookId)
        {
            return await _loanRepository.GetLoansByBookIdAsync(bookId);
        }
        public async Task<Loan> ApproveLoanAsync(Guid loanId)
        { 
           return await _loanRepository.ApproveLoanAsync(loanId);
        }
    }
}
