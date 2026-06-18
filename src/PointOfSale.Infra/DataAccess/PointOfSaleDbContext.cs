using Microsoft.EntityFrameworkCore;
using PointOfSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Infra.DataAccess
{
    public class PointOfSaleDbContext : DbContext
    {
        public PointOfSaleDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.Username).IsUnique();
            });
        }

    }
}
