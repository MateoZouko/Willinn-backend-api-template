using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
    }
}
