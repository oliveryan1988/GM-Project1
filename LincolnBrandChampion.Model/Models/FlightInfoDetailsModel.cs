using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnBrandChampion.Model.Models
{
    public class FlightInfoDetailsModel
    {
     
        public string STARS_ID { get; set; }
        public decimal? TRIP_NUM { get; set; }
        public decimal? EVENT_ID { get; set; }
        public Nullable<System.DateTime> ARR_DATE { get; set; }
        public string ARR_TIME { get; set; }
        public string ARR_AIRLINE { get; set; }
        public string ARR_FLIGHT_NUM { get; set; }
        public string ARR_CITY { get; set; }
        public Nullable<System.DateTime> CONN_ARR_DATE { get; set; }
        public string CONN_ARR_TIME { get; set; }
        public string CONN_ARR_AIRLINE { get; set; }
        public string CONN_ARR_FLIGHT_NUM { get; set; }
        public string CONN_ARR_CITY { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
    }
}
