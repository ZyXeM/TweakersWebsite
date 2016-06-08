using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC_Application.Models
{
    public class Preview
    {
        public  int Id { get; set; }
       
        
        public  int Prijs { get; set; }

        public string Pluspunten { get; set; }

        public string Minpunten { get; set; }

        public string Text { get; set; }

        public string[,] Review { get; set; }

        public Account Acc { get; set; }

        public void AddPreview()
        {
            
        }

        public void DeletePreview()
        {
            
        }

        
        
    }
}