using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LincolnBrandChampion.Web.Helpers;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.BL.Persisters;
using LincolnBrandChampion.BL.PersisterHelper;
using LincolnBrandChampion.BL.Helpers;

namespace LincolnBrandChampion.Web.Controllers
{
    public class GuestController : Controller
    {
        //
        // GET: /Guest/
        public ActionResult Index()
        {
            WslxEntity curWSLXModel = new WslHelper().validateWSL();
            Session["w_user"] = curWSLXModel.WWSLX;
            ViewBag.homeid = "home";
           
               EventModel evModel = new EventModel();



               return View(evModel);
        }
        /// <summary>
        /// Created a Partial View to trigger the data from the drop down
        /// </summary>
        /// <param name="monthYear"></param>
        /// <returns></returns>
        public PartialViewResult GetSession(string monthYear )
        {
           
            string month = "";
            int year = DateTime.Now.Year;
            string[] MonYr = monthYear.Split(',');
            month = MonYr[0];
            year = Convert.ToInt32(MonYr[1]);
            EventBL _evBl = new EventBL();
            EventModel evModel = new EventModel();

            List<decimal> eventIds = new List<decimal>();
            eventIds = _evBl.GetEventModelByMonYear(month, Convert.ToDecimal(year));
            var wslxId = Session["w_user"].ToString();


            GuestSessionLookupModel guest = new GuestSessionLookupModel();
            GuestBL bl = new GuestBL();

            List<GuestSessionLookupModel> lstSession = new List<GuestSessionLookupModel>();
            evModel.SessionLookupList = bl.GetAll(eventIds);
            evModel.SessionLookupListByUser = bl.GetAllby(eventIds, wslxId);
            foreach (GuestSessionLookupModel item in evModel.SessionLookupList)
            {
                foreach (GuestSessionLookupModel itemUser in evModel.SessionLookupListByUser)
                {
                    if (itemUser.SESSION_ID == item.SESSION_ID)
                    {
                        item.REGISTERED_SESSION = itemUser.SESSION_ID;
                        break;
                    }
                    else
                    {
                        item.REGISTERED_SESSION = 0;
                    }
                }
            }


            Session["Lookuplist"] = evModel.SessionLookupList;
            //ViewBag.SessionList = bl.GetSessionByWSLX(Session["w_user"].ToString());
           



          

            return PartialView("_Sessions", evModel);

        }

        public ActionResult GuestRegister()
        {
            WslxEntity curWSLXModel = new WslHelper().validateWSL();
            Session["w_user"] = curWSLXModel.WWSLX;
            ViewBag.homeid = "home";
            GuestRegistrationModel guest = new GuestRegistrationModel();
            GuestBL bl = new GuestBL();
            guest = bl.GetGuestRegistrationBy(Session["w_user"].ToString());
            if (guest == null)
            {
                GuestRegistrationModel _guest = new GuestRegistrationModel();
                _guest.WSLX_ID =  Session["w_user"].ToString();
                if (_guest.PHONE != null)
                {
                    _guest.phone1 = _guest.PHONE.Substring(0, 3);
                    _guest.phone2 = _guest.PHONE.Substring(3, 3);
                    _guest.phone3 = _guest.PHONE.Substring(6, 4);
                }
                return View(_guest); 
            }
            if (guest.PHONE != null)
            {
                guest.phone1 = guest.PHONE.Substring(0, 3);
                guest.phone2 = guest.PHONE.Substring(3, 3);
                guest.phone3 = guest.PHONE.Substring(6, 4);
            }
            
            return View(guest); 
        }

        [HttpPost]
        public ActionResult GuestRegister(GuestRegistrationModel model)
        {
            GuestBL grBL = new GuestBL();
            model.PHONE = model.phone1 + model.phone2 + model.phone3;
            if (grBL.CheckGuestRegistrationBy(model.WSLX_ID))
            {
                model.UPDATE_DATE = DateTime.Now;
                model.UPDATED_BY = Session["w_user"].ToString();
                grBL.UpdateGuestRegistration(model);
            }
            else
            {
                model.CREATED_DATE = DateTime.Now;
                model.CREATED_BY = Session["w_user"].ToString();
                grBL.SaveGuestRegistration(model);
            }
            return RedirectToAction("GuestConfirm","Guest");
        }

        [HttpPost]
        public ActionResult RegisterSession(FormCollection frm, string wslx)
        {
            GuestBL grBL = new GuestBL();
            GuestSessionModel _gsModel = new GuestSessionModel();
            _gsModel.WSLX_ID = frm["wslx"].ToString();

            if (Session["Lookuplist"] != null)
            {
                var list = Session["Lookuplist"]as List<GuestSessionLookupModel>;
                List<decimal> lstsession = new List<decimal>();
                
                foreach (var item in list)
                {
                    lstsession.Add(item.SESSION_ID);
                
                }


                if (!grBL.removeSesssionRegistered(lstsession, _gsModel.WSLX_ID))
                {
                    throw new Exception("removeSesssionRegistered");
                }
                
            }
            else
            { return RedirectToAction("Index", "Guest"); }

            for (int i = 2; i <= frm.Count-1; i++)
            {
                _gsModel.SESSION_ID = Convert.ToDecimal(frm[i].ToString());
                _gsModel.CREATED_BY = Session["w_user"].ToString();
                _gsModel.CREATED_DATE = DateTime.Now;
                grBL.SaveGuestRegistrationSession(_gsModel);
            }
                

            return RedirectToAction("GuestRegister","Guest");
        }
        
        public ActionResult GuestConfirm()
        {
            WslxEntity curWSLXModel = new WslHelper().validateWSL();
            Session["w_user"] = curWSLXModel.WWSLX;
            ViewBag.homeid = "home";
            return View();
        }
	}
}