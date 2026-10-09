using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ffWeb.UI.MVC.Models
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "Please enter your Email")]
        [RegularExpression("^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?",
            ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "User Name(Use an existing email address)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your SurName")]
        [Display(Name = "Surname")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Please enter your Password")]
        [RegularExpression(@"^(?=\S{6,}$)(?=.*?\d)(?=.*?[a-zA-Z]).+$",
            ErrorMessage = "The Password provided is invalid. Valid Password must at least 6 characters, and to be a mix of letters and numbers with no spaces")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Select how to be Informed")]
        [Display(Name = "Inform By")]
        public string InformBy { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone: Must Conform to International format!")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "You must Accept the Terms and Conditions!")]
        [Display(Name = "Terms and Conditions")]
        public bool TermsAccepted { get; set; }
    }
}