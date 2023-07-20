using Collect.io.BL.Auth;
using Microsoft.AspNetCore.Mvc;
using Collect.io.ViewModels;
using Collect.io.ViewMapper;

namespace Collect.io.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuthBL authBL;
        public RegisterController(IAuthBL authBL)
        {
            this.authBL = authBL;
        }
        [HttpGet]
        [Route("/register")]
        public IActionResult Index() 
        {
            return View("Index", new RegisterViewModel());
        }
        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> IndexPost(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var errorModel = await authBL.ValidateEmail(model.Email ?? "");
                if (errorModel != null)
                {
                    ModelState.TryAddModelError("Email", errorModel.ErrorMessage!);
                }

            }
            if (ModelState.IsValid)
            {
                await authBL.CreateUser(AuthMapper.MapRegisterViewModelToUserModel(model));
                return Redirect("/");
            }

            return View("Index", model);
        }
    }
}
