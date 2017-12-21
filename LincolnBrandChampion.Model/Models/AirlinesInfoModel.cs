using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnBrandChampion.Model.Models
{
    public class AirlinesInfoModel
    {
        public decimal SEQ_NO { get; set; }
        public string AIRLINE_CODE { get; set; }
        public string AIRLINE_NAME { get; set; }
        public string TERMINAL_CODE { get; set; }
        public string TERMINAL_NAME { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
    }
}
