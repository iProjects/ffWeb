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
    public class ProfileModel
    {
        public string OtherNames { get; set; }

        public string DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string Telephone { get; set; }

        public string RefferedBy { get; set; }

        public string InformBy { get; set; }
         
    }


    public class StatisticsModel
    {
        public int LendOffersByMe { get; set; }

        public int LendOffersToMe { get; set; }

        public int BorrowOffersByMe { get; set; }

        public int BorrowOffersToMe { get; set; } 

    }

 
 
 
 


}