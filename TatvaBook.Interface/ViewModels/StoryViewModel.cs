using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatvaBook.Entities.ViewModels
{
    public class StoryViewModel
    {


        public long StoryId { get; set; }
        public long UserId { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [Required]
        public IFormFile UploadedFiles{ get; set; }

        public long? StoryViews { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
