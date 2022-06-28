using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.Enums;

namespace WebApplication.Models
{
    public class Korisnik
    {
        public Korisnik(string korisnickoIme, string lozinka, string ime, string prezime, Pol pol, string email, DateTime datumRodjenja, Uloga uloga, FitnesCentar fitnesCentarTrener, FitnesCentar fitnesCentarVlasnik)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            Email = email;
            DatumRodjenja = datumRodjenja;
            Uloga = uloga;
            FitnesCentarTrener = fitnesCentarTrener;
            FitnesCentarVlasnik = fitnesCentarVlasnik;
            GrupniTreninziPosetilac = new List<GrupniTrening>();
            GrupniTreninziTrener = new List<GrupniTrening>();

        }

        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public Pol Pol { get; set; }
        public string Email { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public Uloga Uloga { get; set; }
        public FitnesCentar FitnesCentarTrener { get; set; }
        public FitnesCentar FitnesCentarVlasnik { get; set; }
        public List<GrupniTrening> GrupniTreninziPosetilac { get; set; }
        public List<GrupniTrening> GrupniTreninziTrener { get; set; }








    }
}