using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TweakersRemake.Models
{
    public class WenslijstViewModel
    {
        public string Naam { get; set; }

        public int Id { get; set; }
        /// <summary>
        /// Voegt een product toe aan deze lijst
        /// </summary>
        /// <param name="Idp">Product ID</param>
        /// <returns></returns>
        public bool AddProduct(int Idp)
        {
            if (Database.AddProductToWishlist(Id, Idp))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Voegt deze wenslijst toe aan de user zijn lijsten
        /// </summary>
        /// <param name="Profielnaam"></param>
        /// <returns></returns>
        public bool AddWishList(string Profielnaam)
        {
            return Database.AddWishlist(Profielnaam,Naam);
        }

    }
}