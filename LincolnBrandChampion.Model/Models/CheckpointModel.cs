using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LincolnBrandChampion.Model.Models
{
    public class CheckpointModel
    {
        public decimal CHECKPOINT_ID { get; set; }
        public string CHK_DESC { get; set; }
        public Nullable<System.DateTime> CHK_START_DATE { get; set; }
        public Nullable<System.DateTime> CHK_END_DATE { get; set; }
        public string CHK_TITLE { get; set; }
        public string IMAGE_NAME { get; set; }
        public List<CheckpointModel> CHECKPOINTS { get; set; }
    }
}
