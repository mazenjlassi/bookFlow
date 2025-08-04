using bookFlow.Data;
using bookFlow.Models;
using bookFlow.Repositories.Implementations;
using bookFlow.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bookFlow.Repositories.Implimentations
{
    public class BookRepository : GenericRepository<Book> ,IBookRepository 
    {
        private readonly UserDbContext _context;

        public BookRepository(UserDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Book?> GetByIsbnAsync(string isbn)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }



    }
}
