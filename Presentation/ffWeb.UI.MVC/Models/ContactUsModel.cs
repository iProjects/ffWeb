using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace ffWeb.UI.MVC.Models
{
    public class ContactUsModel
    {
        [Required(ErrorMessage = "Please enter your first name")]
        [RegularExpression("^[a-zA-Z]+$",
            ErrorMessage = "Please enter valid characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [RegularExpression("^[a-zA-Z]+$",
            ErrorMessage = "Please enter valid characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        //[RegularExpression(@"((\(\d{4}\) ?)|(\d{3}-))?\d{3}-\d{4}",
        //    ErrorMessage = "Please enter a valid phone number 555-555-5555")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
            ErrorMessage = "Please enter a valid email address all lower case")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a subject")]
        //[RegularExpression("^[a-zA-Z]+$",
        //    ErrorMessage = "Please enter a subject without odd characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter your request or statement in the comment box")]
        //[RegularExpression("^[a-zA-Z]+$",
        //    ErrorMessage = "Please enter valid characters")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}