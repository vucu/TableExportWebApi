using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using TableExportWebApi.Models;

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

        // GET: api/root
        [HttpGet]
        public long Get()
        {
            return generateUniqueId();
        }
    }
}
