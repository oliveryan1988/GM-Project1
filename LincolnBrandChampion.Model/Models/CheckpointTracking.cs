using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnBrandChampion.Model.Models
{
   public class CheckpointTracking
    {
        public decimal SEQ_ID { get; set; }
        public string STARS_ID { get; set; }
        public string PA_CODE { get; set; }
        public string DOC_URL { get; set; }
        public string FILE_NAME { get; set; }
        public Nullable<System.DateTime> DOWNLOAD_TIME { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<decimal> CHECKPOINT_ID { get; set; }
    }
}
