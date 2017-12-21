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
    public class RegistrationRepository
    {
        public bool CheckRegistrationBy(string starsId)
        {
            bool haveStarsId = false;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _profile = (from p in context.LBC_REGISTRATION
                                where p.STARS_ID == starsId && p.REGD_STATUS == "A"
                                select p).FirstOrDefault();

                if (_profile != null)
                {
                    haveStarsId = true;
                }

            }
            return haveStarsId;

        }
        /// <summary>
        /// Get the list of registration items by starz id
        /// </summary>
        /// <param name="StarsId"></param>
        /// <returns></returns>
        public List<RegistrationModel> GetRegByStarsId(string StarsId)
        {
            List<RegistrationModel> list = new List<RegistrationModel>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from pr in context.LBC_PROFILE
                          from reg  in context.LBC_REGISTRATION.Where(reg => reg.STARS_ID== pr.STARS_ID).DefaultIfEmpty()
                          where pr.STARS_ID == StarsId 
                          select new RegistrationModel
                          {
                              STARS_ID = pr.STARS_ID,
                              PA_CODE = pr.PA_CODE,
                              WSLX_ID = pr.WSLX_ID,
                              FIRST_NAME = pr.FIRST_NAME,
                              LAST_NAME = pr.LAST_NAME,
                              TITLE = pr.TITLE,
                              EMAIL_ID = pr.EMAIL_ID,
                              DLR_PHONE = pr.DLR_PHONE,
                              PHONE = pr.PHONE,
                              DLR_NAME= pr.DLR_NAME,
                              DLR_ADDRESS = pr.DLR_ADDRESS,
                              DLR_CITY = pr.DLR_CITY,
                              DLR_STATE = pr.DLR_STATE,
                              DLR_ZIP = pr.DLR_ZIP,
                              DIETARY_RESTRICTION= pr.DIETARY_RESTRICTION,
                              ATTENDED= reg.ATTENDED,
                              TRANSPORTATION_MODE = reg.TRANSPORTATION_MODE,
                              REGD_STATUS = reg.REGD_STATUS



                          };

                return lst.ToList<RegistrationModel>();
            }
        }

        /// <summary>
        /// Method to get the registration by Stars ID.
        /// </summary>
        /// <param name="starsId">StarsId as string </param>
        /// <returns>Registration Model</returns>
        /// 
        public RegistrationModel GetRegistrationByStarsId(string StarsId)
        {
            RegistrationModel model = new RegistrationModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _registration = from pr in context.LBC_PROFILE
                                    from reg in context.LBC_REGISTRATION.Where(reg => reg.STARS_ID == pr.STARS_ID && reg.REGD_STATUS == "A").DefaultIfEmpty()
                                    where pr.STARS_ID == StarsId
                                    select new RegistrationModel
                                    {
                                        STARS_ID = pr.STARS_ID,
                                        PA_CODE = pr.PA_CODE,
                                        WSLX_ID = pr.WSLX_ID,
                                        FIRST_NAME = pr.FIRST_NAME,
                                        LAST_NAME = pr.LAST_NAME,
                                        TITLE = pr.TITLE,
                                        EMAIL_ID = pr.EMAIL_ID,
                                        DLR_PHONE = pr.DLR_PHONE,
                                        PHONE = pr.PHONE,
                                        DLR_NAME = pr.DLR_NAME,
                                        DLR_ADDRESS = pr.DLR_ADDRESS,
                                        DLR_CITY = pr.DLR_CITY,
                                        DLR_STATE = pr.DLR_STATE,
                                        DLR_ZIP = pr.DLR_ZIP,
                                        DIETARY_RESTRICTION = pr.DIETARY_RESTRICTION,
                                        LBC_CERT = pr.LBC_CERT,
                                        ATTENDED = reg.ATTENDED,
                                        TRANSPORTATION_MODE = reg.TRANSPORTATION_MODE,
                                        REGD_STATUS = reg.REGD_STATUS,
                                        EVENT_ID = reg.EVENT_ID == null ? 0 : reg.EVENT_ID,
                                        NOTES=reg.NOTES,
                                        ADMIN_NOTES = reg.ADMIN_NOTES


                                    };

                model = _registration.SingleOrDefault();
            }
            return model;
        }
        public RegistrationModel GetRegistrationByStarsEventId(string StarsId, decimal eventid)
        {
            RegistrationModel model = new RegistrationModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _registration = from pr in context.LBC_PROFILE
                          from reg  in context.LBC_REGISTRATION.Where(reg => reg.STARS_ID== pr.STARS_ID && reg.EVENT_ID == eventid).DefaultIfEmpty()
                          where pr.STARS_ID == StarsId
                        select new RegistrationModel
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
                            DIETARY_RESTRICTION = pr.DIETARY_RESTRICTION,
                            ATTENDED = reg.ATTENDED,
                            TRANSPORTATION_MODE = reg.TRANSPORTATION_MODE,
                            TRANSPORTATION_NEED = reg.TRANSPORTATION_NEED,
                            REGD_STATUS = reg.REGD_STATUS,
                            EVENT_ID = reg.EVENT_ID == null ? 0 : reg.EVENT_ID,
                            EVENT_NAME = reg.LBC_EVENT.EVENT_SESSION,
                            NOTES=reg.NOTES,
                            ADMIN_NOTES = reg.ADMIN_NOTES,
                            PROFILE_TYPE = pr.PROFILE_TYPE,
                            PROFILE_NOTE = pr.PROFILE_NOTE,
                            BIOGRAPHY = pr.BIOGRAPHY,
                            DEPARTMENT = pr.DEPARTMENT,
                            SHIRT_SIZE = pr.SHIRT_SIZE



                        };

                model = _registration.SingleOrDefault();
            }
            return model;
        }
        /// <summary>
        /// Method to Insert the data on the Registration table
        /// </summary>
        /// <param name="model"></param>
        public void SaveRegistration(RegistrationModel model)
        {
         
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                LBC_REGISTRATION _registration = new LBC_REGISTRATION()
                {

                    STARS_ID = model.STARS_ID,
                    EVENT_ID = model.EVENT_ID,
                    WSLX_ID= model.WSLX_ID,
                    PA_CODE= model.PA_CODE,
                    REGD_STATUS = "A",
                    TRANSPORTATION_MODE = model.TRANSPORTATION_MODE,
                    TRANSPORTATION_NEED = model.TRANSPORTATION_NEED,
                    CREATED_DATE = model.CREATED_DATE,
                    CREATED_BY = model.CREATED_BY,
                    NOTES=model.NOTES,
                    ADMIN_NOTES = model.ADMIN_NOTES
                };

               context.LBC_REGISTRATION.Add(_registration);
                context.SaveChanges();


            }
        }

        /// <summary>
        /// Method to Update the Registration Table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateRegistration(RegistrationModel model)
        {
            bool save = true;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _registration = (from p in context.LBC_REGISTRATION
                                    where p.STARS_ID == model.STARS_ID && p.EVENT_ID == model.EVENT_ID
                                    select p).FirstOrDefault();



                    _registration.ATTENDED = model.ATTENDED;
                    _registration.TRANSPORTATION_MODE = model.TRANSPORTATION_MODE;
                    _registration.TRANSPORTATION_NEED = model.TRANSPORTATION_NEED;
                    _registration.REGD_STATUS = model.REGD_STATUS;
                    _registration.UPDATE_DATE = model.UPDATE_DATE;
                    _registration.UPDATED_BY = model.UPDATED_BY;
                    _registration.NOTES = model.NOTES;
                    _registration.ADMIN_NOTES = model.ADMIN_NOTES;
                    _registration.CANCEL_REASON = model.CANCEL_REASON;

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
        /// Mapping the model to the Data layer
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private RegistrationModel MapModelFromLBC_REGISTRATION(RegistrationModel entity)
        {
            RegistrationModel model = new RegistrationModel();
            if (entity != null)
            {
                model.STARS_ID = entity.STARS_ID;
                model.PA_CODE = entity.PA_CODE;
                model.WSLX_ID = entity.WSLX_ID;
                model.EVENT_ID = entity.EVENT_ID;
                model.REGD_STATUS = entity.REGD_STATUS;
                model.TRANSPORTATION_MODE = entity.TRANSPORTATION_MODE;
                model.ATTENDED = entity.ATTENDED;
                model.UPDATE_DATE = entity.UPDATE_DATE;
                model.UPDATED_BY = entity.UPDATED_BY;
                model.CREATED_BY = entity.CREATED_BY;
                model.CREATED_DATE = entity.CREATED_DATE;
                model.NOTES = entity.NOTES;

                //model.haveProfileWslxId = string.IsNullOrEmpty(entity.WSLX_ID) ? false : true;

            }

            return model;
        }
        /// <summary>
        /// Filter LBC by Dealer type
        /// </summary>
        /// <param name="starsId"></param>
        /// <returns></returns>
        public bool CheckIsSelectBy(string starsId)
        {
            bool isSelect = false;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _profile = (from p in context.LBC_PROFILE
                                where p.STARS_ID == starsId && p.EMP_STATUS_CODE == "A" && p.SELECT_CONTACT_DLR == "S"
                                select p).FirstOrDefault();

                if (_profile != null)
                {
                    isSelect = true;
                }
            }
            return isSelect;
        }
    }
}
