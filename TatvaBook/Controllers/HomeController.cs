using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TatvaBook.Entities.Data;
using TatvaBook.Entities.ViewModels;
using TatvaBook.Models;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.Extensions.Hosting.Internal;
using TatvaBook.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TatvaBook.Repository.Repository;
using TatvaBook.Entities.Models;

namespace TatvaBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly TatvaBookContext _tatvaBookContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment, TatvaBookContext tatvaBookContext, IHomeRepository homeRepository, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            this._hostEnvironment = hostEnvironment;
            this._tatvaBookContext = tatvaBookContext;
            this._userManager = userManager;
            this._homeRepository = homeRepository;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult PasswordChanged()
        {
            return View();
        }

        public IActionResult ResetPasswordLinkSent()
        {
            return View();
        }




        public IActionResult PlatformLanding()

        {
            var story = _homeRepository.GetStorySection();
            var home = new HomeViewModel();
            /*List<TatvaBookUser> tatvaBookUsers = this._tatvaBookContext.SearchUsers("").ToList();*/
            home.Stories = story;
            return View(home);

        }

    /*    [HttpPost]

        public async Task<IActionResult> PlatformLanding(string UserName)
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.tatvaBookUsers = this._tatvaBookContext.SearchUsers(UserName);
            return View(homeViewModel);
        }*/


        [HttpGet]

        public IActionResult CreateStory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStory(StoryViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (model.UploadedFiles != null)
                {
                    string UploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "UploadedFiles");
                    var extension = System.IO.Path.GetExtension(model.UploadedFiles.FileName);
                    var uniquefilename = Guid.NewGuid().ToString() + extension;
                    String FilePath = Path.Combine(UploadsFolder, uniquefilename);
                    model.UploadedFiles.CopyTo(new FileStream(FilePath, FileMode.Create));
                    var applicationUser = await _userManager.GetUserAsync(User);
                    var userId = applicationUser.Id;
                    _homeRepository.UploadStory(uniquefilename, userId);
                }
            }

            return RedirectToAction("PlatformLanding");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}