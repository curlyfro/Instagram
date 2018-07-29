using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.DataAccess;
using Instagram.Domain;
using Instagram.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ProfileReader _profileReader;

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _profileReader = new ProfileReader(context);
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            
            return Json(_profileReader.GetProfile(userId, currentUser));
        }
    }
}