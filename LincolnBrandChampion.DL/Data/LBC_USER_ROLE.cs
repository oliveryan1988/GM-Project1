//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LincolnBrandChampion.DL.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class LBC_USER_ROLE
    {
        public LBC_USER_ROLE()
        {
            this.LBC_USERS = new HashSet<LBC_USERS>();
        }
    
        public decimal USR_ROLE_ID { get; set; }
        public string USR_ROLE { get; set; }
        public string USR_ROLE_DESC { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
    
        public virtual ICollection<LBC_USERS> LBC_USERS { get; set; }
    }
}
