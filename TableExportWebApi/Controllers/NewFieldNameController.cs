using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TableExportWebApi.Models
{
    [Route("api/new-field-name")]
    public class NewFieldNameController : Controller
    {
        FieldNameContext fieldNameContext;

        public NewFieldNameController(FieldNameContext f)
        {
            fieldNameContext = f;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] FieldName fieldName)
        {
            if (fieldName==null)
            {
                return BadRequest();
            }

            fieldNameContext.FieldNames.Add(fieldName);
            fieldNameContext.SaveChanges();

            return StatusCode(201);
        }
    }
}
