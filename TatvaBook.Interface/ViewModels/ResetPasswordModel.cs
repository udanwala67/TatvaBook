using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatvaBook.Entities.ViewModels
{
    public class ResetPasswordModel
    {

        public string? Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Password Must Contain atleast" +
            " One lower latter,upper latter,number and special character")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Minimum 6 Characters Are Required")]
        public string? Password { get; set; }



        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password And Confirm Password Must Match")]
        public string? ConfirmPassword { get; set; }
        public string? Token { get; set; }
    }
}
