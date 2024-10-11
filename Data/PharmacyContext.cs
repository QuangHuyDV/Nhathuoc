using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nhathuoc.Models;

namespace Nhathuoc.Data
{
    public class PharmacyContext : DbContext
    {
        public PharmacyContext(DbContextOptions options) : base(options) {}

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable(nameof(Product));
            modelBuilder.Entity<Customer>().ToTable(nameof(Customer));
            modelBuilder.Entity<Order>().ToTable(nameof(Order));
            modelBuilder.Entity<OrderDetail>().ToTable(nameof(OrderDetail));
            modelBuilder.Entity<Supplier>().ToTable(nameof(Supplier));
            modelBuilder.Entity<Stock>().ToTable(nameof(Stock));
            modelBuilder.Entity<Invoice>().ToTable(nameof(Invoice));

        }
    }
}