using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.Model.Models;

namespace LincolnBrandChampion.Model.Models
{
    public class GuestSessionModel
    {
        public string WSLX_ID { get; set; }
        public decimal SESSION_ID { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public virtual GuestRegistrationModel LBC_GUEST_REGISTRATION { get; set; }
        public List<GuestSessionLookupModel> GuestsessionLookupModel { get; set; }
    }
}
