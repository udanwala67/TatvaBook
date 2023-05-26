using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatvaBook.Entities.ViewModels
{
    public class ForgotPasswordModel 
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; } 
    }
}
