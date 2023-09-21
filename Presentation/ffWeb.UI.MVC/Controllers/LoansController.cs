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
using fanikiwaGL.Entities;

namespace ffWeb.UI.MVC.Controllers
{
   [HandleError]
    public class LoansController : Controller
    {
        [Authorize]
        public ActionResult ListMyLoans()
        {
            RegistrationComponent rc = new RegistrationComponent();
            LoansComponent lc = new LoansComponent();

            string email = User.Identity.Name;
            Member member = rc.GetMemberByEmail(email);

            List<STO> model = lc.ListMyLoans(member.MemberId);

            return View(model);
        }



    }
}
