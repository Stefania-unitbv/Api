using System.Collections.Generic;
using System.Threading.Tasks;
using api_tema1.Database.Entities;

namespace api_tema1.Database.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksWithReviewsAsync(
            string author = null,
            int? yearFrom = null,
            int? yearTo = null,
            int page = 1,
            int pageSize = 10,
            string sortBy = "title");
        Task<Book> GetBookWithReviewsAsync(int id);
        Task<Book> UpdateBookAsync(int id, BookUpdateDto bookUpdate);
    }
}