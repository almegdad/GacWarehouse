using System;
using System.Collections.Generic;
using System.Text;
using GacWarehouse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GacWarehouse.Data.Database
{
    public partial class GacWarehouseDbContext : DbContext
    {
        public GacWarehouseDbContext()
        {
        }

        public GacWarehouseDbContext(DbContextOptions<GacWarehouseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderDetails> SalesOrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB; Database=GacWarehouseDb; Trusted_Conn‌ection=True; Multiple‌​ActiveResultSets=tru‌​e;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                        .HasIndex(b => b.Username)
                        .IsUnique();

            modelBuilder.Entity<Manufacturer>()
                        .HasIndex(b => b.Code)
                        .IsUnique();

            modelBuilder.Entity<Product>()
                        .HasIndex(b => b.Code)
                        .IsUnique();

            modelBuilder.Entity<SalesOrderDetails>()
                        .HasIndex(b => new { b.OrderId, b.ProductId })
                        .IsUnique();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
