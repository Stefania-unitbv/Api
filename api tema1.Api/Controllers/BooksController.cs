using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        [HttpGet]
        [HttpGet]
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

      //  [HttpPut("{id}")]
        //public async Task<ActionResult<BookDto>> UpdateBook(int id, [FromBody] BookUpdateDto bookUpdate)
        //{
        //    if (bookUpdate == null)
        //        return BadRequest("Book data is required");

        //    var updatedBook = await _bookService.UpdateBookAsync(id, bookUpdate);
        //    return Ok(updatedBook);
        //}
    }
}