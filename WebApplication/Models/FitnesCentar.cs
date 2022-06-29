using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public string Naziv { get; set; }
        public Adresa Adresa { get; set; }
        public int GodinaOtvaranja { get; set; }
        public Korisnik Vlasnik { get; set; }
        public double CenaMesec { get; set; }
        public double CenaGodina { get; set; }
        public double CenaTrening { get; set; }
        public double CenaGrupniTrening { get; set; }
        public double CenaPersonalniTrening { get; set; }


        public static List<FitnesCentar> ReadFromJson()
        {
            List<FitnesCentar> teretane = new List<FitnesCentar>();

            string jsonFromFile;
            using (var reader = new StreamReader("C:\\Users\\Nemanja\\Desktop\\WebProjekat\\pr103-2019-web-projekat\\WebApplication\\TextFiles\\FitnesCentri.json"))
            {
                jsonFromFile = reader.ReadToEnd();
            }
            List<FitnesCentar> fitnesCentri = JsonConvert.DeserializeObject<List<FitnesCentar>>(jsonFromFile);
            foreach (var x in fitnesCentri)
            {
                teretane.Add(x);
            }
            return teretane;
            
        }
    }
}