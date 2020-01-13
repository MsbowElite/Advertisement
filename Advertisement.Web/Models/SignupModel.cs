using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Advertisement.Web.Models
{
    public class SignupModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(6, ErrorMessage ="Password must be at least six characters long.")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password and its confirmation does not match.")]
        public string ConfirmPassword { get; set; }
    }
}
