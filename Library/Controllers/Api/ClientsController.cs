using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library.Models;
using Library.Dtos;
using AutoMapper;

namespace Library.Controllers.Api
{
    public class ClientsController : ApiController
    {

        private ApplicationDbContext _context;

        public ClientsController()
        {
            _context = new ApplicationDbContext();
        }



        //GET /api/clients
        public IEnumerable<ClientDto> GetClients()
        {
            return _context.Clients.ToList().Select(Mapper.Map<Client, ClientDto>);
        }

        //GET /api/clients/1
        public IHttpActionResult GetClient(int id)
        {
            var client = _context.Clients.SingleOrDefault(x => x.Id == id);

            if (client == null)
                return NotFound();

            return Ok(Mapper.Map<Client, ClientDto>(client));
        }

        //POST /api/clients
        [HttpPost]
        public IHttpActionResult CreateClient(ClientDto clientDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var client = Mapper.Map<ClientDto, Client>(clientDto);
             _context.Clients.Add(client);
            _context.SaveChanges();


            clientDto.Id = client.Id;

            return Created(new Uri(Request.RequestUri + "/" + client.Id), clientDto);
        }

        //PUT /api/clients/1
        [HttpPut]
        public void UpdateClient(int id, ClientDto clientDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var clientInDb = _context.Clients.SingleOrDefault(x => x.Id == id);

            if (clientInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map(clientDto, clientInDb);
       

            _context.SaveChanges();
        }


        //DELETE /api/clients/1
        [HttpDelete]
        public void DeleteClient(int id)
        {
            var clientInDb = _context.Clients.SingleOrDefault(x => x.Id == id);

            if (clientInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Clients.Remove(clientInDb);
            _context.SaveChanges();
        }

    }
}
