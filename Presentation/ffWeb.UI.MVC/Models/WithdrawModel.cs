using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace ffWeb.UI.MVC.Models
{
    public class WithdrawModel
    {
        [Required(ErrorMessage = "Please enter an email")]
        [RegularExpression("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
            ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Member(Use Email Address)")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Please enter your amount")]
        [DataType(DataType.Currency)]
        [Range(100, 9999999999, ErrorMessage = "Amount must be between 1000 and 100,000")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

         [Display(Name = "Available Balance")]
        public decimal BookBalance { get; set; }


    }
}