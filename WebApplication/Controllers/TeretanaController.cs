using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TeretanaController : ApiController
    {
        public List<FitnesCentar> Get()
        {
            List<FitnesCentar> teretane = (List<FitnesCentar>)HttpContext.Current.Application["teretane"];
            teretane = FitnesCentar.ReadFromJson();
            return teretane;
        }

        public FitnesCentar FindByName(string name)
        {
            List<FitnesCentar> teretane = (List<FitnesCentar>)HttpContext.Current.Application["teretane"];
            foreach (var x in teretane)
            {
                if (x.Naziv.Equals(name))
                {
                    return x;
                }
            }
            return null;
        }

        public FitnesCentar FindByAddress(Adresa a)
        {
            List<FitnesCentar> teretane = (List<FitnesCentar>)HttpContext.Current.Application["teretane"];
            foreach (var x in teretane)
            {
                if (a.Grad.Equals(x.Adresa.Grad) && a.Ulica.Equals(x.Adresa.Ulica) && a.Broj.Equals(x.Adresa.Broj))
                {
                    return x;
                }
            }
            return null;
        }

    }
}
