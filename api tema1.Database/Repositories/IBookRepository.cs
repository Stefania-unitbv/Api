using System.Collections.Generic;
using System.Threading.Tasks;
using api_tema1.Database.Entities;

namespace api_tema1.Database.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksWithReviewsAsync();
        Task<Book> GetBookWithReviewsAsync(int id);
    }
}