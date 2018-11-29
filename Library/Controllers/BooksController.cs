using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.ViewModels;
using System.Data.Entity;
using System.Net;

namespace Library.Controllers
{
    public class BooksController : Controller
    {

        private ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Books
        public ActionResult Index()
        {
            //var books = _context.Books.Include(x => x.Genre).Include(z => z.Author).ToList();

            return View();
        }

        public ActionResult Details(int id)
        {
            var book = _context.Books.Include(c => c.Genre).Include(x => x.Author).SingleOrDefault(x => x.Id == id);

            if (book == null)
                return HttpNotFound();

            return View(book);
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var authors = _context.Authors.ToList();

            var viewModel = new BookViewModel()
            {
                Genres = genres,
                Authors = authors
            };

            return View("New", viewModel);
        }

        public ActionResult Save(Book book)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new BookViewModel(book)
                {
                    Genres = _context.Genres.ToList(),
                    Authors = _context.Authors.ToList()
                };
                return View("Index", viewModel);
            }

            if (book.Id == 0)
            {
                _context.Books.Add(book);
            }

            else
            {
                var bookInDb = _context.Books.Single(x => x.Id == book.Id);
                bookInDb.Title = book.Title;
                bookInDb.GenreId = book.GenreId;
                bookInDb.NumberInStock = book.NumberInStock;
                bookInDb.NumberOfPages = book.NumberOfPages;
                bookInDb.ReleaseDate = book.ReleaseDate;
                bookInDb.AuthorId = book.AuthorId;

            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Books");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _context.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = _context.Books.Find(id);
           _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if(book == null)
            {
                return HttpNotFound();
            }

            var viewModel = new BookViewModel(book)
            {
                Genres = _context.Genres.ToList(),
                Authors = _context.Authors.ToList()
            };

            return View("New", viewModel);
        }

    }
}