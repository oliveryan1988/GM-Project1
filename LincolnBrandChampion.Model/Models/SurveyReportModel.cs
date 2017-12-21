using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LincolnBrandChampion.Model.Models
{
    public class SurveyReportModel
    {
        public decimal SURVEY_ID { get; set; }
        public Nullable<System.DateTime> DATE { get; set; }
        public string NAME { get; set; }
        public string DLR_NAME { get; set; }
        public string EVENT_SESSION { get; set; }
        public string ANSWER1 { get; set; }
        public string ANSWER2 { get; set; }
        public string ANSWER3 { get; set; }
        public string ANSWER4 { get; set; }
        public string ANSWER5 { get; set; }
        public string ANSWER6 { get; set; }
        public string ANSWER7 { get; set; }
        public string ANSWER8 { get; set; }
        public string ANSWER9 { get; set; }
        public string ANSWER10 { get; set; }
        public string ANSWER11 { get; set; }
        public string ANSWER12 { get; set; }
        public string ANSWER13 { get; set; }
        public string ANSWER14 { get; set; }
        public string ANSWER15 { get; set; }
        public string ANSWER16 { get; set; }
        public List<SurveyQuestionModel> QUESTIONS { get; set; }
    }
}
