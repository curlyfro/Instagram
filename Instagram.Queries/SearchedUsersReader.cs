using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Instagram.DataAccess;
using Instagram.Domain;
using Instagram.ViewModels.ProfileViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Queries
{
    public class SearchedUsersReader
    {
        private readonly ApplicationDbContext _context;

        public SearchedUsersReader(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public List<BaseUserViewModel> GetSearchedUsers(string username, ApplicationUser currentUser)
        {
            var users = _context.Users
                .Where(p => 
                    p.FullName.Contains(username) ||
                    p.UserName.Contains(username)).ToList();

            if (String.IsNullOrEmpty(username))
            {
                users = _context.Users.ToList();
            }

            var userViewModels = new List<BaseUserViewModel>();

            foreach (var user in users)
            {
                userViewModels.Add(new BaseUserViewModel
                {
                    UserId = user.Id,
                    MainPhotoPath = user.MainPhotoPath,
                    UserName = user.UserName,
                });
            }

            return userViewModels;
        }
    }
}
