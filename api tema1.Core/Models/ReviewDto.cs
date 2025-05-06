using System;

namespace api_tema1.Core.Models
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string ReaderName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}