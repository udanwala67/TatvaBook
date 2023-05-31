using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TatvaBook.Entities.Data;
using TatvaBook.Entities.ViewModels;
using TatvaBook.Models;

namespace TatvaBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly TatvaBookContext _tatvaBookContext;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment, TatvaBookContext tatvaBookContext)
        {
            _logger = logger;
            this._hostEnvironment = hostEnvironment;
            this._tatvaBookContext = tatvaBookContext;
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

            return View();
        }


        public async Task<IActionResult> CreateStory(List<IFormFile> UploadedFiles, StoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in UploadedFiles)
                {
                    var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                    bool basePathExists = System.IO.Directory.Exists(basePath);
                    if (!basePathExists) Directory.CreateDirectory(basePath);
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var filePath = Path.Combine(basePath, file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }
                /*  var newmodel = new StoryViewModel()
                  {
                      newmodel.UpoadedFiles = UploadedFiles,

                  };*/
                /*_tatvaBookContext.Add(newmodel);*/
                _tatvaBookContext.SaveChanges();
            }






            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}