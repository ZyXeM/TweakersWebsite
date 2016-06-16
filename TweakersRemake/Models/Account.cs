using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using TweakersRemake;

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

        public string Wachtwoord { get; set; }
        /// <summary>
        /// Checked of dit profiel bestaat
        /// </summary>
        /// <returns></returns>
        public bool Isvalid()
        {
            if (Database.Isvalid(ProfielNaam, Wachtwoord))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Voegt dit profiel toe aan de database
        /// </summary>
        /// <returns></returns>
        public bool Register()
        {
            if (Database.RegisterAccount(this))
            {
                return true;
            }
            return false;
        }



    }
}