using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;
using Library.ViewModels;
using System.Data.Entity;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace Library.Controllers
{
    public class RentalsController : Controller
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Rentals
        public ActionResult Index()
        {
            var rentals = _context.Rentals.Include(x => x.Book).Include(z => z.Client).ToList();
            return View(rentals);
        }

        public ActionResult New()
        {
            var book = _context.Books.ToList();
            var client = _context.Clients.ToList();

            var viewModel = new RentalViewModel
            {
                Books = book,
                Clients = client
            };

            return View("New", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Rental rental)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new RentalViewModel
                {
                    Books = _context.Books.ToList(),
                    Clients = _context.Clients.ToList()
                };
                return View("New", viewModel);
            }

            if (rental.Id == 0)
            {
                _context.Rentals.Add(rental);
                if (rental.DateReturned == null)
                {
                    var book = _context.Books.Where(x => x.Id == rental.BookId).ToList();
                    foreach (var item in book)
                    {
                        if(item.NumberInStock >= 1) { 
                        item.NumberInStock--;
                        }
                        else
                        {
                            return HttpNotFound("Please choose other book");
                        }
                    }
                }      
            }
            else
            {
                var rentalInDb = _context.Rentals.Single(c => c.Id == rental.Id);
                
                rentalInDb.BookId = rental.BookId;
                rentalInDb.ClientId = rental.ClientId;
                rentalInDb.DateRented = rental.DateRented;
                rentalInDb.DateReturned = rental.DateReturned;

                if (rental.DateReturned != null)
                {
                    var book = _context.Books.Where(x => x.Id == rental.BookId).ToList();
                    foreach (var item in book)
                    {
                        
                        item.NumberInStock += 1;
                    }
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Rentals");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = _context.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rental rental = _context.Rentals.Find(id);
            var book = _context.Books.Where(x => x.Id == rental.BookId).ToList();
            foreach (var item in book)
            {
                if (rental.DateReturned == null)
                {
                    item.NumberInStock += 1;
                }
            }

            _context.Rentals.Remove(rental);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var rental = _context.Rentals.SingleOrDefault(x => x.Id == id);

            if (rental == null)
            {
                return HttpNotFound();
            }

            var viewModel = new RentalViewModel(rental)
            {
                Books = _context.Books.ToList(),
                Clients = _context.Clients.ToList()
            };

            return View("New", viewModel);
        }
    }
}