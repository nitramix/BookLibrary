using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Models;

namespace Library.ViewModels
{
    public class RentalViewModel
    {
        public Rental Rental { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public int? Id { get; set; }
        public int? BookId { get; set; }
        public int? ClientId { get; set; }
        public int NumberInStock { get; set; }
        public DateTime DateRented { get; set; } = DateTime.Now;
        public DateTime? DateReturned { get; set; }

        public RentalViewModel()
        {
            Id = 0;
        }

        public RentalViewModel(Rental rental)
        {
            Id = rental.Id;
            BookId = rental.BookId;
            ClientId = rental.ClientId;
            DateRented = DateTime.Now;
            DateReturned = rental.DateReturned;
        }
        public string _Title
        {
            get
            {
                return Id != 0 ? "Edit rental" : "New Rental";
            }
        }
    }
}