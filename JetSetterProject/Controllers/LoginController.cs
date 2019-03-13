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
using Microsoft.Extensions.Logging;

namespace JetSetterProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public LoginController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(SignInVM thisModel)
        {
            ViewBag.LoginMessage = "";

            // ModelState.IsValid performs server side validation.
            // *ALWAYS* perform server side validation.
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(thisModel.LoginVM.Email, thisModel.LoginVM.Password, thisModel.LoginVM.RememberMe, lockoutOnFailure: true);
                if (result.Result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (result.Result.IsLockedOut)
                {
                    ViewBag.LoginMessage = "Login attempt locked out.";
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

        [HttpPost]
        public ActionResult Create(SignInVM thisModel)
        {
            ViewBag.ErrorMessage = "";

            var user = new ApplicationUser { UserName = thisModel.RegisterVM.Email, Email = thisModel.RegisterVM.Email };
            var result = _userManager.CreateAsync(user, thisModel.RegisterVM.Password);
            if (ModelState.IsValid)
            {
                if (result.Result.Succeeded)
                {

                    var code = _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    _emailSender.SendEmailAsync(thisModel.RegisterVM.Email, "Confirm your email",
                         $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.ErrorMessage = "This user already exists.";
                return View("Index", thisModel);
            }
            else
                ViewBag.ErrorMessage = "This entry is invalid.";
            // return view with errors
            return View(thisModel);
        }

    }
}