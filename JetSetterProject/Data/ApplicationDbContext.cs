using System;
using System.Collections.Generic;
using System.Text;
using jetsetterProj.Models;
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


