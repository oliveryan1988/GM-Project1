using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LincolnBrandChampion.Model.Models
{
    public class FlightInfoModel
    {
        public decimal? REGD_SEQ_NUM { get; set; }
        public string STARS_ID { get; set; }
        public decimal? EVENT_ID { get; set; }
        [Required(ErrorMessage = "Arrival date is required")]
        public Nullable<System.DateTime> ARR_DATE { get; set; }
        [Required(ErrorMessage = "Arrival time is required")]
        public string ARR_TIME { get; set; }
        [Required(ErrorMessage = "Please select an airline")]
        public string ARR_AIRLINE { get; set; }
        [Required(ErrorMessage = "Please enter your flight number")]
        public string ARR_FLIGHT_NUM { get; set; }
        [Required(ErrorMessage = "Please enter the arrival city")]
        public string ARR_CITY { get; set; }
        [Required(ErrorMessage = "Departure date is required")]
        public Nullable<System.DateTime> DEP_DATE { get; set; }
        [Required(ErrorMessage = "Departure time is required")]
        public string DEP_TIME { get; set; }
        [Required(ErrorMessage = "Please enter your departure airline")]
        public string DEP_AIRLINE { get; set; }
        [Required(ErrorMessage = "Please enter your departing flight number")]
        public string DEP_FLIGHT_NUM { get; set; }
        [Required(ErrorMessage = "Please enter yout departure city")]
        public string DEP_CITY { get; set; }
        [Required(ErrorMessage = "Please enter the destination city for your departing flight")]
        public string DEP_DEST_CITY { get; set; }
        public string MULTI_FLAG { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public List<FlightInfoDetailsModel> ConnectionFlightInfoList { get; set; }
        [Required(ErrorMessage = "Please enter the departure city for your arrival flight")]
        public string ARR_DEP_CITY { get; set; }
        [Required(ErrorMessage = "Please enter the arrival terminal or accept the default")]
        public string ARR_TERMINAL { get; set; }
        public string CONNECTING_FLIGHT_NOTES { get; set; }
    }
}
