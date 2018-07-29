using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Instagram.DataAccess;
using Instagram.Domain;
using Instagram.Services.Helpers;
using Instagram.Services.Senders;
using Instagram.ViewModels.AccountSettingsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Text.Encodings.Web.Utf8;
using Microsoft.Extensions.Logging;

namespace Instagram.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountSettingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHostingEnvironment _environment;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public AccountSettingsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountSettingsController> logger,
            UrlEncoder urlEncoder,
            ApplicationDbContext context,
            IHostingEnvironment appEnvironment, IHostingEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _environment = environment;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                return BadRequest(Errors.
                    AddErrorToModelState("changePassword_failure", "Couldn't change password.", ModelState));
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return new OkObjectResult(StatusMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Request.Form.Files.Any())
            {
                if (Request.Form.Files.First().FileName.EndsWith(".png") ||
                    Request.Form.Files.First().FileName.EndsWith(".jpg") ||
                    Request.Form.Files.First().FileName.EndsWith(".gif") ||
                    Request.Form.Files.First().FileName.EndsWith(".bmp") ||
                    Request.Form.Files.First().FileName.EndsWith(".tif") ||
                    Request.Form.Files.First().FileName.EndsWith(".dib"))
                {
                    string path = "/files/" + Request.Form.Files.First().FileName;
                    FileInfo fileInfo = new FileInfo(_environment.WebRootPath + user.MainPhotoPath);
                    if (!String.IsNullOrEmpty(user.MainPhotoPath))
                    {
                        fileInfo.Delete();
                    }

                    using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                    {
                        await Request.Form.Files.First().CopyToAsync(fileStream);
                    }

                    user.MainPhotoPath = path;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    model.CurrentMainPhotoPath = user.MainPhotoPath;
                    StatusMessage = "Your profile has been changed successfully";
                }
                else
                {
                    StatusMessage = "You can download only Image format files";
                }
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            if (model.FullName != user.FullName)
            {
                user.FullName = model.FullName;
                await _userManager.UpdateAsync(user);
            }

            if (model.ProfileDesc != user.ProfileDesc)
            {
                user.ProfileDesc = model.ProfileDesc;
                await _userManager.UpdateAsync(user);
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(EditProfile));
        }
    }
}