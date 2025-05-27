using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_tema1.Core.Models;
using api_tema1.Database.Repositories;

namespace api_tema1.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksWithReviewsAsync(
     string author = null,
     int? yearFrom = null,
     int? yearTo = null,
     int page = 1,
     int pageSize = 10,
     string sortBy = "title")
        {
            var books = await _bookRepository.GetAllBooksWithReviewsAsync(author, yearFrom, yearTo, page, pageSize, sortBy);

            // Map to DTOs
            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Year = b.Year,
                ISBN = b.ISBN,
                Reviews = b.Reviews?.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    ReaderName = r.ReaderName,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate
                }).ToList() ?? new List<ReviewDto>()
            }).ToList();
        }
        public async Task<BookDto> GetBookWithReviewsAsync(int id)
        {
            var book = await _bookRepository.GetBookWithReviewsAsync(id);

            if (book == null)
                return null;

            // Map to DTO
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                ISBN = book.ISBN,
                Reviews = book.Reviews?.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    ReaderName = r.ReaderName,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate
                }).ToList() ?? new List<ReviewDto>()
            };
        }
        //public async Task<BookDto> UpdateBookAsync(int id, BookUpdateDto bookUpdate)
        //{
        //    var book = await _bookRepository.UpdateBookAsync(id, bookUpdate);

        //    return new BookDto
        //    {
        //        Id = book.Id,
        //        Title = book.Title,
        //        Author = book.Author,
        //        Year = book.Year,
        //        ISBN = book.ISBN,
        //        Reviews = book.Reviews?.Select(r => new ReviewDto
        //        {
        //            Id = r.Id,
        //            ReaderName = r.ReaderName,
        //            Rating = r.Rating,
        //            Comment = r.Comment,
        //            ReviewDate = r.ReviewDate
        //        }).ToList() ?? new List<ReviewDto>()
        //    };
        //}
    }
}