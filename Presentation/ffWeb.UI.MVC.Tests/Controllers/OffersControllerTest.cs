using DotNetOpenAuth.AspNet;
using ffWeb.UI.MVC.Filters;
using ffWeb.UI.MVC.Models;
using fPeerLending.Business;
using fPeerLending.Entities;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace ffWeb.UI.MVC.Tests.Controllers
{

    [TestClass]
    public class OffersController
    {

        //[TestMethod]
        //public void AcceptLendOffer()
        //{
        //    DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());

        //    //Get borrower 
        //    RegistrationComponent rc = new RegistrationComponent();
        //    AcceptOfferComponent ac = new AcceptOfferComponent();
        //    ListOffersComponent lc = new ListOffersComponent();

        //    string email = "kevin@softwareproviders.co.ke";
        //    Member borrower = rc.GetMemberByEmail(email);
        //    //Get offer               
        //    Offer offer = lc.GetOfferById(16);
        //    if (offer.Status.Equals("Closed"))
        //    {
        //    }
        //    if (!offer.Status.Equals("Closed"))
        //    {
        //        ac.AcceptLendOffer(borrower, offer);
        //    }
        //}


        //[TestMethod]
        //public void AcceptBorrowOffer()
        //{
        //    DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());

        //    //Get Lender 
        //    RegistrationComponent rc = new RegistrationComponent();
        //    AcceptOfferComponent ac = new AcceptOfferComponent();
        //    ListOffersComponent lc = new ListOffersComponent();

        //    string email = "kevin@softwareproviders.co.ke";
        //    Member Lender = rc.GetMemberByEmail(email);
        //    //Get offer               
        //    Offer offer = lc.GetOfferById(44);
        //    if (offer.Status.Equals("Closed"))
        //    {
        //    }
        //    if (!offer.Status.Equals("Closed"))
        //    {
        //        ac.AcceptBorrowOffer(Lender, offer);
        //    }
        //}


        //[TestMethod]
        //public void AcceptPartialBorrowOffer()
        //{
        //    DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());

        //    //Get borrower 
        //    RegistrationComponent rc = new RegistrationComponent();
        //    AcceptOfferComponent ac = new AcceptOfferComponent();
        //    ListOffersComponent lc = new ListOffersComponent();

        //    string email = "kevin@softwareproviders.co.ke";
        //    Member Lender = rc.GetMemberByEmail(email);
        //    //Get offer               
        //    Offer offer = lc.GetOfferById(15);
        //    if (offer.Status.Equals("Closed"))
        //    {
        //    }
        //    if (!offer.Status.Equals("Closed"))
        //    {
        //        ac.AcceptPartialBorrowOffer(Lender, offer);
        //    }
        //}


    }

    public class IdValue
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public IdValue(string id, string value)
        {
            Id = id; Value = value;
        }
    }



}
