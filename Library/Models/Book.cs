using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public int NumberInStock { get; set; }
        public int ReleaseDate { get; set; }
        public int NumberOfPages { get; set; }
    }
}