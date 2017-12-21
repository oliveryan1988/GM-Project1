using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LincolnBrandChampion.Model.Models
{
    public class SurveyQuestionAnswerModel
    {
        public decimal QUESTION_ID { get; set; }
        public decimal ANSWER_ID { get; set; }
        public string ANSWER { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string INPUT_TYPE { get; set; }
    }
}
