using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LincolnBrandChampion.Web.Helpers;
using LincolnBrandChampion.BL.Persisters;
using LincolnBrandChampion.Model.Models;
using ReportManagement;
using System.IO;
using System.Globalization;

namespace LincolnBrandChampion.Web.Controllers
{
    [AuthorizedAttributeHelper(LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
    public class MetricsController : Controller
    {
        //
        // GET: /Metrics/
        public ActionResult Index()
        {
            ViewBag.homeid = "metrics";
            return View();
        }

        public ActionResult MetricsReport()
        {
            return View();
        }

        public ActionResult MetricsReportStatus()
        {
            return View();
        }

        public ActionResult SurveyReport()
        {
            return View();
        }

        public ActionResult GuestRegistrationReport()
        {
            return View();
        }
        public ActionResult MetricsRegistrationReport()
        {
            return View();
        }
        public ActionResult MetricsDepartureManifest()
        {
            return View();
        }
        public ActionResult MetricsHousingReport()
        {
            return View();
        }
        public ActionResult MetricsArrivalManifest()
        {
            return View();
        }
        public ActionResult MetricsAttendanceReport()
        {
            return View();
        }
        public ActionResult MetricsDietaryReport()
        {
            return View();
        }
        //-- --//
        public ActionResult MetricsCheckpointReport()
        {
            return View();
        }
        public ActionResult MetricsCancellationReport()
        {
            return View();
        }
        //-- --//
        public ActionResult RegistrationReportByMarketEvent(string market = "ALL", decimal eventid = 0)
        {
            ViewBag.homeid = "metrics";

            market = market.Equals("ALL") ? String.Empty : market;
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getRegistrationByMarketEvent(market, eventid);
            ViewBag.market = market;
            ViewBag.eventid = eventid;
            return PartialView("RegistrationReport", list);
        }
        public ActionResult RegistrationReport()
        {
            ViewBag.homeid = "metrics";

            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getRegistrationByStarsEventList();
            return PartialView(list);
            
        }
        /// <summary>
        /// Ajax call to fill the Pop up
        /// </summary>
        /// <param name="tarsId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public PartialViewResult GetRegistrationInfo(string starsId, decimal? eventId)
        {
            ViewBag.homeid = "metrics";

            MetricsBL _metricsREG = new MetricsBL();
            RegistrationReportModel model = _metricsREG.getRegistrationByStarsEvent(starsId, eventId);

            return PartialView("_RegistrationInfo", model);
        }
        /// <summary>
        /// Method to save/update the registration info for the Registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegistrationInfo(RegistrationReportModel Model)
        {
            RegistrationBL _regbl = new RegistrationBL();
            if (_regbl.CheckRegistrationBy(Model.STARS_ID))
            {
                RegistrationModel regModel = _regbl.GetRegistrationByStarsEventId(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID));
                string prevStatus = regModel.REGD_STATUS;
                
                regModel.REGD_STATUS = Model.REGD_STATUS;
                regModel.TRANSPORTATION_MODE = Model.TRANSPORT_METHOD;
                regModel.TRANSPORTATION_NEED = Model.TRANSPORTATION_NEED;
                regModel.NOTES = Model.REGISTRATION_NOTES;
                regModel.ADMIN_NOTES = Model.ADMIN_NOTES;
                regModel.ATTENDED = Model.ATTENDED;
                bool flag = _regbl.UpdateRegistraion(regModel);

                if (flag && prevStatus != Model.REGD_STATUS)
                {
                    EventBL evBL = new EventBL();
                    EventModel evmodel = new EventModel();

                    evmodel = evBL.GetEventModelByID(Convert.ToDecimal(Model.EVENT_ID));
                    evBL.UpdateEventCountDecrease(evmodel);
                }
            }

            if (Model.TRANSPORT_METHOD.Equals("FLY"))
            {
                FlightInfoBL _fibl = new FlightInfoBL();
                if (_fibl.CheckFlightInfoBy(Model.STARS_ID, Model.EVENT_ID))
                {
                    FlightInfoModel fiModel = new FlightInfoModel();
                    if (Model.REGD_STATUS == "A")
                    {
                        fiModel = _fibl.GetFlightInfoByStarsId(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID));
                    }
                    else
                    { 
                        fiModel = _fibl.GetFlightInfoByStarsIdAfterChange(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID));
                    }
                        fiModel.ARR_DATE = Model.ARR_DATE;
                    fiModel.ARR_TIME = Model.ARR_TIME;
                    fiModel.ARR_AIRLINE = Model.ARR_AIRLINE;
                    fiModel.ARR_FLIGHT_NUM = Model.ARR_FLIGHT_NUM;
                    fiModel.ARR_DEP_CITY = Model.ARR_DEP_CITY;
                    fiModel.CONNECTING_FLIGHT_NOTES = Model.CONNECTING_FLIGHT_NOTES;
                    fiModel.DEP_DATE = Model.DEP_DATE;
                    fiModel.DEP_TIME = Model.DEP_TIME;
                    fiModel.DEP_AIRLINE = Model.DEP_AIRLINE;
                    fiModel.DEP_FLIGHT_NUM = Model.DEP_FLIGHT_NUM;
                    fiModel.DEP_CITY = Model.DEP_CITY;
                    fiModel.DEP_DEST_CITY = Model.DEP_DEST_CITY;
                    fiModel.UPDATE_DATE = DateTime.Now;
                    _fibl.UpdateFlightInfo(fiModel);
                }
                else
                {
                    FlightInfoModel fiModel = new FlightInfoModel();
                    fiModel.STARS_ID = Model.STARS_ID;
                    fiModel.EVENT_ID = Model.EVENT_ID;
                    fiModel.ARR_DATE = Model.ARR_DATE;
                    fiModel.ARR_TIME = Model.ARR_TIME;
                    fiModel.ARR_AIRLINE = Model.ARR_AIRLINE;
                    fiModel.ARR_FLIGHT_NUM = Model.ARR_FLIGHT_NUM;
                    fiModel.ARR_DEP_CITY = Model.ARR_DEP_CITY;
                    fiModel.CONNECTING_FLIGHT_NOTES = Model.CONNECTING_FLIGHT_NOTES;
                    fiModel.DEP_DATE = Model.DEP_DATE;
                    fiModel.DEP_TIME = Model.DEP_TIME;
                    fiModel.DEP_AIRLINE = Model.DEP_AIRLINE;
                    fiModel.DEP_FLIGHT_NUM = Model.DEP_FLIGHT_NUM;
                    fiModel.DEP_CITY = Model.DEP_CITY;
                    fiModel.DEP_DEST_CITY = Model.DEP_DEST_CITY;
                    fiModel.CREATED_DATE = DateTime.Now;
                    fiModel.CREATED_BY = Session["w_user"].ToString();
                    _fibl.SaveFlightInfo(fiModel);
                }
            }

            HotelInfoBL _htbl = new HotelInfoBL();            
            if (_htbl.ExistHotelForUser(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID)))
            {
                HotelCarModel htModel = _htbl.GetRegistrationByStarsId(Model.STARS_ID);
                htModel.CAR_CONFIRM = Model.CAR_CONFIRM;
                htModel.HOTEL_CONF = Model.HOTEL_CONF;
                htModel.UPDATE_DATE = DateTime.Now;
                _htbl.UpdateHotelInfo(htModel);
            }
            else if (!String.IsNullOrEmpty(Model.CAR_CONFIRM) || !String.IsNullOrEmpty(Model.HOTEL_CONF))
            {
                HotelCarModel htModel = new HotelCarModel();
                htModel.STARS_ID = Model.STARS_ID;
                htModel.EVENT_ID = Convert.ToDecimal(Model.EVENT_ID);
                htModel.CAR_CONFIRM = Model.CAR_CONFIRM;
                htModel.HOTEL_CONF = Model.HOTEL_CONF;
                htModel.CREATED_DATE = DateTime.Now;
                htModel.CREATED_BY = Session["w_user"].ToString();
                _htbl.SaveHotelInfo(htModel);
            }

            ProfileBL _prbl = new ProfileBL();
            ProfileModel prModel = _prbl.GetAnyProfileByStarsId(Model.STARS_ID);
            if (prModel != null)
            {
                prModel.PHONE = Model.PHONE;
                prModel.EMAIL_ID = Model.EMAIL_ID;
                prModel.DIETARY_RESTRICTION = Model.DIETARY_RESTRICTION;
                prModel.BADGE_NAME = ReportFormatHelper.FormatBadgeName(Model.BADGE_NAME);
                _prbl.UpdateProfileByStarsId(prModel);
            }

            return RedirectToAction("MetricsRegistrationReport");
        }
        //-- --//
        public ActionResult DepartureManifest()
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList();
            return PartialView(list);

        }
        public ActionResult DepartureManifestByMarketEvent(string market = "ALL", decimal eventid = 0)
        {
            market = market.Equals("ALL") ? String.Empty : market;
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByMarketEvent(market, eventid);
            ViewBag.market = market;
            ViewBag.eventid = eventid;
            return PartialView("DepartureManifest", list);
        }
        /// <summary>
        /// Ajax call to fill the Pop up
        /// </summary>
        /// <param name="starsId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public PartialViewResult GetDepartureInfo(string starsId, decimal? eventId)
        {
            MetricsBL _metricsREG = new MetricsBL();
            RegistrationReportModel model = _metricsREG.getRegistrationByStarsEvent(starsId, eventId);

            return PartialView("_DepartureInfo", model);
        }
        /// <summary>
        /// Method to save/update the departure info for the Registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DepartureInfo(RegistrationReportModel Model)
        {
            RegistrationBL _regbl = new RegistrationBL();
            if(_regbl.CheckRegistrationBy(Model.STARS_ID))
            {
                RegistrationModel regModel = _regbl.GetRegistrationByStarsEventId(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID));
                regModel.TRANSPORTATION_MODE = Model.TRANSPORT_METHOD;
                regModel.TRANSPORTATION_NEED = Model.TRANSPORTATION_NEED;
                regModel.ADMIN_NOTES = Model.ADMIN_NOTES;
                _regbl.UpdateRegistraion(regModel);
            }

            if (Model.TRANSPORT_METHOD.Equals("FLY"))
            {
                FlightInfoBL _fibl = new FlightInfoBL();
                if (_fibl.CheckFlightInfoBy(Model.STARS_ID, Model.EVENT_ID))
                {
                    FlightInfoModel fiModel = _fibl.GetFlightInfoByStarsId(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID));
                    fiModel.DEP_DATE = Model.DEP_DATE;
                    fiModel.DEP_TIME = Model.DEP_TIME;
                    fiModel.DEP_AIRLINE = Model.DEP_AIRLINE;
                    fiModel.DEP_FLIGHT_NUM = Model.DEP_FLIGHT_NUM;
                    fiModel.DEP_CITY = Model.DEP_CITY;
                    fiModel.DEP_DEST_CITY = Model.DEP_DEST_CITY;
                    fiModel.UPDATE_DATE = DateTime.Now;
                    _fibl.UpdateFlightInfo(fiModel);
                }
                else
                {
                    FlightInfoModel fiModel = new FlightInfoModel();
                    fiModel.STARS_ID = Model.STARS_ID;
                    fiModel.EVENT_ID = Model.EVENT_ID;
                    fiModel.DEP_DATE = Model.DEP_DATE;
                    fiModel.DEP_TIME = Model.DEP_TIME;
                    fiModel.DEP_AIRLINE = Model.DEP_AIRLINE;
                    fiModel.DEP_FLIGHT_NUM = Model.DEP_FLIGHT_NUM;
                    fiModel.DEP_CITY = Model.DEP_CITY;
                    fiModel.DEP_DEST_CITY = Model.DEP_DEST_CITY;
                    fiModel.CREATED_DATE = DateTime.Now;
                    fiModel.CREATED_BY = Session["w_user"].ToString();
                    _fibl.SaveFlightInfo(fiModel);
                }
            }

            HotelInfoBL _htbl = new HotelInfoBL();            
            if (_htbl.ExistHotelForUser(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID)))
            {
                HotelCarModel htModel = _htbl.GetRegistrationByStarsId(Model.STARS_ID);
                htModel.CAR_CONFIRM = Model.CAR_CONFIRM;
                htModel.UPDATE_DATE = DateTime.Now;
                _htbl.UpdateHotelInfo(htModel);
            }
            else if (!String.IsNullOrEmpty(Model.CAR_CONFIRM))
            {
                HotelCarModel htModel = new HotelCarModel();
                htModel.STARS_ID = Model.STARS_ID;
                htModel.EVENT_ID = Convert.ToDecimal(Model.EVENT_ID);
                htModel.CAR_CONFIRM = Model.CAR_CONFIRM;
                htModel.CREATED_DATE = DateTime.Now;
                htModel.CREATED_BY = Session["w_user"].ToString();
                _htbl.SaveHotelInfo(htModel);
            }

            ProfileBL _prbl = new ProfileBL();
            ProfileModel prModel = _prbl.GetAnyProfileByStarsId(Model.STARS_ID);
            if (prModel != null)
            {
                prModel.PHONE = Model.PHONE;
                prModel.EMAIL_ID = Model.EMAIL_ID;
                prModel.BADGE_NAME = ReportFormatHelper.FormatBadgeName(Model.BADGE_NAME);
                _prbl.UpdateProfileByStarsId(prModel);
            }

            return RedirectToAction("MetricsDepartureManifest");
        }
        public ActionResult HousingReport()
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList();
            return PartialView(list);

        }
        public ActionResult HousingReportByMarketEvent(string market = "ALL", decimal eventid = 0)
        {
            market = market.Equals("ALL") ? String.Empty : market;
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByMarketEvent(market, eventid);
            ViewBag.market = market;
            ViewBag.eventid = eventid;
            return PartialView("HousingReport", list);
        }
        /// <summary>
        /// Ajax call to fill the Pop up
        /// </summary>
        /// <param name="starsId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public PartialViewResult GetHousingInfo(string starsId, decimal? eventId)
        {
            MetricsBL _metricsREG = new MetricsBL();
            RegistrationReportModel model = _metricsREG.getRegistrationByStarsEvent(starsId, eventId);

            return PartialView("_HousingInfo", model);
        }
        /// <summary>
        /// Method to save/update the housing info for the Registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HousingInfo(RegistrationReportModel Model)
        {
            RegistrationBL _regbl = new RegistrationBL();
            if (_regbl.CheckRegistrationBy(Model.STARS_ID))
            {
                RegistrationModel regModel = _regbl.GetRegistrationByStarsEventId(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID));
                regModel.ADMIN_NOTES = Model.ADMIN_NOTES;
                _regbl.UpdateRegistraion(regModel);
            }

            HotelInfoBL _htbl = new HotelInfoBL();
            if (_htbl.ExistHotelForUser(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID)))
            {
                HotelCarModel htModel = _htbl.GetRegistrationByStarsId(Model.STARS_ID);
                htModel.HOTEL_CONF = Model.HOTEL_CONF;
                htModel.UPDATE_DATE = DateTime.Now;
                _htbl.UpdateHotelInfo(htModel);
            }
            else if(!String.IsNullOrEmpty(Model.HOTEL_CONF))
            {
                HotelCarModel htModel = new HotelCarModel();
                htModel.STARS_ID = Model.STARS_ID;
                htModel.EVENT_ID = Convert.ToDecimal(Model.EVENT_ID);
                htModel.HOTEL_CONF = Model.HOTEL_CONF;
                htModel.CREATED_DATE = DateTime.Now;
                htModel.CREATED_BY = Session["w_user"].ToString();
                _htbl.SaveHotelInfo(htModel);
            }

            ProfileBL _prbl = new ProfileBL();
            ProfileModel prModel = _prbl.GetAnyProfileByStarsId(Model.STARS_ID);
            if (prModel != null)
            {
                prModel.PHONE = Model.PHONE;
                prModel.EMAIL_ID = Model.EMAIL_ID;
                prModel.BADGE_NAME = ReportFormatHelper.FormatBadgeName(Model.BADGE_NAME);
                _prbl.UpdateProfileByStarsId(prModel);
            }

            return RedirectToAction("MetricsHousingReport");
        }
        public ActionResult UploadHousingCsv()
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase datafile = Request.Files[0];
                string line = string.Empty;
                
                StreamReader file = new StreamReader(datafile.InputStream);
                file.ReadLine(); // skip header
                while ((line = file.ReadLine()) != null)
                {
                    var cols = line.Split(',');
                    HotelInfoBL _htbl = new HotelInfoBL();
                    HotelCarModel htModel = _htbl.GetHotelInfoByStarsEvent(cols[1], Convert.ToDecimal(cols[2]));
                    if (htModel != null)
                    {
                        htModel.HOTEL_CONF = cols[3];
                        htModel.UPDATE_DATE = DateTime.Now;
                        htModel.UPDATED_BY = Session["w_user"].ToString();
                        _htbl.UpdateHotelInfo(htModel);
                    }
                    else
                    {
                        htModel = new HotelCarModel()
                        {
                            STARS_ID = cols[1],
                            EVENT_ID = Convert.ToDecimal(cols[2]),
                            HOTEL_CONF = cols[3],
                            CREATED_DATE = DateTime.Now,
                            CREATED_BY = Session["w_user"].ToString()
                        };
                        _htbl.SaveHotelInfo(htModel);
                    }
                }
                return Json(new { status = "success" });
            }
            return Json(new { status = "error" });          
        }
        //-- --//
        public ActionResult ArrivalManifest()
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList();
            return PartialView(list);

        }
        public ActionResult ArrivalManifestByMarketEvent(string market = "ALL", decimal eventid = 0)
        {
            market = market.Equals("ALL") ? String.Empty : market;
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByMarketEvent(market, eventid);
            ViewBag.market = market;
            ViewBag.eventid = eventid;
            return PartialView("ArrivalManifest", list);
        }
        /// <summary>
        /// Ajax call to fill the Pop up
        /// </summary>
        /// <param name="starsId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public PartialViewResult GetArrivalInfo(string starsId, decimal? eventId)
        {
            MetricsBL _metricsREG = new MetricsBL();
            RegistrationReportModel model = _metricsREG.getRegistrationByStarsEvent(starsId, eventId);

            return PartialView("_ArrivalInfo", model);
        }
        /// <summary>
        /// Method to save/update the arrival info for the Registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ArrivalInfo(RegistrationReportModel Model)
        {
            RegistrationBL _regbl = new RegistrationBL();
            if (_regbl.CheckRegistrationBy(Model.STARS_ID))
            {
                RegistrationModel regModel = _regbl.GetRegistrationByStarsEventId(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID));
                regModel.TRANSPORTATION_MODE = Model.TRANSPORT_METHOD;
                regModel.ADMIN_NOTES = Model.ADMIN_NOTES;
                _regbl.UpdateRegistraion(regModel);
            }

            if (Model.TRANSPORT_METHOD.Equals("FLY"))
            {
                FlightInfoBL _fibl = new FlightInfoBL();
                if (_fibl.CheckFlightInfoBy(Model.STARS_ID, Model.EVENT_ID))
                {
                    FlightInfoModel fiModel = _fibl.GetFlightInfoByStarsId(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID));
                    fiModel.ARR_DATE = Model.ARR_DATE;
                    fiModel.ARR_TIME = Model.ARR_TIME;
                    fiModel.ARR_AIRLINE = Model.ARR_AIRLINE;
                    fiModel.ARR_FLIGHT_NUM = Model.ARR_FLIGHT_NUM;
                    fiModel.ARR_DEP_CITY = Model.ARR_DEP_CITY;
                    fiModel.CONNECTING_FLIGHT_NOTES = Model.CONNECTING_FLIGHT_NOTES;
                    fiModel.UPDATE_DATE = DateTime.Now;
                    _fibl.UpdateFlightInfo(fiModel);
                }
                else
                {
                    FlightInfoModel fiModel = new FlightInfoModel();
                    fiModel.STARS_ID = Model.STARS_ID;
                    fiModel.EVENT_ID = Model.EVENT_ID;
                    fiModel.ARR_DATE = Model.ARR_DATE;
                    fiModel.ARR_TIME = Model.ARR_TIME;
                    fiModel.ARR_AIRLINE = Model.ARR_AIRLINE;
                    fiModel.ARR_FLIGHT_NUM = Model.ARR_FLIGHT_NUM;
                    fiModel.ARR_DEP_CITY = Model.ARR_DEP_CITY;
                    fiModel.CONNECTING_FLIGHT_NOTES = Model.CONNECTING_FLIGHT_NOTES;
                    fiModel.CREATED_DATE = DateTime.Now;
                    fiModel.CREATED_BY = Session["w_user"].ToString();
                    _fibl.SaveFlightInfo(fiModel);
                }
            }

            HotelInfoBL _htbl = new HotelInfoBL();            
            if (_htbl.ExistHotelForUser(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID)))
            {
                HotelCarModel htModel = _htbl.GetRegistrationByStarsId(Model.STARS_ID);
                htModel.CAR_CONFIRM = Model.CAR_CONFIRM;
                htModel.UPDATE_DATE = DateTime.Now;
                _htbl.UpdateHotelInfo(htModel);
            }
            else if (!String.IsNullOrEmpty(Model.CAR_CONFIRM))
            {
                HotelCarModel htModel = new HotelCarModel();
                htModel.STARS_ID = Model.STARS_ID;
                htModel.EVENT_ID = Convert.ToDecimal(Model.EVENT_ID);
                htModel.CAR_CONFIRM = Model.CAR_CONFIRM;
                htModel.CREATED_DATE = DateTime.Now;
                htModel.CREATED_BY = Session["w_user"].ToString();
                _htbl.SaveHotelInfo(htModel);
            }

            ProfileBL _prbl = new ProfileBL();
            ProfileModel prModel = _prbl.GetAnyProfileByStarsId(Model.STARS_ID);
            if (prModel != null)
            {
                prModel.PHONE = Model.PHONE;
                prModel.EMAIL_ID = Model.EMAIL_ID;
                _prbl.UpdateProfileByStarsId(prModel);
            }

            return RedirectToAction("MetricsArrivalManifest");
        }
        public ActionResult UploadArrivalCsv()
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase datafile = Request.Files[0];
                string line = string.Empty;

                StreamReader file = new StreamReader(datafile.InputStream);
                file.ReadLine(); // skip header
                while ((line = file.ReadLine()) != null)
                {
                    var cols = line.Split(',');
                    HotelInfoBL _htbl = new HotelInfoBL();
                    HotelCarModel htModel = _htbl.GetHotelInfoByStarsEvent(cols[1], Convert.ToDecimal(cols[2]));
                    if (htModel != null)
                    {
                        htModel.CAR_CONFIRM = cols[3];
                        htModel.UPDATE_DATE = DateTime.Now;
                        htModel.UPDATED_BY = Session["w_user"].ToString();
                        _htbl.UpdateHotelInfo(htModel);
                    }
                    else
                    {
                        htModel = new HotelCarModel()
                        {
                            STARS_ID = cols[1],
                            EVENT_ID = Convert.ToDecimal(cols[2]),
                            CAR_CONFIRM = cols[3],
                            CREATED_DATE = DateTime.Now,
                            CREATED_BY = Session["w_user"].ToString()
                        };
                        _htbl.SaveHotelInfo(htModel);
                    }
                }
                return Json(new { status = "success" });
            }
            return Json(new { status = "error" });
        }
        //-- --//
        public ActionResult AttendanceReport()
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList();
            return PartialView(list);

        }
        public ActionResult AttendanceReportByMarketEvent(string market = "ALL", decimal eventid = 0)
        {
            market = market.Equals("ALL") ? String.Empty : market;
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByMarketEvent(market, eventid);
            ViewBag.market = market;
            ViewBag.eventid = eventid;
            return PartialView("AttendanceReport", list);
        }
        public PartialViewResult GetAttendanceInfo(string starsId, decimal? eventId)
        {
            MetricsBL _metricsREG = new MetricsBL();
            RegistrationReportModel model = _metricsREG.getRegistrationByStarsEvent(starsId, eventId);

            return PartialView("_AttendanceInfo", model);
        }
        /// <summary>
        /// Update the Attendace info for registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AttendanceInfo(RegistrationReportModel Model)
        {
            RegistrationBL _regbl = new RegistrationBL();
            if (_regbl.CheckRegistrationBy(Model.STARS_ID))
            {
                RegistrationModel regModel = _regbl.GetRegistrationByStarsEventId(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID));
                regModel.ATTENDED = Model.ATTENDED;
                regModel.ADMIN_NOTES = Model.ADMIN_NOTES;
                _regbl.UpdateRegistraion(regModel);
            }

            ProfileBL _prbl = new ProfileBL();
            ProfileModel prModel = _prbl.GetAnyProfileByStarsId(Model.STARS_ID);
            if (prModel != null)
            {
                prModel.PHONE = Model.PHONE;
                prModel.EMAIL_ID = Model.EMAIL_ID;
                _prbl.UpdateProfileByStarsId(prModel);
            }

            return RedirectToAction("MetricsAttendanceReport");
        }
        public ActionResult DietaryReport()
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList();
            return PartialView(list);

        }
        public ActionResult DietaryReportByMarketEvent(string market = "ALL", decimal eventid = 0)
        {
            market = market.Equals("ALL") ? String.Empty : market;
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByMarketEvent(market, eventid);
            ViewBag.market = market;
            ViewBag.eventid = eventid;
            return PartialView("DietaryReport", list);
        }
        public PartialViewResult GetDietaryInfo(string starsId, decimal? eventId)
        {
            MetricsBL _metricsREG = new MetricsBL();
            RegistrationReportModel model = _metricsREG.getRegistrationByStarsEvent(starsId, eventId);

            return PartialView("_DietaryInfo", model);
        }
        [HttpPost]
        public ActionResult DietaryInfo(RegistrationReportModel Model)
        {
            RegistrationBL _regbl = new RegistrationBL();
            if (_regbl.CheckRegistrationBy(Model.STARS_ID))
            {
                RegistrationModel regModel = _regbl.GetRegistrationByStarsEventId(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID));
                regModel.NOTES = Model.REGISTRATION_NOTES;
                regModel.ADMIN_NOTES = Model.ADMIN_NOTES;
                _regbl.UpdateRegistraion(regModel);
            }

            FlightInfoBL _fibl = new FlightInfoBL();
            if (_fibl.CheckFlightInfoBy(Model.STARS_ID, Model.EVENT_ID))
            {
                FlightInfoModel fiModel = _fibl.GetFlightInfoByStarsId(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID));
                fiModel.ARR_DATE = Model.ARR_DATE;
                fiModel.DEP_DATE = Model.DEP_DATE;
                fiModel.UPDATE_DATE = DateTime.Now;
                _fibl.UpdateFlightInfo(fiModel);
            }

            ProfileBL _prbl = new ProfileBL();
            ProfileModel prModel = _prbl.GetAnyProfileByStarsId(Model.STARS_ID);
            if (prModel != null)
            {
                prModel.PHONE = Model.PHONE;
                prModel.EMAIL_ID = Model.EMAIL_ID;
                prModel.DIETARY_RESTRICTION = Model.DIETARY_RESTRICTION;
                _prbl.UpdateProfileByStarsId(prModel);
            }

            return RedirectToAction("MetricsDietaryReport");
        }
        /// <summary>
        /// Ajax call to fill the Pop up
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult GetHotelInfo(string id)
        {
            ViewBag.homeid = "metrics";

            HotelInfoBL hotel = new HotelInfoBL();
            HotelCarModel model = new HotelCarModel();
           
            // _Event = _Event.Equals("ALL") ? null : _Event;
            model = hotel.GetRegistrationByStarsId(id);

            return PartialView("_HotelInfo", model);

        }
        /// <summary>
        /// Method to save/update the hotel info for the Registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HotelInfo(HotelCarModel model)
        {
            HotelInfoBL _hotel = new HotelInfoBL();
            model.CREATED_DATE = DateTime.Now;
            model.UPDATE_DATE = DateTime.Now;
            model.UPDATED_BY = Session["w_user"].ToString();
            model.CREATED_BY = Session["w_user"].ToString();
            if (_hotel.ExistHotelForUser(model.STARS_ID, model.EVENT_ID))
            {
                _hotel.UpdateHotelInfo(model);
            }
            else
            {
             _hotel.SaveHotelInfo(model);
            }

            return RedirectToAction("MetricsRegistrationReport");
        }
        public ActionResult UserReport()
        {
            ProfileBL _profile = new ProfileBL();
            List<ProfileModel> list = _profile.GetListProfileByStarsId();
            return PartialView(list);
        }

        [HttpPost]
        public ActionResult UserReportByMarket(string market = "ALL")
        {
            market = market.Equals("ALL") ? String.Empty : market;
            ProfileBL _profile = new ProfileBL();
            List<ProfileModel> list = _profile.GetListProfileBy(market);
            ViewBag.market = market;
            return PartialView("UserReport", list);
        }

        public ActionResult UserReportStatus()
        {
            ProfileBL _profile = new ProfileBL();
            List<ProfileModel> list = _profile.GetListProfileAndRecognitionStatusBy();
            return PartialView(list);
        }

        [HttpPost]
        public ActionResult UserReportStatusByMarket(string market = "ALL")
        {
            market = market.Equals("ALL") ? String.Empty : market;
            ProfileBL _profile = new ProfileBL();
            List<ProfileModel> list = _profile.GetListProfileAndRecognitionStatusBy(market);
            ViewBag.market = market;
            return PartialView("UserReportStatus", list);
        }

        public ActionResult SurveyReportStatus()
        {
            SurveyBL _surBL = new SurveyBL();
            List<SurveyReportModel> list = _surBL.GetSurveyReport(0);
            return PartialView(list);
        }

        public ActionResult GuestReportStatus()
        {
            GuestBL _profile = new GuestBL();
            List<GuestRegistrationModel> list = _profile.GetListGuestAndRegistrationByWslxEvent();
            return PartialView(list);
        }

        [HttpPost]
        public ActionResult GuestReportStatusByEvent(decimal eventId = 0)
        {
            GuestBL _profile = new GuestBL();
            List<GuestRegistrationModel> list = _profile.GetListGuestAndRegistrationBy(eventId);
            ViewBag.eventid = eventId;
            return PartialView("GuestReportStatus", list);
        }

        //-- --//
        public ActionResult CheckpointReport()
        {
            ViewBag.homeid = "metrics";

            CheckpointReportBL _chrBL = new CheckpointReportBL();
            List<CheckpointReportModel> list = _chrBL.GetCheckpointReportByStarsIdList();
            return PartialView(list);

        }
        [HttpPost]
        public ActionResult CheckpointReportById(decimal checkpoint = 0)
        {
            CheckpointReportBL _chrBL = new CheckpointReportBL();
            List<CheckpointReportModel> list = _chrBL.GetCheckpointReportBy(checkpoint);
            ViewBag.checkpoint = checkpoint;
            return PartialView("CheckpointReport", list);
        }
        public PartialViewResult GetCheckpointInfo(string id)
        {
            ViewBag.homeid = "metrics";

            CheckpointReportBL _chrBL = new CheckpointReportBL();
            CheckpointReportModel model = new CheckpointReportModel();

            // _Event = _Event.Equals("ALL") ? null : _Event;
            model = _chrBL.GetCheckpointInfoByStarsId(id);
            return PartialView("_CheckpointInfo", model);

        }
        [HttpPost]
        public ActionResult CheckpointInfo(CheckpointReportModel model, FormCollection frm)
        {
            CheckpointReportBL _chrBL = new CheckpointReportBL();

            string id = (frm.GetValue("attend").AttemptedValue);
            model.SHIPPING_STATUS = id;
            _chrBL.UpdateCheckpointStatus(model);

            return RedirectToAction("MetricsCheckpointReport");
        }
        //-- --//
        public ActionResult CancellationReport()
        {
            ViewBag.homeid = "metrics";

            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getCancelledRegistration();
            return PartialView(list);
        }
        public ActionResult CancellationReportByMarketEvent(string market = "ALL", decimal eventid = 0)
        {
            market = market.Equals("ALL") ? String.Empty : market;
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getCancelledByMarketEvent(market, eventid);
            ViewBag.market = market;
            ViewBag.eventid = eventid;
            return PartialView("CancellationReport", list);
        }
        /// <summary>
        /// Ajax call to fill the Pop up
        /// </summary>
        /// <param name="tarsId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public PartialViewResult GetCancellationInfo(string starsId, decimal? eventId)
        {
            ViewBag.homeid = "metrics";

            MetricsBL _metricsREG = new MetricsBL();
            RegistrationReportModel model = _metricsREG.getRegistrationByStarsEvent(starsId, eventId);

            return PartialView("_CancellationInfo", model);
        }
        [HttpPost]
        public ActionResult CancellationInfo(RegistrationReportModel Model)
        {
            RegistrationBL _regbl = new RegistrationBL();
            if(!_regbl.CheckRegistrationBy(Model.STARS_ID))
            {
                RegistrationModel regModel = _regbl.GetRegistrationByStarsEventId(Model.STARS_ID, Convert.ToDecimal(Model.EVENT_ID));
                string prevStatus = regModel.REGD_STATUS;
                
                regModel.REGD_STATUS = Model.REGD_STATUS;
                regModel.ADMIN_NOTES = Model.ADMIN_NOTES;
                bool flag = _regbl.UpdateRegistraion(regModel);

                if (flag && prevStatus != Model.REGD_STATUS)
                {
                    EventBL evBL = new EventBL();
                    EventModel evmodel = new EventModel();

                    evmodel = evBL.GetEventModelByID(Convert.ToDecimal(Model.EVENT_ID));
                    evBL.UpdateEventCount(evmodel);
                }
            }

            ProfileBL _prbl = new ProfileBL();            
            ProfileModel prModel = _prbl.GetAnyProfileByStarsId(Model.STARS_ID);
            if (prModel != null)
            {
                prModel.PHONE = Model.PHONE;
                prModel.EMAIL_ID = Model.EMAIL_ID;
                _prbl.UpdateProfileByStarsId(prModel);
            }

            return RedirectToAction("MetricsCancellationReport");
        }
        //-- --//
        public ActionResult UserReportExcel(List<string> StarsIdList)
        {
            ProfileBL _profile = new ProfileBL();
            List<ProfileModel> list = _profile.GetListProfileByStarsId(StarsIdList);
            return PartialView(list);
        }



        public ActionResult GuestReportStatusExcel(List<string> WslxList = null, List<decimal> EventIdList = null)
        {
            GuestBL _profile = new GuestBL();
            List<GuestRegistrationModel> list = _profile.GetListGuestAndRegistrationByWslxEvent(WslxList, EventIdList);
            return PartialView(list);
        }

        public ActionResult UserStatusReportExcel(List<string> StarsIdList)
        {
            ProfileBL _profile = new ProfileBL();
            List<ProfileModel> list = _profile.GetListProfileAndRecognitionStatusBy(StarsIdList);
            return PartialView(list);
        }
        public ActionResult SurveyReportExcel(List<decimal> SurveyIdList)
        {
            SurveyBL _surBL = new SurveyBL();
            List<SurveyReportModel> list = _surBL.GetSurveyReportByIdList(SurveyIdList);
            return PartialView(list);
        }
        /*/// <summary>
        /// Export the Registration Report to Excel
        /// </summary>
        /// <param name="dlrRegion"></param>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public ActionResult RegistrationReportExcel(string dlrRegion = "All", decimal eventid = 0)
        {
            MetricsBL _metricsREG = new MetricsBL();
            dlrRegion = dlrRegion.Equals("ALL") ? string.Empty : dlrRegion;
            // _Event = _Event.Equals("ALL") ? null : _Event;
            List<RegistrationReportModel> list = _metricsREG.getRegistrationByMarketEvent(dlrRegion, eventid);
            return PartialView(list);
        }*/

        /// <summary>
        /// Export the Registration Report to Excel
        /// </summary>
        /// <param name="StarsIdList"></param>
        /// <param name="EventIdList"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegistrationReportExcel(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getRegistrationByStarsEventList(StarsIdList, EventIdList);
            return PartialView(list);
        }

        /// <summary>
        /// Export the Departure Manifest to Excel
        /// </summary>
        /// <param name="StarsIdList"></param>
        /// <param name="EventIdList"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DepartureManifestExcel(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList(StarsIdList, EventIdList);
            return PartialView(list);
        }

        /// <summary>
        /// Export the Housing Report to Excel
        /// </summary>
        /// <param name="StarsIdList"></param>
        /// <param name="EventIdList"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HousingReportExcel(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList(StarsIdList, EventIdList);
            return PartialView(list);
        }

        /// <summary>
        /// Export the Arrival Manifest to Excel
        /// </summary>
        /// <param name="StarsIdList"></param>
        /// <param name="EventIdList"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ArrivalManifestExcel(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList(StarsIdList, EventIdList);
            return PartialView(list);
        }

        /// <summary>
        /// Export the Attendance Report to Excel
        /// </summary>
        /// <param name="dlrRegion"></param>
        /// <param name="eventid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AttendanceReportExcel(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList(StarsIdList, EventIdList);
            return PartialView(list);
        }

        /// <summary>
        /// Export the Dietary Restrictions Report to Excel
        /// </summary>
        /// <param name="StarsIdList"></param>
        /// <param name="EventIdList"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DietaryReportExcel(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList(StarsIdList, EventIdList);
            return PartialView(list);
        }

        [HttpPost]
        public ActionResult CheckpointReportExcel(List<string> StarsIdList, List<decimal> ChkIdList)
        {
            CheckpointReportBL _chrBL = new CheckpointReportBL();
            List<CheckpointReportModel> list = _chrBL.GetCheckpointReportByStarsIdList(StarsIdList, ChkIdList);
            return PartialView("CheckpointReportExcel", list);
        }

        /// <summary>
        /// Export the Cancellation Report to Excel
        /// </summary>
        /// <param name="StarsIdList"></param>
        /// <param name="EventIdList"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CancellationReportExcel(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getCancelledRegistration(StarsIdList, EventIdList);
            return PartialView(list);
        }
    }

    public class MetricsPdfController : PdfViewController
    {

        public ActionResult PrintUserReport(List<string> StarsIdList)
        {
            ProfileBL _profile = new ProfileBL();
            List<ProfileModel> list = _profile.GetListProfileByStarsId(StarsIdList);

            return this.ViewPdfLandscape("User Profile Report", "PrintUserReport", list);
        }

        public ActionResult PrintUserStatusReport(List<string> StarsIdList)
        {
            ProfileBL _profile = new ProfileBL();
            List<ProfileModel> list = _profile.GetListProfileAndRecognitionStatusBy(StarsIdList);

            return this.ViewPdfLandscape("User Status Report", "PrintUserStatusReport", list);
        }

        public ActionResult PrintSurveyReport(List<decimal> SurveyIdList)
        {
            SurveyBL _surBL = new SurveyBL();
            List<SurveyReportModel> list = _surBL.GetSurveyReportByIdList(SurveyIdList);
            return this.ViewPdfLandscape("Survey Report", "PrintSurveyReport", list);
        }

        public ActionResult PrintGuestReportStatus(List<string> WslxList = null, List<decimal> EventIdList = null)
        {
            GuestBL _profile = new GuestBL();
            List<GuestRegistrationModel> list = _profile.GetListGuestAndRegistrationByWslxEvent(WslxList, EventIdList);

            return this.ViewPdfLandscape("Guest Registration Report", "PrintGuestReportStatus", list);
        }
        /*/// <summary>
        /// Method to print the Report on PDF
        /// </summary>
        /// <param name="dlrRegion"></param>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public ActionResult PrintRegistrationReport(string dlrRegion = "All", decimal eventid = 0)
        {
            MetricsBL _metricsREG = new MetricsBL();
            dlrRegion = dlrRegion.Equals("ALL") ? string.Empty : dlrRegion;
            // _Event = _Event.Equals("ALL") ? null : _Event;
            List<RegistrationReportModel> list = _metricsREG.getRegistrationByMarketEvent(dlrRegion, eventid);
            //return PartialView(list);

            return this.ViewPdfLandscape("Registration  Report", "PrintRegistrationReport", list);
        }*/
        /// <summary>
        /// Method to print the Report on PDF
        /// </summary>
        /// <param name="dlrRegion"></param>
        /// <returns></returns>
        public ActionResult PrintRegistrationReport(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getRegistrationByStarsEventList(StarsIdList, EventIdList);

            return this.ViewPdfLandscape("Event Registration Master Report", "PrintRegistrationReport", list);
        }
        /// <summary>
        /// Method to print the Departure Manifest on PDF
        /// </summary>
        /// <param name="StarsIdList"></param>
        /// <param name="EventIdList"></param>
        /// <returns></returns>
        public ActionResult PrintDepartureManifest(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList(StarsIdList, EventIdList);

            return this.ViewPdfLandscape("Departure Manifest", "PrintDepartureManifest", list);
        }
        /// <summary>
        /// Method to print the Housing Report on PDF
        /// </summary>
        /// <param name="StarsIdList"></param>
        /// <param name="EventIdList"></param>
        /// <returns></returns>
        public ActionResult PrintHousingReport(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList(StarsIdList, EventIdList);

            return this.ViewPdfLandscape("Housing Report", "PrintHousingReport", list);
        }
        /// <summary>
        /// Method to print the Arrival Manifest on PDF
        /// </summary>
        /// <param name="StarsIdList"></param>
        /// <param name="EventIdList"></param>
        /// <returns></returns>
        public ActionResult PrintArrivalManifest(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList(StarsIdList, EventIdList);

            return this.ViewPdfLandscape("ArrivalManifest", "PrintArrivalManifest", list);
        }
        /// <summary>
        /// Method to print the Cancellation Report on PDF
        /// </summary>
        /// <param name="StarsIdList"></param>
        /// <param name="EventIdList"></param>
        /// <returns></returns>
        public ActionResult PrintCancellationReport(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getCancelledRegistration(StarsIdList, EventIdList);

            return this.ViewPdfLandscape("Cancellation  Report", "PrintCancellationReport", list);
        }
        /// <summary>
        /// MEthod to print the Attendance Report on PDF
        /// </summary>
        /// <param name="dlrRegion"></param>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public ActionResult PrintAttendanceReport(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList(StarsIdList, EventIdList);

            return this.ViewPdfLandscape("Attendance Report", "PrintAttendanceReport", list);
        }
        /// <summary>
        /// MEthod to print the Dietary Restrictions Report on PDF
        /// </summary>
        /// <param name="dlrRegion"></param>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public ActionResult PrintDietaryReport(List<string> StarsIdList, List<decimal?> EventIdList)
        {
            MetricsBL _metricsREG = new MetricsBL();
            List<RegistrationReportModel> list = _metricsREG.getActiveByStarsEventList(StarsIdList, EventIdList);

            return this.ViewPdfLandscape("Dietary Restrictions Report", "PrintDietaryReport", list);
        }

        public ActionResult PrintCheckpointReport(List<string> StarsIdList, List<decimal> ChkIdList)
        {
            CheckpointReportBL _chrBL = new CheckpointReportBL();
            List<CheckpointReportModel> list = _chrBL.GetCheckpointReportByStarsIdList(StarsIdList, ChkIdList);
            //return PartialView(list);

            return this.ViewPdfLandscape("Checkpoint  Report", "PrintCheckpointReport", list);
        }
    }

}