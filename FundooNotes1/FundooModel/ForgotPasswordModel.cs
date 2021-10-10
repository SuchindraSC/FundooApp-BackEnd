using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooModel
{
    public class ForgotPasswordModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]+([\.+\-_][A-Za-z0-9]+)*@[a-zA-Z0-9]+\.?[A-Za-z]+\.?[A-Za-z]{2,}$", ErrorMessage = "")]
        public string Emailid { get; set; }
    }
}
