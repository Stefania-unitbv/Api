using System.Collections.Generic;
using System.Threading.Tasks;
using api_tema1.Core.Models;

namespace api_tema1.Core.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksWithReviewsAsync(
            string author = null,
            int? yearFrom = null,
            int? yearTo = null,
            int page = 1,
            int pageSize = 10,
            string sortBy = "title");
        Task<BookDto> GetBookWithReviewsAsync(int id);
        Task<BookDto> UpdateBookAsync(int id, BookUpdateDto bookUpdate);
    }
}