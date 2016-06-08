using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Database = TweakersRemake.Database;

namespace ASP.NET_MVC_Application.Models
{
    public class Product
    {
        public  int Id { get; set; }
        public string Naam  { get; set; }
        public string  Categorie { get; set; }
        public string Foto_Url { get; set; }

        public  List<Product_Link> Links { get; set; }

        public List<Preview> Reviews { get; set; }

        public void FillLink()
        {
            Links = Database.GetLinks(Id);
        }

        public void FillReview()
        {
            Reviews = Database.GetPreviews(Categorie, Id);



        }
    }
}