using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Models;

namespace Library.Dtos
{
    public class RentalDto
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public int NumberInStock { get; set; }
        public Rental Rentals { get; set; }
        public DateTime DateRented { get; set; } = DateTime.Now;
        public DateTime? DateReturned { get; set; }
    }
}