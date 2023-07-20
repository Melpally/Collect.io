using Collect.io.ViewModels;
using Collect.io.DAL.Models;


namespace Collect.io.ViewMapper
{
    public class AuthMapper
    {
        public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel model)
        {
            return new UserModel()
            { 
                UserName = model.UserName!,
                Email = model.Email!,
                Password = model.Password!
            };
        }
    }
}
