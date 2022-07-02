using System;
using System.Collections.Generic;
using System.Globalization;
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
            int dan = Int32.Parse(gt.DatumTreninga.Split('/')[0]);
            int mesec = Int32.Parse(gt.DatumTreninga.Split('/')[1]);
            int trenutniDan = Int32.Parse(DateTime.Now.ToString("d", CultureInfo.GetCultureInfo("en-US")).Split('/')[1]);
            int trenutniMesec = Int32.Parse(DateTime.Now.ToString("d", CultureInfo.GetCultureInfo("en-US")).Split('/')[0]);

            if (dan < trenutniDan + 3)
            {
                ViewBag.message = $"Trening mora biti zakazan najmanje 3 dana u napred!";
                return View("Add");
            }
            if (mesec < trenutniMesec || (dan < trenutniDan && mesec == trenutniDan))
            {
                ViewBag.message = $"Trening moze biti zakazan samo za buducnost";
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
                if (x.Naziv.Equals(gt.Naziv) && !stariTrening.Naziv.Equals(gt.Naziv))
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

            

            if (gt.MaxPosetioci < stariTrening.Posetioci.Count)
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