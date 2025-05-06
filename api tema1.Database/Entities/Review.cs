using System;

namespace api_tema1.Database.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string ReaderName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}