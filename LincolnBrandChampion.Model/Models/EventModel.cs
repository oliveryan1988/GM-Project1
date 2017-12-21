using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnBrandChampion.Model.Models
{
   public class EventModel
    {
       public decimal EVENT_ID { get; set; }
       public decimal? EVENT_YEAR { get; set; }
       public string EVENT_MONTH { get; set; }
       public Nullable<System.DateTime> EVENT_START_DATE { get; set; }
       public Nullable<System.DateTime> EVENT_END_DATE { get; set; }
       public string EVENT_TIME { get; set; }
       public string EVENT_SESSION { get; set; }
       public string EVENT_LOCATION{ get; set; }
       public decimal? OPEN_LIMIT { get; set; }
       public decimal? CURRENT_COUNT { get; set; }
       public string CREATED_BY { get; set; }
       public Nullable<System.DateTime> CREATED_DATE { get; set; }
       public string UPDATED_BY { get; set; }
       public Nullable<System.DateTime> UPDATE_DATE { get; set; }
       public string Reg_Status { get; set; }
       public List<GuestSessionLookupModel> SessionLookupList { get; set; }
       public List<GuestSessionLookupModel> SessionLookupListByUser{ get; set; }
      
    }
}
