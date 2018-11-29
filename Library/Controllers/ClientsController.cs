using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.ViewModels;
using System.Net;

namespace Library.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext _context;


        public ClientsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Clients
        public ActionResult Index()
        {
           // var clients = _context.Clients.ToList();
            return View();
        }

        public ActionResult Details(int id)
        {
            var client = _context.Clients.SingleOrDefault(x => x.Id == id);

            return View(client);
        }

        public ActionResult New()
        {
            var viewModel = new ClientViewModel
            {
                Client = new Client()
            };

            return View("New", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Client client)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ClientViewModel
                {
                    Client = client
                };
                return View("New", viewModel);
            }

            if (client.Id == 0)
            {
                _context.Clients.Add(client);
            }
            else
            {
                var clientInDb = _context.Clients.Single(c => c.Id == client.Id);

                clientInDb.Name = client.Name;
                clientInDb.Phone = client.Phone;
                clientInDb.Address = client.Address;
                clientInDb.City = client.City;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Clients");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = _context.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = _context.Clients.Find(id);
            _context.Clients.Remove(client);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var client = _context.Clients.SingleOrDefault(x => x.Id == id);

            if (client == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ClientViewModel
            {
                Client = client
            };

            return View("New", viewModel);
        }

    }
}