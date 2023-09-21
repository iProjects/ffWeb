using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fPeerLending.Entities;
using fPeerLending.Business;

namespace ffWeb.UI.MVC.Controllers
{
    [HandleError]
    public class OffereesController : Controller
    {
        //
        // GET: /Offerees/

        public ActionResult Index(int Offerid)
        {
            ListOffersComponent lc = new ListOffersComponent();
            List<OfferReceipient> offerRecepients = lc.GetOfferReceipients(Offerid);
            ViewBag.OfferId = Offerid;

            return PartialView(offerRecepients);
        }

        //
        // GET: /Offerees/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Offerees/Create

        public ActionResult Create(int OfferId)
        {
            OfferReceipient or = new OfferReceipient();
            or.OfferId = OfferId;
            return View(or);
        }

        //
        // POST: /Offerees/Create

        [HttpPost]
        public ActionResult Create([Bind] OfferReceipient offerReceipient)
        {
        
            ListOffersComponent lc = new ListOffersComponent();
            lc.CreateOfferReceipient(offerReceipient);
            return View();
        }
        //r
        // GET: /Offerees/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Offerees/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Offerees/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Offerees/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
