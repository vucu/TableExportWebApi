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
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        CustomerDataContext customerDataContext;
        FieldNameContext fieldNameContext;

        public CustomersController(CustomerDataContext c, FieldNameContext f)
        {
            customerDataContext = c;
            fieldNameContext = f;
        }

        // GET api/customers
        [HttpGet]
        public IEnumerable<long> Get()
        {
            HashSet<long> result = new HashSet<long>();

            foreach (var customer in customerDataContext.CustomerDatas)
            {
                long id = customer.customerId;
                result.Add(id);
            }

            return result;
        }

        // POST api/customers
        [HttpPost]
        public IActionResult Post([FromBody] CustomerData parameter)
        {
            if (parameter == null)
            {
                return BadRequest();
            }

            // If the field name does not exist, bad request
            bool isError = true;
            string name = parameter.fieldName;
            foreach (var fieldName in fieldNameContext.FieldNames)
            {
                if (name == fieldName.name) isError = false;
            }
            if (isError)
            {
                return BadRequest();
            }

            // Check if the data already exists
            var data = customerDataContext.CustomerDatas.SingleOrDefault(d 
                => (d.customerId == parameter.customerId) && (d.fieldName == parameter.fieldName));

            if (data != null)
            {
                // data already exists
                data.fieldValue = parameter.fieldValue;
                customerDataContext.SaveChanges();
            } else
            {
                // data does not exist, create new
                customerDataContext.CustomerDatas.Add(parameter);
                customerDataContext.SaveChanges();
            }

            return StatusCode(201);
        }

        // GET api/customers/{id}
        [HttpGet("{id}")]
        public IEnumerable<CustomerData> Get(long id)
        {
            List<CustomerData> result = new List<CustomerData>();

            foreach (var customer in customerDataContext.CustomerDatas)
            {
                if (customer.customerId == id)
                {
                    result.Add(customer);
                }
            }

            return result;
        }

        // Allow OPTIONS
        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
    }
}
