﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TrenerController : Controller
    {
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
    }
}