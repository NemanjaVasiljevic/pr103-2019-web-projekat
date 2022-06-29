using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Komentar
    {
        public Komentar()
        {
            Ocena = 0;
        }

        public Komentar(Korisnik posetilac, FitnesCentar fitnesCentar, string tekst, int ocena)
        {
            Posetilac = posetilac;
            FitnesCentar = fitnesCentar;
            Tekst = tekst;
            Ocena = ocena;
        }

        public Korisnik Posetilac { get; set; }
        public FitnesCentar FitnesCentar { get; set; }
        public string Tekst { get; set; }
        public int Ocena { get; set; }
    }
}