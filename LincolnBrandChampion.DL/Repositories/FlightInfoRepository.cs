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
    public class FlightInfoRepository
    {


        public bool CheckFlightInfoBy(string starsId, decimal? eventid)
        {
            bool haveStarsId = false;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _profile = (from p in context.LBC_FLIGHT_INFO
                                where p.STARS_ID == starsId && p.EVENT_ID == eventid
                                select p).FirstOrDefault();

                if (_profile != null)
                {
                    haveStarsId = true;
                }

            }
            return haveStarsId;

        }
        public FlightInfoModel GetFlightInfoByStarsId(string StarsId, decimal? eventid)
        {
            FlightInfoModel model = new FlightInfoModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _flight = from rg in context.LBC_REGISTRATION
                               from fl in context.LBC_FLIGHT_INFO.Where(fl => fl.STARS_ID == rg.STARS_ID && fl.EVENT_ID == rg.EVENT_ID ).DefaultIfEmpty()
                              where rg.STARS_ID == StarsId && rg.EVENT_ID == eventid && rg.REGD_STATUS == "A"
                              select new FlightInfoModel
                              {
                                  REGD_SEQ_NUM=fl.REGD_SEQ_NUM,
                                  STARS_ID = rg.STARS_ID,
                                  EVENT_ID = rg.EVENT_ID,
                                  ARR_DATE = fl.ARR_DATE,
                                  ARR_TIME = fl.ARR_TIME,
                                  ARR_AIRLINE = fl.ARR_AIRLINE,
                                  ARR_FLIGHT_NUM = fl.ARR_FLIGHT_NUM,
                                  ARR_CITY = fl.ARR_CITY,
                                  DEP_DATE = fl.DEP_DATE,
                                  DEP_TIME = fl.DEP_TIME,
                                  DEP_AIRLINE = fl.DEP_AIRLINE,
                                  DEP_FLIGHT_NUM = fl.DEP_FLIGHT_NUM,
                                  DEP_CITY = fl.DEP_CITY,
                                  DEP_DEST_CITY = fl.DEP_DEST_CITY,
                                  MULTI_FLAG = fl.MULTI_FLAG,
                                  CREATED_BY = fl.CREATED_BY,
                                  CREATED_DATE = fl.CREATED_DATE,
                                  UPDATED_BY=fl.UPDATED_BY,
                                  UPDATE_DATE = fl.UPDATE_DATE,
                                  ARR_DEP_CITY = fl.ARR_DEP_CITY,
                                  ARR_TERMINAL = fl.ARR_TERMINAL,
                                  CONNECTING_FLIGHT_NOTES = fl.CONNECTING_FLIGHT_NOTES
                              };
                model = _flight.SingleOrDefault();
            }
            return model;
        }

          public FlightInfoModel GetFlightInfoByStarsIdAfterChange(string StarsId, decimal? eventid)
        {
            FlightInfoModel model = new FlightInfoModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _flight = from rg in context.LBC_REGISTRATION
                               from fl in context.LBC_FLIGHT_INFO.Where(fl => fl.STARS_ID == rg.STARS_ID && fl.EVENT_ID == rg.EVENT_ID ).DefaultIfEmpty()
                              where rg.STARS_ID == StarsId && rg.EVENT_ID == eventid && rg.REGD_STATUS == "C"
                              select new FlightInfoModel
                              {
                                  REGD_SEQ_NUM=fl.REGD_SEQ_NUM,
                                  STARS_ID = rg.STARS_ID,
                                  EVENT_ID = rg.EVENT_ID,
                                  ARR_DATE = fl.ARR_DATE,
                                  ARR_TIME = fl.ARR_TIME,
                                  ARR_AIRLINE = fl.ARR_AIRLINE,
                                  ARR_FLIGHT_NUM = fl.ARR_FLIGHT_NUM,
                                  ARR_CITY = fl.ARR_CITY,
                                  DEP_DATE = fl.DEP_DATE,
                                  DEP_TIME = fl.DEP_TIME,
                                  DEP_AIRLINE = fl.DEP_AIRLINE,
                                  DEP_FLIGHT_NUM = fl.DEP_FLIGHT_NUM,
                                  DEP_CITY = fl.DEP_CITY,
                                  DEP_DEST_CITY = fl.DEP_DEST_CITY,
                                  MULTI_FLAG = fl.MULTI_FLAG,
                                  CREATED_BY = fl.CREATED_BY,
                                  CREATED_DATE = fl.CREATED_DATE,
                                  UPDATED_BY=fl.UPDATED_BY,
                                  UPDATE_DATE = fl.UPDATE_DATE,
                                  ARR_DEP_CITY = fl.ARR_DEP_CITY,
                                  ARR_TERMINAL = fl.ARR_TERMINAL,
                                  CONNECTING_FLIGHT_NOTES = fl.CONNECTING_FLIGHT_NOTES
                              };
                model = _flight.SingleOrDefault();
            }
            return model;
        }
        public decimal? GetEventIdByStarsId(string StarsId)
        {
            
            FlightInfoModel model = new FlightInfoModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _flight = from rg in context.LBC_REGISTRATION
                              where rg.STARS_ID == StarsId && rg.REGD_STATUS == "A"
                              select new FlightInfoModel
                              {
                                  EVENT_ID = rg.EVENT_ID
                              };
                model = _flight.SingleOrDefault();
            }
            
            return model.EVENT_ID;
        }
        /// <summary>
        /// Method to Insert the data on the FlightInfo table
        /// </summary>
        /// <param name="model"></param>
        public void SaveFlightInfo(FlightInfoModel model)
        {

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                LBC_FLIGHT_INFO _flightInfo = new LBC_FLIGHT_INFO()
                {
                    //REGD_SEQ_NUM = model.REGD_SEQ_NUM,
                    STARS_ID = model.STARS_ID,
                    EVENT_ID = model.EVENT_ID,
                    ARR_DATE = model.ARR_DATE,
                    ARR_TIME = model.ARR_TIME,
                    ARR_AIRLINE=model.ARR_AIRLINE,
                    ARR_FLIGHT_NUM = model.ARR_FLIGHT_NUM,
                    ARR_CITY = model.ARR_CITY,
                    DEP_DATE = model.DEP_DATE,
                    DEP_TIME=model.DEP_TIME,
                    DEP_AIRLINE = model.DEP_AIRLINE,
                    DEP_FLIGHT_NUM = model.DEP_FLIGHT_NUM,
                    DEP_CITY = model.DEP_CITY,
                    DEP_DEST_CITY= model.DEP_DEST_CITY,
                    MULTI_FLAG=model.MULTI_FLAG,
                    CREATED_DATE = model.CREATED_DATE,
                    CREATED_BY = model.CREATED_BY,
                    ARR_DEP_CITY= model.ARR_DEP_CITY,
                    ARR_TERMINAL=model.ARR_TERMINAL,
                    CONNECTING_FLIGHT_NOTES=model.CONNECTING_FLIGHT_NOTES,

                };

                context.LBC_FLIGHT_INFO.Add(_flightInfo);
                context.SaveChanges();


            }
        }

        /// <summary>
        /// Method to Update the FlightInfo Table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateFlightInfo(FlightInfoModel model)
        {
            bool save = true;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _flightInfo = (from p in context.LBC_FLIGHT_INFO
                                         where p.STARS_ID == model.STARS_ID && p.EVENT_ID == model.EVENT_ID
                                         select p).FirstOrDefault();
                    _flightInfo.ARR_DATE = model.ARR_DATE;
                    _flightInfo.ARR_TIME = model.ARR_TIME;
                    _flightInfo.ARR_AIRLINE = model.ARR_AIRLINE;
                    _flightInfo.ARR_FLIGHT_NUM = model.ARR_FLIGHT_NUM;
                    _flightInfo.ARR_CITY = model.ARR_CITY;
                    _flightInfo.ARR_DEP_CITY = model.ARR_DEP_CITY;
                    _flightInfo.ARR_TERMINAL = model.ARR_TERMINAL;
                    _flightInfo.CONNECTING_FLIGHT_NOTES = model.CONNECTING_FLIGHT_NOTES;
                    _flightInfo.DEP_DATE = model.DEP_DATE;
                    _flightInfo.DEP_AIRLINE = model.DEP_AIRLINE;
                    _flightInfo.DEP_FLIGHT_NUM = model.DEP_FLIGHT_NUM;
                    _flightInfo.DEP_CITY = model.DEP_CITY;
                    _flightInfo.DEP_DEST_CITY = model.DEP_DEST_CITY;
                    _flightInfo.DEP_TIME = model.DEP_TIME;
                    _flightInfo.MULTI_FLAG = model.MULTI_FLAG;
                    _flightInfo.UPDATE_DATE = model.UPDATE_DATE;
                    _flightInfo.UPDATED_BY = model.UPDATED_BY;

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

        private FlightInfoModel MapModelFromLBC_FLIGHT_INFO(FlightInfoModel entity)
        {
            FlightInfoModel model = new FlightInfoModel();
            if (entity != null)
            {
                model.REGD_SEQ_NUM = entity.REGD_SEQ_NUM;
                model.STARS_ID = entity.STARS_ID;
                model.EVENT_ID = entity.EVENT_ID;
                model.ARR_DATE = entity.ARR_DATE;
                model.ARR_TIME = entity.ARR_TIME;
                model.ARR_AIRLINE = entity.ARR_AIRLINE;
                model.ARR_FLIGHT_NUM = entity.ARR_FLIGHT_NUM;
                model.ARR_CITY = entity.ARR_CITY;
                model.DEP_DATE = entity.DEP_DATE;
                model.DEP_TIME = entity.DEP_TIME;
                model.DEP_AIRLINE = entity.DEP_AIRLINE;
                model.DEP_FLIGHT_NUM = entity.DEP_FLIGHT_NUM;
                model.DEP_CITY = entity.DEP_CITY;
                model.DEP_DEST_CITY = entity.DEP_DEST_CITY;
                model.MULTI_FLAG = entity.MULTI_FLAG;
                model.ARR_DEP_CITY = entity.ARR_DEP_CITY;
                model.ARR_TERMINAL = entity.ARR_TERMINAL;
                model.CONNECTING_FLIGHT_NOTES = entity.CONNECTING_FLIGHT_NOTES;
                model.UPDATE_DATE = entity.UPDATE_DATE;
                model.UPDATED_BY = entity.UPDATED_BY;
                model.CREATED_BY = entity.CREATED_BY;
                model.CREATED_DATE = entity.CREATED_DATE;

                //model.haveProfileWslxId = string.IsNullOrEmpty(entity.WSLX_ID) ? false : true;

            }

            return model;
        }
    }

   
}
