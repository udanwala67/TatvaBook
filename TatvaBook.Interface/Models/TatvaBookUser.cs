using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatvaBook.Entities.Models
{
    public class TatvaBookUser : IdentityUser
    {
      
        public string? FirstName { get; set; }
      
        public string? LastName { get; set; }

        public string? Full_Name { get; set; }


    }
}
