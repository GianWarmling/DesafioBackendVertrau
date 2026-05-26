using DesafioBackendVertrau.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackendVertrau.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
