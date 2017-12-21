using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.Model.Models;

namespace LincolnBrandChampion.Model.Models
{
    public class UserModel
    {
        public string USR_WSLX_ID { get; set; }
        public string USR_FIRST_NAME { get; set; }
        public string USR_LAST_NAME { get; set; }
        public Nullable<decimal> USR_ROLE_ID { get; set; }
        public string USR_STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }

        public virtual UserRolModel LBC_USER_ROLE { get; set; }

       

    }
}
