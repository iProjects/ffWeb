using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace ffWeb.UI.MVC.Models
{
    public class AcceptOfferInfo
    {    
        public string TemplateMessage { get; set; }    
    }

    public class AcceptOfferDTO
    {
        public bool TemplateMessage { get; set; }
    }

}