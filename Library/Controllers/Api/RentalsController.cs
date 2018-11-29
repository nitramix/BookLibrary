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
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/rentals
        public IEnumerable<RentalDto> GetRentals(string query = null)
        {
            var rentalQuery = _context.Rentals
                .Include(x => x.Book).Include(c => c.Client)
                .Where(m => m.NumberInStock >= 0);

            if (!String.IsNullOrWhiteSpace(query))
                rentalQuery = rentalQuery.Where(m => m.Id.ToString().Contains(query));

            return rentalQuery
                .ToList()
                .Select(Mapper.Map<Rental, RentalDto>);
        }

        //GET /api/rentals/1
        public IHttpActionResult GetRental(int id)
        {
            var rental = _context.Rentals.SingleOrDefault(x => x.Id == id);

            if (rental == null)
                return NotFound();

            return Ok(Mapper.Map<Rental, RentalDto>(rental));
        }

        //POST /api/rentals/1
        [HttpPost]
        public IHttpActionResult CreateRental(RentalDto rentalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var rental = Mapper.Map<RentalDto, Rental>(rentalDto);
            _context.Rentals.Add(rental);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + rental.Id), rentalDto);
        }

        //PUT /api/rentals/1
        [HttpPut]
        public void UpdateRental(int id, RentalDto rentalDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var rentalInDb = _context.Rentals.SingleOrDefault(x => x.Id == id);

            if (rentalInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(rentalDto, rentalInDb);

            _context.SaveChanges();
        }

        //DELETE /api/rentals/1
        [HttpDelete]
        public void DeleteRental(int id)
        {
            var rentalInDb = _context.Rentals.SingleOrDefault(x => x.Id == id);

            if (rentalInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Rentals.Remove(rentalInDb);
            _context.SaveChanges();
        }


    }
}
