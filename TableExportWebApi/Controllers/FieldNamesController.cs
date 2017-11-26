using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;

using TableExportWebApi.Models;
using System.Net.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TableExportWebApi.Controllers
{
    [Route("api/field-names")]
    public class FieldNamesController : Controller
    {
        CustomerDataContext customerDataContext;
        FieldNameContext fieldNameContext;

        private long generateUniqueId()
        {
            return DateTime.Now.ToFileTime();
        }

        public FieldNamesController(CustomerDataContext c, FieldNameContext f)
        {
            customerDataContext = c;
            fieldNameContext = f;

            // Add a few field names if it doesn't have any
            if (fieldNameContext.FieldNames.Count() == 0)
            {
                fieldNameContext.FieldNames.Add(new FieldName { name = "Họ" });
                fieldNameContext.FieldNames.Add(new FieldName { name = "Đệm" });
                fieldNameContext.FieldNames.Add(new FieldName { name = "Tên" });
                fieldNameContext.SaveChanges();
            }
        }

        // GET: get all field names
        [HttpGet]
        public IEnumerable<FieldName> Get()
        {
            return fieldNameContext.FieldNames.AsEnumerable(); ;
        }

        // POST: adding a new field name
        [HttpPost]
        public IActionResult Post([FromBody]FieldName parameter)
        {
            if (parameter == null)
            {
                return BadRequest();
            }

            // Assign an unique id for the field name
            parameter.id = generateUniqueId();

            // Add it to the database
            fieldNameContext.FieldNames.Add(parameter);
            fieldNameContext.SaveChanges();

            return StatusCode(201);
        }

        // DELETE api/field-names/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            string fieldName = "";

            // Mark all records of this field name to delete
            var fieldNameToDelete = fieldNameContext.FieldNames
                .Where(f => f.id == id);
            foreach (FieldName f in fieldNameToDelete)
            {
                fieldName = f.name;
                fieldNameContext.FieldNames.Remove(f);
            }

            // Mark all customer data having this field to delete
            var customerDataToDelete = customerDataContext.CustomerDatas
                .Where(c => c.fieldName == fieldName);
            foreach (CustomerData cd in customerDataToDelete)
            {
                customerDataContext.CustomerDatas.Remove(cd);
            }

            // Save changes
            fieldNameContext.SaveChanges();
            customerDataContext.SaveChanges();

            return StatusCode(205);
        }

        // Allow OPTIONS
        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [HttpOptions("{id}")]
        public HttpResponseMessage Options(long id)
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
    }
}
