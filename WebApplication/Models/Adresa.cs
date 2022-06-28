using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Adresa
    {
        public Adresa(string ulica, string broj, string grad, int postanskiBroj)
        {
            Ulica = ulica;
            Broj = broj;
            Grad = grad;
            PostanskiBroj = postanskiBroj;
        }
        public Adresa()
        {
            Ulica = "";
            Broj = "";
            Grad = "";
            PostanskiBroj = 0;
        }

        public string Ulica { get; set; }
        public string Broj { get; set; }
        public string Grad { get; set; }
        public int PostanskiBroj { get; set; }



    }
}