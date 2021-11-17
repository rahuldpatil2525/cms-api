using Microsoft.EntityFrameworkCore;
using DbUser = CMS.User.Api.Database.Models.User;

namespace CMS.User.Api.Database
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbUser>().HasIndex(s => s.UserName).IsUnique();
        }

        public DbSet<DbUser> Users { get; set; }
    }
}
