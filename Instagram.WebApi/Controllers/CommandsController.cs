using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Instagram.Commands;
using Instagram.DataAccess;
using Instagram.Domain;
using Instagram.Services.Hashtag;
using Instagram.ViewModels.ProfileViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.WebApi.Controllers
{
    public class CommandsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private PutLikeCommand _putLikeCommand;
        private FollowCommand _followCommand;
        private CreatePostCommand _createPostCommand;

        public CommandsController(
            ApplicationDbContext context,
            HashtagFinder hashtagFinder,
            IHostingEnvironment appEnvironment, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _putLikeCommand = new PutLikeCommand(context);
            _followCommand = new FollowCommand(context);
            _createPostCommand = new CreatePostCommand(context,hashtagFinder,appEnvironment);
        }

        [HttpPost]
        public async Task<IActionResult> PutLike(Guid postId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            try
            {
                _putLikeCommand.PutLike(postId, user);

                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Follow(string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);

            try
            {
                _followCommand.Follow(userId, user);

                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            try
            {
                _createPostCommand.CreatePost(user, model);

                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}