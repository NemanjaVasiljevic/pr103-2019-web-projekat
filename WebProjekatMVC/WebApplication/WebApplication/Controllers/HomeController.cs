using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private static List<FitnesCentar> listaTeretana = new List<FitnesCentar>();
        //private static List<FitnesCentar> pronadjeneTeretane = new List<FitnesCentar>();
        public ActionResult Index()
        {
            if (listaTeretana.Count == 0)
            {
                List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
                return View(teretane);
            }
            else
            {
                return View(listaTeretana);
            }
        }

        public ActionResult Profil()
        {
            Korisnik k = (Korisnik)Session["user"];
            if(k == null)
            {
                ViewBag.message = "Ulogujte se da biste mogli pogledati profil";
                return RedirectToAction("Index", "RegLog");
            }
            ViewBag.korisnik = k;
            return View();
        }

        public ActionResult Details(string fc)
        {
            FitnesCentar teretana = null;
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            List<GrupniTrening> grupniTreninzi = GrupniTrening.ReadFromJson();
            List<GrupniTrening> spisakTreninga = new List<GrupniTrening>();

            foreach (GrupniTrening x in grupniTreninzi)
            {
                if (x.Fitnes_centar.Naziv.Equals(fc))
                {
                    spisakTreninga.Add(x);
                }
            }

            foreach (FitnesCentar x in teretane)
            {
                if(x.Naziv == fc)
                {
                    teretana = x;
                }
            }
            ViewBag.teretana = teretana;
            ViewBag.grupniTreninzi = spisakTreninga;
            return View();
        }

        public ActionResult SortByNameAsc()
        {
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            listaTeretana = teretane.OrderBy(x => x.Naziv).ToList();
            return RedirectToAction("Index");
        }
        public ActionResult SortByNameDesc()
        {
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            listaTeretana = teretane.OrderBy(x => x.Naziv).Reverse().ToList();
            return RedirectToAction("Index");
        }



        public ActionResult SortByAddressAsc()
        {
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            listaTeretana = teretane.OrderBy(x => x.Adresa.Ulica).ToList();
            return RedirectToAction("Index");
        }
        public ActionResult SortByAddressDesc()
        {
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            listaTeretana = teretane.OrderBy(x => x.Adresa.Ulica).Reverse().ToList();
            return RedirectToAction("Index");
        }


        public ActionResult SortByYearAsc()
        {
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            listaTeretana = teretane.OrderBy(x => x.GodinaOtvaranja).ToList();
            return RedirectToAction("Index");
        }
        public ActionResult SortByYearDesc()
        {
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            listaTeretana = teretane.OrderBy(x => x.GodinaOtvaranja).Reverse().ToList();
            return RedirectToAction("Index");
        }


        public ActionResult SearchByName(string Naziv)
        {
            listaTeretana.Clear();
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            foreach (FitnesCentar x in teretane)
            {
                if(x.Naziv == Naziv)
                {
                    listaTeretana.Add(x);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult SearchByAddress(Adresa a)
        {
            listaTeretana.Clear();
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            foreach (FitnesCentar x in teretane)
            {
                if (x.Adresa.Ulica.Equals(a.Ulica) && x.Adresa.Broj.Equals(a.Broj) && x.Adresa.Grad.Equals(a.Grad))
                {
                    listaTeretana.Add(x);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult SearchByYear(int from, int to)
        {
            listaTeretana.Clear();
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            foreach (FitnesCentar x in teretane)
            {
                if (x.GodinaOtvaranja >= from && x.GodinaOtvaranja <= to)
                {
                    listaTeretana.Add(x);
                }
            }

            return RedirectToAction("Index");
        }
    }
}