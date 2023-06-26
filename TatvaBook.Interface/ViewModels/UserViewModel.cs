using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatvaBook.Entities.Models;

namespace TatvaBook.Entities.ViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public List<TatvaBookUser>? tatvaBookUsers { get; set; }
        public string? LastName { get; set; }
    }
}
