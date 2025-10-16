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

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<CartItem> CartItem { get; set; } = default!;
        public DbSet<User> User { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.Owner)
                .WithMany()
                .HasForeignKey(r => r.Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.Location)
                .WithMany(l => l.Restaurants)
                .HasForeignKey(r => r.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
            // Cascade delete: Restaurant → MenuCategory
            modelBuilder.Entity<MenuCategory>()
                .HasOne(c => c.Restaurant)
                .WithMany(r => r.MenuCategories)
                .HasForeignKey(c => c.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cascade delete: MenuCategory → MenuItem
            modelBuilder.Entity<MenuItem>()
                .HasOne(i => i.Category)
                .WithMany(c => c.MenuItems)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
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


            modelBuilder.Entity<MenuItem>()
                .Property(m => m.ItemPrice)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.DeliveryCharges)
                .HasColumnType("decimal(10,2)");

        }
    }
}
