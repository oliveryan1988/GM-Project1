using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LincolnBrandChampion.Model.Models
{
    public class CheckpointReportModel
    {
        public string STARS_ID { get; set; }
        public string PA_CODE { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string DLR_NAME { get; set; }
        public string DLR_ADDRESS { get; set; }
        public string DLR_CITY { get; set; }
        public string DLR_STATE { get; set; }
        public string DLR_ZIP { get; set; }
        public string DLR_PHONE { get; set; }
        public decimal? CHECKPOINT_ID { get; set; }
        public string TOOL_ORDERED { get; set; }
        public string EMAIL_ID { get; set; }
        public string SHIPPING_STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
        public string SELECT_CONTACT_DLR { get; set; }
        public Nullable<decimal> CheckPoint1TeamMeetingPDF { get; set; }
        public Nullable<decimal> Checkpoint1SelfStudyPDF { get; set; }
        public Nullable<decimal> Checkpoint1SelfStudyPPTX { get; set; }
        public Nullable<decimal> CheckPoint1TeamMeetingPPTX { get; set; }
        public string STARS_COMPLETION { get; set; }
    }
}
