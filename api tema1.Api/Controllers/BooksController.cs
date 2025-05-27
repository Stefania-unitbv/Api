using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using api_tema1.Core.Models;
using api_tema1.Core.Services;

namespace api_tema1.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(
            [FromQuery] string author = null,
            [FromQuery] int? yearFrom = null,
            [FromQuery] int? yearTo = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "title")
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var validSortOptions = new[] { "title", "author", "year", "id" };
            if (!validSortOptions.Contains(sortBy.ToLower()))
                sortBy = "title";

            var books = await _bookService.GetAllBooksWithReviewsAsync(author, yearFrom, yearTo, page, pageSize, sortBy);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _bookService.GetBookWithReviewsAsync(id);

            if (book == null)
                return NotFound($"Book with ID {id} not found");

            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateBook(int id, [FromBody] BookUpdateDto bookUpdate)
        {
            if (bookUpdate == null)
                return BadRequest("Book data is required");

            try
            {
               
                var serviceBookUpdate = new api_tema1.Core.Services.BookUpdateDto
                {
                    Title = bookUpdate.Title,
                    Author = bookUpdate.Author,
                    Year = bookUpdate.Year,
                    ISBN = bookUpdate.ISBN
                };

                var updatedBook = await _bookService.UpdateBookAsync(id, serviceBookUpdate);
                return Ok(updatedBook);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Book with ID {id} not found");
            }
        }
    }

    public class BookUpdateDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
    }
}