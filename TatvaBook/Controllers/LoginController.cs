using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using System.Text;
using TatvaBook.Entities.Data;
using TatvaBook.Entities.Models;
using TatvaBook.Entities.ViewModels;
using TatvaBook.Repository;
using TatvaBook.Repository.Interface;

namespace TatvaBook.Controllers
{
    public class LoginController : Controller

    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILoginRepository _LoginRepository;

        public LoginController(ILogger<LoginController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILoginRepository loginRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _LoginRepository = loginRepository;

        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationUser user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                var TatvabookUser = new TatvaBookUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.Email,
                    Full_Name = user.FirstName + " " + user.LastName,   
                    Email = user.Email,
                    PasswordHash = user.Password,
                    TwoFactorEnabled = true
                };

                /*var exisitingUser = await _userManager.FindByEmailAsync(user.Email);    
                if (exisitingUser != null)
                {
                    ModelState.AddModelError("Email", "Email Already Exist, please Enter Another Email");
                    return View();
                }*/

                IdentityResult result = await _userManager.CreateAsync(TatvabookUser, user.Password);

                if (result.Succeeded)
                {
                    /* await _signInManager.SignInAsync(TatvabookUser, isPersistent: false);*/
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(TatvabookUser);

                    var RegistrationLink = Url.Action("UserRegistred", "Login",
                        new { email = user.Email, token = token }, Request.Scheme);

                    var fromAddress = new MailAddress("buiesnessenquiry@gmail.com", "TatvaBook");
                    var toAddress = new MailAddress(user.Email);

                    var subject = "Email Confirmation For Registration";
                    var body = $"Hi,<br/><br />Please click on the following link to Register Yourself:<br /><br /><a href='{RegistrationLink}'>{RegistrationLink}</a>";
                    var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("buiesnessenquiry@gmail.com", "hytotoezdomcvbkk"),
                        EnableSsl = true
                    };
                    smtpClient.Send(message);

                   /* TempData["Success"] = "Registration link has been sent successfully to your Registred Email.";*/
                    return RedirectToAction("RegistrationConfirmation", "Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Please Enter Valid Password", error.Description);
                }

                return View();

            }
        }

        public async Task<IActionResult> RegistrationConfirmation()
        {
            return View();
        }


        public async Task<IActionResult> UserRegistred(string email, string token)
        {

            var user = await _userManager.FindByNameAsync(email);
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return View();
            }
            else
            {
                return BadRequest();
            }
        }
        public IActionResult Login()
        {

            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Login(LoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                TatvaBookUser ExistingUser = (TatvaBookUser)await _userManager.FindByEmailAsync(user.Email);

                if (ExistingUser != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(ExistingUser, user.Password, user.RememberMe, false);

                    if (result.Succeeded)
                        return RedirectToAction("CreateStory", "Home"); 

                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToAction("LoginTwoStep", new { ExistingUser.Email, user });
                    }
                    ModelState.AddModelError(nameof(user.Email), "Login Failed: Invalid Email or password");
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    return View();
                }
            }
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            /* await _userManager.IsEmailConfirmedAsync(User)*/
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var existingUser = await _userManager.FindByNameAsync(model.Email);
                if (existingUser == null)
                {
                    ModelState.AddModelError("Email", "Please Enter Valid Email Address");
                }

                else
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);

                    var EmailAddress1 = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(model.Email));

                    var passwordResetLink = Url.Action("ResetPassword", "Login",
                       new { email = EmailAddress1, token = token }, Request.Scheme);

                    /* await _userManager.SendEmailAsync(model.Email, "Reset Password", "Please reset your password by clicking here: <a href=\"" + passwordResetLink + "\">link </a>");
                  return View("ForgotPasswordConfirmation");*/

                    //if you use above code then var EmailAddress will be removed and in passwordresetlink email =  Model.Email



                    _logger.Log(LogLevel.Warning, passwordResetLink);
                    /*TempData["success"] = "Token Generated SuccessFully";*/

                    var fromAddress = new MailAddress("buiesnessenquiry@gmail.com", "TatvaBook");
                    var toAddress = new MailAddress(model.Email);

                    var subject = "Password reset request";
                    var body = $"Hi,<br/><br />Please click on the following link to reset your password:<br /><br /><a href='{passwordResetLink}'>{passwordResetLink}</a>";
                    var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("buiesnessenquiry@gmail.com", "hytotoezdomcvbkk"),
                        EnableSsl = true
                    };
                    smtpClient.Send(message);

                    TempData["success"] = "reset password link has been sent successfully";
                    /*return RedirectToAction("ResetPasswordLinkSent", "Home");*/


                }
                return View(model);
            }

        }

        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid Token");
            }
            return View();
        }


        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {

            var decodedBytes = WebEncoders.Base64UrlDecode(model.Email);
            var originalEmail = Encoding.UTF8.GetString(decodedBytes);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Password", "Something Went wrong");
                return View();
            }
            else
            {
                var user = await _userManager.FindByNameAsync(originalEmail);

                if (user == null)
                {
                    ModelState.AddModelError("Email", "Please Enter Valid Email Address and Token");
                }
                else
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        TempData["Success"] = "Password Changed Successfully,please click on login button";
                       /* return RedirectToAction("PasswordChanged", "Home");*/

                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);

                }
                return View(model);
            }

        }

        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(string email)
        {
            TempData["Success"] = "the confirmation code has been sent to your registred email successfully";
            var user = await _userManager.FindByEmailAsync(email);

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            var fromAddress = new MailAddress("buiesnessenquiry@gmail.com", "TatvaBook");
            var toAddress = new MailAddress(email);



            var subject = "Login Code For TatvaBook";
            var body = "please enter this code for tatvabook access: " + token;
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("buiesnessenquiry@gmail.com", "hytotoezdomcvbkk"),
                EnableSsl = true
            };
            smtpClient.Send(message);

            return View();

        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(TwoFactorModel twoFactorModel)
        {
            if (!ModelState.IsValid)
            {
                return View(twoFactorModel.TwoFactorCode);
            }

            var result = await _signInManager.TwoFactorSignInAsync("Email", twoFactorModel.TwoFactorCode, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("PlatformLanding", "Home");
                /* return PartialView("_Posts");*/
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View();
            }
        }
    }
}
