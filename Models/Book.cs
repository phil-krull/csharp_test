using System;
using System.ComponentModel.DataAnnotations;

namespace BooksAndAuthors.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Book()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}