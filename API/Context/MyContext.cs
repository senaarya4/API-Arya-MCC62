﻿using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profilling> Profillings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(b => b.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profilling)
                .WithOne(b => b.Account)
                .HasForeignKey<Profilling>(b => b.NIK);

            modelBuilder.Entity<Profilling>()
                .HasOne(e => e.Education)
                .WithMany(c => c.profillings);

            modelBuilder.Entity<Education>()
                 .HasOne(a => a.University)
                 .WithMany(b => b.Educations);

            //modelBuilder.Entity<University>()
            //            .HasMany(a => a.Educations)
            //            .WithOne(b => b.University);
        }
    }
}
