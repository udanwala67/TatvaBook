using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TatvaBook.Repository.Interface;

namespace TatvaBook.Controllers
{
    public class TwoFactorAuthenticationContoller : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILoginRepository _LoginRepository;

        public TwoFactorAuthenticationContoller(ILogger<LoginController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILoginRepository loginRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _LoginRepository = loginRepository;

        }

        public async Task<IActionResult> OnPostAsync()
        {
            return View();  
        }
    }
}
