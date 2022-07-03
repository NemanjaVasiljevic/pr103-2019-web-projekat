using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class RegLogController : Controller
    {
        public ActionResult Index(){
            Korisnik k = (Korisnik)Session["user"];
            if (k != null)
            {
                ViewBag.message = $"Vec ste ulogovani kao {k.KorisnickoIme}";
                return View("Notification");
            }
            return View("Login");
        }

        public ActionResult IndexProfil()
        {
            Korisnik k = (Korisnik)Session["user"];
            if (k == null)
            {
                ViewBag.message = "Ulogujte se da biste mogli pogledati profil";
                return View("Login");
            }
            return View("Login");
        }

        public ActionResult Register()
        {
            Korisnik k = new Korisnik();
            Session["user"] = k;
            return View(k);
        }
        [HttpPost]
        public ActionResult Register(Korisnik k)
        {
            List<Korisnik> defaultKorisnici = Korisnik.ReadFromJson();
            foreach (Korisnik x in defaultKorisnici)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    ViewBag.Message = $"Korisnik {k.KorisnickoIme} vec postoji!";
                    return View("Register");
                }
            }
            foreach (Korisnik x in defaultKorisnici)
            {
                if (x.Email.Equals(k.Email))
                {
                    ViewBag.Message = $"Korisnik sa mejlon {k.Email} vec postoji!";
                    return View("Register");
                }
            }
            if (k.KorisnickoIme == "" || k.Lozinka == "" || k.Ime == "" || k.Prezime == "" ||
               k.Pol.ToString() == "" || k.Email == "" || k.DatumRodjenja == "")
            {
                ViewBag.message = "Sva polja moraju biti popunjena";
                return View("Register");
            }
            if (k.KorisnickoIme == null || k.Lozinka == null || k.Ime == null || k.Prezime == null ||
              k.Pol.ToString() == null || k.Email == null || k.DatumRodjenja == null)
            {
                ViewBag.message = "Sva polja moraju biti popunjena";
                return View("Register");
            }

            if (k.Email.Split('@').Length == 1)
            {
                ViewBag.message = "Email nije unet kako treba";
                return View("Register");
            }
            defaultKorisnici.Add(k);
            Korisnik.WriteToJson(defaultKorisnici);
            Session["user"] = k;
            return View("Login");
        }

        public ActionResult Login(string username, string password)
        {
           
            if (username.Equals(String.Empty) || username.Equals(String.Empty))
            {
                ViewBag.message = "Sva polja moraju biti popunjena";
                return View("Login");
            }
            List<Korisnik> defaultKorisnici = Korisnik.ReadFromJson();
            Korisnik user = defaultKorisnici.Find(u => u.KorisnickoIme.Equals(username) && u.Lozinka.Equals(password));
            if (user == null)
            {
                ViewBag.Message = $"Korisnik sa unetim korisnickim imenom i lozinkom ne postoji!";
                return View("Login");
            }

            Session["user"] = user;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session["user"] = null;

            return RedirectToAction("Index", "Home");

        }
    }
}