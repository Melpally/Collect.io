using Collect.io.DAL.Models;
using Collect.io.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;


namespace Collect.io.DAL
{
    public class AuthDAL : IAuthDAL
    {

        //private readonly AppDbContext database;
        private readonly IDbContextFactory<AppDbContext> _dtb;


       
        public AuthDAL(IDbContextFactory<AppDbContext> dtb)
        {
            _dtb = dtb;
        }

        #region Users
        public async Task<UserModel> GetUser(string email)
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                return await database.Users.FirstOrDefaultAsync(i => i.Email == email);
            }
            catch (Exception ex)
            {
                return new UserModel();
            }
        }
        public async Task<List<UserModel>> GetUsers()
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                return await database.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<UserModel>();
            }
        }
        public async Task<UserModel> GetUser(int id)
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                return await database.Users.FirstOrDefaultAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                return new UserModel();
            }
        }
        public async Task<int> CreateUser(UserModel user)
        {

            using var database = _dtb.CreateDbContext();
            try
            {
                
                await database.Users.AddAsync(user);
                await database.SaveChangesAsync();
                return user.Id;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<UserModel> UpdateUser(UserModel user)
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                database.Entry(user).State = EntityState.Modified;
                await database.SaveChangesAsync();
                return user;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<(bool, string)> DeleteUser(UserModel user)
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                var dbUser = await database.Users.FindAsync(user.Id);

                if (dbUser == null)
                {
                    return (false, "User could not be found");
                }

                database.Users.Remove(dbUser);
                await database.SaveChangesAsync();

                return (true, "User got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion
        #region Collections
        public async Task<CollectionModel> GetCollection(int id)
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                return await database.Collections.FirstOrDefaultAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                return new CollectionModel();
            }
        }
        public async Task<List<CollectionModel>> GetCollectionsById(List<int> ids)
        {
            using var database = _dtb.CreateDbContext();
            List<CollectionModel> res = new List<CollectionModel>(5);
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    res.Add(await GetCollection(ids[i]));
                }
                return res;
            }
            catch (Exception ex)
            {
                return new List<CollectionModel>();
            }
        }
        public async Task<List<CollectionModel>> GetCollections()
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                return await database.Collections.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<CollectionModel>();
            }
        }

        public async Task<int> CreateCollection(CollectionModel collection)
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                await database.Collections.AddAsync(collection);
                await database.SaveChangesAsync();
                return collection.Id;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<CollectionModel> UpdateCollection(CollectionModel collection)
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                database.Entry(collection).State = EntityState.Modified;
                await database.SaveChangesAsync();
                return collection;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<(bool, string)> DeleteCollection(CollectionModel collection)
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                var dbCollection = await database.Collections.FindAsync(collection.Id);

                if (dbCollection == null)
                {
                    return (false, "Collection could not be found");
                }

                database.Collections.Remove(dbCollection);
                await database.SaveChangesAsync();

                return (true, "Collection got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred. Error Message: {ex.Message}");
            }
        }

        #endregion
        #region Items

        public async Task<List<ItemModel>> GetItems()
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                return await database.Items.ToListAsync();

            }
            catch (Exception ex)
            {
                return new List<ItemModel>();
            }
        }
        public async Task<ItemModel> GetItemModel(int Id)
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                return await database.Items.FirstOrDefaultAsync(i => i.Id == Id);

            }
            catch (Exception ex)
            {
                return new ItemModel();
            }
        }
        public async Task<List<ItemModel>> GetItemsByTag(string tag)
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                var res = (from item in database.Items
                           where item.Tags.Contains(tag)
                           select item).ToList();

                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<string>> GetTags()
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                var res = (from item in database.Items
                           select item.Tags).ToList();
                return res;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public async Task<List<ItemModel>> GetLatestItems()
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                return database.Items.OrderByDescending(i => i.Date).Take(3).ToList();
            }
            catch (Exception ex)
            {
                return new List<ItemModel>();
            }

        }
        public async Task<(List<int>, List<int>)> GetLargestCollections()
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                var ress = database.Items
                    .GroupBy(i => i.CollectionId)
                    .OrderByDescending(ig => ig.Count())
                    .Select(ig => ig.Count())
                    .Take(5)
                    .ToList();
                var res = database.Items
                    .GroupBy(i => i.CollectionId)
                    .OrderByDescending(ig => ig.Count())
                    .Select(k => k.Key)
                    .Take(5)
                    .ToList();
                
                return (res, ress);

            }
            catch (Exception ex)
            {
                return (null, null);
            }
        }
        public async Task<int> CreateItem(ItemModel model)
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                await database.Items.AddAsync(model);
                await database.SaveChangesAsync();
                return model.Id;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<ItemModel> UpdateItem(ItemModel model)
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                database.Entry(model).State = EntityState.Modified;
                await database.SaveChangesAsync();
                return model;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<(bool, string)> DeleteItem(ItemModel model)
        {
            using var database = _dtb.CreateDbContext();
            try
            {
                var dbItem = await database.Items.FindAsync(model.Id);

                if (dbItem == null)
                {
                    return (false, "Item could not be found");
                }

                database.Items.Remove(dbItem);
                await database.SaveChangesAsync();

                return (true, "Item got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred. Error Message: {ex.Message}");
            }
        }
        #endregion
    }
}
