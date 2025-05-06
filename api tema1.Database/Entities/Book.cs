using System.Collections.Generic;

namespace api_tema1.Database.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}