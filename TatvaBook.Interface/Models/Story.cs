using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatvaBook.Entities.Models
{
    public class Story
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StoryId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime? PublishedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Url { get; set; }
        /* public List<IFormFile> UploadedFiles { get; set; }*/
        public long? StoryViews { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
