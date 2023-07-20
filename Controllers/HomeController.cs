using Collect.io.BL.Auth;
using Collect.io.Models;
using Collect.io.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Collect.io.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ICurrentUser currentUser;
        private readonly IAuthBL authBL;
        public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser, IAuthBL authBL)
        {
            this.logger = logger;
            this.currentUser = currentUser;
            this.authBL = authBL;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (currentUser.IsLoggedIn())
            {
                var collectionIds = await authBL.GetLargestCollections();
                var latestItems = await authBL.GetLatestItems();
                var tags = await authBL.GetTags( );
                var collections = await authBL.GetCollectionsById(collectionIds.Item1);
                return View("Index", new HomeViewModel(collectionIds.Item1, collectionIds.Item2,latestItems, tags, collections));
            }
            
            return Redirect("/login");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}