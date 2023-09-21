
using fPeerLending.Business;
using fPeerLending.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ffWeb.UI.MVC.Controllers
{
    [HandleError]
    public class MyProfileController : Controller
    {


        [Authorize]
        public ActionResult Edit()
        {
            string email = User.Identity.Name;
            RegistrationComponent rg = new RegistrationComponent();
            Member logdinmember = rg.GetMemberByEmail(email);
            return View(logdinmember);
        }


        [HttpPost]
        public ActionResult Edit([Bind] Member member)
        {
            //update the member
            RegistrationComponent rg = new RegistrationComponent();
            rg.UpdateMember(member);
            return RedirectToAction("Index", "Home");

        }








    }
}
