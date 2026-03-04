using Microsoft.EntityFrameworkCore;
using TransportApi.Models;

namespace TransportApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>訂單資料表</summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>車輛資料表</summary>
        public DbSet<Vehicle> Vehicles { get; set; }

        /// <summary>司機資料表</summary>
        public DbSet<Driver> Drivers { get; set; }

        /// <summary>客戶資料表</summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>系統使用者資料表</summary>
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 設定 decimal 精度，避免 EF Core 產生警告
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.Capacity)
                .HasPrecision(10, 2);
        }
    }
}