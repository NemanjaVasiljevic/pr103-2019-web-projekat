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
        private static List<GrupniTrening> sortiraniPosetilac = new List<GrupniTrening>();
        private static List<GrupniTrening> sortiraniTrener = new List<GrupniTrening>();
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
                ViewBag.treninziPosetilac = sortiraniPosetilac;
                ViewBag.treninziTrener = sortiraniTrener;

            }
            else
            {
                ViewBag.treninzi = treninzi;
            }
           
            return View();
        }


        [HttpPost]
        public ActionResult DeleteGroup(string naziv)
        {
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            Korisnik k = (Korisnik)Session["user"];
            GrupniTrening gt = new GrupniTrening();
            List<Korisnik> korisnici = Korisnik.ReadFromJson();

            foreach (Korisnik x in korisnici)
            {
                if (k.KorisnickoIme.Equals(x.KorisnickoIme))
                {
                    k = x;
                    break;
                }
            }

            foreach (GrupniTrening x in k.GrupniTreninziTrener)
            {
                if (x.Naziv.Equals(naziv))
                {
                    gt = x;
                    break;
                }
            }

            if(gt.Posetioci.Count == 0)
            {
                ViewBag.message = "Ne mozete obrisati ovaj trening jer ima prijavljene posetioce";
                return View("Notification");
            }
            else
            {
                k.GrupniTreninziTrener.Remove(gt);
            }

            Korisnik.WriteToJson(korisnici);
            treninzi = k.GrupniTreninziTrener;
            ViewBag.korisnik = k;
            ViewBag.treninzi = treninzi;
            return View("Index");
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
                    break;
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
                    break;
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
                    break;
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
                    break;
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
                    break;
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
                    break;
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
                    break;
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
                    break;
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
                    break;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziPosetilac;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortiraniPosetilac = gt.OrderBy(x => x.Naziv).ToList();
            try
            {
                sortiraniTrener = k.GrupniTreninziTrener;
            }
            catch
            {
                Console.WriteLine();
            }
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
                    break;
                }
            }

            List<GrupniTrening> gt = k.GrupniTreninziTrener;
            List<GrupniTrening> treninzi = (List<GrupniTrening>)HttpContext.Application["treninzi"];
            treninzi.Clear();
            sortiraniTrener = gt.OrderBy(x => x.Naziv).ToList();
            try
            {
                sortiraniPosetilac = k.GrupniTreninziPosetilac;
            }
            catch (Exception)
            {

                throw;
            }
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
            sortiraniPosetilac = gt.OrderBy(x => x.Naziv).Reverse().ToList();
            try
            {
                sortiraniTrener = k.GrupniTreninziTrener;
            }
            catch (Exception)
            {

                Console.WriteLine();
            }
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
            sortiraniTrener = gt.OrderBy(x => x.Naziv).Reverse().ToList();
            try
            {
                sortiraniPosetilac = k.GrupniTreninziPosetilac;
            }
            catch (Exception)
            {

                Console.WriteLine();
            }
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
            sortiraniPosetilac = gt.OrderBy(x => x.TipTreninga).ToList();
            try
            {
                sortiraniTrener = k.GrupniTreninziTrener;
            }
            catch (Exception)
            {

                Console.WriteLine(); 
            }
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
            sortiraniTrener = gt.OrderBy(x => x.TipTreninga).ToList();
            try
            {
                sortiraniPosetilac = k.GrupniTreninziPosetilac;
            }
            catch (Exception)
            {

                Console.WriteLine(); 
            }
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
            sortiraniPosetilac = gt.OrderBy(x => x.TipTreninga).Reverse().ToList();
            try
            {
                sortiraniPosetilac = k.GrupniTreninziPosetilac;
            }
            catch (Exception)
            {

                Console.WriteLine(); 
            }
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
            sortiraniTrener = gt.OrderBy(x => x.TipTreninga).Reverse().ToList();
            try
            {
                sortiraniPosetilac = k.GrupniTreninziPosetilac;
            }
            catch (Exception)
            {

                Console.WriteLine(); 
            }
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
            sortiraniPosetilac = gt.OrderBy(x => x.DatumTreninga).ToList();
            try
            {
                sortiraniTrener = k.GrupniTreninziPosetilac;
            }
            catch (Exception)
            {

                Console.WriteLine(); 
            }
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
            sortiraniTrener = gt.OrderBy(x => x.DatumTreninga).ToList();
            try
            {
                sortiraniPosetilac = k.GrupniTreninziPosetilac;
            }
            catch (Exception)
            {

                Console.WriteLine(); 
            }
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
            sortiraniPosetilac = gt.OrderBy(x => x.DatumTreninga).Reverse().ToList();
            try
            {
                sortiraniTrener = k.GrupniTreninziTrener;
            }
            catch (Exception)
            {

                Console.WriteLine();
            }
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
            sortiraniTrener = gt.OrderBy(x => x.DatumTreninga).Reverse().ToList();
            try
            {
                sortiraniPosetilac = k.GrupniTreninziPosetilac;
            }
            catch (Exception)
            {

                Console.WriteLine();
            }
            return RedirectToAction("Index", "GrupniTreninzi");

        }
    }
}