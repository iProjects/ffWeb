using DotNetOpenAuth.AspNet;
using fCommon.Utility;
using ffWeb.UI.MVC.Filters;
using ffWeb.UI.MVC.Models;
using fPeerLending.Business;
using fPeerLending.Entities;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
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
    public class OffersControllerTest
    {
        static ILog log;
        static LogWriter ent_logger;

        public OffersControllerTest()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            IConfigurationSource config = ConfigurationSourceFactory.Create();
            ExceptionPolicyFactory factory = new ExceptionPolicyFactory(config);
            Logger.SetLogWriter(new LogWriterFactory(config).Create(), false);
            //ExceptionManager exManager = factory.CreateManager();
            //ExceptionPolicy.SetExceptionManager(factory.CreateManager(), false);

            log4net.Config.BasicConfigurator.Configure();
            log = log4net.LogManager.GetLogger(typeof(OffersControllerTest));

            ent_logger = new LogWriterFactory().Create();
            Logger.SetLogWriter(ent_logger, false);
        }

        [TestMethod]
        public void CreateLendOffer()
        {
            try
            {
                MakeOfferComponent mk = new MakeOfferComponent();
                RegistrationComponent rg = new RegistrationComponent();

                // TODO: Add insert logic here
                string email = "fanikiwa254@gmail.com";
                Member member = rg.GetMemberByEmail(email);

                OfferModel offerModel = new OfferModel();
                offerModel.MemberId = member.MemberId;

                offerModel.Status = OfferStatus.Open.ToString();
                offerModel.CreatedDate = DateTime.Now;
                offerModel.ExpiryDate = offerModel.CreatedDate.AddMonths(Config.GetInt("OFFEREXPIRYTIMESPANINMONTHS"));
                offerModel.OfferType = "L";

                offerModel.Amount = 6000;
                offerModel.Description = "school fees";
                offerModel.Interest = 6.0;
                offerModel.OfferType = "L";
                offerModel.PartialPay = true;
                offerModel.PublicOffer = "B";
                offerModel.Term = 9;

                //Create the offer in the database
                Offer returnedOffer = mk.MakeLendOffer(offerModel);

            }
            catch (Exception ex)
            {
                ent_logger.Write(ex.ToString());
                log.Info(ex.ToString());
            }
        }

        [TestMethod]
        public void ListLendOffers()
        {
            try
            {
                ListOffersComponent lo = new ListOffersComponent();
                RegistrationComponent rg = new RegistrationComponent();

                List<Offer> offers = (from of in lo.GetAllOffers()
                                      //where of.Status == OfferStatus.Open.ToString()
                                      select of).ToList();

                //Display the offers
                foreach (Offer offer in offers)
                {
                    Member member = rg.GetMemberByID(offer.MemberId);
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(member.Email);
                    sb.AppendLine(member.OtherNames + " " + member.Surname);
                    sb.AppendLine(offer.Amount.ToString());
                    sb.AppendLine(offer.CreatedDate.ToString("dd-MM-yyyy HH:mm:ss ttt"));
                    sb.AppendLine(offer.ExpiryDate.ToString("dd-MM-yyyy HH:mm:ss ttt"));
                    sb.AppendLine(offer.OfferType);

                    Console.WriteLine(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                ent_logger.Write(ex.ToString());
                log.Info(ex.ToString());
            }
        }

        //[TestMethod]
        //public void CreateBorrowOffer()
        //{
        //    try
        //    { 
        //        MakeOfferComponent mk = new MakeOfferComponent();
        //        RegistrationComponent rg = new RegistrationComponent();

        //        // TODO: Add insert logic here
        //        string email = "fanikiwa254@gmail.com";
        //        Member member = rg.GetMemberByEmail(email);

        //        OfferModel offerModel = new OfferModel();
        //        offerModel.MemberId = member.MemberId;

        //        offerModel.Status = OfferStatus.Open.ToString();
        //        offerModel.CreatedDate = DateTime.Today;
        //        offerModel.ExpiryDate = offerModel.CreatedDate.AddMonths(Config.GetInt("OFFEREXPIRYTIMESPANINMONTHS"));
        //        offerModel.OfferType = "B";

        //        //Create the offer in the database
        //        Offer returnedOffer = mk.MakeBorrowOffer(offerModel);

        //    }
        //    catch (Exception ex)
        //    {
        //        ent_logger.Write(ex.ToString());
        //        log.Info(ex.ToString());
        //    }
        //}


        //[TestMethod]
        //public void AcceptLendOffer()
        //{ 
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
