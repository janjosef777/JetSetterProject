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
        public DbSet<Rating> Ratings { get; set; }
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
                .HasForeignKey(fk => new { fk.UserId })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Ad>()
               .HasOne(au => au.Vendor) // Parent
               .WithMany(i => i.Ads) // Child
               .HasForeignKey(fk => new { fk.VendorID })
               .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Rating>()
              .HasOne(au => au.ApplicationUser) // Parent
              .WithMany(i => i.Ratings) // Child
              .HasForeignKey(fk => new { fk.UserID })
              .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Rating>()
              .HasOne(au => au.Diary) // Parent
              .WithMany(i => i.Ratings) // Child
              .HasForeignKey(fk => new { fk.DiaryID })
              .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

        }

    }
}

//public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//{
//    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//        : base(options)
//    {
//    }
//}


