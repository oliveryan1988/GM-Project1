using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.DL.Data;
using LincolnBrandChampion.DL.Helpers;


namespace LincolnBrandChampion.DL.Repositories
{
    public class SurveyRepository
    {
        /// <summary>
        /// Checks if survey was taken by stars id.
        /// </summary>
        /// <param name="pacode"></param>
        /// <param name="surveyId"></param>
        /// <returns>bool</returns>
        public bool CheckSurveyTakenBy(string pacode, decimal surveyId)
        {
            bool surveyTaken = false;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _survey = (from s in context.LBC_SURVERY_COMPLETED
                               where s.PA_CODE == pacode && s.SURVEY_ID == surveyId
                               select s).FirstOrDefault();
                if (_survey != null)
                {
                    surveyTaken = true;
                }
            }
            return surveyTaken;
        }

        /// <summary>
        /// Get survey number
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public List<SurveyMasterModel> getSurveyMaster(decimal surveyId)
        {
            List<SurveyMasterModel> list = new List<SurveyMasterModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from sm in context.LBC_SURVEY_MASTER
                          where sm.SURVEY_ID == surveyId
                          select new SurveyMasterModel 
                          { 
                            SURVEY_ID = sm.SURVEY_ID,
                            SURVEY_DESC = sm.SURVEY_DESC
                          };

                return lst.ToList<SurveyMasterModel>();
            }
        }

        /// <summary>
        /// Get survey questions
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public List<SurveyQuestionModel> getQuestions(decimal surveyId)
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from sq in context.LBC_SURVEY_QUESTIONS
                              where sq.SURVEY_ID == surveyId
                              select new SurveyQuestionModel 
                              { 
                                SURVEY_ID = sq.SURVEY_ID,
                                QUESTION_ID = sq.QUESTION_ID,
                                QUESTION_DESC = sq.QUESTION_DESC,
                                QUESTION_TYPE = sq.QUESTION_TYPE
                              };

                return lst.ToList<SurveyQuestionModel>();
            }
        }

        /// <summary>
        /// Get stock question answers
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public List<SurveyQuestionAnswerModel> getAnswers(decimal surveyId)
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from sq in context.LBC_SURVEY_QUESTIONS
                          from sqa in context.LBC_SURVEY_QUESTION_ANSWER.Where(sqa => sqa.QUESTION_ID == sq.QUESTION_ID)
                          where sq.SURVEY_ID == surveyId
                              select new SurveyQuestionAnswerModel 
                              { 
                                  QUESTION_ID = sqa.QUESTION_ID,
                                  ANSWER_ID = sqa.ANSWER_ID,
                                  ANSWER = sqa.ANSWER,
                                  INPUT_TYPE = sqa.INPUT_TYPE
                              };

                return lst.ToList<SurveyQuestionAnswerModel>();
            }
        }

        /// <summary>
        /// Get user answers
        /// </summary>
        /// <param name="pacode"></param>
        /// <returns></returns>
        public SurveyModel getUserAnswers(string pacode)
        {
            SurveyModel model = new SurveyModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var survey = from r in context.LBC_REGISTRATION
                              from s in context.LBC_SURVEY_TAKEN.Where(s => s.STARS_ID == r.PA_CODE).DefaultIfEmpty()
                              where r.PA_CODE.Equals(pacode)
                              select new SurveyModel();

                model = survey.SingleOrDefault(); 
            }
            return model;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SurveyQuestionModel> GetAllQuestions()
        { 
            SurveyQuestionModel model = new SurveyQuestionModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from q in context.LBC_SURVEY_QUESTIONS
                            join a in context.LBC_SURVEY_QUESTION_ANSWER on q.QUESTION_ID equals a.QUESTION_ID
                            orderby q.QUESTION_ID ascending
                            select new SurveyQuestionModel();

                return lst.ToList<SurveyQuestionModel>();
            }
        }

        public bool SaveSurvey(List<SurveyTakenModel> lst, SurveyCompledModel _model)
        {
            bool flag = false;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    foreach (var item in lst)
                    {

                        LBC_SURVEY_TAKEN _survey = new LBC_SURVEY_TAKEN()
                        {
                            STARS_ID = item.STARS_ID,
                            SURVEY_ID = item.SURVEY_ID,
                            QUESTION_ID = item.QUESTION_ID,
                            ANSWER_ID = item.ANSWER_ID,
                            ANSWER_MSG = item.ANSWER_MSG,
                            CREATED_BY = item.CREATED_BY,
                            CREATED_DATE = DateTime.Now
                        };
                        context.LBC_SURVEY_TAKEN.Add(_survey);

                    }
                    LBC_SURVERY_COMPLETED _survey_completed = new LBC_SURVERY_COMPLETED()
                    {
                        COMPLETED_DATE = DateTime.Now,
                        SURVEY_ID =_model.SURVEY_ID,
                        STARS_ID = _model.STARS_ID,
                        PA_CODE = _model.PA_CODE,
                        CREATED_DATE = _model.CREATED_DATE,
                        CREATED_BY = _model.CREATED_BY
                        
                   

                    };
                   
                    context.LBC_SURVERY_COMPLETED.Add(_survey_completed);

                    context.SaveChanges();
                    flag = true;
                }
            }

            catch (Exception ex)
            {
                flag = false;

            }

            return flag;
        }

        public List<SurveyQuestionModel> GetReportQuestions()
        {
            SurveyQuestionModel model = new SurveyQuestionModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from q in context.LBC_SURVEY_QUESTIONS
                          orderby q.QUESTION_ID ascending
                          select new SurveyQuestionModel
                          {
                              QUESTION_ID = q.QUESTION_ID,
                              QUESTION_DESC = q.QUESTION_DESC
                          };

                return lst.ToList<SurveyQuestionModel>();
            }
        }
        
        public List<SurveyReportModel> GetSurveyReport(decimal surveyId)
        {
            List<SurveyReportModel> lst = new List<SurveyReportModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                if ( surveyId != 0 )
                {
                    var list = (from entity in context.LBC_SURVEY_COMPLETED_VIEW
                                where entity.SURVEY_ID == surveyId
                                select entity).ToList();
                    foreach (LBC_SURVEY_COMPLETED_VIEW item in list)
                    {
                        lst.Add(MapModelFromLBC_SURVEY_COMPLETED_VIEW(item));
                    }
                }
                else
                {
                    var list = (from entity in context.LBC_SURVEY_COMPLETED_VIEW
                                select entity).ToList();
                    foreach (LBC_SURVEY_COMPLETED_VIEW item in list)
                    {
                        lst.Add(MapModelFromLBC_SURVEY_COMPLETED_VIEW(item));
                    }
                }
            }
            foreach (SurveyReportModel item in lst)
            {
                SurveyRepository _recognition = new SurveyRepository();
                item.QUESTIONS = _recognition.GetReportQuestions();
            }
            return lst;
        }

        public List<SurveyReportModel> GetSurveyReportByIdList(List<decimal> SurveyIdList)
        {
            List<SurveyReportModel> lst = new List<SurveyReportModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var list = (from entity in context.LBC_SURVEY_COMPLETED_VIEW
                            select entity).ToList();
                foreach (LBC_SURVEY_COMPLETED_VIEW item in list)
                {
                    lst.Add(MapModelFromLBC_SURVEY_COMPLETED_VIEW(item));
                }
            }
            List<SurveyReportModel> result = new List<SurveyReportModel>();

            if (SurveyIdList != null)
            {
                for (int i = 0; i < SurveyIdList.Count(); i++)
                {
                    var entry = lst.Where(q => q.SURVEY_ID == SurveyIdList[i]).FirstOrDefault();
                    if (entry != null)
                    {
                        result.Add(entry);
                    }
                }
            }
            else
            {
                result = lst;
            }

            
            foreach (SurveyReportModel item in result)
            {
                SurveyRepository _recognition = new SurveyRepository();
                item.QUESTIONS = _recognition.GetReportQuestions();
            }

            return result;
        }

        private SurveyReportModel MapModelFromLBC_SURVEY_COMPLETED_VIEW(LBC_SURVEY_COMPLETED_VIEW entity)
        {
            SurveyReportModel model = new SurveyReportModel();
            if (entity != null)
            {
                model.SURVEY_ID = entity.SURVEY_ID;
                model.DATE = entity.DATE_COMPLETED;
                model.NAME = entity.Lincoln_Brand_Champion_Name;
                model.DLR_NAME = entity.DealerShip_Name;
                model.EVENT_SESSION = entity.Registration_Information;
                model.ANSWER1 = entity.Q1;
                model.ANSWER2 = entity.Q2;
                model.ANSWER3 = entity.Q3;
                model.ANSWER4 = entity.Q4;
                model.ANSWER5 = entity.Q5;
                model.ANSWER6 = entity.Q6;
                model.ANSWER7 = entity.Q7;
                model.ANSWER8 = entity.Q8;
                model.ANSWER9 = entity.Q9;
                model.ANSWER10 = entity.Q10;
                model.ANSWER11 = entity.Q11;
                model.ANSWER12 = entity.Q12;
                model.ANSWER13 = entity.Q13;
                model.ANSWER14 = entity.Q14;
                model.ANSWER15 = entity.Q15;
                model.ANSWER16 = entity.Q16;
            }
            return model;
        }
    }
}