using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class Register
    {
        [Required(ErrorMessage ="pleaze enter your name")]
        public string FullName { get; set; }
        [EmailAddress(ErrorMessage ="Invalid E-mail")]
        [Required(ErrorMessage ="pleaze provide the e-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage ="pleaze enter a strong password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="re-Type the password")]
        [Required(ErrorMessage = "pleaze re-type the password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Not Matched")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="enter your phone number")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage ="Enter your Date Of Birth")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
    }
}
