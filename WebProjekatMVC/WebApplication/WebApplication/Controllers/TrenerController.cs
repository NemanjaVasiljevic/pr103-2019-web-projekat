using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Models.Enums;

namespace WebApplication.Controllers
{
    public class TrenerController : Controller
    {
        private static GrupniTrening stariTrening = new GrupniTrening();

        // GET: Trener
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGroup(GrupniTrening gt)
        {
            bool nasao = false;
            List<FitnesCentar> sveTeretane = FitnesCentar.ReadFromJson();
            List<Korisnik> allUsers = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];
            List<GrupniTrening> sviTreninzi = GrupniTrening.ReadFromJson();

            foreach (GrupniTrening x in sviTreninzi)
            {
                if(x.Naziv == gt.Naziv)
                {
                    ViewBag.message = $"Trening pod nazivom {gt.Naziv} vec postoji";
                    return View("Add");
                }
            }


            foreach (Korisnik x in allUsers)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                    break;
                }
            }

            foreach (FitnesCentar x in sveTeretane)
            {
                if(x.Naziv == gt.Fitnes_centar)
                {
                    nasao = true;
                }
            }

            if (!nasao)
            {
                ViewBag.message = $"Teretana sa nazivom {gt.Fitnes_centar} ne postoji, probajte opet";
                return View("Add");
            }

            string datum = gt.DatumTreninga;
            string dan = datum.Split('/')[0];
            string trenutniDatum = DateTime.Now.ToString("dd/MM/yyyy");
            string trenutniDan = trenutniDatum.Split('/')[0];

            int danBroj = Int32.Parse(dan);
            int trenutniDanBroj = Int32.Parse(trenutniDan);
            
            if(danBroj < trenutniDanBroj + 3)
            {
                ViewBag.message = $"Teretana sa nazivom {gt.Fitnes_centar} ne postoji, probajte opet";
                return View("Add");
            }

            gt.Posetioci = new List<Korisnik>();
            sviTreninzi.Add(gt);
            k.GrupniTreninziTrener.Add(gt);
            GrupniTrening.WriteToJson(sviTreninzi);
            Korisnik.WriteToJson(allUsers);

            return RedirectToAction("Profil","Home");
        }

        
        public ActionResult PogledajPrijavljene(string naziv)
        {
            List<Korisnik> allUsers = Korisnik.ReadFromJson();
            Korisnik k = (Korisnik)Session["user"];
        

            foreach (Korisnik x in allUsers)
            {
                if (x.KorisnickoIme.Equals(k.KorisnickoIme))
                {
                    k = x;
                    break;
                }
            }

            foreach (GrupniTrening x in k.GrupniTreninziTrener)
            {
                if (x.Naziv.Equals(naziv))
                {
                    ViewBag.posetioci = x.Posetioci;
                    break;
                }
            }

            if(ViewBag.posetioci.Count == 0)
            {
                ViewBag.message = "Niko nije prijavljen na ovaj trening";
                return View("Notification");
            }

            return View("Prijavljeni");
        }

        public ActionResult ModifikujView(string naziv)
        {
            List<GrupniTrening> treninzi = GrupniTrening.ReadFromJson();
            GrupniTrening gt = new GrupniTrening();

            foreach (GrupniTrening x in treninzi)
            {
                if (x.Naziv.Equals(naziv))
                {
                    gt = x;
                    break;
                }
            }

            stariTrening = gt;
            ViewBag.trening = gt;

            /*string datum = "06/06/2022";
            DateTime konvertovan = Convert.ToDateTime(datum);*/
            return View();
        }

        [HttpPost]
        public ActionResult Modifikuj(GrupniTrening gt)
        {
            Korisnik k = (Korisnik)Session["user"];
            List<GrupniTrening> sviTreninzi = GrupniTrening.ReadFromJson();
            List<Korisnik> sviKorisnici = Korisnik.ReadFromJson();

            int index = 0;
            int indexToReplace = 0;

            foreach (GrupniTrening x in sviTreninzi)
            {
                if (x.Naziv.Equals(gt.Naziv))
                {
                    ViewBag.message = $"Trening pod nazivom {x.Naziv} vec postoji!";
                    return View("Notification");
                }
                if (x.Naziv.Equals(stariTrening.Naziv))
                {
                    indexToReplace = index;
                }
                index++;
            }

            try
            {
                string datum = gt.DatumTreninga;
                DateTime unetDatum = Convert.ToDateTime(datum);
            }
            catch
            {
                ViewBag.message = $"Uneti datum rodjenja je pogresnog formata";
                return View("Notification");
            }

            if(gt.MaxPosetioci < stariTrening.Posetioci.Count)
            {
                ViewBag.message = $"Maksimalni broj posetioca ne moze biti manji od broja trenutno prijavljenih";
                return View("Notification");
            }
            if (gt.MaxPosetioci < 0)
            {
                ViewBag.message = $"Maksimalni broj posetioca ne moze negativan broj";
                return View("Notification");
            }

            gt.Posetioci = stariTrening.Posetioci;
            sviTreninzi[indexToReplace] = gt;

            int indexTreninga = 0;

            foreach (Korisnik x in sviKorisnici)
            {
                indexTreninga = 0;
                if (x.Uloga.ToString().Equals(Uloga.POSETILAC.ToString()))
                {
                    foreach (GrupniTrening g in x.GrupniTreninziPosetilac)
                    {
                        if (g.Naziv.Equals(stariTrening.Naziv))
                        {
                            x.GrupniTreninziPosetilac[indexTreninga] = gt;
                            break;
                        }
                        indexTreninga++;
                    }
                }else if (x.Uloga.ToString().Equals(Uloga.TRENER.ToString()))
                {
                    foreach (GrupniTrening g in x.GrupniTreninziTrener)
                    {
                        if (g.Naziv.Equals(stariTrening.Naziv))
                        {
                            x.GrupniTreninziTrener[indexTreninga] = gt;
                            break;
                        }
                        indexTreninga++;
                    }
                }
            }

            GrupniTrening.WriteToJson(sviTreninzi);
            Korisnik.WriteToJson(sviKorisnici);
            return RedirectToAction("Profil", "Home");
        }
    }
}