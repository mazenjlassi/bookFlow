using bookFlow.Models;

namespace bookFlow.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(Book book);
        Task<bool> UpdateAvailabilityAsync(Guid id);
        Task<bool> DeleteAsync(Book book);
        Task<Book?> GetByIsbnAsync(string isbn);
        Task<Book?> GetByNameAsync(string Title);

    }
}
