using Microsoft.EntityFrameworkCore;
using SoftSignAPI.Model;

namespace SoftSignAPI.Context
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options) { }

        public DbSet<Society> Societies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<UserDocument> UserDocuments { get; set; }
        public DbSet<Field> Fields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Society>().Property(x => x.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<User>().Property(x => x.Id).HasDefaultValueSql("newid()");
        }
    }
}
