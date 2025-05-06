using System.Collections.Generic;
using System.Threading.Tasks;
using api_tema1.Core.Models;

namespace api_tema1.Core.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksWithReviewsAsync();
        Task<BookDto> GetBookWithReviewsAsync(int id);
    }
}