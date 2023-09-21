using fanikiwaGL.Framework.ExceptionTypes;
using fCommon.Utility;
using ffWeb.UI.MVC.Models;
using fPeerLending.Business;
using fPeerLending.Entities;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ffWeb.UI.MVC.Controllers
{
    [HandleError]
    public class OffersController : Controller
    {

        // GET: /Offers/
        [Authorize]
        public ActionResult ListLendOffers()
        {

            //Get logged in Member
            string email = User.Identity.Name;
            RegistrationComponent rg = new RegistrationComponent();
            Member member = rg.GetMemberByEmail(email);

            //Retrieve member's offers
            ListOffersComponent lo = new ListOffersComponent();
            List<Offer> offers = (from of in lo.ListLendOffers(member)
                                  //where of.Status == OfferStatus.Open.ToString()
                                  select of).ToList();

            //Display the offers
            return View(offers.OrderByDescending(r => r.Amount));
        }

        [Authorize]
        public ActionResult ListMyOffers()
        {
            //Get logged in Member
            string email = User.Identity.Name;
            RegistrationComponent rg = new RegistrationComponent();
            Member member = rg.GetMemberByEmail(email);

            //Retrieve member's offers
            ListOffersComponent lo = new ListOffersComponent();
            List<Offer> offers = (from of in lo.ListMyOffers(member)
                                  where of.Status != OfferStatus.Closed.ToString()
                                  select of).ToList();

            //Display the offers
            return View(offers);

        }

        [Authorize]
        public ActionResult ListBorrowOffers()
        {
            //Get logged in Member
            string email = User.Identity.Name;
            RegistrationComponent rg = new RegistrationComponent();
            Member member = rg.GetMemberByEmail(email);

            //Retrieve member's offers
            ListOffersComponent lo = new ListOffersComponent();
            List<Offer> offers = (from of in lo.ListBorrowOffers(member)
                                  //where of.Status != OfferStatus.Closed.ToString()
                                  select of).ToList();

            //Display the offers
            return View(offers.OrderByDescending(r => r.Amount));

        }
        [Authorize]
        public ActionResult ListMyInvestMents()
        {
            //Get logged in Member
            string email = User.Identity.Name;
            RegistrationComponent rg = new RegistrationComponent();
            Member member = rg.GetMemberByEmail(email);

            //Retrieve member's offers
            ListOffersComponent lo = new ListOffersComponent();
            List<Offer> offers = (from of in lo.ListMyOffers(member)
                                  where of.Status != OfferStatus.Closed.ToString()
                                  select of).ToList();

            //Display the offers
            return View(offers);

        }
        //
        // GET: /Offers/AcceptLendOffer/5
        [Authorize]
        [HandleError(View = "SimulationException", ExceptionType = typeof(SimulationException))]
        [HandleError(View = "BatchSimulationException", ExceptionType = typeof(BatchSimulationException))]
        [HandleError()]
        public ActionResult AcceptLendOffer(int id)
        {
            //Get borrower 

            //Get logged in Member
            string email = User.Identity.Name;
            RegistrationComponent rg = new RegistrationComponent();
            Member borrower = rg.GetMemberByEmail(email);
            //Get offer
            ListOffersComponent lo = new ListOffersComponent();
            Offer offer = lo.GetOfferById(id);
            //
            AcceptOfferComponent ap = new AcceptOfferComponent();
            ap.AcceptLendOffer(borrower, offer);

            //ffWeb.UI.MVC.Helpers.Messager.Inform(borrower, "Offer Acccepted successfullly.");

            return View();

        }

        [Authorize]
        [HandleError(View = "SimulationException", ExceptionType = typeof(SimulationException))]
        [HandleError(View = "BatchSimulationException", ExceptionType = typeof(BatchSimulationException))]
        [HandleError(View = "Error")]
        public ActionResult AcceptBorrowOffer(int id)
        {
            //Get borrower 

            //Get logged in Member
            string email = User.Identity.Name;
            RegistrationComponent rg = new RegistrationComponent();
            Member Lender = rg.GetMemberByEmail(email);
            //Get offer
            ListOffersComponent lo = new ListOffersComponent();
            Offer offer = lo.GetOfferById(id);
            //
            AcceptOfferComponent ap = new AcceptOfferComponent();
            ap.AcceptBorrowOffer(Lender, offer);

            //ffWeb.UI.MVC.Helpers.Messager.Inform(Lender, "Offer Acccepted successfullly.");

            return View();
        }

        [Authorize]
        [HandleError(View = "SimulationException", ExceptionType = typeof(SimulationException))]
        [HandleError(View = "BatchSimulationException", ExceptionType = typeof(BatchSimulationException))]
        public ActionResult AcceptPartialBorrowOffer(int id)
        {
            try
            {
                //Get borrower 

                //Get logged in Member
                string email = User.Identity.Name;
                RegistrationComponent rg = new RegistrationComponent();
                Member Lender = rg.GetMemberByEmail(email);
                //Get offer
                ListOffersComponent lo = new ListOffersComponent();
                Offer offer = lo.GetOfferById(id);
                //
                AcceptOfferComponent ap = new AcceptOfferComponent();
                ap.AcceptPartialBorrowOffer(Lender, offer);

                //ffWeb.UI.MVC.Helpers.Messager.Inform(Lender, "Offer Acccepted successfullly.");

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }
        //
        // GET: /Offers/Create
        [Authorize]
        public ActionResult Create()
        {

            return View();
        }

        //
        // POST: /Offers/Create

        [HttpPost]
        [HandleError(View = "StaticPostingException", ExceptionType = typeof(StaticPostingException))] //
        public ActionResult Create([Bind] OfferModel offerModel)
        {
            MakeOfferComponent mk = new MakeOfferComponent();
            RegistrationComponent rg = new RegistrationComponent();


            // TODO: Add insert logic here
            string email = User.Identity.Name;
            Member member = rg.GetMemberByEmail(email);
            offerModel.MemberId = member.MemberId;

            offerModel.Status = OfferStatus.Open.ToString();
            offerModel.CreatedDate = DateTime.Today;
            offerModel.ExpiryDate = offerModel.CreatedDate.AddMonths(Config.GetInt("OFFEREXPIRYTIMESPANINMONTHS"));

            //Create the offer in the database
            Offer returnedOffer;
            if (offerModel.OfferType.Equals("L"))
            {
                returnedOffer = mk.MakeLendOffer(offerModel);
            }
            else if (offerModel.OfferType.Equals("B"))
            {
                returnedOffer = mk.MakeBorrowOffer(offerModel);
            }
            else
            {
                throw new ArgumentException("OfferType not known [" + offerModel.OfferType + "]");
            }

            if (offerModel.PublicOffer.Equals("V")) //for private offer, edit the offer 
            {
                return RedirectToAction("Edit", new { id = returnedOffer.Id });
            }
            else
            {
                return View("Successful");
            }
        }

        [Authorize]
        public ActionResult CreateBorrowOffer()
        {
            return View();
        }

        [HttpPost]
        [HandleError(View = "StaticPostingException", ExceptionType = typeof(StaticPostingException))]
        public ActionResult CreateBorrowOffer([Bind] OfferModel offerModel)
        {
            MakeOfferComponent mk = new MakeOfferComponent();
            RegistrationComponent rg = new RegistrationComponent();


            // TODO: Add insert logic here
            string email = User.Identity.Name;
            Member member = rg.GetMemberByEmail(email);
            offerModel.MemberId = member.MemberId;

            offerModel.Status = OfferStatus.Open.ToString();
            offerModel.CreatedDate = DateTime.Today;
            offerModel.ExpiryDate = offerModel.CreatedDate.AddMonths(Config.GetInt("OFFEREXPIRYTIMESPANINMONTHS"));
            offerModel.OfferType = "B";

            //Create the offer in the database
            Offer returnedOffer = mk.MakeBorrowOffer(offerModel);

            if (offerModel.PublicOffer.Equals("V")) //for private offer, edit the offer 
            {
                return RedirectToAction("Edit", new { id = returnedOffer.Id });
            }
            else
            {
                return View("Successful");
            }
        }


        [Authorize]
        public ActionResult CreateLendOffer()
        {
            return View();

        }
        [HttpPost]
        [HandleError(View = "StaticPostingException", ExceptionType = typeof(StaticPostingException))]
        public ActionResult CreateLendOffer([Bind] OfferModel offerModel)
        {
            MakeOfferComponent mk = new MakeOfferComponent();
            RegistrationComponent rg = new RegistrationComponent();


            // TODO: Add insert logic here
            string email = User.Identity.Name;
            Member member = rg.GetMemberByEmail(email);
            offerModel.MemberId = member.MemberId;

            offerModel.Status = OfferStatus.Open.ToString();
            offerModel.CreatedDate = DateTime.Today;
            offerModel.ExpiryDate = offerModel.CreatedDate.AddMonths(Config.GetInt("OFFEREXPIRYTIMESPANINMONTHS"));
            offerModel.OfferType = "L";

            //Create the offer in the database
            Offer returnedOffer = mk.MakeLendOffer(offerModel);

            if (offerModel.PublicOffer.Equals("V")) //for private offer, edit the offer 
            {
                return RedirectToAction("Edit", new { id = returnedOffer.Id });
            }
            else
            {
                return View("Successful");
            }
        }

        //
        // GET: /Offers/Edit/5

        public ActionResult Edit(int id)
        {
            ListOffersComponent lc = new ListOffersComponent();
            AcceptOfferComponent ac = new AcceptOfferComponent();

            Offer offer = lc.GetOfferById(id);

            OfferModel offerModel = new OfferModel();
            offerModel.Amount = offer.Amount;
            offerModel.CreatedDate = offer.CreatedDate;
            offerModel.Description = offer.Description;
            offerModel.ExpiryDate = offer.ExpiryDate;
            offerModel.Id = offer.Id;
            offerModel.Interest = offer.Interest;
            offerModel.MemberId = offer.MemberId;
            offerModel.OfferType = offer.OfferType;
            offerModel.PartialPay = offer.PartialPay;
            offerModel.PublicOffer = offer.PublicOffer;
            offerModel.Status = offer.Status;
            offerModel.Term = offer.Term;

            return View(offerModel);
        }

        //
        // POST: /Offers/Edit/5

        [HttpPost]
        public ActionResult Edit([Bind] OfferModel model)
        {
            try
            {
                ListOffersComponent lc = new ListOffersComponent();
                AcceptOfferComponent ac = new AcceptOfferComponent();

                Offer offer = lc.GetOfferById(model.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Offers/Delete/5

        public ActionResult Delete(int id, string description)
        {
            ConfirmDeleteModel model = new ConfirmDeleteModel();
            model.Id = id;
            model.Description = description;
            ListOffersComponent lo = new ListOffersComponent();
            Offer offer = lo.GetOfferById(model.Id);
            if (offer.Status.Equals("Open"))
            {
                //change status to deleted
                StaticOfferChange.SetOfferStatus(offer, OfferStatus.Closed);
                return View("ConfirmDeleteOfferView", model);
            }
            else
            { 
                return View("DeleteDenied", model);
            }
        }

        //
        // POST: /Offers/Delete/5

        [HttpPost]
        public ActionResult Delete([Bind] ConfirmDeleteModel model)
        {
            try
            {
                // TODO: Add delete logic here
                ListOffersComponent lo = new ListOffersComponent();
                Offer offer = lo.GetOfferById(model.Id);
                lo.DeleteOfferById(offer);
                return RedirectToAction("ListMyOffers");
            }
            catch
            {
                return View();
            }
        }
        [Authorize]
        public ActionResult Details(int id)
        {
            RegistrationComponent rc = new RegistrationComponent();
            ListOffersComponent lo = new ListOffersComponent();

            Offer model = lo.GetOfferById(id);
            Member _OfferOwner = rc.GetMemberByID(model.MemberId);
            ViewBag.OfferOwner = _OfferOwner.Email + " - " + _OfferOwner.Surname + "  " + _OfferOwner.OtherNames;
            return View(model);
        }

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
