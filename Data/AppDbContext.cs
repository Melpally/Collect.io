using Microsoft.EntityFrameworkCore;
using Collect.io.DAL.Models;

namespace Collect.io.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Setting the Database Initializer as DropCreateDatabaseAlways
            //Database.SetInitializer(new DropCreateDatabaseAlways<EFCodeFirstContext>());
        }
        public DbSet<ItemModel> Items { get; set; }
        public DbSet<FieldConfModel> FieldTypes { get; set; }
        public DbSet<FieldValuesModel> FieldValues { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CollectionModel> Collections { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to many - user -> collection 
            modelBuilder.Entity<UserModel>()
                .HasMany(e => e.Collections)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

            //one to many - collection -> item
            modelBuilder.Entity<CollectionModel>()
                .HasMany(e => e.Items)
                .WithOne(e => e.Collection)
                .HasForeignKey(e => e.CollectionId);
            //one to many - collection -> fieldconf
            modelBuilder.Entity<CollectionModel>()
                .HasMany(e => e.Fields)
                .WithOne(e => e.Collection)
                .HasForeignKey(e => e.CollectionId);
            //one to one - fieldconf -> fieldvalue
            modelBuilder.Entity<FieldConfModel>()
                .HasOne(e => e.ValueId)
                .WithOne(e => e.Type)
                .HasForeignKey<FieldValuesModel>(e => e.TypeId);
            //one to one - item -> fieldvalue
            modelBuilder.Entity<ItemModel>()
                .HasOne(e => e.FieldValue)
                .WithOne(e => e.Item)
                .HasForeignKey<FieldValuesModel>(e => e.ItemId);

            //Seed the initial db with demo data 
            new DbInitializer(modelBuilder).Seed();
        }
        

        
    }
}
