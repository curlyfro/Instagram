using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.DataAccess;
using Instagram.Domain;
using Instagram.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Instagram.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly NewsReader _newsReader;

        public NewsController(
            UserManager<ApplicationUser> userManager, 
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _newsReader=new NewsReader(context);
        }

        [HttpGet]
        public async Task<IActionResult> News()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);

            return Json(_newsReader.GetNews(user));
        }
    }
}