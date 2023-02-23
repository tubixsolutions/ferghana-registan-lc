using Microsoft.EntityFrameworkCore;
using RegistanFerghanaLC.Domain.Entities.Users;

namespace RegistanFerghanaLC.DataAccess.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
            : base(contextOptions)
        {

        }

        public virtual DbSet<Admin> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
