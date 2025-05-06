using System.Collections.Generic;

namespace api_tema1.Core.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
}