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
        public void Login(string username, string password)
        {
            List<Korisnik> korisnici = Korisnik.ReadFromJson();

            /*foreach (var x in korisnici)
            {
                if(x.KorisnickoIme.Equals(username) && x.Lozinka.Equals(password))
                {
                    return Ok();
                }
            }*/
            Korisnik.WriteToJson(new Korisnik(username,password));
        }

        [HttpPost]
        public IHttpActionResult Register(Korisnik k)
        {
            List<Korisnik> korisnici = Korisnik.ReadFromJson();

            if (korisnici.Contains(k))
            {
                return BadRequest();
            }
            else
            {
                Korisnik.WriteToJson(k);
                return Ok(k);
            }
        }
    }
}
