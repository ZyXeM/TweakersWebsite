using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace ASP.NET_MVC_Application.Models
{
    public class Product_Link
    {
        public Uitgever Uitgever { get; set; }
        
        public double Prijs { get; set; }

        public string Url { get; set; }

        public string Levertijd { get; set; }


    }
}