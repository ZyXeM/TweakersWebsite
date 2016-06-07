using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC_Application.Models
{
    public class Uitgever
    {
       public int Id { get; set; }

        public  string Naam { get; set; }

        public  List<Aureview> Reviews { get; set; }


    }
}