using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using api_tema1.Database.Context;
using api_tema1.Database.Entities;

namespace api_tema1.Database.Repositories
{
   
    public class BookUpdateDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
    }

    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithReviewsAsync(
            string author = null,
            int? yearFrom = null,
            int? yearTo = null,
            int page = 1,
            int pageSize = 10,
            string sortBy = "title")
        {
            IQueryable<Book> query = _context.Books.Include(b => b.Reviews);

            // Filtru după autor
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.ToLower().Contains(author.ToLower()));
            }

            // Filtru după an - de la
            if (yearFrom.HasValue)
            {
                query = query.Where(b => b.Year >= yearFrom.Value);
            }

            // Filtru după an - până la
            if (yearTo.HasValue)
            {
                query = query.Where(b => b.Year <= yearTo.Value);
            }

            // Sortare
            switch (sortBy.ToLower())
            {
                case "author":
                    query = query.OrderBy(b => b.Author);
                    break;
                case "year":
                    query = query.OrderBy(b => b.Year);
                    break;
                case "id":
                    query = query.OrderBy(b => b.Id);
                    break;
                case "title":
                default:
                    query = query.OrderBy(b => b.Title);
                    break;
            }

            // Paginare
            var skip = (page - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<Book> GetBookWithReviewsAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> UpdateBookAsync(int id, BookUpdateDto bookUpdate)
        {
            var book = await _context.Books
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found");

            if (!string.IsNullOrWhiteSpace(bookUpdate.Title))
                book.Title = bookUpdate.Title;

            if (!string.IsNullOrWhiteSpace(bookUpdate.Author))
                book.Author = bookUpdate.Author;

            if (bookUpdate.Year > 0)
                book.Year = bookUpdate.Year;

            if (!string.IsNullOrWhiteSpace(bookUpdate.ISBN))
                book.ISBN = bookUpdate.ISBN;

            await _context.SaveChangesAsync();
            return book;
        }
    }
}