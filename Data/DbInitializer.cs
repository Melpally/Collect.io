using Microsoft.EntityFrameworkCore;
using Collect.io.DAL.Models;

namespace Collect.io.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _builder;
        public DbInitializer(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void Seed()
        {
            _builder.Entity<UserModel>(u =>
            {
                u.HasData(new UserModel
                {
                    Id = 1,
                    UserName = "Melpally",
                    Email = "m.temurova@yahoo.com",
                    Password = "Password",
                    Salt = "" 
                });
            });
            _builder.Entity<CollectionModel>(c =>
            {
                c.HasData(new CollectionModel
                {
                    Id = 1,
                    Title = "Books",
                    Description = "Description of the collection",
                    Topic = Topic.Books,
                    UserId = 1
                });
            });
            _builder.Entity<ItemModel>(c =>
            {
                c.HasData(new ItemModel
                {
                    Id = 1,
                    Name = "Pride and Prejudice",
                    Date = DateTime.Now,
                    Tags = "classics,romance,drama",
                    CollectionId = 1
                    
                });
            });
            _builder.Entity<FieldValuesModel>(u =>
            {
                u.HasData(new FieldValuesModel
                {
                    Id = 1,
                    ItemId = 1,
                    TypeId = 1,
                    Value = "1813"
                });
            });
            _builder.Entity<FieldConfModel>(u =>
            {
                u.HasData(new FieldConfModel
                {
                    Id = 1,
                    CollectionId = 1,
                    Name = "Year",
                    FieldType = "int"
                });
            });
        }
    }
}
