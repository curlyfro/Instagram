using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Instagram.DataAccess;
using Instagram.Domain;

namespace Instagram.Services.Hashtag
{
    public class HashtagFinder
    {
        private ApplicationDbContext _context;
        private List<Domain.Hashtag> tags;
        public HashtagFinder(ApplicationDbContext context)
        {
            _context = context;
            tags = _context.Hashtags.ToList();
        }
        public MatchCollection GetTags(string text)
        {
            Regex regex = new Regex(@"(?<=#)\w+");
            var matches = regex.Matches(text);
            return matches;
        }
        public void FindHashtags(Post post)
        {
            var matches = GetTags(post.Description);

            foreach (Match match in matches)
            {
                string tagAsString = match.Value;
                var tag = tags.SingleOrDefault(p => p.Name == tagAsString);
                if (tag == null)
                {
                    tag = new Domain.Hashtag
                    {
                        Name = tagAsString
                    };
                    _context.Hashtags.Add(tag);
                    _context.SaveChanges();
                }

                _context.PostTags.Add(new PostTag
                {
                    Tag = tag,
                    Post = post
                });
                _context.SaveChanges();
            }
        }
    }
}
