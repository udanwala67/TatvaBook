﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatvaBook.Entities.Models
{
    public class Story
    {
        public long StoryId { get; set; }
        public long UserId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? PublishedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        /*public string? url { get; set; }*/
       /* public List<IFormFile> UploadedFiles { get; set; }*/
        public long? StoryViews { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
