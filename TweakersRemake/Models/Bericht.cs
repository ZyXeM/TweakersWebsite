using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC_Application.Models
{
    public class Bericht
    {
        public  int Id { get; set; }
        public string Message { get; set; }
        public  Bericht Reachtie { get; set; }

        public  string Onderwerp { get; set; }
        
    }
}