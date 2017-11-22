using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TableExportWebApi.Models
{
    public class CustomerData
    {
        public long customerId { get; set; }
        public string fieldName { get; set; }
        public string fieldValue { get; set; }
    }
}
