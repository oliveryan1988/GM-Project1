using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LincolnBrandChampion.Web.Helpers;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.BL.Persisters;
using System.Globalization;

namespace LincolnBrandChampion.Web.Controllers
{
    public class RegistrationController : Controller
    {
        #region Index / Event Table
        // GET: /Registration/
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.GenAdmin, LBC_Role.Super_Admin)]
        public ActionResult Index()
        {
            ViewBag.homeid = "home";

            EventBL _evBl = new EventBL();
            RegistrationBL _reg = new RegistrationBL();
            RegistrationModel regmodel = new RegistrationModel();
            RegistrationModel regmodelCanceled = new RegistrationModel();

            List<EventModel> EventList = new List<EventModel>();
            if (Convert.ToInt32(Session["ROLE_ID"]) == 1)
            {
                if (Session["StarsIdProfile"] != null)
                {
                    if (!_reg.CheckIsSelectBy(Session["StarsIdProfile"].ToString()))
                    {
                        regmodel = _reg.GetRegistrationByStarsId(Session["StarsIdProfile"].ToString());
                    }
                    else
                    {
                        return RedirectToAction("NoAuthorized", "HttpErrors");
                    }
                }
                else
                {
                    return RedirectToAction("Welcome", "LBC");
                }
            }
            else
            {
                if (Session["StarsIdProfile"] != null)
                {
                        regmodel = _reg.GetRegistrationByStarsId(Session["StarsIdProfile"].ToString());
                }
                else
                {
                    return RedirectToAction("Welcome", "LBC");
                }
            }
            ViewBag.lbcCert = regmodel.LBC_CERT;
            ViewBag.regStatus = regmodel.REGD_STATUS;
            if (regmodel.EVENT_ID != 0)
            {
                ViewBag.eventid = regmodel.EVENT_ID;
                ViewBag.registered = 1;
            }
            else
            {
                ViewBag.eventid = 0;
                ViewBag.registered = 0;
            }
            EventList = _evBl.GetAll(Session["StarsIdProfile"].ToString());
            return View(EventList);
        }
        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            ViewBag.homeid = "home";

            decimal? eventId;
            eventId = Convert.ToDecimal(frm.GetValue("radio").AttemptedValue);
            Session["EventId"] = eventId;

            return RedirectToAction("Register", "Registration");
        }
        #endregion

        #region Register
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.GenAdmin, LBC_Role.Super_Admin)]
        public ActionResult Register()
        {
            ViewBag.homeid = "home";
            List<RegistrationModel> regModel = new List<RegistrationModel>();
            RegistrationModel model = new RegistrationModel();
            RegistrationBL _regbl = new RegistrationBL();

            var _id = Session["StarsIdProfile"].ToString();

            if (Convert.ToDecimal(Session["EventId"]) != 0)
            {
                model = _regbl.GetRegistrationByStarsEventId(_id, Convert.ToDecimal(Session["EventId"]));


            }
            else
            {

                return RedirectToAction("Index", "Registration");
            }


            if (model.EVENT_ID != 0)
            {
                ViewBag.eventid = model.EVENT_ID;
                ViewBag.registered = 1;
            }
            else
            {
                ViewBag.eventid = 0;
                ViewBag.registered = 0;

            }

            model.EVENT_ID = Convert.ToDecimal(Session["EventId"]);


            if (model != null)
            {
                model.EVENT_ID = Convert.ToDecimal(Session["EventId"]);
                if (!String.IsNullOrWhiteSpace(model.DLR_PHONE))
                {
                    model.phone1 = model.DLR_PHONE.Substring(0, 3);
                    model.phone2 = model.DLR_PHONE.Substring(3, 3);
                    model.phone3 = model.DLR_PHONE.Substring(6, 4);
                }
                if (!String.IsNullOrWhiteSpace(model.PHONE))
                {
                    model.mobile1 = model.PHONE.Substring(0, 3);
                    model.mobile2 = model.PHONE.Substring(3, 3);
                    model.mobile3 = model.PHONE.Substring(6, 4);
                }
            }
            return View(model);
        }
        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.GenAdmin, LBC_Role.Super_Admin)]
        public ActionResult Register(RegistrationModel Model)
        {
            ProfileModel _prModel = new ProfileModel();
            _prModel.STARS_ID = Model.STARS_ID;
            _prModel.PA_CODE = Model.PA_CODE;
            _prModel.DLR_NAME = Model.DLR_NAME;
            _prModel.FIRST_NAME = Model.FIRST_NAME;
            _prModel.LAST_NAME = Model.LAST_NAME;
            _prModel.BADGE_NAME = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Model.BADGE_NAME.ToLower());
            _prModel.TITLE = Model.TITLE;
            _prModel.EMAIL_ID = Model.EMAIL_ID;
            _prModel.DLR_ADDRESS = Model.DLR_ADDRESS;
            _prModel.DLR_CITY = Model.DLR_CITY;
            _prModel.DLR_STATE = Model.DLR_STATE;
            _prModel.DLR_ZIP = Model.DLR_ZIP;
            _prModel.PROFILE_NOTE = Model.PROFILE_NOTE;
            _prModel.PROFILE_TYPE = Model.PROFILE_TYPE;
            _prModel.DIETARY_RESTRICTION = Model.DIETARY_RESTRICTION;
            _prModel.DLR_PHONE = Model.phone1 + Model.phone2 + Model.phone3;
            _prModel.PHONE = Model.mobile1 + Model.mobile2 + Model.mobile3;
            _prModel.BIOGRAPHY = Model.BIOGRAPHY;
            _prModel.DEPARTMENT = Model.DEPARTMENT;
            _prModel.SHIRT_SIZE = Model.SHIRT_SIZE;
            _prModel.WSLX_ID = Model.WSLX_ID;
            _prModel.UPDATED_BY = System.Web.HttpContext.Current.Session["w_user"].ToString();
            ProfileBL profile = new ProfileBL();
            EventBL evBL = new EventBL();
            EventModel evmodel = new EventModel();
            profile.UpdateProfileByStarsId(_prModel);
            evmodel = evBL.GetEventModelByID(Model.EVENT_ID);
            Model.WSLX_ID = String.IsNullOrWhiteSpace(Model.WSLX_ID) ? System.Web.HttpContext.Current.Session["w_user"].ToString() : Model.WSLX_ID;//removed the Session. it has to be the user that is Log
            Model.CREATED_DATE = DateTime.Now;
            Model.CREATED_BY = System.Web.HttpContext.Current.Session["w_user"].ToString();


            RegistrationBL _regBl = new RegistrationBL();
            if (_regBl.CheckRegistrationBy(Model.STARS_ID))
            {
                Model.REGD_STATUS = "A";
                Model.UPDATE_DATE = DateTime.Now;
                Model.UPDATED_BY = System.Web.HttpContext.Current.Session["w_user"].ToString();
                ViewBag.registered = 1;
                _regBl.UpdateRegistraion(Model);
            }
            else
            {
                ViewBag.registered = 1;
                _regBl.SaveRegistration(Model);
                evBL.UpdateEventCount(evmodel);

                EmailHelper.SendConfEMail(Model.EMAIL_ID);

            }



            return RedirectToAction("Confirmation");
        }

        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.GenAdmin, LBC_Role.Super_Admin)]
        public ActionResult CancelRegistration(FormCollection frm)
        {

            RegistrationBL _regBl = new RegistrationBL();
            RegistrationModel Model = new RegistrationModel();
            Model.REGD_STATUS = "C";
            Model.STARS_ID = frm["STARS_ID"];
            Model.EVENT_ID = Convert.ToDecimal(frm["EVENT_ID"]);
            Model.TRANSPORTATION_MODE = frm["TRANSPORTATION_MODE"];
            Model.FIRST_NAME = frm["FIRST_NAME"];
            Model.LAST_NAME = frm["LAST_NAME"];
            Model.DLR_NAME = frm["DLR_NAME"];
            Model.EMAIL_ID = frm["EMAIL_ID"];
            Model.PHONE = frm["PHONE"];
            Model.PA_CODE = frm["PA_CODE"];
            Model.CANCEL_REASON = frm["CancelReason"];
            Model.TRANSPORTATION_NEED = frm["TRANSPORTATION_NEED"];
            Model.NOTES =frm["NOTES"];
            Model.ADMIN_NOTES = frm["ADMIN_NOTES"];
            string eventname = "";
            Model.UPDATED_BY = System.Web.HttpContext.Current.Session["w_user"].ToString();
            Model.UPDATE_DATE = DateTime.Now;
            bool flag = _regBl.UpdateRegistraion(Model);
            if (flag)
            {
                EventBL evBL = new EventBL();
                EventModel evmodel = new EventModel();

                evmodel = evBL.GetEventModelByID(Model.EVENT_ID);
                evBL.UpdateEventCountDecrease(evmodel);
                eventname = evmodel.EVENT_SESSION;

            }

            EmailHelper.SendCancelEMail(Model, eventname);

            return Json(new
            {
                redirectUrl = Url.Action("Index", "Registration"),
                isRedirect = true
            });

            //return Json(new { error = 1 }, JsonRequestBehavior.AllowGet);// RedirectToAction("Index", "Registration");
        }

        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.GenAdmin, LBC_Role.Super_Admin)]
        public ActionResult ResumeRegistration(FormCollection frm)
        {

            RegistrationBL _regBl = new RegistrationBL();
            RegistrationModel Model = new RegistrationModel();
            Model.REGD_STATUS = "A";
            Model.STARS_ID = frm["STARS_ID"];
            Model.EVENT_ID = Convert.ToDecimal(frm["EVENT_ID"]);
            Model.TRANSPORTATION_MODE = frm["TRANSPORTATION_MODE"];
            Model.FIRST_NAME = frm["FIRST_NAME"];
            Model.LAST_NAME = frm["LAST_NAME"];
            Model.DLR_NAME = frm["DLR_NAME"];
            Model.EMAIL_ID = frm["EMAIL_ID"];
            Model.PHONE = frm["PHONE"];
            Model.PA_CODE = frm["PA_CODE"];
            Model.UPDATE_DATE = DateTime.Now;
            Model.TRANSPORTATION_NEED = frm["TRANSPORTATION_NEED"];
            Model.NOTES = frm["NOTES"];
            Model.ADMIN_NOTES = frm["ADMIN_NOTES"];
            string eventname = "";
            Model.UPDATED_BY = System.Web.HttpContext.Current.Session["w_user"].ToString();
            bool flag = _regBl.UpdateRegistraion(Model);
            if (flag)
            {
                EventBL evBL = new EventBL();
                EventModel evmodel = new EventModel();

                evmodel = evBL.GetEventModelByID(Model.EVENT_ID);
                evBL.UpdateEventCount(evmodel);
                eventname = evmodel.EVENT_SESSION;

            }
            EmailHelper.SendConfEMail(Model.EMAIL_ID);

            return Json(new
            {
                redirectUrl = Url.Action("Index", "Registration"),
                isRedirect = true
            });

            //return Json(new { error = 1 }, JsonRequestBehavior.AllowGet);// RedirectToAction("Index", "Registration");
        }
        #endregion



        #region travel
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.GenAdmin, LBC_Role.Super_Admin)]
        public ActionResult Travel(string starsid = "")
        {
            ViewBag.homeid = "home";
            FlightInfoModel model = new FlightInfoModel();
            FlightInfoBL _flightBl = new FlightInfoBL();
            RegistrationBL _regBl = new RegistrationBL();

            if (Session["StarsIdProfile"] != null)
            {
                var _id = Session["StarsIdProfile"].ToString();

                if (_regBl.CheckRegistrationBy(_id) == true)
                {
                    decimal? eventid = _flightBl.GetEventIdByStarsId(_id);
                    model = _flightBl.GetFlightInfoByStarsId(_id, eventid);
                    // model.EVENT_ID =  _flightBl.GetEventIdByStarsId(_id);
                    // eventid; // Run a Query against Registration table to get only event id by Stars ID
                    if (model == null)
                    {

                        return View(model);

                    }

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Registration");
                }

                
            }
            else
            {
               return RedirectToAction("Welcome", "LBC");
            }
            
        }
        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.GenAdmin, LBC_Role.Super_Admin)]
        public ActionResult Travel(FlightInfoModel model)
        {
            FlightInfoBL _flightBl = new FlightInfoBL();


            if (_flightBl.CheckFlightInfoBy(model.STARS_ID, model.EVENT_ID))
            {
                model.UPDATE_DATE = DateTime.Now;
                model.UPDATED_BY = System.Web.HttpContext.Current.Session["w_user"].ToString();
                _flightBl.UpdateFlightInfo(model);
            }
            else
            {
                model.CREATED_DATE = DateTime.Now;
                model.CREATED_BY = System.Web.HttpContext.Current.Session["w_user"].ToString();
                _flightBl.SaveFlightInfo(model);
            }
            return RedirectToAction("Welcome", "LBC");
        }
        /// <summary>
        /// Json Method to populate the terminal from the Flight info
        /// </summary>
        /// <param name="Airline"></param>
        /// <returns></returns>
        public ActionResult GetTerminal(string Airline)
        {


            AirlinesInfoBL _regbl = new AirlinesInfoBL();
            string terminal = _regbl.getTerminal(Airline);

            return Json(new
            {
                error = 0,
                term = terminal,
            }, JsonRequestBehavior.AllowGet);


        }
        #endregion

        #region registerAdmin
        [AuthorizedAttributeHelper(LBC_Role.GenAdmin, LBC_Role.Super_Admin)]
        public ActionResult registerAdmin(string search)
        {
            ViewBag.homeid = "home";
            RegistrationBL _regbl = new RegistrationBL();
            ViewBag.search = string.IsNullOrEmpty(search) ? string.Empty : search;
            List<ProfileModel> lstModel = _regbl.GetListProfileByPaCode(search);
            return View(lstModel);
        }
        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.GenAdmin, LBC_Role.Super_Admin)]
        public ActionResult registerAdmin(FormCollection frm)
        {

            string id = (frm.GetValue("regLBC").AttemptedValue);

            if (id != null)
            {
                Session["StarsIdProfile"] = id;
            }


            //RegistrationBL _regbl = new RegistrationBL();
            //ViewBag.search = string.IsNullOrEmpty(search) ? string.Empty : search;
            //List<ProfileModel> lstModel = _regbl.GetListProfileByPaCode(search);
            return RedirectToAction("Index");
        }
        // Get the list of the LBC to fill the drop down
        public ActionResult GetLBCList(string PaCode)
        {


            RegistrationBL _regbl = new RegistrationBL();
            ViewBag.search = string.IsNullOrEmpty(PaCode) ? string.Empty : PaCode;
            List<ProfileModel> lstModel = _regbl.GetListProfileByPaCode(PaCode);


            if (lstModel != null && lstModel.Count > 0)
            {
                List<ProfileModel> lst = new List<ProfileModel>();
                for (int i = 0; i < lstModel.Count; i++)
                {
                    ProfileModel item = new ProfileModel()
                    {
                        STARS_ID = lstModel.ElementAt(i).STARS_ID,
                        FIRST_NAME = lstModel.ElementAt(i).FIRST_NAME + " " + lstModel.ElementAt(i).LAST_NAME
                    };
                    lst.Add(item);
                }
                return Json(new
                {
                    error = 0,
                    lstModel = lst,
                }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { error = 1 }, JsonRequestBehavior.AllowGet);

        }


        #endregion
        public ActionResult Confirmation()
        {
            ViewBag.homeid = "home";
            ViewBag.registered = 1;
            return View();
        }
    }

}