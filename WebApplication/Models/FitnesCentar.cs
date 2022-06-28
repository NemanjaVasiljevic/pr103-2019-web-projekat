using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class FitnesCentar
    {
        public FitnesCentar(string naziv, Adresa adresa, int godinaOtvaranja, Korisnik vlasnik, double cenaMesec, double cenaGodina, double cenaTrening, double cenaGrupniTrening, double cenaPersonalniTrening)
        {
            Naziv = naziv;
            Adresa = adresa;
            GodinaOtvaranja = godinaOtvaranja;
            Vlasnik = vlasnik;
            CenaMesec = cenaMesec;
            CenaGodina = cenaGodina;
            CenaTrening = cenaTrening;
            CenaGrupniTrening = cenaGrupniTrening;
            CenaPersonalniTrening = cenaPersonalniTrening;
        }

        public FitnesCentar()
        {
            Naziv = "";
            Adresa = new Adresa();
            GodinaOtvaranja = 0;
            Vlasnik = new Korisnik();
            CenaMesec = 0;
            CenaGodina = 0;
            CenaTrening = 0;
            CenaGrupniTrening = 0;
            CenaPersonalniTrening = 0;
        }

        public string Naziv { get; set; }
        public Adresa Adresa { get; set; }
        public int GodinaOtvaranja { get; set; }
        public Korisnik Vlasnik { get; set; }
        public double CenaMesec { get; set; }
        public double CenaGodina { get; set; }
        public double CenaTrening { get; set; }
        public double CenaGrupniTrening { get; set; }
        public double CenaPersonalniTrening { get; set; }



    }
}