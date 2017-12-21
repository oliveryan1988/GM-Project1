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
    public class MetricsBL
    {
        /// <summary>
        /// Method to get the list of registered user by market/event
        /// </summary>
        /// <param name="market">string</param>
        /// <param name="eventid">decimal</param>
        /// <returns>List of Registration Report Model</returns>
        public List<RegistrationReportModel> getRegistrationByMarketEvent(string market, decimal eventid)
        {
            MetricsRepository _mericsReg = new MetricsRepository();

            return _mericsReg.getRegistrationByMarketEvent(market, eventid);

        }
        /// <summary>
        /// Method to get the registration by stars/event
        /// </summary>
        /// <param name="starsId">string</param>
        /// <param name="eventId">decimal</param>
        /// <returns>Registration Report Model</returns>
        public RegistrationReportModel getRegistrationByStarsEvent(string starsId, decimal? eventId)
        {
            MetricsRepository _mericsReg = new MetricsRepository();

            return _mericsReg.getRegistrationByStarsEvent(starsId, eventId);

        }
        /// <summary>
        /// Method to get the list of registered user by filter
        /// </summary>
        /// <returns>List of Registration Report Model</returns>
        public List<RegistrationReportModel> getRegistrationByStarsEventList(List<string> StarsIdList = null, List<decimal?> EventIdList = null)
        {
            MetricsRepository _mericsReg = new MetricsRepository();
             
            return _mericsReg.getRegistrationByStarsEventList(StarsIdList, EventIdList);

        }
        /// <summary>
        /// Method to get the list of active registrations by filter
        /// </summary>
        /// <returns>List of Registration Report Model</returns>
        public List<RegistrationReportModel> getActiveByStarsEventList(List<string> StarsIdList = null, List<decimal?> EventIdList = null)
        {
            MetricsRepository _mericsReg = new MetricsRepository();

            return _mericsReg.getActiveByStarsEventList(StarsIdList, EventIdList);

        }
        /// <summary>
        /// Method to get the list of active registrations by Market/Event
        /// </summary>
        /// <returns>List of Registration Report Model</returns>
        public List<RegistrationReportModel> getActiveByMarketEvent(string market, decimal _event)
        {
            MetricsRepository _mericsReg = new MetricsRepository();

            return _mericsReg.getActiveByMarketEvent(market, _event);

        }
        /// <summary>
        /// Method to get the list of cancelled registrations by Market/Event
        /// </summary>
        /// <returns>List of Registration Report Model</returns>
        public List<RegistrationReportModel> getCancelledByMarketEvent(string market, decimal _event)
        {
            MetricsRepository _mericsReg = new MetricsRepository();

            return _mericsReg.getCancelledByMarketEvent(market, _event);

        }
        /// <summary>
        /// Method to get the list of cancelled registered user by filter
        /// </summary>
        /// <returns>List of Registration Report Model</returns>
        public List<RegistrationReportModel> getCancelledRegistration(List<string> StarsIdList = null, List<decimal?> EventIdList = null)
        {
            MetricsRepository _mericsReg = new MetricsRepository();

            return _mericsReg.getCancelledRegistration(StarsIdList, EventIdList);

        }
        /// <summary>
        /// Method to get the list of registered user Attendance by market/event
        /// </summary>
        /// <param name="market">string</param>
        /// <param name="eventid">decimal</param>
        /// <returns>list of Attendance Report Model</returns>
        public List<AttendanceReportModel> getAttendanceByMarketEvent(string market, decimal eventid)
        {
            MetricsRepository _mericsAtt = new MetricsRepository();

            return _mericsAtt.getAttendanceByMarketEvent(market, eventid);

        }
    }
}
