using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP.NET_MVC_Application.Models;

namespace TweakersRemake.Models
{
    public class Games : Product
    {
        public string Platvorm { get; set; }
        public string Genre { get; set; }
        public int Leeftijd { get; set; }
        public string Ontwikkelaar { get; set; }
    }

    public class Console : Product
    {
        public string Kleur { get; set; }
        public string Bijzonderheden { get; set; }
        public string Uitvoering { get; set; }
        public string Opslag { get; set; }
    }

    public class Tv : Product
    {
        public string Scherm { get; set; }
        public string Hd { get; set; }
        public string  Refresh { get; set; }
        public string Smart { get; set; }

    }

    public class GameCrit : Preview
    {
        public double Gameplay { get; set; }
        public double Beeld { get; set; }
        public double Geluid { get; set; }
        public double Prestaties { get; set; }
        public double Replay { get; set; }
        public double Kwaliteit { get; set; }
    }

    public class ConsoleCrits : Preview
    {
        public double Bouw { get; set; }
        public double Media { get; set; }
        public double Multi { get; set; }
        public double Spelaanbod { get; set; }
        public double Acces { get; set; }
    }

    public class TvCrit : Preview
    {
        public double Feature { get; set; }
        public double Kleur { get; set; }
        public double Contrast { get; set; }
        public double Ghost { get; set; }
        public double Pixel { get; set; }
        public double TyBouwpe { get; set; }
        public double Vorm { get; set; }
        public double Inbrand { get; set; }
    }

}