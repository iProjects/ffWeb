using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Security;

namespace ffWeb.UI.MVC.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Please Select how to be Informed")]
        [Display(Name = "Inform By")]
        public string InformBy { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone: Must Conform to International format!")]
        public string Telephone { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(ErrorMessage = "Please enter your Current Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Please enter your New Password")]
        [RegularExpression(@"^(?=\S{6,}$)(?=.*?\d)(?=.*?[a-zA-Z]).+$",
            ErrorMessage = "The Password provided is invalid. Valid Password must at least 6 characters, and to be a mix of letters and numbers with no spaces")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

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

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

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

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
    
    [DataContract]
    public class CustomExpMsg
    {

        public CustomExpMsg()
        {
            this.ErrorMsg = "Service encountered an error";
        }

        public CustomExpMsg(string message)
        {
            this.ErrorMsg = message;
        }

        private int errorNumber;

        [DataMember(Order = 0)]
        public int ErrorNumber
        {
            get { return errorNumber; }
            set { errorNumber = value; }
        }

        private string errrorMsg;

        [DataMember(Order = 1)]
        public string ErrorMsg
        {
            get { return errrorMsg; }
            set { errrorMsg = value; }
        }

        private string description;
        [DataMember(Order = 2)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

    }

}
