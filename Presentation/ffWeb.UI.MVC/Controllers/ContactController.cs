
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

namespace ffWeb.UI.MVC.Controllers
{
    [HandleError]
    public class ContactController : Controller
    {
        public ViewResult ContactUsThankYou()
        {
            int hour = DateTime.Now.Hour;
            ViewData["greeting"] = hour < 12 ? "Good Morning!" : "Good Afternoon";
            return View();
        }

        [HttpGet]
        public ViewResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ViewResult ContactUs(ContactUsModel model)
        { 
                if (ModelState.IsValid)
                {

                    RegistrationComponent rc = new RegistrationComponent();
                    fPeerLending.Entities.ContactUs _ContactUs = new fPeerLending.Entities.ContactUs();
                    _ContactUs.FirstName = model.FirstName;
                    _ContactUs.LastName = model.LastName;
                    _ContactUs.Telephone = model.Phone;
                    _ContactUs.Email = model.Email;
                    _ContactUs.Subject = model.Subject;
                    _ContactUs.Comment = model.Comment;

                    rc.CreateContactUs(_ContactUs);

                    //rc.InformVisitor(model.Email);

                    return View("ContactUsThankYou", model);
                }

                else
                {
                    return View();
                }
             
        }






    }
}
