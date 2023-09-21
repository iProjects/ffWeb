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
    public class MailingGroupsController : Controller
    {


        [Authorize]
        public ActionResult CreateMailingGroup(int id)
        {
            MailingGroupsComponent mc = new MailingGroupsComponent();
            MailingGroup model = new MailingGroup();
            model.ParentGroupId = id;
            MailingGroup Parent = mc.GetMailingGroupById(id);
            if (Parent != null)
            {
                ViewBag.ParentShortcode = Parent.ShortCode.Trim();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateMailingGroup([Bind] MailingGroup model)
        {
            MailingGroupsComponent mc = new MailingGroupsComponent();
            RegistrationComponent rg = new RegistrationComponent();
            MailingGroup mg = new MailingGroup();

            string email = User.Identity.Name;
            Member member = rg.SelectMemberByEmail(email);

            mg.ShortCode = model.ShortCode;
            mg.ParentGroupId = model.ParentGroupId;
            mg.Creator = member.MemberId;
            mg.CreatedOn = DateTime.Today;
            mg.LastModified = DateTime.Today;

            mc.CreateMailingGroup(mg);

            return RedirectToAction("MailingGroups");
        }

        [Authorize]
        public ActionResult EditMailingGroup(int id)
        {
            MailingGroupsComponent mc = new MailingGroupsComponent();
            MailingGroup model = new MailingGroup();
            model = mc.GetMailingGroupById(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditMailingGroup([Bind] MailingGroup model)
        {
            MailingGroupsComponent mc = new MailingGroupsComponent();
            model.LastModified = DateTime.Now;
            mc.UpdateMailingGroup(model);

            return RedirectToAction("MailingGroups");
        }

        [Authorize]
        public ActionResult MailingGroupDetails(int id)
        {
            MailingGroupsComponent mc = new MailingGroupsComponent();
            MailingGroup model = new MailingGroup();
            model = mc.GetMailingGroupById(id);

            return View(model);
        }

        [Authorize]
        public ActionResult DeleteMailingGroup(int id)
        {
            MailingGroupsComponent mc = new MailingGroupsComponent();
            MailingGroup model = new MailingGroup();
            model = mc.GetMailingGroupById(id);

            return View(model);
        }

        [Authorize]
        public ActionResult MailingGroups()
        {
            MailingGroupsComponent mc = new MailingGroupsComponent();
            RegistrationComponent rg = new RegistrationComponent();
            MailingGroup model = new MailingGroup();

            string email = User.Identity.Name;
            Member member = rg.SelectMemberByEmail(email);
            model = mc.GetTreeViewList(member.MemberId);

            return View(model);
        }
     
        [Authorize]
        public ActionResult Index()
        {
            MailingGroupsComponent mc = new MailingGroupsComponent();
            RegistrationComponent rg = new RegistrationComponent();
            string email = User.Identity.Name;
            Member member = rg.SelectMemberByEmail(email);

            MailingGroup treeView = mc.GetTreeViewList(member.MemberId);
            return PartialView(treeView);
        }

        [Authorize]
        public PartialViewResult ChildNode()
        {
            return PartialView();
        }


    }
}
