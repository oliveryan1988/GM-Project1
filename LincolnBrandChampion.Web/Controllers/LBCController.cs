using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LincolnBrandChampion.Web.Helpers;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.BL.Persisters;
using PagedList;

namespace LincolnBrandChampion.Web.Areas.LBC.Controllers
{
    public class LBCController : Controller
    {
        ProfileBL _profileBl = new ProfileBL();
        //
        // GET: /LBC/LBC/
        //[Route]

        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult Welcome()
        {
            ViewBag.homeid = "home";
            ProfileBL _profile = new ProfileBL();
            ProfileModel model = new ProfileModel();
            var _id = System.Web.HttpContext.Current.Session["w_user"].ToString();
            var paCode = System.Web.HttpContext.Current.Session["w_pacode"].ToString();
            var _popUp = System.Web.HttpContext.Current.Session["ShowPopUpS"];
            //bool _showVideo = Convert.ToBoolean(Session["showVideo"]);
            model.WSLX_ID = _id;
            model.PA_CODE = paCode;

            if (_popUp != null)
            {
                if ((bool)_popUp == true)
                {
                    model.haveProfileWslxId = false;
                    return View(model);
                }

            }
            else
            {
                model.haveProfileWslxId = true;
            }
            //if (_showVideo != false)
            //{
            //    return RedirectToAction("Video", "LBC");
            //}

            return View(model);
        }
        //[AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        //public ActionResult Video()
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult SaveProfileWslxId(ProfileModel model)
        {
            ProfileBL _profile = new ProfileBL();
            if (_profile.UpdateProfileWslxIdByStarsId(model))
            { return RedirectToAction("Welcome", "LBC"); }
            else
            {
                return RedirectToAction("NoAuthorized", "HttpErrors");
            }


        }
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult UnderConstruction()
        {
            ViewBag.homeid = "construction";
            return View();
        }

        public ActionResult Logout()
        {
            // Clear the sessions first
            Session["w_user"] = null;
            Session["ROLE_ID"] = null;
            Session["w_pacode"] = null;

            string redirectTo = "http://www.password.dealerconnection.com/login/logout.cgi?back=" + Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + "/" ;
            return RedirectPermanent(redirectTo);
        }

    }
}