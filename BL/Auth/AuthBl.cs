using Collect.io.DAL.Models;
using Collect.io.DAL;
using System.ComponentModel.DataAnnotations;
using Collect.io.BL;
namespace Collect.io.BL.Auth
{
    public class AuthBl : IAuthBL
    {
        private readonly IAuthDAL authDAL;
        private readonly IEncrypt encrypt;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthBl(IAuthDAL authDAL, IEncrypt encrypt, IHttpContextAccessor httpContextAccessor)
        {
            this.authDAL = authDAL;
            this.encrypt = encrypt;
            this.httpContextAccessor = httpContextAccessor;

        }
        //Collection
        public async Task<List<CollectionModel>> GetCollectionsById(List<int> ids)
        {
            return await authDAL.GetCollectionsById(ids);
        }
        public async Task<int> CreateCollection(CollectionModel model)
        {
            return await authDAL.CreateCollection(model);
        }
        public async Task<CollectionModel> UpdateCollection(CollectionModel model)
        {
            return await authDAL.UpdateCollection(model);
        }
        public async Task<(bool, string)> DeleteCollection(CollectionModel model)
        {
            return await authDAL.DeleteCollection(model);
        }
        public async Task<CollectionModel> GetCollection(int id)
        {
            return await authDAL.GetCollection(id);
        }
        public async Task<List<CollectionModel>> GetCollections()
        {
            return await authDAL.GetCollections();
        }
        //Item
        public async Task<int> CreateItem(ItemModel model)
        {
            return await authDAL.CreateItem(model);
        }
        public async Task<List<ItemModel>> GetLatestItems()
        {
            return await authDAL.GetLatestItems();
        }
        public async Task<ItemModel> UpdateItem(ItemModel model)
        {
            return await authDAL.UpdateItem(model);
        }
        public async Task<List<string>> GetTags()
        {
            return await authDAL.GetTags();
        }
        public async Task<(List<int>, List<int>)> GetLargestCollections()
        {
            return await authDAL.GetLargestCollections();
        }
        //User
        public async Task<int> CreateUser(UserModel model)
        {
            model.Salt = Guid.NewGuid().ToString();
            model.Password = encrypt.HashPassword(model.Password, model.Salt);
            int id = await authDAL.CreateUser(model);
            Login(id); 
            return id;
           
        }
        public void Login(int id)
        {
            httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME, id);
        }
        public async Task<List<UserModel>> GetUsers()
        {
            return await authDAL.GetUsers();
        }
        public async Task<UserModel> GetUser(int id)
        {
            return await authDAL.GetUser(id);
        }
        public async Task<UserModel> UpdateUser(UserModel model)
        {
            return await authDAL.UpdateUser(model);
        }
        public async Task<(bool, string)> DeleteUser(UserModel model)
        {
            return await authDAL.DeleteUser(model);
        }

        public async Task<int> Authenticate(string email, string password, bool rememberMe)
        {
            var user = await authDAL.GetUser(email);
            if (user != null)
            {
                if (user.Password == encrypt.HashPassword(password, user.Salt))
                {
                    Login(user.Id);
                    return user.Id;
                }
                throw new AuthorizationException();

            }
            throw new AuthorizationException();
            
           
        }
        public async Task<ValidationResult?> ValidateEmail(string email)
        {
            var user = await authDAL.GetUser(email);
            if (user != null)
            {
                //Localize
                return new ValidationResult("Email already exists");
            }
            return null;
        }
        
    }
}
