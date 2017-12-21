using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.DL.Data;
using LincolnBrandChampion.DL.Helpers;
namespace LincolnBrandChampion.DL.Repositories
{
    public class MetricsRepository
    {
        /// <summary>
        /// Get the list of registration items by market/Event
        /// /// </summary>
        /// <param name="StarsId"></param>
        /// <returns></returns>
        public List<RegistrationReportModel> getRegistrationByMarketEvent(string market, decimal _event)
        {
            List<RegistrationReportModel> list = new List<RegistrationReportModel>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {

                var lst = (from rg in context.LBC_REGISTRATION
                           join _ev in context.LBC_EVENT on rg.EVENT_ID equals _ev.EVENT_ID
                           join pr in context.LBC_PROFILE on rg.STARS_ID equals pr.STARS_ID
                           from tr in context.LBC_FLIGHT_INFO.Where(tr => tr.STARS_ID == rg.STARS_ID && tr.EVENT_ID == rg.EVENT_ID).DefaultIfEmpty()
                           from ht in context.LBC_HOTEL_CAR_INFO.Where(ht => ht.STARS_ID == rg.STARS_ID).DefaultIfEmpty()
                           from dr in context.LBC_DIETARY_RESTRICTIONS.Where(dr => dr.DIETARY_CODE == pr.DIETARY_RESTRICTION).DefaultIfEmpty()
                           select new RegistrationReportModel
                           {
                               STARS_ID = pr.STARS_ID,
                               PA_CODE = pr.PA_CODE,
                               WSLX_ID = pr.WSLX_ID,
                               FIRST_NAME = pr.FIRST_NAME,
                               LAST_NAME = pr.LAST_NAME,
                               BADGE_NAME = pr.BADGE_NAME,
                               TITLE = pr.TITLE,
                               EMAIL_ID = pr.EMAIL_ID,
                               DLR_PHONE = pr.DLR_PHONE,
                               PHONE = pr.PHONE,
                               DLR_NAME = pr.DLR_NAME,
                               DLR_ADDRESS = pr.DLR_ADDRESS,
                               DLR_CITY = pr.DLR_CITY,
                               DLR_STATE = pr.DLR_STATE,
                               DLR_ZIP = pr.DLR_ZIP,
                               ATTENDED = rg.ATTENDED,
                               REGD_STATUS = rg.REGD_STATUS,
                               EVENT = _ev.EVENT_SESSION,
                               EVENT_ID = _ev.EVENT_ID,
                               MARKET = pr.DLR_REGION,
                               HOTEL_NAME = ht.HOTEL_NAME,
                               HOTEL_CONF = ht.HOTEL_CONF,
                               HOTEL_CHECK_IN = ht.HOTEL_CHECK_IN,
                               HOTEL_CHECK_OUT = ht.HOTEL_CHECK_OUT,
                               CAR_CONFIRM = ht.CAR_CONFIRM,
                               ADMIN_NOTES = rg.ADMIN_NOTES,
                               CAR_NOTES = ht.CAR_NOTES,
                               TRANSPORT_METHOD = rg.TRANSPORTATION_MODE,
                               // 16 fields added, 2/5/2015 - Van
                               DIETARY_RESTRICTION = dr.DIETARY_DESC,
                               REGISTRATION_NOTES = rg.NOTES,
                               ARR_DATE = tr.ARR_DATE,
                               ARR_AIRLINE = tr.ARR_AIRLINE,
                               ARR_FLIGHT_NUM = tr.ARR_FLIGHT_NUM,
                               ARR_CITY = tr.ARR_CITY,
                               ARR_DEP_CITY = tr.ARR_DEP_CITY,
                               ARR_TIME = tr.ARR_TIME,
                               ARR_TERMINAL = tr.ARR_TERMINAL,
                               CONNECTING_FLIGHT_NOTES = tr.CONNECTING_FLIGHT_NOTES,
                               DEP_DATE = tr.DEP_DATE,
                               DEP_AIRLINE = tr.DEP_AIRLINE,
                               DEP_FLIGHT_NUM = tr.DEP_FLIGHT_NUM,
                               DEP_CITY = tr.DEP_CITY,
                               DEP_DEST_CITY = tr.DEP_DEST_CITY,
                               DEP_TIME = tr.DEP_TIME,
                               CREATED_DATE = rg.CREATED_DATE,
                               // 3 fields added, 2/12/2015
                               ZONE = pr.ZONE,
                               SALESCODE = pr.SALESCODE,
                               DEPT_INFO = pr.DEPT_INFO,
                               //TODO: add ref to pr.Dealer_Type
                               DEALER_TYPE = pr.SELECT_CONTACT_DLR
                           }).ToList<RegistrationReportModel>();

                if (!String.IsNullOrWhiteSpace(market))
                {
                    lst = lst.Where(q => q.MARKET.Equals(market)).ToList();
                }

                if (_event > 0)
                {
                    lst = lst.Where(q => q.EVENT_ID == _event).ToList();
                }

                return lst;
            }
        }

        /// <summary>
        /// Get the list of registration items by FIlter
        /// /// </summary>
        /// <param name="StarsId"></param>
        /// <returns></returns>
        public List<RegistrationReportModel> getRegistrationByStarsEventList(List<string> StarsIdList = null, List<decimal?> EventIdList = null)
        {
            List<RegistrationReportModel> list = new List<RegistrationReportModel>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {

                var lst = (from rg in context.LBC_REGISTRATION
                            join _ev in context.LBC_EVENT on rg.EVENT_ID equals _ev.EVENT_ID
                            join pr in context.LBC_PROFILE on rg.STARS_ID equals pr.STARS_ID
                            from tr in context.LBC_FLIGHT_INFO.Where(tr => tr.STARS_ID == rg.STARS_ID && tr.EVENT_ID == rg.EVENT_ID).DefaultIfEmpty()
                            from ht in context.LBC_HOTEL_CAR_INFO.Where(ht => ht.STARS_ID == rg.STARS_ID).DefaultIfEmpty()
                            from dr in context.LBC_DIETARY_RESTRICTIONS.Where(dr => dr.DIETARY_CODE == pr.DIETARY_RESTRICTION).DefaultIfEmpty()
                            select new RegistrationReportModel
                            {
                                STARS_ID = pr.STARS_ID,
                                PA_CODE = pr.PA_CODE,
                                WSLX_ID = pr.WSLX_ID,
                                FIRST_NAME = pr.FIRST_NAME,
                                LAST_NAME = pr.LAST_NAME,
                                BADGE_NAME = pr.BADGE_NAME,
                                TITLE = pr.TITLE,
                                EMAIL_ID = pr.EMAIL_ID,
                                DLR_PHONE = pr.DLR_PHONE,
                                PHONE = pr.PHONE,
                                DLR_NAME = pr.DLR_NAME,
                                DLR_ADDRESS = pr.DLR_ADDRESS,
                                DLR_CITY = pr.DLR_CITY,
                                DLR_STATE = pr.DLR_STATE,
                                DLR_ZIP = pr.DLR_ZIP,
                                ATTENDED = rg.ATTENDED,
                                REGD_STATUS = rg.REGD_STATUS,
                                EVENT = _ev.EVENT_SESSION,
                                EVENT_ID = _ev.EVENT_ID,
                                MARKET = pr.DLR_REGION,
                                HOTEL_NAME = ht.HOTEL_NAME,
                                HOTEL_CONF = ht.HOTEL_CONF,
                                HOTEL_CHECK_IN = ht.HOTEL_CHECK_IN,
                                HOTEL_CHECK_OUT = ht.HOTEL_CHECK_OUT,
                                CAR_CONFIRM = ht.CAR_CONFIRM,
                                ADMIN_NOTES = rg.ADMIN_NOTES,
                                CAR_NOTES = ht.CAR_NOTES,
                                TRANSPORT_METHOD = rg.TRANSPORTATION_MODE,
                                // 16 fields added, 2/5/2015 - Van
                                DIETARY_RESTRICTION = dr.DIETARY_DESC,
                                REGISTRATION_NOTES = rg.NOTES,
                                ARR_DATE = tr.ARR_DATE,
                                ARR_AIRLINE = tr.ARR_AIRLINE,
                                ARR_FLIGHT_NUM = tr.ARR_FLIGHT_NUM,
                                ARR_CITY = tr.ARR_CITY,
                                ARR_DEP_CITY = tr.ARR_DEP_CITY,
                                ARR_TIME = tr.ARR_TIME,
                                ARR_TERMINAL = tr.ARR_TERMINAL,
                                CONNECTING_FLIGHT_NOTES = tr.CONNECTING_FLIGHT_NOTES,
                                DEP_DATE = tr.DEP_DATE,
                                DEP_AIRLINE = tr.DEP_AIRLINE,
                                DEP_FLIGHT_NUM = tr.DEP_FLIGHT_NUM,
                                DEP_CITY = tr.DEP_CITY,
                                DEP_DEST_CITY = tr.DEP_DEST_CITY,
                                DEP_TIME = tr.DEP_TIME,
                                CREATED_DATE = rg.CREATED_DATE,
                                // 3 fields added, 2/12/2015
                                ZONE = pr.ZONE,
                                SALESCODE = pr.SALESCODE,
                                DEPT_INFO = pr.DEPT_INFO,
                                //TODO: add ref to pr.Dealer_Type
                                DEALER_TYPE = pr.SELECT_CONTACT_DLR
                            }).ToList<RegistrationReportModel>();

                List<RegistrationReportModel> result = new List<RegistrationReportModel>();

                if (StarsIdList != null && EventIdList != null)
                {
                    for (int i = 0; i < StarsIdList.Count(); i++)
                    {
                        var entry = lst.Where(q => q.STARS_ID == StarsIdList[i] && q.EVENT_ID == EventIdList[i]).FirstOrDefault();
                        if (entry != null)
                        {
                            result.Add(entry);
                        }
                    }
                }
                else
                {
                    result = lst;
                }
                    
                return result;
                }
        }

        public List<RegistrationReportModel> getActiveByStarsEventList(List<string> StarsIdList = null, List<decimal?> EventIdList = null)
        {
            return getRegistrationByStarsEventList(StarsIdList, EventIdList).Where(q => q.REGD_STATUS == "A").ToList();
        }

        public List<RegistrationReportModel> getActiveByMarketEvent(string market, decimal _event)
        {
            return getRegistrationByMarketEvent(market, _event).Where(q => q.REGD_STATUS == "A").ToList();
        }

        public List<RegistrationReportModel> getCancelledByMarketEvent(string market, decimal _event)
        {
            return getRegistrationByMarketEvent(market, _event).Where(q => q.REGD_STATUS == "C").ToList();
        }

        /// <summary>
        /// Get the list of Registration Attendance Items by market/Event
        /// </summary>
        /// <param name="market"></param>
        /// <param name="_event"></param>
        /// <returns></returns>
        public List<AttendanceReportModel> getAttendanceByMarketEvent(string market, decimal _event)
        {
            List<AttendanceReportModel> list = new List<AttendanceReportModel>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {

                if (!String.IsNullOrWhiteSpace(market) && _event == 0)
                {
                    var lst = from rg in context.LBC_REGISTRATION
                              join _ev in context.LBC_EVENT on rg.EVENT_ID equals _ev.EVENT_ID
                              join pr in context.LBC_PROFILE on rg.STARS_ID equals pr.STARS_ID
                              //from ht in context.LBC_HOTEL_CAR_INFO.Where(ht => ht.STARS_ID == rg.STARS_ID).DefaultIfEmpty()
                              where pr.DLR_REGION == market && rg.REGD_STATUS == "A"
                              select new AttendanceReportModel
                              {
                                  STARS_ID = pr.STARS_ID,
                                  PA_CODE = pr.PA_CODE,
                                  WSLX_ID = pr.WSLX_ID,
                                  TITLE = pr.TITLE,
                                  EMAIL_ID = pr.EMAIL_ID,
                                  CONTACT = pr.DLR_PHONE,
                                  DLR_NAME = pr.DLR_NAME,
                                  DLR_ADDRESS = pr.DLR_ADDRESS,
                                  DLR_CITY = pr.DLR_CITY,
                                  DLR_STATE = pr.DLR_STATE,
                                  ATTENDED = rg.ATTENDED,
                                  EVENT = _ev.EVENT_SESSION,
                                  EVENT_ID = _ev.EVENT_ID,
                                  MARKET = pr.DLR_REGION,
                                  PARTICIPANT_COMPANY ="Ma",
                                  PARTICIPANT_ROLE ="GUEST",
                                  REGISTERED_PARTICIPANT = pr.FIRST_NAME + " " + pr.LAST_NAME,
                                  FIRST_NAME = pr.FIRST_NAME,
                                  LAST_NAME = pr.LAST_NAME,
                                  DEPT_INFO = pr.DEPT_INFO




                              };
                    return lst.ToList<AttendanceReportModel>();
                }

                else if (String.IsNullOrWhiteSpace(market) && _event != 0)
                {
                    var lst = from rg in context.LBC_REGISTRATION
                              join _ev in context.LBC_EVENT on rg.EVENT_ID equals _ev.EVENT_ID
                              join pr in context.LBC_PROFILE on rg.STARS_ID equals pr.STARS_ID
                              //from ht in context.LBC_HOTEL_CAR_INFO.Where(ht => ht.STARS_ID == rg.STARS_ID).DefaultIfEmpty()
                              where  rg.EVENT_ID == _event && rg.REGD_STATUS == "A"
                              select new AttendanceReportModel
                              {
                                  STARS_ID = pr.STARS_ID,
                                  PA_CODE = pr.PA_CODE,
                                  WSLX_ID = pr.WSLX_ID,
                                  TITLE = pr.TITLE,
                                  EMAIL_ID = pr.EMAIL_ID,
                                  CONTACT = pr.DLR_PHONE,
                                  DLR_NAME = pr.DLR_NAME,
                                  DLR_ADDRESS = pr.DLR_ADDRESS,
                                  DLR_CITY = pr.DLR_CITY,
                                  DLR_STATE = pr.DLR_STATE,
                                  ATTENDED = rg.ATTENDED,
                                  EVENT = _ev.EVENT_SESSION,
                                  EVENT_ID = _ev.EVENT_ID,
                                  MARKET = pr.DLR_REGION,
                                  PARTICIPANT_COMPANY = "Ma",
                                  PARTICIPANT_ROLE = "GUEST",
                                  REGISTERED_PARTICIPANT = pr.FIRST_NAME + " " + pr.LAST_NAME,
                                  FIRST_NAME = pr.FIRST_NAME,
                                  LAST_NAME = pr.LAST_NAME,
                                  DEPT_INFO = pr.DEPT_INFO



                              };
                    return lst.ToList<AttendanceReportModel>();
                }
                else if (!String.IsNullOrWhiteSpace(market) && _event != 0)
                {
                    var lst = from rg in context.LBC_REGISTRATION
                              join _ev in context.LBC_EVENT on rg.EVENT_ID equals _ev.EVENT_ID
                              join pr in context.LBC_PROFILE on rg.STARS_ID equals pr.STARS_ID
                              //from ht in context.LBC_HOTEL_CAR_INFO.Where(ht => ht.STARS_ID == rg.STARS_ID).DefaultIfEmpty()
                              where pr.DLR_REGION == market && rg.EVENT_ID == _event && rg.REGD_STATUS == "A"
                              select new AttendanceReportModel
                              {
                                  STARS_ID = pr.STARS_ID,
                                  PA_CODE = pr.PA_CODE,
                                  WSLX_ID = pr.WSLX_ID,
                                  TITLE = pr.TITLE,
                                  EMAIL_ID = pr.EMAIL_ID,
                                  CONTACT = pr.DLR_PHONE,
                                  DLR_NAME = pr.DLR_NAME,
                                  DLR_ADDRESS = pr.DLR_ADDRESS,
                                  DLR_CITY = pr.DLR_CITY,
                                  DLR_STATE = pr.DLR_STATE,
                                  ATTENDED = rg.ATTENDED,
                                  EVENT = _ev.EVENT_SESSION,
                                  EVENT_ID = _ev.EVENT_ID,
                                  MARKET = pr.DLR_REGION,
                                  PARTICIPANT_COMPANY = "Ma",
                                  PARTICIPANT_ROLE = "GUEST",
                                  REGISTERED_PARTICIPANT = pr.FIRST_NAME + " " + pr.LAST_NAME,
                                  FIRST_NAME = pr.FIRST_NAME,
                                  LAST_NAME = pr.LAST_NAME,
                                  DEPT_INFO = pr.DEPT_INFO



                              };
                    return lst.ToList<AttendanceReportModel>();
                }

                else
                {
                    var lst = from rg in context.LBC_REGISTRATION
                              join _ev in context.LBC_EVENT on rg.EVENT_ID equals _ev.EVENT_ID
                              join pr in context.LBC_PROFILE on rg.STARS_ID equals pr.STARS_ID
                              where rg.REGD_STATUS =="A"
                              //from ht in context.LBC_HOTEL_CAR_INFO.Where(ht => ht.STARS_ID == rg.STARS_ID).DefaultIfEmpty()

                              select new AttendanceReportModel
                              {
                                  STARS_ID = pr.STARS_ID,
                                  PA_CODE = pr.PA_CODE,
                                  WSLX_ID = pr.WSLX_ID,
                                  TITLE = pr.TITLE,
                                  EMAIL_ID = pr.EMAIL_ID,
                                  CONTACT = pr.DLR_PHONE,
                                  DLR_NAME = pr.DLR_NAME,
                                  DLR_ADDRESS = pr.DLR_ADDRESS,
                                  DLR_CITY = pr.DLR_CITY,
                                  DLR_STATE = pr.DLR_STATE,
                                  ATTENDED = rg.ATTENDED,
                                  EVENT = _ev.EVENT_SESSION,
                                  EVENT_ID = _ev.EVENT_ID,
                                  MARKET = pr.DLR_REGION,
                                  PARTICIPANT_COMPANY = "Ma",
                                  PARTICIPANT_ROLE = "GUEST",
                                  REGISTERED_PARTICIPANT = pr.FIRST_NAME + " " + pr.LAST_NAME,
                                  FIRST_NAME = pr.FIRST_NAME,
                                  LAST_NAME = pr.LAST_NAME,
                                  DEPT_INFO = pr.DEPT_INFO




                              };
                    return lst.ToList<AttendanceReportModel>();
                }

                //return lst.ToList<RegistrationReportModel>();
            }
        }
        /// <summary>
        /// Get the list of registration items by Stars/Event Id
        /// /// </summary>
        /// <param name="StarsId"></param>
        /// <param name="EventId"></param>
        /// <returns></returns>
        public RegistrationReportModel getRegistrationByStarsEvent(string StarsId, decimal? EventId)
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                return (from rg in context.LBC_REGISTRATION
                          join _ev in context.LBC_EVENT on rg.EVENT_ID equals _ev.EVENT_ID
                          join pr in context.LBC_PROFILE on rg.STARS_ID equals pr.STARS_ID
                          from tr in context.LBC_FLIGHT_INFO.Where(tr => tr.STARS_ID == rg.STARS_ID && tr.EVENT_ID == rg.EVENT_ID).DefaultIfEmpty()
                          from ht in context.LBC_HOTEL_CAR_INFO.Where(ht => ht.STARS_ID == rg.STARS_ID).DefaultIfEmpty()
                          from dr in context.LBC_DIETARY_RESTRICTIONS.Where(dr => dr.DIETARY_CODE == pr.DIETARY_RESTRICTION).DefaultIfEmpty()
                          where rg.STARS_ID == StarsId && rg.EVENT_ID == EventId
                          select new RegistrationReportModel
                          {
                              STARS_ID = pr.STARS_ID,
                              PA_CODE = pr.PA_CODE,
                              WSLX_ID = pr.WSLX_ID,
                              FIRST_NAME = pr.FIRST_NAME,
                              LAST_NAME = pr.LAST_NAME,
                              BADGE_NAME = pr.BADGE_NAME,
                              TITLE = pr.TITLE,
                              EMAIL_ID = pr.EMAIL_ID,
                              DLR_PHONE = pr.DLR_PHONE,
                              PHONE = pr.PHONE,
                              DLR_NAME = pr.DLR_NAME,
                              DLR_ADDRESS = pr.DLR_ADDRESS,
                              DLR_CITY = pr.DLR_CITY,
                              DLR_STATE = pr.DLR_STATE,
                              DLR_ZIP = pr.DLR_ZIP,
                              ATTENDED = rg.ATTENDED,
                              REGD_STATUS = rg.REGD_STATUS,
                              EVENT = _ev.EVENT_SESSION,
                              EVENT_ID = rg.EVENT_ID,
                              MARKET = pr.DLR_REGION,
                              HOTEL_NAME = ht.HOTEL_NAME,
                              HOTEL_CONF = ht.HOTEL_CONF,
                              HOTEL_CHECK_IN = ht.HOTEL_CHECK_IN,
                              HOTEL_CHECK_OUT = ht.HOTEL_CHECK_OUT,
                              CAR_CONFIRM = ht.CAR_CONFIRM,
                              ADMIN_NOTES = rg.ADMIN_NOTES,
                              CAR_NOTES = ht.CAR_NOTES,
                              TRANSPORT_METHOD = rg.TRANSPORTATION_MODE,
                              // 16 fields added, 2/5/2015 - Van
                              DIETARY_RESTRICTION = dr.DIETARY_DESC,
                              REGISTRATION_NOTES = rg.NOTES,
                              ARR_DATE = tr.ARR_DATE,
                              ARR_AIRLINE = tr.ARR_AIRLINE,
                              ARR_FLIGHT_NUM = tr.ARR_FLIGHT_NUM,
                              ARR_CITY = tr.ARR_CITY,
                              ARR_DEP_CITY = tr.ARR_DEP_CITY,
                              ARR_TIME = tr.ARR_TIME,
                              ARR_TERMINAL = tr.ARR_TERMINAL,
                              CONNECTING_FLIGHT_NOTES = tr.CONNECTING_FLIGHT_NOTES,
                              DEP_DATE = tr.DEP_DATE,
                              DEP_AIRLINE = tr.DEP_AIRLINE,
                              DEP_FLIGHT_NUM = tr.DEP_FLIGHT_NUM,
                              DEP_CITY = tr.DEP_CITY,
                              DEP_DEST_CITY = tr.DEP_DEST_CITY,
                              DEP_TIME = tr.DEP_TIME,
                              CREATED_DATE = rg.CREATED_DATE,
                              // 3 fields added, 2/12/2015
                              ZONE = pr.ZONE,
                              SALESCODE = pr.SALESCODE,
                              DEPT_INFO = pr.DEPT_INFO,
                              // 2 fields added, 2/06/2015
                              UPDATE_DATE = rg.UPDATE_DATE,
                              TRANSPORTATION_NEED = rg.TRANSPORTATION_NEED,
                              //TODO: add ref to pr.Dealer_Type
                              DEALER_TYPE = pr.SELECT_CONTACT_DLR
                          }).FirstOrDefault();
            }
        }
        /// <summary>
        /// Get the list of cancelled registration items by Stars/Event
        /// /// </summary>
        /// <param name="StarsIdList"></param>
        /// <param name="EventIdList"></param>
        /// <returns></returns>
        public List<RegistrationReportModel> getCancelledRegistration(List<string> StarsIdList = null, List<decimal?> EventIdList = null)
        {
            return getRegistrationByStarsEventList(StarsIdList, EventIdList).Where(q => q.REGD_STATUS == "C").ToList();
        }
    }
}
