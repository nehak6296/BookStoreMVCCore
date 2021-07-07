using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelsLayer
{
    public class Login
    {
        [Required(ErrorMessage = "please enter your email"), EmailAddress(ErrorMessage = "please enter valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "please enter strong password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
