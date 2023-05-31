using Microsoft.AspNetCore.Mvc;

namespace TatvaBook.Controllers
{
    public class StoryController : Controller
    {
        private readonly ILogger<LoginController> _logger;


        public StoryController(ILogger<LoginController> logger)
        {
            _logger = logger;   
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
