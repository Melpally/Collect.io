using Collect.io.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Collect.io.BL.Auth
{
    public interface IAuthBL
    {
        //Users
        Task<int> Authenticate(string email, string password, bool rememberMe);
        Task<ValidationResult?> ValidateEmail(string email);
        Task<int> CreateUser(UserModel model);
        Task<List<UserModel>> GetUsers();
        Task<UserModel> GetUser(int Id);
        Task<UserModel> UpdateUser(UserModel model);
        Task<(bool, string)> DeleteUser(UserModel model);
        //Collection
        Task<List<CollectionModel>> GetCollectionsById(List<int> id);
        Task<int> CreateCollection(CollectionModel model);
        Task<CollectionModel> UpdateCollection(CollectionModel model);
        Task<CollectionModel> GetCollection(int id);
        Task<List<CollectionModel>> GetCollections();
        Task<(bool, string)> DeleteCollection(CollectionModel collection);
        //Item
        Task<List<ItemModel>> GetLatestItems();
        Task<int> CreateItem(ItemModel model);
        Task<ItemModel> UpdateItem(ItemModel model);
        Task<(List<int>, List<int>)> GetLargestCollections();
        Task<List<string>> GetTags();

    }
}
