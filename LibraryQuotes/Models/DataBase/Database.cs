using LibraryQuotes.Models.DataBase.Configuration;
using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryQuotes.Models.DataBase
{
    public class Database(DbContextOptions options) : DbContext(options), IDatabase
    {
        public DbSet<Copy> copy {  get; set; }
        public DbSet<Users> users {  get; set; }

        public async Task<bool> SaveAsync()
        {
            return await SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityTypeConfiguration(modelBuilder);
        }

        private void EntityTypeConfiguration(ModelBuilder modelBuilder)
        {
            new CopyConfiguration(modelBuilder.Entity<Copy>());
            new CopyConfiguration(modelBuilder.Entity<Users>());
        }
    }
}
