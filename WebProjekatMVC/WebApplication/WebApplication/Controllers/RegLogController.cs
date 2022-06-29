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
            if (defaultKorisnici.Contains(k))
            {
                ViewBag.Message = $"User with {k.KorisnickoIme} already exists!";
                return View();
            }

            defaultKorisnici.Add(k);
            Korisnik.WriteToJson(defaultKorisnici);
            Session["user"] = k;
            return View("Login");
        }

        public ActionResult Login(string username, string password)
        {
            if(username.Equals(String.Empty) || username.Equals(String.Empty))
            {
                ViewBag.message = "All fields must be filled";
                return View("Login");
            }
            List<Korisnik> defaultKorisnici = Korisnik.ReadFromJson();
            Korisnik user = defaultKorisnici.Find(u => u.KorisnickoIme.Equals(username) && u.Lozinka.Equals(password));
            if (user == null)
            {
                ViewBag.Message = $"User with credentials does not exist!";
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