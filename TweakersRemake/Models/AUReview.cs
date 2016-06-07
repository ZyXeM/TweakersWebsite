using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC_Application.Models
{
    public class Aureview
    {
        public int Id { get; set; }

        public  string Reviews { get; set; }
        public  double Beschrijving { get; set; }
        public  double Communicatie { get; set; }
        public  double Aflevering { get; set; }
        public  double Algemeen { get; set; }
        public  double Verzending { get; set; }
        public  double KlantenService { get; set; }
        public string Soort { get; set; }

        public Account From { get; set; }
        public Account To { get; set; }
        public Uitgever Uitgever { get; set; }

    }
}