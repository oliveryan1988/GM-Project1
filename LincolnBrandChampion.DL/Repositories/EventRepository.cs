using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.DL.Data;
using LincolnBrandChampion.DL.Helpers;
using System.Data.Objects.SqlClient;

namespace LincolnBrandChampion.DL.Repositories
{
    public class EventRepository
    {
        /// <summary>
        /// Get all the Events
        /// </summary>
        /// <returns></returns>
        public List<EventModel> GetAllEvents(string starzid)
        {
            List<EventModel> list = new List<EventModel>();
             using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from entity in context.LBC_EVENT
                          where entity.EVENT_END_DATE > DateTime.Now
                           select new EventModel {
                           EVENT_ID = entity.EVENT_ID,
                           EVENT_YEAR = entity.EVENT_YEAR,
                           EVENT_MONTH = entity.EVENT_MONTH,
                           EVENT_START_DATE = entity.EVENT_START_DATE,
                           EVENT_END_DATE= entity.EVENT_END_DATE,
                           EVENT_LOCATION = entity.EVENT_LOCATION,
                           EVENT_SESSION = entity.EVENT_SESSION,
                           CURRENT_COUNT = entity.CURRENT_COUNT,
                           OPEN_LIMIT = entity.OPEN_LIMIT,
                           
                           };

                var lst2 = (from reg in context.LBC_REGISTRATION
                            join ev in context.LBC_EVENT on reg.EVENT_ID equals ev.EVENT_ID
                            where reg.STARS_ID == starzid
                            select reg).ToList();
                
                 list = lst.ToList<EventModel>();
                 int count = 0;
                 foreach (EventModel item in list)
                 {
                     
                     for (int i = 0; i < lst2.Count; i++)
                     {
                         if (item.EVENT_ID == lst2[i].EVENT_ID)
                         { 
                             item.Reg_Status = lst2[i].REGD_STATUS;
                         };
                         
                     }
                     count = count + 1;

                 }

                return list;
              //  return lst.ToList<EventModel>();
            }
        }
        public List<EventModel> GetAllEvents()
        {
            List<EventModel> list = new List<EventModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from entity in context.LBC_EVENT
                          select new EventModel
                          {
                              EVENT_ID = entity.EVENT_ID,
                              EVENT_YEAR = entity.EVENT_YEAR,
                              EVENT_MONTH = entity.EVENT_MONTH,
                              EVENT_START_DATE = entity.EVENT_START_DATE,
                              EVENT_END_DATE = entity.EVENT_END_DATE,
                              EVENT_LOCATION = entity.EVENT_LOCATION,
                              EVENT_SESSION = entity.EVENT_SESSION,
                              CURRENT_COUNT = entity.CURRENT_COUNT,
                              OPEN_LIMIT = entity.OPEN_LIMIT,

                          };


                return lst.ToList<EventModel>();
            }
        }
        /// <summary>
/// Get the event by ID
/// </summary>
/// <param name="EventId"></param>
/// <returns></returns>
        public List<EventModel> GetEventsByID(Decimal EventId)
        {
            List<EventModel> list = new List<EventModel>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from ev in context.LBC_EVENT
                          where ev.EVENT_ID == EventId
                          select new EventModel
                          {
                              EVENT_YEAR = Convert.ToInt32(ev.EVENT_YEAR),
                              EVENT_MONTH = ev.EVENT_MONTH,
                              EVENT_START_DATE = ev.EVENT_START_DATE,
                              EVENT_END_DATE = ev.EVENT_END_DATE,
                              EVENT_TIME = ev.EVENT_TIME,
                              EVENT_SESSION = ev.EVENT_SESSION,
                              EVENT_LOCATION = ev.EVENT_LOCATION,
                              OPEN_LIMIT = Convert.ToInt32(ev.OPEN_LIMIT),
                              CURRENT_COUNT = Convert.ToInt32(ev.CURRENT_COUNT)

                          };

                return lst.ToList<EventModel>();
            }
        }


        public EventModel GetEventModelByID(decimal EventId)
        {
            EventModel model = new EventModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from ev in context.LBC_EVENT
                          where ev.EVENT_ID == EventId
                          select new EventModel
                          {
                              EVENT_YEAR = ev.EVENT_YEAR,
                              EVENT_MONTH = ev.EVENT_MONTH,
                              EVENT_START_DATE = ev.EVENT_START_DATE,
                              EVENT_END_DATE = ev.EVENT_END_DATE,
                              EVENT_ID = ev.EVENT_ID,
                              EVENT_TIME = ev.EVENT_TIME,
                              EVENT_SESSION = ev.EVENT_SESSION,
                              EVENT_LOCATION = ev.EVENT_LOCATION,
                              OPEN_LIMIT = ev.OPEN_LIMIT,
                              CURRENT_COUNT = ev.CURRENT_COUNT

                          };

                model = lst.SingleOrDefault();
            }
            return model;
        }
        /// <summary>
        /// Update the event count to check the max;
        /// </summary>
        /// <param name="model"></param>
        public void UpdateEventCount(EventModel model)
        {
           // LBC_EVENT _event = new LBC_EVENT();
           // bool flag = false;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _event = (from p in context.LBC_EVENT
                                  where p.EVENT_ID == model.EVENT_ID
                                  select p).FirstOrDefault();

                    _event.CURRENT_COUNT = model.CURRENT_COUNT + 1;
                    _event.UPDATED_BY = model.UPDATED_BY;
                    _event.UPDATE_DATE = DateTime.Now;
                    context.SaveChanges();

                }
            }

            catch(Exception ex)
            {
                throw ex;
            
            }
        }
        /// <summary>
        /// Method to decrease the event count when the they cancel
        /// </summary>
        /// <param name="model"></param>
        public void UpdateEventCountDecrease(EventModel model)
        {
            // LBC_EVENT _event = new LBC_EVENT();
            // bool flag = false;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _event = (from p in context.LBC_EVENT
                                  where p.EVENT_ID == model.EVENT_ID
                                  select p).FirstOrDefault();

                    _event.CURRENT_COUNT = model.CURRENT_COUNT - 1;
                    _event.UPDATED_BY = model.UPDATED_BY;
                    _event.UPDATE_DATE = DateTime.Now;
                    context.SaveChanges();

                }
            }

            catch (Exception ex)
            {
                throw ex;

            }
        }

        //public List<EventModel> GetEventListMonth()
        //{
        //    List<EventModel> lst = new List<EventModel>();
        //    using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
        //    {

        //        var list = (from entity in context.LBC_EVENT
        //                   //where entity.DLR_REGION == dlrRegion || string.IsNullOrEmpty(entity.DLR_REGION)
        //                   select new EventModel
        //                   {
        //                       EVENT_MONTH = entity.EVENT_MONTH,
        //                       EVENT_YEAR = entity.EVENT_YEAR,
        //                      // EVENT_ID = entity.EVENT_ID
        //                   }).ToList().Distinct();


        //        lst = list.ToList();


        //    }
        //    return lst;
        //}


        public List<string> GetEventListMonth()
        {
            List<string> lst = new List<string>();
            List<EventModel> lstmodel = new List<EventModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {

                var list = (from entity in context.LBC_EVENT
                            //where entity.DLR_REGION == dlrRegion || string.IsNullOrEmpty(entity.DLR_REGION)
                            select new EventModel
                            {
                                EVENT_MONTH = entity.EVENT_MONTH,
                                EVENT_YEAR = entity.EVENT_YEAR,
                                // EVENT_ID = entity.EVENT_ID
                            }).ToList().Distinct();


                lstmodel = list.ToList();


            }

            foreach (var item in lstmodel)
            {
                lst.Add(item.EVENT_MONTH + "," + item.EVENT_YEAR.ToString());
                
            
            }
            return lst.Distinct().ToList<string>();
        }
        public List<decimal> GetEventModelByMonYear(string month, decimal year)
        {
           List<decimal>  list = new List<decimal>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = (from ev in context.LBC_EVENT
                           where ev.EVENT_MONTH == month && ev.EVENT_YEAR == year
                           select  ev.EVENT_ID);

                list = lst.ToList();
                
            }
            return list;
        }
    }
}
