using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnBrandChampion.Model.Models
{
    public class SurveyCompledModel
    {
        public string STARS_ID { get; set; }
        public decimal SURVEY_ID { get; set; }
        public string PA_CODE { get; set; }
        public Nullable<System.DateTime> COMPLETED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
    }
}
