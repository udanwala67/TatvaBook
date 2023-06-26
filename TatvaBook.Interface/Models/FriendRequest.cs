using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatvaBook.Entities.Models
{
    public class FriendRequest
    {
        [Key]
        public long RequestID { get; set; }

        [ForeignKey("Sender")]
        public string? SenderID { get; set; }
        public virtual TatvaBookUser? Sender { get; set; }

        [ForeignKey("Receiver")]
        public string? ReceiverID { get; set; }
        public virtual TatvaBookUser? Receiver { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Status { get; set; }  // pending, requestaccepted ,requestdeclined

    }
}
