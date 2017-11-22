using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TableExportWebApi.Models
{
    public class CustomerDataContext : DbContext
    {
        public CustomerDataContext(DbContextOptions<CustomerDataContext> options):base(options)
        {
        }

        public DbSet<CustomerData> CustomerDatas {
            get;
            set;
        }
    }
}
