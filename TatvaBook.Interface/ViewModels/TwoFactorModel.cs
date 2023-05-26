using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatvaBook.Entities.ViewModels
{
    public class TwoFactorModel
    {
        [Required]
        public string? TwoFactorCode { get; set; }
    }
}
