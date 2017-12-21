using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LincolnBrandChampion.Web.Helpers;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.BL.Persisters;

namespace LincolnBrandChampion.Web.Controllers

{
    public class ResourcesController : Controller
    {
        //
        // GET: /Resources/
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult Index()
        {
            ViewBag.homeid = "metrics";
            return View();
        }
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult Survey()
        {
            ViewBag.homeid = "survey";
            if (Convert.ToInt32(Session["ROLE_ID"]) == 1)
            {
                if (Session["StarsIdProfile"] != null)
                {
                    var starsId = Session["StarsIdProfile"].ToString();
                    string pacode = Session["w_pacode"].ToString();
                    decimal surveyId = 1;
                    SurveyBL _surBL = new SurveyBL();
                    RegistrationBL _regBL = new RegistrationBL();

                    if (_surBL.CheckSurveyTakenBy(pacode, surveyId))
                    {
                        return RedirectToAction("Completed", "Resources");
                    }
                    else if (_regBL.CheckRegistrationBy(starsId))
                    {
                        SurveyModel model = new SurveyModel();
                        model.STARS_ID = pacode;
                        model.surveyMasterList = _surBL.getSurveyMaster(surveyId);
                        model.surveyQuestionList = _surBL.getQuestions(surveyId);
                        model.surveyQuestionAnswerList = _surBL.getAnswers(surveyId);
                        return View(model);
                    }
                    else
                    {
                        return RedirectToAction("Register", "Resources");
                    }
                }
                else
                { 
                    return RedirectToAction("Welcome", "LBC"); 
                }
            }
            else
            {
                return RedirectToAction("Welcome", "LBC");
            }
        }
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        [HttpPost]
        public ActionResult Survey(FormCollection frm)
        {
            
            SurveyBL _surBL = new SurveyBL();

            decimal surveyId = 1; // Make sure you get the Survey Id from the table or session
            List<SurveyQuestionModel> lstQuestions = _surBL.getQuestions(surveyId);
            
            List<SurveyTakenModel> lst = new List<SurveyTakenModel>();
           
            
            var starsId = Session["StarsIdProfile"].ToString();
            var pacode = Session["w_pacode"].ToString();
            var wslx = Session["w_user"].ToString();


            for (int i = 0 ; i <= frm.Count -1; i++)
            {
                string  type = "";
                //string test1 = frm.GetValue(Convert.ToString(frm[i])).AttemptedValue;
               // string[] arr1 = frm.GetValues(i);
                foreach (var item in lstQuestions)
                {
                    if (item.QUESTION_ID == Convert.ToDecimal(frm.GetKey(i)))
                    {
                        type = item.QUESTION_TYPE;
                        break;
                    
                    }
                
                }
                if (type == "FI")
                {
                    string[] arr = frm.GetValue(Convert.ToString(frm.GetKey(i))).AttemptedValue.Split(',');
                    lst.Add(new SurveyTakenModel()
                    {
                        STARS_ID = starsId,
                        SURVEY_ID = surveyId,
                        QUESTION_ID = Convert.ToDecimal(frm.GetKey(i).ToString()),
                        ANSWER_ID = 1,
                        ANSWER_MSG = arr[0].ToString(),
                        CREATED_BY = wslx
                    });
                    
                    
                  
                }
                else if (type == "MS")
                {
                    string[] arr = frm.GetValue(Convert.ToString(frm.GetKey(i))).AttemptedValue.Split(',');
                    if (arr.Count() > 0)
                    {

                        for (int j = 0; j <= arr.Count() - 1; j = j + 2)
                        {

                            lst.Add(new SurveyTakenModel()
                            {

                                STARS_ID = starsId,
                                SURVEY_ID = surveyId,
                                QUESTION_ID = Convert.ToDecimal(arr[j].ToString()),
                                ANSWER_ID = Convert.ToDecimal(arr[j + 1].ToString()),
                                ANSWER_MSG = string.Empty,
                                CREATED_BY = wslx
                            });


                        }
                    }
                
                }

                else
                {
                    string[] arr = frm.GetValue(Convert.ToString(frm.GetKey(i))).AttemptedValue.Split(',');

                    lst.Add(new SurveyTakenModel()
                        {

                            STARS_ID = starsId,
                            SURVEY_ID = surveyId,
                            QUESTION_ID = Convert.ToDecimal(arr[0].ToString()),
                            ANSWER_ID = Convert.ToDecimal(arr[1].ToString()),
                            ANSWER_MSG = string.Empty,
                             CREATED_BY = wslx
                        });

                }
            }
            SurveyCompledModel _surveyModel = new SurveyCompledModel();
            _surveyModel.COMPLETED_DATE = DateTime.Now;
            _surveyModel.PA_CODE = pacode;
            _surveyModel.STARS_ID = starsId;
            _surveyModel.SURVEY_ID = surveyId;
            _surveyModel.CREATED_DATE = DateTime.Now;
            _surveyModel.CREATED_BY = wslx;

            if (_surBL.SaveSurvey(lst, _surveyModel))
             return RedirectToAction("Confirmation", "Resources");
            return RedirectToAction("Survey", "Resources");
            //return RedirectToAction("Welcome", "LBC");
        }
        public ActionResult Confirmation()
        {
            ViewBag.homeid = "survey";
            return View();
        }
        public ActionResult Completed()
        {
            ViewBag.homeid = "survey";
            return View();
        }
        public ActionResult Register()
        {
            ViewBag.homeid = "survey";
            return View();
        }
/// <summary>
/// Tracking the file downloads
/// </summary>
/// <param name="file"></param>
/// <returns></returns>
/// 
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult Clicks(string file)
        {
            if (Session["StarsIdProfile"] != null)
            {

                ResourceBL _resBL = new ResourceBL();
                ResourceTracking resModel = new ResourceTracking();

                string filename = "";
                int Position = file.LastIndexOf("/");
                filename = file.Substring(Position + 1);


                // CheckpointModel chkpPModel = new CheckpointModel();
                // chkpPModel = _chkBL.GetActiveCheckpoint();
                resModel.SECTION = "PDF";// Convert.ToDecimal(Session["CheckId"].ToString());
                resModel.CREATED_DATE = DateTime.Now;
                resModel.DOWNLOAD_TIME = DateTime.Now;
                resModel.CREATED_BY = Session["w_user"].ToString().ToUpper();
                resModel.DOC_URL = file;
                resModel.FILE_NAME = filename;
                resModel.WSL_ID = Session["w_user"].ToString().ToUpper();
                resModel.PA_CODE = (Session["w_pacode"] != null) ? Session["w_pacode"].ToString() : string.Empty;
                resModel.STARS_ID = (Session["StarsIdProfile"] != null) ? Session["StarsIdProfile"].ToString() : null;

                _resBL.SaveResourceTracking(resModel);
                //saveClick();
            }
            return Json(new
            {
                //redirectUrl = Url.Action("Presentation", "Toolkit"),
                isRedirect = true,
                JsonRequestBehavior.AllowGet
            });
        }
	}
}