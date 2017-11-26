using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using TableExportWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TableExportWebApi.Controllers
{
    [Route("api/field-names")]
    public class FieldNamesController : Controller
    {
        FieldNameContext fieldNameContext;

        public FieldNamesController(FieldNameContext f)
        {
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
        public void Post([FromBody]FieldName parameter)
        {
            if (parameter == null)
            {
                return;
            }

            fieldNameContext.FieldNames.Add(parameter);
            fieldNameContext.SaveChanges();

            return;
        }
    }
}
