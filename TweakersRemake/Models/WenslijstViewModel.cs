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

        public bool AddProduct(int Idp)
        {
            if (Database.AddProductToWishlist(Id, Idp))
            {
                return true;
            }
            return false;
        }

        public bool AddWishList(string Profielnaam)
        {
            return Database.AddWishlist(Profielnaam,Naam);
        }

    }
}