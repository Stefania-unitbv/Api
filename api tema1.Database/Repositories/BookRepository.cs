using System.Collections.Generic;
using System.Threading.Tasks;
using api_tema1.Database.Context;
using api_tema1.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_tema1.Database.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithReviewsAsync()
        {
            return await _context.Books
                .Include(b => b.Reviews)
                .ToListAsync();
        }

        public async Task<Book> GetBookWithReviewsAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}