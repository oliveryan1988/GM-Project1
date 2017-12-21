using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnBrandChampion.Model.Models
{
    public class GuestSessionLookupModel
    {
      
            public decimal SESSION_ID { get; set; }
            public decimal EVENT_ID { get; set; }
            public string SESSION_DESC { get; set; }
            public System.DateTime SESSION_DATE { get; set; }
            public string CREATED_BY { get; set; }
            public Nullable<System.DateTime> CREATED_DATE { get; set; }
            public string UPDATED_BY { get; set; }
            public Nullable<System.DateTime> UPDATE_DATE { get; set; }
            public string WSLX_ID { get; set; }
            public string EVENT_SESSION { get; set; }
            public string EVENT_LOCATION { get; set; }
            public decimal? EVENT_YEAR { get; set; }
            public string EVENT_MONTH { get; set; }                
            public virtual EventModel LBC_EVENT { get; set; }
            public decimal REGISTERED_SESSION { get; set; }
        
    }
}
