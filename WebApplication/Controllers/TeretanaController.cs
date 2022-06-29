using System.Collections.Generic;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TeretanaController : ApiController
    {
        [HttpGet]
        public List<FitnesCentar> Get()
        {
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            return teretane;
        }

        [HttpGet]
        [ActionName("FindByName")]
        public FitnesCentar FindByName(string name)
        {
            List<FitnesCentar>  teretane = FitnesCentar.ReadFromJson();
            foreach (var x in teretane)
            {
                if (x.Naziv.Equals(name))
                {
                    return x;
                }
            }
            return null;
        }

        [HttpGet()]
        [ActionName("FindByAddress")]
        public FitnesCentar FindByAddress(string grad, string ulica, string broj, int pb)
        {
            Adresa a = new Adresa(ulica, broj, grad, pb);
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            foreach (var x in teretane)
            {
                if (a.Grad.Equals(x.Adresa.Grad) && a.Ulica.Equals(x.Adresa.Ulica) && a.Broj.Equals(x.Adresa.Broj))
                {
                    return x;
                }
            }
            return null;
        }

        [HttpGet]
        [ActionName("FindByGodinaOtvaranja")]
        public List<FitnesCentar> FindByGodinaOtvaranja(int from, int to)
        {
            List<FitnesCentar> pom = new List<FitnesCentar>();
            List<FitnesCentar> teretane = FitnesCentar.ReadFromJson();
            foreach (var x in teretane)
            {
                if (x.GodinaOtvaranja >= from && x.GodinaOtvaranja<=to)
                {
                    pom.Add(x);
                }
            }
            return pom;
        }
    }
}
