using Collect.io.BL;
using Collect.io.BL.Auth;
using Collect.io.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Collect.io.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthBL authBL;
        public LoginController(IAuthBL authBL)
        {
            this.authBL = authBL;
        }
        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> IndexPost(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBL.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch (Collect.io.BL.AuthorizationException)
                {
                    ModelState.AddModelError("Email", "Invalid email or username");
                }
                
            }
            return View("Index", model);
        }
    }
}
