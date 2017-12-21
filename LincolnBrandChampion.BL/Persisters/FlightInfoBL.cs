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
    public class FlightInfoBL
    {
        public FlightInfoModel GetFlightInfoByStarsId(string starsId, decimal? eventid)
        {
            FlightInfoRepository  _FlightInfo = new FlightInfoRepository();

            return _FlightInfo.GetFlightInfoByStarsId(starsId, eventid);
        }
        public FlightInfoModel GetFlightInfoByStarsIdAfterChange(string starsId, decimal? eventid)
        {
            FlightInfoRepository _FlightInfo = new FlightInfoRepository();

            return _FlightInfo.GetFlightInfoByStarsIdAfterChange(starsId, eventid);
        }
        public decimal?  GetEventIdByStarsId(string starsId)
        {
            FlightInfoRepository _FlightInfo = new FlightInfoRepository();

            return _FlightInfo.GetEventIdByStarsId(starsId);
        }
        /// <summary>
        /// Method to Save Flight Info
        /// </summary>
        /// <param name="model"></param>
        public void SaveFlightInfo(FlightInfoModel model)
        {
            FlightInfoRepository _flightInfo = new FlightInfoRepository();
            _flightInfo.SaveFlightInfo(model);
        }
        /// <summary>
        /// Update the Flight Info
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateFlightInfo(FlightInfoModel model)
        {
            FlightInfoRepository _flightInfo = new FlightInfoRepository();
            return _flightInfo.UpdateFlightInfo(model);
        }
        public bool CheckFlightInfoBy(string starsId, decimal? eventid)
        {
            FlightInfoRepository _flightInfo = new FlightInfoRepository();
            return _flightInfo.CheckFlightInfoBy(starsId, eventid);
        }
    }
}
