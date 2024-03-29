﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SparkAutoRazor.Model;

namespace SparkAutoRazor.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        public DbSet<ApplicationUser>  ApplicationUsers { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<ServiceShoppingCart> ServiceShoppingCarts { get; set; }

        public DbSet<ServiceHeader> ServiceHeaders { get; set; }

        public DbSet<ServiceDetails> ServiceDetailses { get; set; }

    }
}
