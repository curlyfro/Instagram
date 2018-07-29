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
    public class ExploreController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ExploreReader _exploreReader;
        private readonly SearchedPostsReader _searchedPostsReader;
        private readonly SearchedUsersReader _searchedUsersReader;

        public ExploreController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _exploreReader = new ExploreReader(context);
            _searchedPostsReader = new SearchedPostsReader(context);
            _searchedUsersReader = new SearchedUsersReader(context);
        }

        [HttpGet]
        public async Task<IActionResult> Explore()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);

            return Json(_exploreReader.GetExplore(user));
        }

        [HttpPost]
        public async Task<IActionResult> SearchUsers(string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);

            return Json(_searchedUsersReader.GetSearchedUsers(username, user));
        }

        [HttpPost]
        public async Task<IActionResult> SearchPosts(string tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);

            return Json(_searchedPostsReader.GetSearchedPosts(tag, user));
        }
    }
}