using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceOcr.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceOcr.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.InvoiceNumber)
                      .IsRequired(); 
                entity.Property(e => e.InvoiceDate)
                      .IsRequired(); 
                entity.Property(e => e.CustomerName)
                      .IsRequired(); 
                entity.Property(e => e.TotalAmount)
                      .HasColumnType("DECIMAL(10,2)")
                      .IsRequired(); 
                entity.Property(e => e.Vat)
                      .HasColumnType("DECIMAL(5,2)"); 
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.InvoiceId)
                      .IsRequired(); 
                entity.Property(e => e.Description)
                      .IsRequired(); 
                entity.Property(e => e.Quantity)
                      .IsRequired(); 
                entity.Property(e => e.UnitPrice)
                      .HasColumnType("DECIMAL(10,2)")
                      .IsRequired(); 
                entity.Property(e => e.LineTotal)
                      .HasColumnType("DECIMAL(10,2)")
                      .IsRequired(); 

                entity.HasOne(d => d.Invoice)
                      .WithMany(i => i.Details)
                      .HasForeignKey(d => d.InvoiceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlite("Data Source=invoiceocr.db")
                .UseLazyLoadingProxies();
        }
    }
}
