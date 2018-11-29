using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Models;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class BookViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public Book Book { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int? GenreId { get; set; }

        [Required]
        [Display(Name = "Author")]
        public int? AuthorId { get; set; }

        [Display(Name = "Release Year")]
        [Required]
        public int? ReleaseDate { get; set; }

        [Display(Name = "Number in stock")]
        [Range(1, 20)]
        [Required]
        public int? NumberInStock { get; set; }

        [Required]
        public int NumberOfPages { get; set; }


        public string _Title
        {
            get
            {
                return Id != 0 ? "Edit Book" : "New Book";
            }
        }

        public BookViewModel()
        {
            Id = 0;
        }

        public BookViewModel(Book book)
        {
            Id = book.Id;
            Title = book.Title;
            GenreId = book.GenreId;
            AuthorId = book.AuthorId;
            NumberInStock = book.NumberInStock;
            NumberOfPages = book.NumberOfPages;
            ReleaseDate = book.ReleaseDate;
        }
    }
}