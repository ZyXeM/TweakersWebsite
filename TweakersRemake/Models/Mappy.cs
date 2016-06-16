using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP.NET_MVC_Application.Models;

namespace TweakersRemake.Models
{
    public class Mappy
    {
        public int Id { get; set; }

        public string Naam { get; set; }
        public string Hoofdonderwerp { get; set; }

      public  List<Post> posts { get; set; }
        /// <summary>
        /// Krijgt alle hoofd posts
        /// </summary>
        /// <returns></returns>
        public bool GetMainPost()
        {
            posts = Database.GetPosts(Id);
            return true;
        }
        /// <summary>
        /// Krijgt alle gespreken die in deze map horen
        /// </summary>
        /// <param name="Onderwerp"></param>
        /// <returns></returns>
        public bool GetChainPost(string Onderwerp)
        {
            posts = Database.GetChainPost(Onderwerp);
            return true;
        }
    }
}