using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class VlasnikController : Controller
    {
        private static FitnesCentar novaTeretana = new FitnesCentar();

        [HttpPost]
        public ActionResult AddNewTeretana()
        {
            Korisnik vlasnik = (Korisnik)Session["user"];
            ViewBag.vlasnik = vlasnik;
            return View("NewTeretana");
        }


        [HttpPost]
        public ActionResult AdresaTeretane(Adresa a)
        {
            try
            {
                bool unet = a.Ulica.Equals(null) || a.Broj.Equals(null) || a.Grad.Equals(null);
            }
            catch (Exception)
            {

                ViewBag.message = "Sva polja moraju biti popunjena!";
                return View("AdresaTeretane");
            }

            if(a.PostanskiBroj == 0)
            {
                ViewBag.message = "Sva polja moraju biti popunjena!";
                return View("AdresaTeretane");
            }
            if (a.Ulica.Equals(" ") || a.Broj.Equals(" ") || a.Grad.Equals(" "))
            {
                ViewBag.message = "Sva polja moraju biti popunjena!";
                return View("AdresaTeretane");
            }

            novaTeretana.Adresa = a;

            List<FitnesCentar> sveTeretane = FitnesCentar.ReadFromJson();
            foreach (FitnesCentar x in sveTeretane)
            {
                if (x.Naziv.Equals(novaTeretana.Naziv) && x.Adresa.Grad.Equals(novaTeretana.Adresa.Grad))
                {
                    ViewBag.message = $"Teretana sa nazivom {x.Naziv} vec postoji u gradu {x.Adresa.Grad}";
                    return View("NewTeretana");
                }
            }

            List<Korisnik> sviKorisnici = Korisnik.ReadFromJson();
            foreach (Korisnik x in sviKorisnici)
            {
                if (x.KorisnickoIme.Equals(novaTeretana.Vlasnik.KorisnickoIme))
                {
                    foreach (FitnesCentar fc in x.FitnesCentarVlasnik)
                    {
                        if (fc.Naziv.Equals(novaTeretana.Naziv) && fc.Adresa.Grad.Equals(novaTeretana.Adresa.Grad))
                        {
                            ViewBag.message = $"Teretana sa nazivom {fc.Naziv} vec postoji u gradu {fc.Adresa.Grad} i vi ste vlasnik";
                            return View("NewTeretana");
                        }
                    }
                    x.FitnesCentarVlasnik.Add(novaTeretana);
                }
            }


            Console.WriteLine();

            sveTeretane.Add(novaTeretana);

            FitnesCentar.WriteToJson(sveTeretane);
            Korisnik.WriteToJson(sviKorisnici);

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult NovaTeretana(FitnesCentar teretana)
        {
            List<FitnesCentar> sveTeretane = FitnesCentar.ReadFromJson();
            Korisnik vlasnik = (Korisnik)Session["user"];
            teretana.Vlasnik = vlasnik;
            novaTeretana = teretana;

            try
            {
                bool prazan =teretana.Naziv.Equals(null);
            }
            catch
            {
                ViewBag.message = "Sva polja moraju biti popunjena!";
                return View("NewTeretana");
            }

            if (teretana.GodinaOtvaranja == 0 ||
                teretana.CenaMesec == 0 || teretana.CenaGodina == 0 ||                    
                teretana.CenaGrupniTrening == 0 || teretana.CenaPersonalniTrening == 0 ||
                teretana.CenaTrening == 0)
            {
                ViewBag.message = "Sva polja moraju biti popunjena!";
                return View("NewTeretana");
            }

            if (teretana.Naziv.Equals(" "))
            {
                ViewBag.message = "Sva polja moraju biti popunjena!";
                return View("NewTeretana");
            }

            if (teretana.CenaMesec < 0 || teretana.CenaGodina < 0 || teretana.CenaTrening < 0 ||
               teretana.CenaPersonalniTrening < 0 || teretana.CenaGrupniTrening < 0)
            {
                ViewBag.message = "Cena ne moze biti manja od 0!";
                return View("NewTeretana");
            }

            return View("AdresaTeretane");
        }
    }
}