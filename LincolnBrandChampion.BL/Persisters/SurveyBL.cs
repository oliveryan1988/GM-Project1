using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.DL.Repositories;
using LincolnBrandChampion.DL.Properties;
using LincolnBrandChampion.Model.Models;

namespace LincolnBrandChampion.BL.Persisters
{
    public class SurveyBL
    {
        public bool CheckSurveyTakenBy(string pacode, decimal surveyId)
        {
            SurveyRepository _survey = new SurveyRepository();
            return _survey.CheckSurveyTakenBy(pacode, surveyId);
        }
        public List<SurveyMasterModel> getSurveyMaster(decimal surveyId)
        {
            SurveyRepository _survey = new SurveyRepository();
            return _survey.getSurveyMaster(surveyId);
        }
        public List<SurveyQuestionModel> getQuestions(decimal surveyId)
        {
            SurveyRepository _survey = new SurveyRepository();
            return _survey.getQuestions(surveyId);
        }
        public List<SurveyQuestionAnswerModel> getAnswers(decimal surveyId)
        {
            SurveyRepository _survey = new SurveyRepository();
            return _survey.getAnswers(surveyId);
        }
        public SurveyModel getUserAnswers(string pacode)
        {
            SurveyRepository _survey = new SurveyRepository();
            return _survey.getUserAnswers(pacode);
        }
        public List<SurveyQuestionModel> GetAllQuestions()
        {
            SurveyRepository _survey = new SurveyRepository();
            return _survey.GetAllQuestions();
        }
        //public List<SurveyQuestionModel> GetReportQuestions()
        //{
        //    SurveyRepository _survey = new SurveyRepository();
        //    return _survey.GetReportQuestions();
        //}
        public bool SaveSurvey(List<SurveyTakenModel> lst, SurveyCompledModel _model)
        {
            SurveyRepository _survey = new SurveyRepository();
           return _survey.SaveSurvey(lst, _model);
        }
        public List<SurveyReportModel> GetSurveyReport(decimal surveyId)
        {
            SurveyRepository _survey = new SurveyRepository();
            return _survey.GetSurveyReport(surveyId);
        }

        public List<SurveyReportModel> GetSurveyReportByIdList(List<decimal> SurveyIdList)
        {
            SurveyRepository _survey = new SurveyRepository();
            return _survey.GetSurveyReportByIdList(SurveyIdList);
        }
    }
}