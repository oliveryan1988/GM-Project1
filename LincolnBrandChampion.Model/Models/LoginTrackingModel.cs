using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnBrandChampion.Model.Models
{
    public class LoginTrackingModel
    {
        public decimal SEQ_ID { get; set; }
        public System.DateTime LOGIN_DATE { get; set; }
        public decimal LOGIN_TIME { get; set; }
        public string USERID { get; set; }
        public string ACI { get; set; }
        public string SITE { get; set; }
        public string ORGCODE { get; set; }
        public string EMPCODE { get; set; }
        public string MRROLE { get; set; }
        public string ORG { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string UPDATED_BY { get; set; }
    }
}
