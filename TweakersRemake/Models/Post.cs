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

        public void AddPost( string profielnaam)
        {
            Database.AddPosts(this, profielnaam);
        }

        public void ReactPosts(string profielnaam)
        {
            Database.ReactPosts(this, profielnaam);
        }

        public bool DeleteChainPost(int Mappy)
        {
            return Database.DeleteChainPost(Mappy,Onderwerp);
        }

        public bool DeletePost()
        {
            return Database.DeletePost(Id);
        }

    }





    }
