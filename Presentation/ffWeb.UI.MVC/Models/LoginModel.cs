using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ffWeb.UI.MVC.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression("^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?",
            ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "User Name(Use your email address)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your Password")]
        [RegularExpression(@"^(?=\S{6,}$)(?=.*?\d)(?=.*?[a-zA-Z]).+$",
            ErrorMessage = "The Password provided is invalid. Valid Password must at least 6 characters, and to be a mix of letters and numbers with no spaces")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        [Display(Name = "Show Password")]
        public bool ShowPassword { get; set; }
    }
}