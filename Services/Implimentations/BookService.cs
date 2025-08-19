using bookFlow.Models;
using bookFlow.Repositories.Implimentations;
using bookFlow.Repositories.Interfaces;
using bookFlow.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bookFlow.Services.Implimentations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> CreateAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
            return await _bookRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync (Book book)
        {
            _bookRepository.Delete(book);
            return await _bookRepository.SaveChangesAsync();
        }
         
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync(); 
        }

        public Task<Book?> GetByIdAsync(Guid id)
        {
            return _bookRepository.GetByIdAsync(id); 
        }

        public async Task<bool> UpdateAvailabilityAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return false;

            book.IsAvailable = !book.IsAvailable;
            await _bookRepository.UpdateAsync(book);
            return true;
        }



        public Task<Book?> GetByIsbnAsync(string isbn)
        {
            return _bookRepository.GetByIsbnAsync(isbn);
        }

        public Task<Book?> GetByNameAsync(string Title)
        {
            return _bookRepository.GetByNameAsync(Title);
        }
    }
}
