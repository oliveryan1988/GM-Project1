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
    public class HotelInfoRepository
    {
        /// <summary>
        /// Method to get the Hotel Info based on Starz Id
        /// </summary>
        /// <param name="StarsId"></param>
        /// <returns></returns>
        public HotelCarModel GetHotelInfoByStarsId(string StarsId)
        {
            
  

            HotelCarModel model = new HotelCarModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _registration = from pr in context.LBC_PROFILE
                                   join reg in context.LBC_REGISTRATION on pr.STARS_ID equals reg.STARS_ID
                                    from ht in context.LBC_HOTEL_CAR_INFO.Where(ht => ht.STARS_ID == pr.STARS_ID).DefaultIfEmpty()
                                    where pr.STARS_ID == StarsId && reg.REGD_STATUS =="A"
                                    select new HotelCarModel
                                    {
                                        STARS_ID = pr.STARS_ID,
                                        PA_CODE = pr.PA_CODE,
                                        FIRST_NAME = pr.FIRST_NAME,
                                        LAST_NAME = pr.LAST_NAME,
                                        DEALERSHIP_NAME = pr.DLR_NAME,
                                        HOTEL_NAME = ht.HOTEL_NAME,
                                        HOTEL_CONF = ht.HOTEL_CONF,
                                        HOTEL_CHECK_IN = ht.HOTEL_CHECK_IN,
                                        HOTEL_CHECK_OUT = ht.HOTEL_CHECK_OUT,
                                        CAR_CONFIRM = ht.CAR_CONFIRM,
                                        CAR_NOTES = ht.CAR_NOTES,
                                        EVENT_ID = reg.EVENT_ID,
                                        ADMIN_NOTES = ht.ADMIN_NOTES
                                    };

                model = _registration.SingleOrDefault();
            }
            return model;
        }

        /// <summary>
        /// Method to get the Hotel Info based on Stars/Event
        /// </summary>
        /// <param name="StarsId"></param>
        /// <returns></returns>
        public HotelCarModel GetHotelInfoByStarsEvent(string StarsId, decimal EventId)
        {
            HotelCarModel model = new HotelCarModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _registration = from pr in context.LBC_PROFILE
                                    join reg in context.LBC_REGISTRATION on pr.STARS_ID equals reg.STARS_ID
                                    from ht in context.LBC_HOTEL_CAR_INFO.Where(ht => ht.STARS_ID == pr.STARS_ID && ht.EVENT_ID == reg.EVENT_ID).DefaultIfEmpty()
                                    where pr.STARS_ID == StarsId && reg.EVENT_ID == EventId && reg.REGD_STATUS == "A"
                                    select new HotelCarModel
                                    {
                                        STARS_ID = pr.STARS_ID,
                                        PA_CODE = pr.PA_CODE,
                                        FIRST_NAME = pr.FIRST_NAME,
                                        LAST_NAME = pr.LAST_NAME,
                                        DEALERSHIP_NAME = pr.DLR_NAME,
                                        HOTEL_NAME = ht.HOTEL_NAME,
                                        HOTEL_CONF = ht.HOTEL_CONF,
                                        HOTEL_CHECK_IN = ht.HOTEL_CHECK_IN,
                                        HOTEL_CHECK_OUT = ht.HOTEL_CHECK_OUT,
                                        CAR_CONFIRM = ht.CAR_CONFIRM,
                                        CAR_NOTES = ht.CAR_NOTES,
                                        EVENT_ID = reg.EVENT_ID,
                                        ADMIN_NOTES = ht.ADMIN_NOTES
                                    };

                model = _registration.SingleOrDefault();
            }
            return model;
        }
        /// <summary>
        /// Method to check if the user have already hotel information
        /// </summary>
        /// <param name="starsId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool ExistHotelForUser(string starsId, decimal eventId)
        {
            bool haveStarsId = false;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _hotel = (from p in context.LBC_HOTEL_CAR_INFO
                                where p.STARS_ID == starsId && p.EVENT_ID == eventId
                                select p).FirstOrDefault();

                if (_hotel != null)
                {
                    haveStarsId = true;
                }

            }
            return haveStarsId;

        }

    /// <summary>
    /// Method to Create entry 
    /// </summary>
    /// <param name="model"></param>
        public void SaveHotelInfo(HotelCarModel model)
        {

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                LBC_HOTEL_CAR_INFO _hotel = new LBC_HOTEL_CAR_INFO()
                {

                    STARS_ID = model.STARS_ID,
                    EVENT_ID = model.EVENT_ID,
                    HOTEL_NAME = model.HOTEL_NAME,
                    HOTEL_CONF = model.HOTEL_CONF,
                    HOTEL_CHECK_IN = model.HOTEL_CHECK_IN,
                    HOTEL_CHECK_OUT = model.HOTEL_CHECK_OUT,
                    CAR_CONFIRM = model.CAR_CONFIRM,
                    ADMIN_NOTES = model.ADMIN_NOTES,
                    CAR_NOTES = model.CAR_NOTES,
                    CREATED_DATE = model.CREATED_DATE,
                    CREATED_BY = model.CREATED_BY
                };

                context.LBC_HOTEL_CAR_INFO.Add(_hotel);
                context.SaveChanges();


            }
        }

        public bool UpdateHotelInfo(HotelCarModel model)
        {
            bool save = true;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _hotel = (from p in context.LBC_HOTEL_CAR_INFO
                                         where p.STARS_ID == model.STARS_ID && p.EVENT_ID == model.EVENT_ID
                                         select p).FirstOrDefault();



                    _hotel.HOTEL_NAME = model.HOTEL_NAME;
                    _hotel.HOTEL_CONF = model.HOTEL_CONF;
                    _hotel.HOTEL_CHECK_IN = model.HOTEL_CHECK_IN;
                    _hotel.HOTEL_CHECK_OUT = model.HOTEL_CHECK_OUT;
                    _hotel.CAR_CONFIRM = model.CAR_CONFIRM;
                    _hotel.CAR_NOTES = model.CAR_NOTES;
                    _hotel.ADMIN_NOTES = model.ADMIN_NOTES;
                    _hotel.UPDATE_DATE = model.UPDATE_DATE;
                    _hotel.UPDATED_BY = model.UPDATED_BY;

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
