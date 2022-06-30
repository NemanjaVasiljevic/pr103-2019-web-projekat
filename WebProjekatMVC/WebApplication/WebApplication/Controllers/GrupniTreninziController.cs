using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class GrupniTreninziController : Controller
    {
        private static List<GrupniTrening> sortirani = new List<GrupniTrening>();
        // GET: GrupniTreninzi
        public ActionResult Index()
        {
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];

            Korisnik k = (Korisnik)Session["user"];
            if (k == null)
            {
                ViewBag.message = "Ulogujte se da biste mogli pogledati profil";
                return RedirectToAction("IndexProfil", "RegLog");
            }
            ViewBag.korisnik = k;
            if(treninzi.Count == 0)
            {
                ViewBag.treninzi = sortirani;
            }
            else
            {
                ViewBag.treninzi = treninzi;
            }
           
            return View();
        }

        public ActionResult SearchByNaziv(string naziv)
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziPosetilac;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();

            if (naziv.Equals(""))
            {
                return RedirectToAction("Profil", "Home");
            }

            foreach (GrupniTrening x in gt)
            {
                if (x.Naziv.Equals(naziv))
                {
                    treninzi.Add(x);
                }
            }

            return RedirectToAction("Index", "GrupniTreninzi");
        }

        public ActionResult SearchByNazivTrener(string naziv)
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziTrener;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();

            if (naziv.Equals(""))
            {
                return RedirectToAction("Profil", "Home");
            }

            foreach (GrupniTrening x in gt)
            {
                if (x.Naziv.Equals(naziv))
                {
                    treninzi.Add(x);
                }
            }

            return RedirectToAction("Index", "GrupniTreninzi");
        }

        public ActionResult SearchByTipTreninga(string tipTreninga)
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziPosetilac;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();

            if (tipTreninga.Equals(""))
            {
                return RedirectToAction("Profil", "Home");
            }

            foreach (GrupniTrening x in gt)
            {
                if (x.TipTreninga.Equals(tipTreninga))
                {
                    treninzi.Add(x);
                }
            }

            return RedirectToAction("Index", "GrupniTreninzi");
        }

        public ActionResult SearchByTipTreningaTrener(string tipTreninga)
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziTrener;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();

            if (tipTreninga.Equals(""))
            {
                return RedirectToAction("Profil", "Home");
            }

            foreach (GrupniTrening x in gt)
            {
                if (x.TipTreninga.Equals(tipTreninga))
                {
                    treninzi.Add(x);
                }
            }

            return RedirectToAction("Index", "GrupniTreninzi");
        }

        public ActionResult SearchByNazivCentra(string centar)
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziPosetilac;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();

            if (centar.Equals(""))
            {
                return RedirectToAction("Profil", "Home");
            }

            foreach (GrupniTrening x in gt)
            {
                if (x.Fitnes_centar.Equals(centar))
                {
                    treninzi.Add(x);
                }
            }

            return RedirectToAction("Index", "GrupniTreninzi");
        }

        public ActionResult SearchByDatumTrener(string datum, string vreme)
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziTrener;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();

            if (datum.Equals("") || vreme.Equals(""))
            {
                return RedirectToAction("Profil", "Home");
            }

            foreach (GrupniTrening x in gt)
            {
                if (x.DatumTreninga.Equals(datum) && x.VremeTreninga.Equals(vreme))
                {
                    treninzi.Add(x);
                }
            }

            return RedirectToAction("Index", "GrupniTreninzi");
        }

        public ActionResult KombinovanaPretraga(string naziv, string tipTreninga, string centar)
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziPosetilac;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();

            if (centar.Equals("") || tipTreninga.Equals("") || naziv.Equals(""))
            {
                return RedirectToAction("Profil", "Home");
            }

            foreach (GrupniTrening x in gt)
            {
                if (x.Naziv.Equals(naziv) && x.TipTreninga.Equals(tipTreninga) &&x.Fitnes_centar.Equals(centar))
                {
                    treninzi.Add(x);
                }
            }

            return RedirectToAction("Index", "GrupniTreninzi");
        }

        public ActionResult KombinovanaPretragaTrener(string naziv, string tipTreninga, string datum, string vreme)
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziTrener;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();

            if (naziv.Equals("") || tipTreninga.Equals("") || datum.Equals("") || vreme.Equals(""))
            {
                return RedirectToAction("Profil", "Home");
            }

            foreach (GrupniTrening x in gt)
            {
                if (x.Naziv.Equals(naziv) && x.TipTreninga.Equals(tipTreninga) && x.DatumTreninga.Equals(datum) && x.VremeTreninga.Equals(vreme))
                {
                    treninzi.Add(x);
                }
            }

            return RedirectToAction("Index", "GrupniTreninzi");
        }


        public ActionResult SortByNazivAsc()
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziPosetilac;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortirani = gt.OrderBy(x => x.Naziv).ToList();
            return RedirectToAction("Index", "GrupniTreninzi");

        }
        public ActionResult SortByNazivAscTrener()
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziTrener;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortirani = gt.OrderBy(x => x.Naziv).ToList();
            return RedirectToAction("Index", "GrupniTreninzi");

        }
        public ActionResult SortByNazivDesc()
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziPosetilac;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortirani = gt.OrderBy(x => x.Naziv).Reverse().ToList();
            return RedirectToAction("Index", "GrupniTreninzi");

        }
        public ActionResult SortByNazivDescTrener()
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziTrener;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortirani = gt.OrderBy(x => x.Naziv).Reverse().ToList();
            return RedirectToAction("Index", "GrupniTreninzi");

        }



        public ActionResult SortByTipTreningaAsc()
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziPosetilac;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortirani = gt.OrderBy(x => x.TipTreninga).ToList();
            return RedirectToAction("Index", "GrupniTreninzi");

        }
        public ActionResult SortByTipTreningaAscTrener()
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziTrener;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortirani = gt.OrderBy(x => x.TipTreninga).ToList();
            return RedirectToAction("Index", "GrupniTreninzi");

        }
        public ActionResult SortByTipTreningaDesc()
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziPosetilac;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortirani = gt.OrderBy(x => x.TipTreninga).Reverse().ToList();
            return RedirectToAction("Index", "GrupniTreninzi");

        }
        public ActionResult SortByTipTreningaDescTrener()
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziTrener;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortirani = gt.OrderBy(x => x.TipTreninga).Reverse().ToList();
            return RedirectToAction("Index", "GrupniTreninzi");

        }



        public ActionResult SortByDatumAsc()
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziPosetilac;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortirani = gt.OrderBy(x => x.DatumTreninga).ToList();
            return RedirectToAction("Index", "GrupniTreninzi");

        }
        public ActionResult SortByDatumAscTrener()
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziTrener;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortirani = gt.OrderBy(x => x.DatumTreninga).ToList();
            return RedirectToAction("Index", "GrupniTreninzi");

        }
        public ActionResult SortByDatumDesc()
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziPosetilac;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortirani = gt.OrderBy(x => x.DatumTreninga).Reverse().ToList();
            return RedirectToAction("Index", "GrupniTreninzi");

        }
        public ActionResult SortByDatumDescTrener()
        {
            List<Korisnik> users = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];

            foreach (Korisnik x in users)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziTrener;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortirani = gt.OrderBy(x => x.DatumTreninga).Reverse().ToList();
            return RedirectToAction("Index", "GrupniTreninzi");

        }
    }
}