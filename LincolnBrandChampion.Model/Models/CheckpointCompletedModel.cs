using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnBrandChampion.Model.Models
{
    public class CheckpointCompletedModel
    {
        public string STARS_ID { get; set; }
        public string PA_CODE { get; set; }
        public string TOOL_ORDERED { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
        public decimal CHECKPOINT_ID { get; set; }
        public string EMAIL_ID { get; set; }
        public string SHIPPING_STATUS { get; set; }
    }
}
