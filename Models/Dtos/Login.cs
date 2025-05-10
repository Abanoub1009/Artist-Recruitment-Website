using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class Login
    {
        [EmailAddress(ErrorMessage = "Invalid E-mail")]
        [Required(ErrorMessage = "pleaze provide the e-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "pleaze enter a strong password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool remmberMe { get; set; }
    }
}
