using GameStore_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GameStore_API.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
