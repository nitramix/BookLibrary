using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Library.Models;

namespace Library.ViewModels
{
    public class ClientViewModel
    {
        public Client Client { get; set; }

        public string _Title
        {
            get
            {
                return Client.Id != 0 ? "Edit client" : "New client";
            }
        }
    }
}