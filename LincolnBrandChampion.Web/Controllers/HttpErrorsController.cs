using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LincolnBrandChampion.Web.Helpers;

namespace LincolnBrandChampion.Web.Controllers
{
    //[AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
    public class HttpErrorsController : Controller
    {
        public ActionResult Http404()
        {
            ViewBag.homeid = "construction";
            return View();
        }

        public ActionResult NoAuthorized()
        {
            ViewBag.homeid = "Home";
            return View();
        }

        public ActionResult ApplicationError()
        {
            return RedirectToAction("Http404", "HttpErrorsController");
             
        }
	}
}