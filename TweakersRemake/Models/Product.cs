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
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Categorie { get; set; }
        public string Foto_Url { get; set; }

        public List<Product_Link> Links { get; set; }

        public List<Preview> Reviews { get; set; }

        /// <summary>
        /// Vult het product met alle links naar de uitgever
        /// </summary>
        public void FillLink()
        {
            Links = Database.GetLinks(Id);
        }

        /// <summary>
        /// Vult het prduct met all zijn reviews
        /// </summary>
        public void FillReview()
        {
            Reviews = Database.GetPreviews(Categorie, Id);



        }
        /// <summary>
        /// Voegt het product toe aan het compare lijst
        /// </summary>
        /// <param name="profielnaam"></param>
        /// <returns></returns>
        public bool AddToCompare(string profielnaam)
        {
            return Database.AddToCompare(this, profielnaam);
        }
        /// <summary>
        /// Verwijderd het product uit het betreffende wenslijst
        /// </summary>
        /// <param name="profielnaam"></param>
        /// <param name="Wenslijst">Wenslijst naam</param>
        /// <returns></returns>
        public bool RemoveProduct(string profielnaam, string Wenslijst)
        {
            return Database.RemoveProductFromWishList(Wenslijst, Id, profielnaam);
        }
}
}