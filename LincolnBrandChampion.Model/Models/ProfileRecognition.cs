using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnBrandChampion.Model.Models
{
    public class ProfileRecognition
    {
        public decimal RECOGNITION_ID { get; set; }
        public string STARS_ID { get; set; }
        public string STATUS { get; set; }
       public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
    }
}
