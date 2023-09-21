using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ffWeb.UI.MVC.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Changing Lives";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }
        public ActionResult Help()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult SiteMap()
        {
            ViewBag.Message = "";

            return View();
        }
    }
}
