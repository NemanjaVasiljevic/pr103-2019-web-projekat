using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TeretanaController : ApiController
    {
        public List<FitnesCentar> Get()
        {
            return FitnesCentar.ReadFromJson();
        }
    }
}
