using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using jetsetterProj.Data;
using JetSetterProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JetSetterProject.Controllers
{
    public class AccountRecoveryController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        IConfiguration _configuration;

        public AccountRecoveryController(ApplicationDbContext context,
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
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM thisModel)
        {
            System.Threading.Thread.Sleep(2000);
            ViewBag.Message = "";
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Please check your email to reset your password.";
                var user = await _userManager.FindByEmailAsync(thisModel.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return View("ForgotPassword");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var callbackUrl = Url.Link("Default", new { Controller = "AccountRecovery", Action = "ResetPassword", code = code });

                await _emailSender.SendEmailAsync(
                    thisModel.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return View("ForgotPassword");
            }
            return View("ForgotPassword");
        }

        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                ResetPasswordVM model = new ResetPasswordVM { Code = code };
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM thisModel)
        {
            System.Threading.Thread.Sleep(2000);
            ViewBag.Message = "";
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(thisModel.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return View("ResetConfirm");
                }

                var result = await _userManager.ResetPasswordAsync(user, thisModel.Code, thisModel.Password);
                if (result.Succeeded)
                {
                    return View("ResetConfirm");
                }
                var errorList = new List<string>();
                foreach (var errors in result.Errors)
                {
                    errorList.Add(errors.Description);
                }
                ViewBag.ErrorMessage = errorList;
            }
            return View(thisModel);
        }
    }
}