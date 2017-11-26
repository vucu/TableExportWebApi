using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using TableExportWebApi.Models;
using System.Net.Http;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TableExportWebApi.Controllers
{
    [Route("api/new-customer")]
    public class NewCustomerController : Controller
    {
        private long generateUniqueId()
        {
            return DateTime.Now.ToFileTime();
        }

        [HttpGet]
        public long Get()
        {
            return generateUniqueId();
        }

        // Allow OPTIONS
        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
    }
}
