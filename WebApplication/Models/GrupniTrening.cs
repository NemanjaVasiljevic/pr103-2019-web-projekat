using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class GrupniTrening
    {
        public GrupniTrening(string naziv, string tipTreninga, FitnesCentar fitnes_centar, TimeSpan trajanjeTreninga, DateTime datumTreninga, int maxPosetioci)
        {
            Naziv = naziv;
            TipTreninga = tipTreninga;
            Fitnes_centar = fitnes_centar;
            TrajanjeTreninga = trajanjeTreninga;
            DatumTreninga = datumTreninga;
            MaxPosetioci = maxPosetioci;
            Posetioci = new List<Korisnik>();
        }

        public string Naziv { get; set; }
        public string TipTreninga { get; set; }
        public FitnesCentar Fitnes_centar { get; set; }
        public TimeSpan TrajanjeTreninga { get; set; }
        public DateTime DatumTreninga { get; set; }
        public int MaxPosetioci { get; set; }
        public List<Korisnik>Posetioci { get; set; }




    }
}