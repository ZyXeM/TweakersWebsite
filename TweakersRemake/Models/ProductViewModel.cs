using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP.NET_MVC_Application.Models;

namespace TweakersRemake.Models
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }

        public List<WenslijstViewModel> Wenslijst { get; set; }

        /// <summary>
        /// Krijgt alle producten die vergerleken met elkaar moeten worden
        /// </summary>
        /// <param name="profielnaam"></param>
        public void GetCompare(string profielnaam)
        {
           Products =  Database.GetCompare(profielnaam);
        }
    }
}