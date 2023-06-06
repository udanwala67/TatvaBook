using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatvaBook.Entities.ViewModels;

namespace TatvaBook.Repository.Interface
{
    public interface IHomeRepository
    {
        public bool UploadStory(string url, string userId);

        public List<StorySectionViewModel> GetStorySection();
    }
}
