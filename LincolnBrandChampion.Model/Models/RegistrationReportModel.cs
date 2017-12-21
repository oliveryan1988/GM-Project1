using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnBrandChampion.Model.Models
{
    public class RegistrationReportModel
    {
        public string STARS_ID { get; set; }
        public string PA_CODE { get; set; }
        public string WSLX_ID { get; set; }
        public string REGD_STATUS { get; set; }
        public string EVENT { get; set; }
        public decimal? EVENT_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string BADGE_NAME { get; set; }
        public string BADGE_NAME_FIRST { get; set; }
        public string BADGE_NAME_LAST { get; set; }
        public string TITLE { get; set; }
        public string MARKET { get; set; }
        public string DLR_NAME { get; set; }
        public string DLR_ADDRESS { get; set; }
        public string DLR_CITY { get; set; }
        public string DLR_STATE { get; set; }
        public string DLR_ZIP { get; set; }
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "{0} is not in the correct format")]           
        public string EMAIL_ID { get; set; }
        public string PHONE { get; set; }
        public string DLR_PHONE { get; set; }
        public string TRANSPORT_METHOD { get; set; }
        public string TRANSPORTATION_NEED { get; set; }
        public string REGISTRATION_NOTES { get; set; }
        public string HOTEL_NAME { get; set; }
        public string HOTEL_CONF { get; set; }
        public Nullable<System.DateTime> HOTEL_CHECK_IN { get; set; }
        public Nullable<System.DateTime> HOTEL_CHECK_OUT { get; set; }
        public string CAR_CONFIRM { get; set; }
        public string CAR_NOTES { get; set; }
        public string ADMIN_NOTES { get; set; }
        public string ATTENDED { get; set; }
        public List<HotelCarModel> HotelInfo { get; set; }
        public Nullable<System.DateTime> ARR_DATE { get; set; }
        public string ARR_TIME { get; set; }
        public string ARR_AIRLINE { get; set; }
        public string ARR_FLIGHT_NUM { get; set; }
        public string ARR_CITY { get; set; }
        public Nullable<System.DateTime> DEP_DATE { get; set; }
        public string DEP_TIME { get; set; }
        public string DEP_AIRLINE { get; set; }
        public string DEP_FLIGHT_NUM { get; set; }
        public string DEP_CITY { get; set; }
        public string DEP_DEST_CITY { get; set; }
        public string MULTI_FLAG { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public List<FlightInfoDetailsModel> ConnectionFlightInfoList { get; set; }
        public string ARR_DEP_CITY { get; set; }
        public string ARR_TERMINAL { get; set; }
        public string CONNECTING_FLIGHT_NOTES { get; set; }
        public string DIETARY_RESTRICTION { get; set; }
        public string ZONE { get; set; }
        public string SALESCODE { get; set; }
        public string DEPT_INFO { get; set; }
        public string DEALER_TYPE { get; set; }
        public string REG_ADMIN_NOTES { get; set; }
    }
}
