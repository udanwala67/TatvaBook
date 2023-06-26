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
using Microsoft.EntityFrameworkCore;
using TatvaBook.Repository;

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



        public IActionResult FilterAddToFriends(string? searchQuery)
        {

            List<TatvaBookUser> user = _tatvaBookContext.TatvaBookUsers.ToList();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                user = user.Where(m => m.Full_Name.ToLower().Contains(searchQuery)).ToList();
            }

            return PartialView("_UsersListPartial", new friendRequestViewModel() { tatvaBookUsers = user });
        }



        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(string senderId, string receiverId)
        {
            bool existingFriendRequest = _tatvaBookContext.FriendRequests
            .Any(fr => fr.SenderID == senderId && fr.ReceiverID == receiverId);

            if (existingFriendRequest)
            {

                return BadRequest("Friend request already exists.");
            }
            else
            {
                senderId = _userManager.GetUserId(User);

                // Check if the sender and receiver exist in the database

                var sender = await _userManager.FindByIdAsync(senderId);
                var receiver = await _userManager.FindByIdAsync(receiverId);

                if (sender == null || receiver == null)
                {
                    return RedirectToAction("AddToFriends", "Home");
                }


                // Create a new friend request
                var friendRequest = new FriendRequest
                {
                    SenderID = senderId,
                    ReceiverID = Convert.ToString(receiverId),
                    Status = "pending",// Set the initial status of the friend request
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                };

                // Add the friend request to the database
                _tatvaBookContext.FriendRequests.Add(friendRequest);
                await _tatvaBookContext.SaveChangesAsync();


                return RedirectToAction("AddToFriends");
            }

        }

        public async Task<IActionResult> AddToFriends()
        {
            var userId = _userManager.GetUserId(User);

            List<FriendRequest> requests = _tatvaBookContext.FriendRequests.Where(u => u.ReceiverID == userId && u.Status == "pending").ToList();

            var friendRequests = new List<friendRequestViewModel>();

            foreach (var request in requests)
            {
                var friendRequest = new friendRequestViewModel();
                friendRequest.RequestID = request.RequestID;
                friendRequest.SenderID = request.SenderID;
                friendRequest.SenderName = _tatvaBookContext.TatvaBookUsers.Where(u => u.Id == request.SenderID).Select(u => u.Full_Name).First();
                friendRequest.ReceiverID = request.ReceiverID;
                friendRequests.Add(friendRequest);
            }
            return View(friendRequests);
        }
       
        public async Task<IActionResult> UpdateFriendRequest(long requestId, bool acceptRequest)
        {
            try
            {
                FriendRequest existingFriendRequest = await _tatvaBookContext.FriendRequests.FindAsync(requestId);

                if (existingFriendRequest == null)
                {
                    return View("Error");
                }

                if (acceptRequest == true)
                {
                    existingFriendRequest.Status = Services.Confirm;
                }
                else
                {
                    existingFriendRequest.Status = Services.Decline;
                }

                _tatvaBookContext.Update(existingFriendRequest);

                await _tatvaBookContext.SaveChangesAsync();


                if (existingFriendRequest.Status == Services.Confirm)
                {

                    Friends friends = new Friends();

                    friends.CreatedAt = DateTime.Now;
                    friends.FriendId = existingFriendRequest.SenderID;
                    friends.IsDeleted = false;
                    friends.UserId = existingFriendRequest.ReceiverID;

                    _tatvaBookContext.Add(friends);
                    _tatvaBookContext.SaveChanges();
                }
            }catch (Exception ex)
            {
                throw;
            }
           


            return RedirectToAction("AddToFriends", "Home"); // Redirect to the appropriate page after updating the friend request
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

