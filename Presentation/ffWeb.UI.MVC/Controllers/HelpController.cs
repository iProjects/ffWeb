using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ffWeb.UI.MVC.Controllers
{
    [HandleError]
    public class HelpController : Controller
    {
        //
        // GET: /Help/

        public ActionResult Help()
        {
            return View();
        }


    }
}
