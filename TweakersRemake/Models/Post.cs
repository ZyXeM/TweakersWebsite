﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

      





    }
}