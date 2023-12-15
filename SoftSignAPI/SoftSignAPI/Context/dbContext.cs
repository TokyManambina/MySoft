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


        public DbSet<Attachement> Attachements{ get; set; }
        public DbSet<Historique> Historiques{ get; set; }
        public DbSet<DocumentLink> DocumentLinks{ get; set; }
        public DbSet<DynamicField> DynamicFields{ get; set; }
        public DbSet<DynamicFieldItem> DynamicFieldItems{ get; set; }
        public DbSet<DocumentDynamicField> DocumentDynamicFields{ get; set; }

        public DbSet<Flow> Flows{ get; set; }
        public DbSet<UserFlow> UserFlows{ get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            //multiple Key
			modelBuilder.Entity<DocumentDynamicField>().HasKey(e => new { e.DocumentCode, e.DocumentDetailId });

            //DefaultValue of Guid
			modelBuilder.Entity<Society>().Property(x => x.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<User>().Property(x => x.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<Subscription>().Property(x => x.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<Historique>().Property(x => x.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<Attachement>().Property(x => x.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<Flow>().Property(x => x.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<UserFlow>().Property(x => x.Id).HasDefaultValueSql("newid()");

		}
    }
}
