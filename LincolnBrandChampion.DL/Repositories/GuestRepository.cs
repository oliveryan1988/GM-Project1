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
    public class GuestRepository
    {
        /// <summary>
        /// Checks to see if guest registration exists.
        /// </summary>
        /// <param name="wslxId"></param>
        /// <returns>bool registered</returns>
        public bool CheckGuestRegistrationBy(string wslxId)
        {
            bool registered = false;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _guestRegistered = (from p in context.LBC_GUEST_REGISTRATION
                                        where p.WSLX_ID == wslxId
                                        select p).FirstOrDefault();

                if (_guestRegistered != null)
                {
                    registered = true;
                }

            }
            return registered;
        }
        /// <summary>
        /// Checks to see if guest session exists.
        /// </summary>
        /// <param name="wslxId"></param>
        /// <returns>bool registered</returns>
        public bool CheckGuestSessionBy(string wslxId)
        {
            bool registered = false;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _guestRegistered = (from p in context.LBC_GUEST_REGD_SESSION
                                        where p.WSLX_ID == wslxId
                                        select p).FirstOrDefault();

                if (_guestRegistered != null)
                {
                    registered = true;
                }

            }
            return registered;
        }
        /// <summary>
        /// Method to retrieve GuestRegistration
        /// </summary>
        /// <param name="wslxId"></param>
        /// <returns>GuestRegistrationModel</returns>
        public GuestRegistrationModel GetGuestRegistrationBy(string wslxId)
        {
            GuestRegistrationModel model = new GuestRegistrationModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _registration = from r in context.LBC_GUEST_REGISTRATION
                                    where r.WSLX_ID == wslxId
                                    select new GuestRegistrationModel
                                    {
                                        WSLX_ID = r.WSLX_ID,
                                        FIRST_NAME = r.FIRST_NAME,
                                        LAST_NAME = r.LAST_NAME,
                                        COMPANY_NAME = r.COMPANY_NAME,
                                        DEPARTMENT = r.DEPARTMENT,
                                        TITLE = r.TITLE,
                                        EMAIL_ID = r.EMAIL_ID,
                                        PHONE = r.PHONE,
                                        DIETARY_RESTRICTION = r.DIETARY_RESTRICTION,
                                        PROFILE_NOTE = r.PROFILE_NOTE,
                                        HOTEL_REQUIRED = r.HOTEL_REQUIRED

                                    };

                model = _registration.SingleOrDefault();
            }
            return model;
        }

        /// <summary>
        /// Method to return a list of guest registrations
        /// </summary>
        /// <param name="wslxId"></param>
        /// <returns>list guestRegistration</returns>
        public List<GuestRegistrationModel> GetGuestRegListBy(string wslxId)
        {
            List<GuestRegistrationModel> list = new List<GuestRegistrationModel>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from r in context.LBC_GUEST_REGISTRATION
                          //from s in context.LBC_GUEST_REGD_SESSION.Where(s => s.WSLX_ID == r.WSLX_ID).DefaultIfEmpty()
                          where r.WSLX_ID == wslxId
                          select new GuestRegistrationModel
                          {
                              WSLX_ID = r.WSLX_ID,
                              FIRST_NAME = r.FIRST_NAME,
                              LAST_NAME = r.LAST_NAME,
                              COMPANY_NAME = r.COMPANY_NAME,
                              DEPARTMENT = r.DEPARTMENT,
                              TITLE = r.TITLE,
                              EMAIL_ID = r.EMAIL_ID,
                              PHONE = r.PHONE,
                              DIETARY_RESTRICTION = r.DIETARY_RESTRICTION,
                              PROFILE_NOTE = r.PROFILE_NOTE,
                              HOTEL_REQUIRED = r.HOTEL_REQUIRED

                          };

                return lst.ToList<GuestRegistrationModel>();
            }
        }

        /// <summary>
        /// Insert the guest registration record
        /// </summary>
        /// <param name="model"></param>
        public void SaveGuestRegistration(GuestRegistrationModel model)
        {

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                LBC_GUEST_REGISTRATION _registration = new LBC_GUEST_REGISTRATION()
                {
                    WSLX_ID = model.WSLX_ID,
                    FIRST_NAME = model.FIRST_NAME,
                    LAST_NAME = model.LAST_NAME,
                    COMPANY_NAME = model.COMPANY_NAME,
                    DEPARTMENT = model.DEPARTMENT,
                    TITLE = model.TITLE,
                    EMAIL_ID = model.EMAIL_ID,
                    PHONE = model.PHONE,
                    DIETARY_RESTRICTION = model.DIETARY_RESTRICTION,
                    PROFILE_NOTE = model.PROFILE_NOTE,
                    HOTEL_REQUIRED = model.HOTEL_REQUIRED,
                    CREATED_BY = model.CREATED_BY,
                    CREATED_DATE = model.CREATED_DATE
                };

                context.LBC_GUEST_REGISTRATION.Add(_registration);
                context.SaveChanges();


            }
        }

        /// <summary>
        /// Method to update existing guest registration.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>save</returns>
        public bool UpdateGuestRegistration(GuestRegistrationModel model)
        {
            bool save = true;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _registration = (from p in context.LBC_GUEST_REGISTRATION
                                         where p.WSLX_ID == model.WSLX_ID
                                         select p).FirstOrDefault();

                    _registration.FIRST_NAME = model.FIRST_NAME;
                    _registration.LAST_NAME = model.LAST_NAME;
                    _registration.COMPANY_NAME = model.COMPANY_NAME;
                    _registration.DEPARTMENT = model.DEPARTMENT;
                    _registration.TITLE = model.TITLE;
                    _registration.EMAIL_ID = model.EMAIL_ID;
                    _registration.PHONE = model.PHONE;
                    _registration.DIETARY_RESTRICTION = model.DIETARY_RESTRICTION;
                    _registration.PROFILE_NOTE = model.PROFILE_NOTE;
                    _registration.HOTEL_REQUIRED = model.HOTEL_REQUIRED;
                    _registration.UPDATED_BY = model.UPDATED_BY;
                    _registration.UPDATE_DATE = model.UPDATE_DATE;

                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw ex;
                // save = false;
            }
            return save;
        }

        /// <summary>
        /// Mapping the Model to the Data layer
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>GuestRegistrationModel</returns>
        private GuestRegistrationModel MapModelFromLBC_GUEST_REGISTRATION(GuestRegistrationModel entity)
        {
            GuestRegistrationModel model = new GuestRegistrationModel();
            if (entity != null)
            {
                model.WSLX_ID = entity.WSLX_ID;
                model.FIRST_NAME = entity.FIRST_NAME;
                model.LAST_NAME = entity.LAST_NAME;
                model.COMPANY_NAME = entity.COMPANY_NAME;
                model.TITLE = entity.TITLE;
                model.EMAIL_ID = entity.EMAIL_ID;
                model.PHONE = entity.PHONE;
                model.DIETARY_RESTRICTION = entity.DIETARY_RESTRICTION;
                model.PROFILE_NOTE = entity.PROFILE_NOTE;
                model.HOTEL_REQUIRED = entity.HOTEL_REQUIRED;
                model.CREATED_BY = entity.CREATED_BY;
                model.CREATED_DATE = entity.CREATED_DATE;
                model.UPDATED_BY = entity.UPDATED_BY;
                model.UPDATE_DATE = entity.UPDATE_DATE;
                model.DEPARTMENT = entity.DEPARTMENT;

            }

            return model;
        }

        public List<GuestSessionLookupModel> GetAll(List<decimal> eventids)
        {
            List<GuestSessionLookupModel> list = new List<GuestSessionLookupModel>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from ev in context.LBC_EVENT
                          join esl in context.LBC_EVENT_SESSION_LOOKUP on ev.EVENT_ID equals esl.EVENT_ID
                           where eventids.Contains(ev.EVENT_ID)//ev.EVENT_ID.ToString().Contains(eventids)
                          select new GuestSessionLookupModel
                          {
                              EVENT_ID = esl.EVENT_ID,
                              //LBC_EVENT = MapModelFromLBC_EVENT(LBC_EVENT),
                              SESSION_DATE = esl.SESSION_DATE,
                              SESSION_DESC = esl.SESSION_DESC,
                              SESSION_ID = esl.SESSION_ID,
                              EVENT_MONTH = ev.EVENT_MONTH,
                              EVENT_LOCATION = ev.EVENT_LOCATION,
                              EVENT_SESSION = ev.EVENT_SESSION,
                              EVENT_YEAR = ev.EVENT_YEAR


                          };

                return lst.ToList<GuestSessionLookupModel>();
            }
        }
        public bool removeSesssionRegistered(List<decimal> SessionId, string wslxid)
        {
            bool flag = false;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                List<LBC_GUEST_REGD_SESSION> _sesssion = (from p in context.LBC_GUEST_REGD_SESSION
                                                          where p.WSLX_ID == wslxid && SessionId.Contains(p.SESSION_ID)
                                                          select p).ToList<LBC_GUEST_REGD_SESSION>();

                if (_sesssion.Count() > 0)
                {
                    try
                    {
                        foreach (LBC_GUEST_REGD_SESSION item in _sesssion)
                        {
                            context.LBC_GUEST_REGD_SESSION.Remove(item);
                            context.SaveChanges();
                            flag = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        flag = false;
                    }
                }
                else
                { flag = true; }
            }
            return flag;


        }

        public List<GuestSessionLookupModel> GetByWSLX(string wslxId)
        {
            List<GuestSessionLookupModel> list = new List<GuestSessionLookupModel>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from grs in context.LBC_GUEST_REGD_SESSION
                          join esl in context.LBC_EVENT_SESSION_LOOKUP on grs.SESSION_ID equals esl.SESSION_ID
                          join _ev in context.LBC_EVENT on esl.EVENT_ID equals _ev.EVENT_ID
                          where grs.WSLX_ID == wslxId
                          select new GuestSessionLookupModel
                          {
                              EVENT_ID = esl.EVENT_ID,
                             // LBC_EVENT = MapModelFromLBC_EVENT(esl.LBC_EVENT),
                              SESSION_DATE = esl.SESSION_DATE,
                              SESSION_DESC = esl.SESSION_DESC,
                              SESSION_ID = esl.SESSION_ID,
                              WSLX_ID = grs.WSLX_ID


                          };

                return lst.ToList<GuestSessionLookupModel>();
            }
        }

        public List<decimal> GetSessionByWSLX(string wslxId)
        {
            List<decimal> list = new List<decimal>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = (from grs in context.LBC_GUEST_REGD_SESSION
                           join esl in context.LBC_EVENT_SESSION_LOOKUP on grs.SESSION_ID equals esl.SESSION_ID
                           join _ev in context.LBC_EVENT on esl.EVENT_ID equals _ev.EVENT_ID
                           where grs.WSLX_ID == wslxId
                           select esl.SESSION_ID);

                return lst.ToList<decimal>();
            }
        }

        public List<GuestSessionLookupModel> GetAllby(List<decimal> eventids, string wslxId)
        {
            List<GuestSessionLookupModel> list = new List<GuestSessionLookupModel>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from ev in context.LBC_EVENT
                          join esl in context.LBC_EVENT_SESSION_LOOKUP on ev.EVENT_ID equals esl.EVENT_ID
                          join grs in context.LBC_GUEST_REGD_SESSION on esl.SESSION_ID equals grs.SESSION_ID
                          where eventids.Contains(ev.EVENT_ID) && wslxId.Contains(grs.WSLX_ID)
                          select new GuestSessionLookupModel
                          {
                              EVENT_ID = esl.EVENT_ID,
                              SESSION_DATE = esl.SESSION_DATE,
                              SESSION_DESC = esl.SESSION_DESC,
                              SESSION_ID = esl.SESSION_ID,
                              EVENT_MONTH = ev.EVENT_MONTH,
                              EVENT_LOCATION = ev.EVENT_LOCATION,
                              EVENT_SESSION = ev.EVENT_SESSION,
                              EVENT_YEAR = ev.EVENT_YEAR,
                              REGISTERED_SESSION = grs.SESSION_ID


                          };

                return lst.ToList<GuestSessionLookupModel>();
            }
        }
        private EventModel MapModelFromLBC_EVENT(LBC_EVENT _event)
        {
            EventModel model = new EventModel();
            if (_event != null)
            {
                model.EVENT_ID = _event.EVENT_ID;
                model.EVENT_MONTH = _event.EVENT_MONTH;
                model.EVENT_LOCATION = _event.EVENT_LOCATION;
                model.EVENT_SESSION = _event.EVENT_SESSION;
                model.EVENT_YEAR = _event.EVENT_YEAR;
                model.EVENT_START_DATE = _event.EVENT_START_DATE;
                model.EVENT_END_DATE = _event.EVENT_END_DATE;


            }
            return model;
        }
        public void SaveGuestRegistrationSession(GuestSessionModel model)
        {

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                LBC_GUEST_REGD_SESSION _registration = new LBC_GUEST_REGD_SESSION()
                {
                    WSLX_ID = model.WSLX_ID,
                    SESSION_ID = model.SESSION_ID,
                    CREATED_BY = model.CREATED_BY,
                    CREATED_DATE = model.CREATED_DATE
                };

                context.LBC_GUEST_REGD_SESSION.Add(_registration);
                context.SaveChanges();


            }
        }
        /// <summary>
        /// Get a list of guest registrations, and filter by event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>List of GuestRegistrationModels</returns>
        public List<GuestRegistrationModel> GetListGuestAndRegistrationBy(decimal eventId)
        {
            
            List<GuestRegistrationModel> lst = new List<GuestRegistrationModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                if (eventId != null && eventId != 0)
                {
                    var _id = Convert.ToInt32(eventId);
                    var list = (from gr in context.LBC_GUEST_REGISTRATION
                                join rs in context.LBC_GUEST_REGD_SESSION on gr.WSLX_ID equals rs.WSLX_ID
                                join esl in context.LBC_EVENT_SESSION_LOOKUP on rs.SESSION_ID equals esl.SESSION_ID
                                join evt in context.LBC_EVENT on esl.EVENT_ID equals evt.EVENT_ID
                                from dr in context.LBC_DIETARY_RESTRICTIONS.Where(dr => dr.DIETARY_CODE == gr.DIETARY_RESTRICTION).DefaultIfEmpty()
                                where  esl.EVENT_ID == eventId 
                                orderby gr.WSLX_ID
                                select new GuestRegistrationModel
                          {
                              WSLX_ID = gr.WSLX_ID,
                              EVENT_ID = esl.EVENT_ID,
                            FIRST_NAME = gr.FIRST_NAME,
                            LAST_NAME = gr.LAST_NAME,
                            COMPANY_NAME = gr.COMPANY_NAME,
                            TITLE = gr.TITLE,
                            EMAIL_ID = gr.EMAIL_ID,
                            PHONE = gr.PHONE,
                            DIETARY_RESTRICTION = dr.DIETARY_DESC,
                            PROFILE_NOTE = gr.PROFILE_NOTE,
                            HOTEL_REQUIRED = gr.HOTEL_REQUIRED,
                            CREATED_BY = gr.CREATED_BY,
                            CREATED_DATE = rs.CREATED_DATE,
                            UPDATED_BY = gr.UPDATED_BY,
                            UPDATE_DATE = gr.UPDATE_DATE,
                            DEPARTMENT = gr.DEPARTMENT,
                            SSSION_ID = rs.SESSION_ID,
                            Session_Name = esl.SESSION_DESC,
                            WAVE = evt.EVENT_SESSION


                          }).ToList();

                    lst = list;
                    }
                
                else
                {
                    var list = (from gr in context.LBC_GUEST_REGISTRATION
                                join rs in context.LBC_GUEST_REGD_SESSION on gr.WSLX_ID equals rs.WSLX_ID
                                join esl in context.LBC_EVENT_SESSION_LOOKUP on rs.SESSION_ID equals esl.SESSION_ID
                                join evt in context.LBC_EVENT on esl.EVENT_ID equals evt.EVENT_ID
                                from dr in context.LBC_DIETARY_RESTRICTIONS.Where(dr => dr.DIETARY_CODE == gr.DIETARY_RESTRICTION).DefaultIfEmpty()
                                orderby gr.WSLX_ID
                                //where gr.WSLX_ID == rs.WSLX_ID 
                                select new GuestRegistrationModel
                          {
                              WSLX_ID = gr.WSLX_ID,
                              EVENT_ID = esl.EVENT_ID,
                            FIRST_NAME = gr.FIRST_NAME,
                            LAST_NAME = gr.LAST_NAME,
                            COMPANY_NAME = gr.COMPANY_NAME,
                            TITLE = gr.TITLE,
                            EMAIL_ID = gr.EMAIL_ID,
                            PHONE = gr.PHONE,
                            DIETARY_RESTRICTION = dr.DIETARY_DESC,
                            PROFILE_NOTE = gr.PROFILE_NOTE,
                            HOTEL_REQUIRED = gr.HOTEL_REQUIRED,
                            CREATED_BY = gr.CREATED_BY,
                            CREATED_DATE = rs.CREATED_DATE,
                            UPDATED_BY = gr.UPDATED_BY,
                            UPDATE_DATE = gr.UPDATE_DATE,
                            DEPARTMENT = gr.DEPARTMENT,
                            SSSION_ID = rs.SESSION_ID,
                            Session_Name = esl.SESSION_DESC,
                            WAVE = evt.EVENT_SESSION


                          }).ToList();

                    lst = list;
                    }
                

            }

            //foreach (GuestRegistrationModel item in lst)
            //{
            //    GuestRepository _recognition = new GuestRepository();
            //    item.registrationList = _recognition.GetAll(item.WSLX_ID);
            //}

            return lst;
        }

        /// <summary>
        /// Get a list of guest registrations, and filter by wslx/event
        /// </summary>
        /// <param name="WslxList"></param>
        /// <param name="EventIdList"></param>
        /// <returns>List of GuestRegistrationModels</returns>
        public List<GuestRegistrationModel> GetListGuestAndRegistrationByWslxEvent(List<string> WslxList = null, List<decimal> EventIdList = null)
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                List<GuestRegistrationModel> lst = (from gr in context.LBC_GUEST_REGISTRATION
                            join rs in context.LBC_GUEST_REGD_SESSION on gr.WSLX_ID equals rs.WSLX_ID
                            join esl in context.LBC_EVENT_SESSION_LOOKUP on rs.SESSION_ID equals esl.SESSION_ID
                            join evt in context.LBC_EVENT on esl.EVENT_ID equals evt.EVENT_ID
                            from dr in context.LBC_DIETARY_RESTRICTIONS.Where(dr => dr.DIETARY_CODE == gr.DIETARY_RESTRICTION).DefaultIfEmpty()
                            orderby gr.WSLX_ID
                                select new GuestRegistrationModel
                                {
                                    WSLX_ID = gr.WSLX_ID,
                                    EVENT_ID = esl.EVENT_ID,
                                    FIRST_NAME = gr.FIRST_NAME,
                                    LAST_NAME = gr.LAST_NAME,
                                    COMPANY_NAME = gr.COMPANY_NAME,
                                    TITLE = gr.TITLE,
                                    EMAIL_ID = gr.EMAIL_ID,
                                    PHONE = gr.PHONE,
                                    DIETARY_RESTRICTION = dr.DIETARY_DESC,
                                    PROFILE_NOTE = gr.PROFILE_NOTE,
                                    HOTEL_REQUIRED = gr.HOTEL_REQUIRED,
                                    CREATED_BY = gr.CREATED_BY,
                                    CREATED_DATE = rs.CREATED_DATE,
                                    UPDATED_BY = gr.UPDATED_BY,
                                    UPDATE_DATE = gr.UPDATE_DATE,
                                    DEPARTMENT = gr.DEPARTMENT,
                                    SSSION_ID = rs.SESSION_ID,
                                    Session_Name = esl.SESSION_DESC,
                                    WAVE = evt.EVENT_SESSION


                                }).ToList();

                    List<GuestRegistrationModel> result = new List<GuestRegistrationModel>();

                    if (WslxList != null && EventIdList != null)
                    {
                        for (int i = 0; i < WslxList.Count(); i++)
                        {
                            var entry = lst.Where(q => q.WSLX_ID == WslxList[i] && q.EVENT_ID == EventIdList[i]).FirstOrDefault();
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
        /// <summary>
        /// Method for returning list of all guest sessions by WSLXID
        /// </summary>
        /// <param name="wslxId"></param>
        /// <returns>List of guest session models</returns>
        public List<GuestSessionModel> GetAll(string wslxId)
        {
            int count = 0;
            List<GuestSessionModel> list = new List<GuestSessionModel>();


            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = (from entity in context.LBC_EVENT_SESSION_LOOKUP
                           select entity).ToList();

                var lst2 = (from esl in context.LBC_GUEST_REGD_SESSION
                            join grs in context.LBC_EVENT_SESSION_LOOKUP on esl.SESSION_ID equals grs.SESSION_ID
                            where esl.WSLX_ID == wslxId 
                            select esl).ToList();


                if (lst2.Count > 0)
                {
                    foreach (LBC_EVENT_SESSION_LOOKUP item in lst)
                    {
                        list.Add(new GuestSessionModel()
                        {
                            SESSION_ID = item.SESSION_ID,
                            CREATED_DATE = item.CREATED_DATE
                        });
                        count = count + 1;
                    }
                }
                else
                {
                    foreach (LBC_EVENT_SESSION_LOOKUP item in lst)
                    {
                        list.Add(new GuestSessionModel()
                        {
                            SESSION_ID = item.SESSION_ID,
                            CREATED_DATE = item.CREATED_DATE
                        });
                    }
                }
            }
            return list;
        }
    }
}
