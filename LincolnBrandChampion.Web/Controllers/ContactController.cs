using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LincolnBrandChampion.Web.Helpers;

namespace LincolnBrandChampion.Web.Controllers
{
    [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
    public class ContactController : Controller
    {
        //
        // GET: /Contact/
        public ActionResult Index()
        {
            ViewBag.homeid = "contact";
            return View();
        }
	}
}