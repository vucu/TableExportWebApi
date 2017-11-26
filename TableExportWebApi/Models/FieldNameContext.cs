using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TableExportWebApi.Models
{
    public class FieldNameContext : DbContext
    {
        public FieldNameContext(DbContextOptions<FieldNameContext> options):base(options)
        {
        }

        public DbSet<FieldName> FieldNames
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set primary key
            modelBuilder.Entity<FieldName>()
                .HasKey(m => new { m.id });
        }
    }
}
