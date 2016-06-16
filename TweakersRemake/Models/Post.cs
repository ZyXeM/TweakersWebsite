using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweakersRemake;

namespace ASP.NET_MVC_Application.Models
{
    public class Post
    {
        public  int Id { get; set; }

        public  Post PrePost { get; set; }

        public  Account From { get; set; }

        public string Message { get; set; }

        public string Onderwerp { get; set; }

        public int Mappy { get; set; }
        /// <summary>
        /// Voegt hoofd post toe
        /// </summary>
        /// <param name="profielnaam"></param>
        public bool AddPost( string profielnaam)
        {
           return Database.AddPosts(this, profielnaam);
        }
        /// <summary>
        /// Voegt deze post toe aansluiten zijn prepost
        /// </summary>
        /// <param name="profielnaam"></param>
        public bool ReactPosts(string profielnaam)
        {
           return Database.ReactPosts(this, profielnaam);
        }
        /// <summary>
        /// Verwijderd alle post met het onderwerp uit deze post in een bepaalde map
        /// </summary>
        /// <param name="Mappy">map id</param>
        /// <returns></returns>
        public bool DeleteChainPost(int Mappy)
        {
            return Database.DeleteChainPost(Mappy,Onderwerp);
        }
        /// <summary>
        /// Verwijderd deze post uit de database
        /// </summary>
        /// <returns></returns>
        public bool DeletePost()
        {
            return Database.DeletePost(Id);
        }

    }





    }
