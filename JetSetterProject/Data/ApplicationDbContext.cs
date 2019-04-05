using System;
using System.Collections.Generic;
using System.Text;
using jetsetterProj.Models;
using JetSetterProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jetsetterProj.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        public DbSet<Diary> Diaries { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Really important.

            //---------------------------------------------------------------
            // Define composite primary keys.
            //---------------------------------------------------------------
            // Define foreign keys here. Do not use foreign key annotations.
            modelBuilder.Entity<Diary>()
                .HasOne(au => au.ApplicationUser) // Parent
                .WithMany(i => i.Diaries) // Child
                .HasForeignKey(fk => new { fk.UserID })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Ad>()
               .HasOne(au => au.Vendor) // Parent
               .WithMany(i => i.Ads) // Child
               .HasForeignKey(fk => new { fk.VendorID })
               .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

          
            modelBuilder.Entity<Vendor>()
             .HasOne(au => au.ApplicationUser) // Parent
             .WithMany(i => i.Vendors) // Child
             .HasForeignKey(fk => new { fk.UserID })

             .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
          


            Seed(modelBuilder);
        }

        void Seed(ModelBuilder builder)
        {
            // Seed parents first and then children since child FK's point to parents.

            builder.Entity<Vendor>().HasData(
                new { VendorID = 1, Name = "Apple", Address = "Apple Street", City = "Cupertino", Province = "California",
                      Monthly = true, Priority = true, Description = "Best Vendor", Website = "www.Apple.com",
                      PostalCode = "V7E 3E4", AdPosted = 2 }
            );
            builder.Entity<Ad>().HasData(
                new { AdID = 1, VendorID = 1, Published = true, Description = "Summer Add", ExpiryDate = new DateTime(2020, 5, 15, 13, 45, 0), Image = ""}
            );
        
            // https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
        }


    }
}




