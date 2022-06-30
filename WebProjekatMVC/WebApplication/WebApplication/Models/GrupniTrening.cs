using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class GrupniTrening
    {
        public GrupniTrening()
        {

        }
        public GrupniTrening(string naziv, string tipTreninga, FitnesCentar fitnes_centar, string trajanjeTreninga, string datumTreninga, string vremeTreninga, int maxPosetioci)
        {
            Naziv = naziv;
            TipTreninga = tipTreninga;
            Fitnes_centar = fitnes_centar;
            TrajanjeTreninga = trajanjeTreninga;
            DatumTreninga = datumTreninga;
            VremeTreninga = vremeTreninga;
            MaxPosetioci = maxPosetioci;
            Posetioci = new List<Korisnik>();
        }

        public string Naziv { get; set; }
        public string TipTreninga { get; set; }
        public FitnesCentar Fitnes_centar { get; set; }
        public string TrajanjeTreninga { get; set; }
        public string DatumTreninga { get; set; }
        public string VremeTreninga { get; set; }
        public int MaxPosetioci { get; set; }
        public List<Korisnik> Posetioci { get; set; }

        public static List<GrupniTrening> ReadFromJson()
        {
            List<GrupniTrening> grupniTreninzi = new List<GrupniTrening>();

            string jsonFromFile;
            using (var reader = new StreamReader("C:\\Users\\Nemanja\\Desktop\\WebProjekat\\pr103-2019-web-projekat\\WebProjekatMVC\\WebApplication\\WebApplication\\TextFiles\\GrupniTreninzi.json"))
            {
                jsonFromFile = reader.ReadToEnd();
            }
            //try
           // {
                List<GrupniTrening> treninzi = JsonConvert.DeserializeObject<List<GrupniTrening>>(jsonFromFile);
                foreach (var x in treninzi)
                {
                    grupniTreninzi.Add(x);
                }
                return grupniTreninzi;
            //}
            //catch
            //{
            //    return grupniTreninzi;
            //}

        }

        public static void WriteToJson(List<GrupniTrening> gt)
        {
            var jsonToWrite = JsonConvert.SerializeObject(gt);
            using (var writer = new StreamWriter("C:\\Users\\Nemanja\\Desktop\\WebProjekat\\pr103-2019-web-projekat\\WebProjekatMVC\\WebApplication\\WebApplication\\TextFiles\\GrupniTreninzi.json"))
            {
                writer.Write(jsonToWrite);
            }
        }
    }
}