using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Data
{
    public class OnlineFoodDeliveryContext : DbContext
    {
        public OnlineFoodDeliveryContext(DbContextOptions<OnlineFoodDeliveryContext> options)
            : base(options)
        {
        }
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<CartItem> CartItem { get; set; } = default!;
        public DbSet<User> User { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Payment> Payment { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(10, 2); // Precision: total digits = 10, scale = 2
            modelBuilder.Entity<Order>()
               .Property(p => p.TotalAmount)
               .HasPrecision(10, 2);
        }


    }
}
