﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using TableExportWebApi.Models;

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

            // Add a few field names if it doesn't have any
            if (fieldNameContext.FieldNames.Count() == 0)
            {
                fieldNameContext.FieldNames.Add(new FieldName { name = "Họ" });
                fieldNameContext.FieldNames.Add(new FieldName { name = "Đệm" });
                fieldNameContext.FieldNames.Add(new FieldName { name = "Tên" });
                fieldNameContext.SaveChanges();
            }
        }

        // api/customers
        [HttpGet]
        public IEnumerable<long> Get()
        {
            HashSet<long> result = new HashSet<long>();
            foreach (var customer in customerDataContext.Customers)
            {
                long id = customer.customerId;
                result.Add(id);
            }
            return result;
        }
        
    }
}