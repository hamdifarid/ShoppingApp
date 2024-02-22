using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingApp.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("ShoppingApp")
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Carts> Carts { get; set; }
    }
}