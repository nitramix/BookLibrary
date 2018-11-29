using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class NewRental
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public List<int> BooksIds { get; set; }
    }
}