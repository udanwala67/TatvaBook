using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatvaBook.Entities.Data;
using TatvaBook.Entities.Models;
using TatvaBook.Entities.ViewModels;
using TatvaBook.Repository.Interface;

namespace TatvaBook.Repository.Repository
{
    public class HomeRepository :IHomeRepository
    {
         
        private readonly TatvaBookContext _tatvaBookContext;

        public HomeRepository(TatvaBookContext tatvaBookContext)
        {
            _tatvaBookContext = tatvaBookContext;
        }
        public bool UploadStory(string url,string userId)
        {
            var stories = new Story()
            {
                UserId = userId,
                Url = url,
                PublishedAt = DateTime.Now
            };
        
            _tatvaBookContext.Stories.Add(stories);
            _tatvaBookContext.SaveChanges();

            return true;
        }

        public List<StorySectionViewModel> GetStorySection()
        {
            var stories = _tatvaBookContext.Stories.ToList();
            var storySectionList = new List<StorySectionViewModel>();

            foreach ( var story in stories)
            {
                TatvaBookUser user = _tatvaBookContext.TatvaBookUsers.FirstOrDefault(x=> x.Id == story.UserId);
                var firstName = user.FirstName;
                var StorySection = new StorySectionViewModel()
                {
                    StoryImage = story.Url,
                    UserAvatar = "volunteer1.png",
                    UserName = user.FirstName
                };

                storySectionList.Add(StorySection);
            }

            return storySectionList;
        }

      
    }
}
