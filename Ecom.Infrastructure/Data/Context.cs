using Ecom.Entity.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data
{
    public class Context : IdentityDbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPrice> Prices { get; set; }

        public DbSet<ProductSpecification> Specifications { get; set; }

        public DbSet<ProductImage> Images { get; set; }
    }
}
