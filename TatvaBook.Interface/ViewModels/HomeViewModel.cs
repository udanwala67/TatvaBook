using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatvaBook.Entities.Migrations;
using TatvaBook.Entities.Models;

namespace TatvaBook.Entities.ViewModels
{
    public class HomeViewModel
    {
        public List<StorySectionViewModel> Stories { get; set; } = new List<StorySectionViewModel>();

        public List<TatvaBookUser> tatvaBookUsers { get; set; } 
        public long RequestID { get; set; }
     

        public string? SenderID { get; set; }



        public string? ReceiverID { get; set; }


        public string? Status { get; set; }  // pending,requestaccepted ,requestdeclined
    }
}
