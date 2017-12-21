using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.DL.Repositories;
using LincolnBrandChampion.DL.Properties;
using LincolnBrandChampion.Model.Models;

namespace LincolnBrandChampion.BL.Persisters
{
    public class HotelInfoBL
    {
        public HotelCarModel GetRegistrationByStarsId(string starsId)
        {
            HotelInfoRepository _hotel = new HotelInfoRepository();

            return _hotel.GetHotelInfoByStarsId(starsId);

        }

        public HotelCarModel GetHotelInfoByStarsEvent(string starsId, decimal eventId)
        {
            HotelInfoRepository _hotel = new HotelInfoRepository();

            return _hotel.GetHotelInfoByStarsEvent(starsId, eventId);

        }

        public void SaveHotelInfo(HotelCarModel model)
        {
            HotelInfoRepository _hotel = new HotelInfoRepository();
            _hotel.SaveHotelInfo(model);
        }

        public bool UpdateHotelInfo(HotelCarModel model)
        {
            HotelInfoRepository _hotel = new HotelInfoRepository();
            return _hotel.UpdateHotelInfo(model);
        }

        public bool ExistHotelForUser(string starsid, decimal eventid)
        {
            HotelInfoRepository _hotel = new HotelInfoRepository();
            return _hotel.ExistHotelForUser(starsid,eventid);
        }
    }
}
