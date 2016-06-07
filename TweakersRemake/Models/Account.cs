using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace ASP.NET_MVC_Application.Models
{
    public class Account
    {
        public  int Id { get; set; }
        public string Naam { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public  string Geslacht { get; set; }
        public string Woonplaats { get; set; }
        public string Opleiding { get; set; }
        public string ProfielNaam { get; set; }
        public DateTime Geregistreerd { get; set; }
        public DateTime Wijziging { get; set; }



    }
}