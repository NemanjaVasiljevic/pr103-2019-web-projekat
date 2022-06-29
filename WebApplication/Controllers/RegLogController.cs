using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class RegLogController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(string username, string password)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Current.Application["users"];

            foreach (var x in korisnici)
            {
                if(x.KorisnickoIme.Equals(username) && x.Lozinka.Equals(password))
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPost]
        public IHttpActionResult Register(Korisnik k)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Current.Application["users"];

            if (korisnici.Contains(k))
            {
                return BadRequest();
            }
            else
            {
                korisnici.Add(k);
                return Ok(k);
            }
        }
    }
}
