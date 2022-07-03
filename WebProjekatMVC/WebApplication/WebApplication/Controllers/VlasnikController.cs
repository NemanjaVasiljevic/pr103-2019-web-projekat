using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Models.Enums;

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


        [HttpPost]
        public ActionResult AddNewTrener()
        {
            Korisnik vlasnik = (Korisnik)Session["user"];
            List<Korisnik> svi = Korisnik.ReadFromJson();
            foreach (Korisnik x in svi)
            {
                if (x.KorisnickoIme.Equals(vlasnik.KorisnickoIme))
                {
                    vlasnik = x;
                    break;
                }
            }

            ViewBag.listaTeretana = vlasnik.FitnesCentarVlasnik;
            return View();
        }

        [HttpPost]
        public ActionResult NovTrener(Korisnik trener)
        {

            List<Korisnik> defaultKorisnici = Korisnik.ReadFromJson();
            Korisnik vlasnik = (Korisnik)Session["user"];
            foreach (Korisnik x in defaultKorisnici)
            {
                if (x.KorisnickoIme.Equals(vlasnik.KorisnickoIme))
                {
                    vlasnik = x;
                    break;
                }
            }
            ViewBag.listaTeretana = vlasnik.FitnesCentarVlasnik;
            foreach (Korisnik x in defaultKorisnici)
            {
                if (x.KorisnickoIme.Equals(trener.KorisnickoIme))
                {
                    ViewBag.Message = $"Korisnik {trener.KorisnickoIme} vec postoji!";
                    return View("AddNewTrener");
                }
            }
            foreach (Korisnik x in defaultKorisnici)
            {
                if (x.Email.Equals(trener.Email))
                {
                    ViewBag.Message = $"Korisnik sa mejlon {trener.Email} vec postoji!";
                    return View("Register");
                }
            }


            if (trener.KorisnickoIme == "" || trener.Lozinka == "" || trener.Ime == "" || trener.Prezime == ""||
               trener.Pol.ToString() == "" || trener.Email == "" || trener.DatumRodjenja == "")
            {
                ViewBag.message = "Sva polja moraju biti popunjena";
                return View("AddNewTrener");
            }
            if (trener.KorisnickoIme == null || trener.Lozinka == null || trener.Ime == null || trener.Prezime == null ||
               trener.Pol.ToString() == null || trener.Email == null || trener.DatumRodjenja == null)
            {
                ViewBag.message = "Sva polja moraju biti popunjena";
                return View("AddNewTrener");
            }

            if (trener.Email.Split('@').Length == 1)
            {
                ViewBag.message = "Email nije unet kako treba";
                return View("AddNewTrener");
            }

            if (trener.FitnesCentarTrener == null)
            {
                trener.FitnesCentarTrener = vlasnik.FitnesCentarVlasnik[0];
            }

            trener.Uloga = Uloga.TRENER;
            defaultKorisnici.Add(trener);
            Korisnik.WriteToJson(defaultKorisnici);

            ViewBag.messageGood = $"Trener {trener.Ime} {trener.Prezime} uspesno dodat!";
            return View("AddNewTrener");
        }
    }
}