using Instagram.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Instagram.Domain;
using Instagram.ViewModels.ProfileViewModels;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Queries
{
    public class EventsReader
    {
        private readonly ApplicationDbContext _context;

        public EventsReader(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public List<EventViewModel> GetEvents(ApplicationUser user)
        {
            var allPosts= _context.Posts
                .Where(p => p.Creator == user)
                .Include(p=>p.Likes)
                .ToList();
            
            
        }
    }
    }
}
