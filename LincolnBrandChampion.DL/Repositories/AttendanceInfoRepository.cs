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
    public class AttendanceInfoRepository
    {

        /// <summary>
        /// Method to Bind the Model
        /// </summary>
        /// <param name="StarsId"></param>
        /// <returns></returns>
        public AttendanceReportModel GetAttendanceInfoByStarsId(string StarsId)
        {
            AttendanceReportModel model = new AttendanceReportModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from rg in context.LBC_REGISTRATION
                          join _ev in context.LBC_EVENT on rg.EVENT_ID equals _ev.EVENT_ID
                          join pr in context.LBC_PROFILE on rg.STARS_ID equals pr.STARS_ID
                                    
                                    where pr.STARS_ID == StarsId && rg.REGD_STATUS == "A"
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
                                        LAST_NAME = pr.LAST_NAME
                                    };

                model = lst.SingleOrDefault();
            }
            return model;
        }
        /// <summary>
        /// MEthod to set the attendance for a registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateAttendanceInfo(AttendanceReportModel model)
        {
            bool save = true;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                       var _attend = (from rg in context.LBC_REGISTRATION
                          where rg.STARS_ID == model.STARS_ID && rg.EVENT_ID == model.EVENT_ID
                                  select rg).FirstOrDefault();



                       _attend.ATTENDED = model.ATTENDED;
                    _attend.UPDATE_DATE = DateTime.Now;
                    _attend.UPDATED_BY = System.Web.HttpContext.Current.Session["w_user"].ToString();

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
    }
}
