using System.Collections.Generic;

namespace BooksAndAuthors.Models
{
    public class IndexView
    {
        public Author FormAuthor;
        public Book FormBook;
        public List<Author> AllAuthors = new List<Author>();
        public List<Book> AllBooks = new List<Book>();

    }
}