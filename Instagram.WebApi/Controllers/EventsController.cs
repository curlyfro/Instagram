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
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EventsReader _eventsReader;

        public EventsController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _eventsReader=new EventsReader(context);
        }

        [HttpGet]
        public async Task<IActionResult> Events()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);

            return Json(_eventsReader.GetEvents(user));
        }
    }
}