using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BooksAndAuthors.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksAndAuthors.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;

        public HomeController(Context contextModel)
        {
            _context = contextModel;
        }
        public IActionResult Index()
        {
            IndexView ShowAuthors = new IndexView();
            ShowAuthors.AllAuthors = _context.Authors.Include(author => author.Books).ToList();
            ShowAuthors.AllBooks = _context.Books.Include(book => book.Author).ToList();

            return View(ShowAuthors);
        }


        [HttpPost("AddAuthor")]
        public IActionResult About(Author formAuthor)
        {
            ViewData["Message"] = "Your application description page.";
            if(ModelState.IsValid)
            {
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine($"author id is {formAuthor.Name}");
                // add author
                _context.Authors.Add(formAuthor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // display the error messages
                IndexView AuthorErrors = new IndexView(){
                    FormAuthor = formAuthor,
                    AllAuthors = _context.Authors.Include(author => author.Books).ToList(),
                    AllBooks = _context.Books.Include(book => book.Author).ToList()
                };

                return View("Index", AuthorErrors);
            }
            // for delete
            // Author AuthorToChange = _context.Authors.FirstOrDefault(author => author.Name.Contains("Dave"));

            // _context.Authors.Remove(AuthorToChange);

        }

        [HttpPost("AddBook")]
        public IActionResult AddBook(Book formBook)
        {
            if(ModelState.IsValid)
            {

                // find the author object to add to the book
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine($"author id is {formBook.AuthorId}");
                Console.WriteLine(formBook.Title);
                Console.WriteLine($"book instance is: {formBook}");
                Console.WriteLine($"book title is {formBook.Title}");
                Author author = _context.Authors.SingleOrDefault(a => a.AuthorId == formBook.AuthorId);
                // Author author = _context.Authors.SingleOrDefault(a => a.AuthorId == 2);
                // set the  book author field the the author instance
                formBook.Author = author;
                // add the book
                _context.Books.Add(formBook);
                // save the book
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                // display the errors
                IndexView AuthorErrors = new IndexView(){
                    FormBook = formBook,
                    AllAuthors = _context.Authors.Include(author => author.Books).ToList(),
                    AllBooks = _context.Books.Include(book => book.Author).ToList()
                };
                return View("Index", AuthorErrors);
            }
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            // for update
            Author AuthorToChange = _context.Authors.FirstOrDefault(author => author.Name.Contains("Dr."));
            AuthorToChange.Name = "Dave";
            _context.SaveChanges();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
