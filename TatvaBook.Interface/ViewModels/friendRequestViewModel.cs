using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatvaBook.Entities.Models;

namespace TatvaBook.Entities.ViewModels
{
    public class friendRequestViewModel
    {

        public string? SenderName { get; set; }
        /*public List<FriendRequest>? FriendRequests { get; set; }*/
        public List<TatvaBookUser>? tatvaBookUsers { get; set; }
        public long? RequestID { get; set; }
        public string? SenderID { get; set; }
        public string? ReceiverID { get; set; }
    }
}
