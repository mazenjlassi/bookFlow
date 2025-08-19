using bookFlow.Models;

namespace bookFlow.Repositories.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book?> GetByIsbnAsync(string isbn);

        Task<Book?> GetByNameAsync(string Title);

        Task<Book?> GetByIdAsync(Guid id);
        Task UpdateAsync(Book book);



    }
}
