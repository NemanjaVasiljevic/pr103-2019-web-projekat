﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication.Models.Enums;

namespace WebApplication.Models
{
    public class Korisnik
    {
        public Korisnik()
        {

        }
        public Korisnik(string korisnickoIme, string lozinka, string ime, string prezime, Pol pol, string email, string datumRodjenja)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            Email = email;
            DatumRodjenja = datumRodjenja;
            Uloga = Uloga.POSETILAC;
            FitnesCentarTrener = new FitnesCentar();
            FitnesCentarVlasnik = new List<FitnesCentar>();
            GrupniTreninziPosetilac = new List<GrupniTrening>();
            GrupniTreninziTrener = new List<GrupniTrening>();

        }

        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public Pol Pol { get; set; }
        public string Email { get; set; }
        public string DatumRodjenja { get; set; }
        public Uloga Uloga { get; set; }
        public FitnesCentar FitnesCentarTrener { get; set; }
        public List<FitnesCentar> FitnesCentarVlasnik { get; set; }
        public List<GrupniTrening> GrupniTreninziPosetilac { get; set; }
        public List<GrupniTrening> GrupniTreninziTrener { get; set; }


        public static List<Korisnik> ReadFromJson()
        {
            List<Korisnik> korisnici = new List<Korisnik>();

            string jsonFromFile;
            using (var reader = new StreamReader("C:\\Users\\Nemanja\\Desktop\\WebProjekat\\pr103-2019-web-projekat\\WebProjekatMVC\\WebApplication\\WebApplication\\TextFiles\\Korisnici.json"))
            {
                jsonFromFile = reader.ReadToEnd();
            }
            //try
            //{
                List<Korisnik> users = JsonConvert.DeserializeObject<List<Korisnik>>(jsonFromFile);
                foreach (var x in users)
                {
                    korisnici.Add(x);
                }
                return korisnici;
            //}
            //catch
            //{
            //    return korisnici;
            //}

        }

        public static void WriteToJson(List<Korisnik> k)
        {
            var jsonToWrite = JsonConvert.SerializeObject(k);
            using (var writer = new StreamWriter("C:\\Users\\Nemanja\\Desktop\\WebProjekat\\pr103-2019-web-projekat\\WebProjekatMVC\\WebApplication\\WebApplication\\TextFiles\\Korisnici.json"))
            {
                writer.Write(jsonToWrite);
            }
        }
    }
}