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

        public DbSet<CustomerData> Customers {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set primary key
            modelBuilder.Entity<CustomerData>()
                .HasKey(m => new { m.customerId, m.fieldName });
        }
    }
}
