using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;


namespace ffWeb.UI.MVC.Models
{
    public class PartialBorrowOfferModel
    {
        public int OfferId { get; set; }

        [Required(ErrorMessage = "Please enter your amount")]
        [DataType(DataType.Currency)]
        //[Range(100, 9999999999, ErrorMessage = "Amount must be between 100 and 9,999,999,999")]
        [Display(Name = "Your Amount")]
        public decimal Amount { get; set; }

        public string MemberEmail { get; set; }

        [Display(Name = "Total Offer Amount")]
        public decimal TotalOfferAmount { get; set; }

        [Display(Name = "Offer Balance")]
        public decimal OfferBalance { get; set; }

    }
}
