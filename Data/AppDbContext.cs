using Microsoft.EntityFrameworkCore;
using MobileStoreAPI.Models;

namespace MobileStoreAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> users{ get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Mobile> mobiles { get; set; }
        public DbSet<Stock> stocks { get; set; }
        public DbSet<Admin> admin { get; set; }    

    }
}
