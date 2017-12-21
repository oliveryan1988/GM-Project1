using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.Model.Models;

namespace LincolnBrandChampion.Model.Models
{
    public class UserRolModel
    {
        public UserRolModel()
        {
            this.LBC_USERS = new HashSet<UserModel>();
        }
    
        public decimal USR_ROLE_ID { get; set; }
        public string USR_ROLE { get; set; }
        public string USR_ROLE_DESC { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }

        public virtual ICollection<UserModel> LBC_USERS { get; set; }
    }
}
