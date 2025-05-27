using System.Collections.Generic;

namespace api_tema1.Core.Models
{
    public class BookUpdateDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
    }
}