using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Instagram.ViewModels.ProfileViewModels
{
    public class PostViewModel
    {
        public Guid PostId { get; set; }
        public string UserName { get; set; }
        public string PhotoPath { get; set; }
        public string Description { get; set; }
        public DateTime DateOfPublication { get; set; }
        public int LikesQuantity { get; set; }
        public bool IsLiked { get; set; }

        private Regex regex = new Regex(@"(?<=#)\w+");

        public void ReplaceDescription(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                var tagMatches = regex.Matches(Description);
                foreach (Match match in tagMatches)
                {
                    Description = Description.Replace($"#{match.Value}",
                        String.Format(
                            $"<a href=\"/Manage/SearchPost?tag={match.Value}\" >" +
                            $"#{match.Value}</a>"));
                }

            }
        }
    }
}
