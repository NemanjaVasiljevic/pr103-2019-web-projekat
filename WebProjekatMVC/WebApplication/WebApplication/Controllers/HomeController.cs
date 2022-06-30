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
            List<Korisnik> allUsers = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];
            if(k == null)
            {
                ViewBag.message = "Ulogujte se da biste mogli pogledati profil";
                return RedirectToAction("IndexProfil", "RegLog");
            }
            else
            {
                foreach (Korisnik x in allUsers)
                {
                    if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                    {
                        k = x;
                        break;
                    }
                }
                ViewBag.korisnik = k;
                return View();
            }
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
                    break;
                }
            }
            Korisnik k = (Korisnik)Session["user"];
            if (k == null)
            {
                ViewBag.korisnik = new Korisnik();
                ViewBag.teretana = teretana;
                ViewBag.grupniTreninzi = spisakTreninga;
                return View();
            }
            ViewBag.teretana = teretana;
            ViewBag.grupniTreninzi = spisakTreninga;
            ViewBag.korisnik = k;
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

        public ActionResult PrijaviSeZaGrupniTrening(string naziv)
        {
            List<GrupniTrening> grupniTreninzi = GrupniTrening.ReadFromJson();
            List<Korisnik> korisnici = Korisnik.ReadFromJson();
            GrupniTrening gt = new GrupniTrening();
            Korisnik user = new Korisnik();
            Korisnik k = (Korisnik)Session["user"];         

            foreach (GrupniTrening x in grupniTreninzi)
            {
                if (x.Naziv.Equals(naziv))
                {
                    gt = x;
                    break;
                }
            }

            
            // ako nije ulogovan 
            if (k == null)
            {
                if(gt.Posetioci.Count < gt.MaxPosetioci)
                {
                    gt.Posetioci.Add(new Korisnik());
                    GrupniTrening.WriteToJson(grupniTreninzi);
                    ViewBag.message = "Uspesno ste se prijavili za trening kao gost";
                    return View("Notification");
                }
                else
                {
                    ViewBag.message = "Sva mesta su popunjena";
                    return View("Notification");
                }

            }
            else
            {
                //ako je ulogovan
                foreach (Korisnik x in korisnici)
                {
                    if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                    {
                        user = x;
                        break;
                    }
                }

                if (user.GrupniTreninziPosetilac == null)
                {
                    gt.Posetioci.Add(k);
                    GrupniTrening.WriteToJson(grupniTreninzi);

                    user.GrupniTreninziPosetilac = new List<GrupniTrening>();
                    user.GrupniTreninziPosetilac.Add(gt);
                    Korisnik.WriteToJson(korisnici);

                    ViewBag.message = $"Uspesno ste se prijavili za trening kao korisnik {k.Ime}";
                    return View("Notification");
                }
                else
                {

                    foreach (GrupniTrening x in user.GrupniTreninziPosetilac)
                    {
                        if (x.Naziv.Equals(gt.Naziv))
                        {
                            ViewBag.Message = $"Vec ste prijavljeni na termin {gt.Naziv} koji je {gt.DatumTreninga}";
                            return View("Notification");
                        }
                    }
                    gt.Posetioci.Add(k);
                    GrupniTrening.WriteToJson(grupniTreninzi);

                    user.GrupniTreninziPosetilac.Add(gt);
                    Korisnik.WriteToJson(korisnici);

                    ViewBag.message = $"Uspesno ste se prijavili za trening kao korisnik {k.Ime}";
                    return View("Notification");
                }
            }          
        }
    }
}