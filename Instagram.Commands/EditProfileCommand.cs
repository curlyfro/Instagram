using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Instagram.DataAccess;
using Instagram.Domain;
using Instagram.ViewModels.AccountSettingsViewModels;
using Microsoft.AspNetCore.Hosting;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Instagram.Commands
{
    public class EditProfileCommand
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public EditProfileCommand(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public void EditProfile(EditProfileViewModel model, ApplicationUser user)
        {
            if (model.NewPhoto != null)
            {
                var fileInfo = new FileInfo(_environment.WebRootPath + user.MainPhotoPath);
                if (!string.IsNullOrEmpty(user.MainPhotoPath))
                {
                    fileInfo.Delete();
                }

                string path = $"/files/{Guid.NewGuid()}_{model.NewPhoto.FileName}";

                using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                {
                    model.NewPhoto.CopyTo(fileStream);
                }

                user.MainPhotoPath = path;
                _context.Users.Update(user);
                _context.SaveChanges();
                model.CurrentMainPhotoPath = user.MainPhotoPath;

            }

            var email = user.Email;
            if (model.Email != email)
            {
                user.Email = model.Email;
                _context.Users.Update(user);
                _context.SaveChanges();
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                user.PhoneNumber = model.PhoneNumber;
                _context.Users.Update(user);
                _context.SaveChanges();
            }

            if (model.FullName != user.FullName)
            {
                user.FullName = model.FullName;
                _context.Users.Update(user);
                _context.SaveChanges();
            }

            if (model.ProfileDesc != user.ProfileDesc)
            {
                user.ProfileDesc = model.ProfileDesc;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }
    }
}
