using Collect.io.DAL.Models;


namespace Collect.io.DAL
{
    public interface IAuthDAL
    {
        //user services
        Task<UserModel> GetUser(string email);
        Task<List<UserModel>> GetUsers();
        Task<UserModel> GetUser(int Id);
        Task<int> CreateUser(UserModel model);
        Task<UserModel> UpdateUser(UserModel model);
        Task<(bool, string)> DeleteUser(UserModel model);
        //collection services
        Task<List<CollectionModel>> GetCollectionsById(List<int> id);
        Task<CollectionModel> GetCollection(int Id);
        Task<List<CollectionModel>> GetCollections();

        Task<int> CreateCollection(CollectionModel model);
        Task<CollectionModel> UpdateCollection(CollectionModel model);
        Task<(bool, string)> DeleteCollection(CollectionModel model);
        //item services
        Task<ItemModel> GetItemModel(int Id);
        Task<List<ItemModel>> GetItemsByTag(string tag);
        Task<List<string>> GetTags();
        Task<List<ItemModel>> GetLatestItems();
        Task<(List<int>, List<int>)> GetLargestCollections();
        Task<int> CreateItem(ItemModel model);
        Task<ItemModel> UpdateItem(ItemModel model);
        Task<(bool, string)> DeleteItem(ItemModel model);
    }
}
