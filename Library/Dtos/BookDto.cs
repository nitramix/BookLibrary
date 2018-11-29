using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Models;

namespace Library.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public GenreDto Genre { get; set; }
        public int GenreId { get; set; }
        public AuthorDto Author { get; set; }
        public int AuthorId { get; set; }
        public int NumberInStock { get; set; }
        public int ReleaseDate { get; set; }
        public int NumberOfPages { get; set; }
    }
}