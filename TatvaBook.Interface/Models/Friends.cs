using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatvaBook.Entities.Models
{
    public class Friends
    {
        [Key]
        public int? Id { get; set; }
        public string? UserId { get; set; }
        public string? FriendId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
