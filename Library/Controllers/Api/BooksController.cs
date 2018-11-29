using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library.Models;
using Library.Dtos;
using System.Data.Entity;
using AutoMapper;

namespace Library.Controllers.Api
{
    public class BooksController : ApiController
    {
        private ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/books

        public IEnumerable<BookDto> GetBooks(string query = null)
        {
            var books = _context.Books.Include(x => x.Genre).Include(c => c.Author).Where(b => b.NumberInStock > 0);

            if (!String.IsNullOrWhiteSpace(query))
            {
                books = books.Where(c => c.Title.Contains(query));
            }

            return books.ToList().Select(Mapper.Map<Book, BookDto>);
        }

        //GET /api/books/1
        public IHttpActionResult GetBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if (book == null)
                return NotFound();

            return Ok(Mapper.Map<Book, BookDto>(book));
        }

        //POST /api/books
        [HttpPost]
        public IHttpActionResult CreateBook(BookDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var book = Mapper.Map<BookDto, Book>(bookDto);
            _context.Books.Add(book);
            _context.SaveChanges();

            bookDto.Id = book.Id;
            return Created(new Uri(Request.RequestUri + "/" + book.Id), bookDto);
        }

        //PUT /api/books/1
        [HttpPut]
        public IHttpActionResult UpdateBook(int id, BookDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var bookInDb = _context.Books.SingleOrDefault(x => x.Id == id);

            if (bookInDb == null)
                return NotFound();

            Mapper.Map(bookDto, bookInDb);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/books/1
        [HttpDelete]
        public IHttpActionResult DeleteBook(int id)
        {
            var bookInDb = _context.Books.SingleOrDefault(x => x.Id == id);

            if (bookInDb == null)
                return NotFound();

            _context.Books.Remove(bookInDb);
            _context.SaveChanges();

            return Ok();
        }
        

    }
}
