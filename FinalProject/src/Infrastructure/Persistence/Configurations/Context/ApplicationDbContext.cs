using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderEvent> OrderEvents { get; set; }
    }
}
