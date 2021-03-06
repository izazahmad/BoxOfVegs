﻿using BoxOfVegs.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxOfVegs.Database
{
    public class BOVContext : DbContext, IDisposable
    {
        public BOVContext() : base("BoxOfVegsConnection")
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<UserReview> UserReviews{ get; set; }

    }
}
