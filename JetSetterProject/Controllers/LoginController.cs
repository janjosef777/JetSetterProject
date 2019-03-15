using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using jetsetterProj.Data;
using JetSetterProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaulMiami.AspNetCore.Mvc.Recaptcha;

namespace JetSetterProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        IConfiguration _configuration;

        public LoginController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["SiteKey"] = _configuration["Authentication:Recaptcha:SiteKey"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInVM thisModel)
        {
            System.Threading.Thread.Sleep(2000);
            ViewBag.LoginMessage = "";

            // ModelState.IsValid performs server side validation.
            // *ALWAYS* perform server side validation.
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(thisModel.LoginVM.Email, thisModel.LoginVM.Password, thisModel.LoginVM.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    ViewBag.LoginMessage = "Login attempt locked out.";
                    return View("Index", thisModel);
                }
                else if (result.IsNotAllowed)
                {
                    ViewBag.LoginMessage = "Please confirm your email before logging in.";
                    return View("Index", thisModel);
                }
                ViewBag.LoginMessage = "Invalid user name or password.";
                return View("Index", thisModel);
            }
            else
                ViewBag.LoginMessage = "This entry is invalid.";
            // return view with errors
            return View("Index", thisModel);
        }

        [ValidateRecaptcha]
        [HttpPost]
        public async Task<IActionResult> Create(SignInVM thisModel)
        {
            System.Threading.Thread.Sleep(2000);
            var user = new ApplicationUser { UserName = thisModel.RegisterVM.Email, Email = thisModel.RegisterVM.Email };

            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, thisModel.RegisterVM.Password);
                if (result.Succeeded)
                {

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "Confirm/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(thisModel.RegisterVM.Email, "Confirm your email",
                         $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    // here we assign the new user the "Traveler" role 
                    await _userManager.AddToRoleAsync(user, "Traveller");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    ViewBag.Email = thisModel.RegisterVM.Email;
                    return View("Create", thisModel);
                }
                var errorList = new List<string>();
                foreach (var errors in result.Errors)
                {
                    errorList.Add(errors.Description);
                }
                ViewBag.ErrorMessage = errorList;
                // Reset the site key if there is an error.
                ViewData["SiteKey"] = _configuration["Authentication:Recaptcha:SiteKey"];
                return View("Index", thisModel);
            }
            else
                ViewData["SiteKey"] = _configuration["Authentication:Recaptcha:SiteKey"];
            return View("Index", thisModel);
        }

    }
}