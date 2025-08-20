using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Buraya modelleri tablo gibi tanımlıyoruz
        public DbSet<User> Users { get; set; }
    }
}
   