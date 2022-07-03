using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Models.Enums;

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

        public ActionResult EditProfil(string naziv)
        {
            List<Korisnik> sviKorisnici = Korisnik.ReadFromJson();
            Korisnik k = new Korisnik();

            foreach (Korisnik x in sviKorisnici)
            {
                if (x.KorisnickoIme.Equals(naziv))
                {
                    k = x;
                    break;
                }
            }
            ViewBag.korisnik = k;
            return View();
        }

        public ActionResult Edit(Korisnik updated)
        {
            bool nasao = false;
            List<Korisnik> allUsers = Korisnik.ReadFromJson();
            Korisnik logged = (Korisnik)Session["user"];
            int index = 0;
            int foundIndex = 0;

            foreach (Korisnik x in allUsers)
            {
                if(x.KorisnickoIme.Equals(logged.KorisnickoIme) && !nasao)
                {
                    logged = x;
                    nasao = true;
                    foundIndex = index;
                }
                if(updated.KorisnickoIme.Equals(x.KorisnickoIme) && !updated.KorisnickoIme.Equals(logged.KorisnickoIme))
                {
                    ViewBag.message = $"Nije moguca promena u korisnicko ime {updated.KorisnickoIme}, vec je zauzeto";
                    return View("Notification");
                }
                if (updated.Email.Equals(x.Email) && !updated.Email.Equals(x.Email))
                {
                    ViewBag.message = $"Nije moguca promena mejla u {updated.Email}, vec je zauzet";
                    return View("Notification");
                }
                index++;
            }

            try
            {
                DateTime unetDatum = Convert.ToDateTime(updated.DatumRodjenja);
            }
            catch
            {
                ViewBag.message = $"Uneti datum rodjenja je pogresnog formata";
                return View("Notification");
            }

            updated.GrupniTreninziPosetilac = logged.GrupniTreninziPosetilac;
            updated.GrupniTreninziTrener = logged.GrupniTreninziTrener;
            updated.FitnesCentarTrener = logged.FitnesCentarTrener;
            updated.FitnesCentarVlasnik = logged.FitnesCentarVlasnik;
            updated.Uloga = logged.Uloga;

            allUsers[foundIndex] = updated;
            Korisnik.WriteToJson(allUsers);
            Session["user"] = null;

            return RedirectToAction("IndexProfil","RegLog");
        }

        public ActionResult Details(string fc)
        {
            FitnesCentar teretana = null;
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            List<GrupniTrening> grupniTreninzi = GrupniTrening.ReadFromJson();
            List<GrupniTrening> spisakTreninga = new List<GrupniTrening>();

            foreach (GrupniTrening x in grupniTreninzi)
            {
                if (x.Fitnes_centar.Equals(fc))
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

        public ActionResult KombinovanaPretraga(string Naziv, string Ulica, string Broj, string Grad, int from, int to)
        {
            listaTeretana.Clear();
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            foreach (FitnesCentar x in teretane)
            {
                if (x.Naziv.Equals(Naziv) && x.Adresa.Ulica.Equals(Ulica) &&
                    x.Adresa.Broj.Equals(Broj) && x.Adresa.Grad.Equals(Grad) &&
                    x.GodinaOtvaranja >= from && x.GodinaOtvaranja <= to)
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
            Korisnik trener = null;
            Korisnik k = (Korisnik)Session["user"];         

            foreach (GrupniTrening x in grupniTreninzi)
            {
                if (x.Naziv.Equals(naziv))
                {
                    gt = x;
                    break;
                }
            }

            foreach (Korisnik x in korisnici)
            {
                if (x.Uloga.ToString().Equals(Uloga.TRENER.ToString()))
                {
                    if (x.GrupniTreninziTrener != null)
                    {
                        foreach (GrupniTrening g in x.GrupniTreninziTrener)
                        {
                            if (g.Naziv.Equals(naziv))
                            {
                                trener = x;
                                break;
                            }
                        }
                        if (trener != null)
                        {
                            break;
                        }
                    }
                }
            }
            
            if(trener == null)
            {
                ViewBag.message = "Trenutno nema angazovanog trenera na ovom treningu";
                return View("Notification");
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
                    

                    user.GrupniTreninziPosetilac = new List<GrupniTrening>();
                    user.GrupniTreninziPosetilac.Add(gt);

                    foreach (GrupniTrening g in trener.GrupniTreninziTrener)
                    {
                        if (g.Naziv.Equals(gt.Naziv))
                        {
                            g.Posetioci.Add(k);
                        }
                    }


                    GrupniTrening.WriteToJson(grupniTreninzi);
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
                    user.GrupniTreninziPosetilac.Add(gt);

                    foreach (GrupniTrening g in trener.GrupniTreninziTrener)
                    {
                        if (g.Naziv.Equals(gt.Naziv))
                        {
                            g.Posetioci.Add(k);
                        }
                    }

                    GrupniTrening.WriteToJson(grupniTreninzi);
                    Korisnik.WriteToJson(korisnici);

                    ViewBag.message = $"Uspesno ste se prijavili za trening kao korisnik {k.Ime}";
                    return View("Notification");
                }
            }          
        }
    }
}