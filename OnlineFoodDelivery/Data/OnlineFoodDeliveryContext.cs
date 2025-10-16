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
        
    }
}
