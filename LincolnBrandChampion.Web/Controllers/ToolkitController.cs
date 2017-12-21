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
    public class ToolkitController : Controller
    {
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers)]
        public ActionResult Index()
        {
            CheckpointBL _chkBL = new CheckpointBL();
            CheckpointModel chkModel = new CheckpointModel();
            chkModel = _chkBL.GetActiveCheckpoint();
            chkModel.CHECKPOINTS = _chkBL.GetInactiveCheckpoints();

            if (Session["StarsIdProfile"] != null)
            {
                Session["CheckId"] = chkModel.CHECKPOINT_ID;
                return View(chkModel);
            }
            return RedirectToAction("Welcome", "LBC");
        }

        [AuthorizedAttributeHelper(LBC_Role.LBCDealers)]
        public ActionResult SelfStudy()
        {
            CheckpointBL _chkBL = new CheckpointBL();
            CheckpointModel chkModel = new CheckpointModel();
            chkModel = _chkBL.GetActiveCheckpoint();

            if (Session["StarsIdProfile"] != null)
            {
                string pacode = Session["w_pacode"].ToString();
                decimal checkpointId = chkModel.CHECKPOINT_ID;

                if (_chkBL.CheckCheckpointCompleted(pacode, checkpointId))
                {
                    return PartialView("_Completed", chkModel);
                }
                if (chkModel.CHECKPOINT_ID == 1)
                { return PartialView("_SelfStudy"); }
                if (chkModel.CHECKPOINT_ID == 2)
                { return PartialView("_SelfStudy2"); }
                
            }
            return RedirectToAction("Welcome", "LBC");
        }

        #region Game
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers)]
        public ActionResult Game()
        {
            if (Session["StarsIdProfile"] != null)
            {
                CheckpointBL _chkBL = new CheckpointBL();
                CheckpointCompletedModel chkModel = new CheckpointCompletedModel();
                chkModel = _chkBL.GetUserEmail(Session["StarsIdProfile"].ToString());
                return PartialView("_Game", chkModel);
            }
            return RedirectToAction("Welcome", "LBC");
        }

        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers)]
        public ActionResult Game(FormCollection frm)
        {
            CheckpointCompletedModel chkModel = new CheckpointCompletedModel();
            chkModel.EMAIL_ID = frm["email"];
            chkModel.STARS_ID = frm["STARS_ID"];
            chkModel.PA_CODE = frm["PA_CODE"];
            Session["email"] = frm["email"];
            CheckpointBL _chkBL = new CheckpointBL();
            _chkBL.SaveUserEmail(chkModel);

            return Json(new
            {
                redirectUrl = Url.Action("PhysicalTool", "Toolkit"),
                isRedirect = true
            });
        }
        #endregion

        #region Physical Tool
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers)]
        public ActionResult PhysicalTool()
        {
            RegistrationBL _reg = new RegistrationBL();
            if (Session["StarsIdProfile"] != null)
            {
                ViewBag.IsSelect = false;
                if (_reg.CheckIsSelectBy(Session["StarsIdProfile"].ToString()))
                { ViewBag.IsSelect = true; }
                if (Session["CheckId"].ToString() == "1")
                {
                    return PartialView("_PhysicalTool");
                }
                if (Session["CheckId"].ToString() == "2")
                {
                    return PartialView("_PhysicalTool2");
                }
            }
            return RedirectToAction("Welcome", "LBC");
        }

        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.Super_Admin)]
        public ActionResult PhysicalTool(FormCollection frm )
        {
            Session["tool"]=frm["radio"];
            
            if (Session["CheckId"].ToString() == "1")
            {
                return Json(new
                {
                    redirectUrl = Url.Action("Presentation", "Toolkit"),
                    isRedirect = true
                });
            }
            if (Session["CheckId"].ToString() == "2")
            {
                if (Session["StarsIdProfile"] != null)
                {
                    CheckpointBL _chkBL = new CheckpointBL();
                    CheckpointCompletedModel chkModel = new CheckpointCompletedModel();
                    chkModel = _chkBL.GetUserEmail(Session["StarsIdProfile"].ToString());
    //
                    CheckpointModel chkpModel = new CheckpointModel();
                    chkpModel = _chkBL.GetActiveCheckpoint();

                    chkModel.STARS_ID = Session["StarsIdProfile"].ToString();
                    chkModel.PA_CODE = Session["w_pacode"].ToString();
                    chkModel.CHECKPOINT_ID = chkpModel.CHECKPOINT_ID;//Convert.ToDecimal(Session["CheckId"].ToString());
                    //chkModel.EMAIL_ID = Session["email"].ToString();
                    chkModel.TOOL_ORDERED = Session["tool"].ToString();
                    //

                    //
               
                    chkModel.CREATED_BY = Session["w_user"].ToString().ToUpper();
                    chkModel.CREATED_DATE = DateTime.Now;

                    if (!(_chkBL.CheckCheckpointCompleted(chkModel.PA_CODE, chkModel.CHECKPOINT_ID)))
                    {
                        _chkBL.SaveCheckpointOrder(chkModel);
                        EmailHelper.SendCheckpointEmail(chkModel.EMAIL_ID, chkModel.TOOL_ORDERED);
                    }
                }
                return Json(new
                {
                    redirectUrl = Url.Action("Completed", "Toolkit"),
                    isRedirect = true
                });
            }
            return RedirectToAction("Welcome", "LBC");
        }
        #endregion

        #region Presentation
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers)]
        public ActionResult Presentation()
        {            
            if (Session["StarsIdProfile"] != null)
            {
                CheckpointBL _chkBL = new CheckpointBL();
                CheckpointModel chkpModel = new CheckpointModel();
                chkpModel = _chkBL.GetActiveCheckpoint();
                CheckpointCompletedModel chkModel = new CheckpointCompletedModel();
                chkModel.STARS_ID = Session["StarsIdProfile"].ToString();
                chkModel.PA_CODE = Session["w_pacode"].ToString();
                chkModel.CHECKPOINT_ID = chkpModel.CHECKPOINT_ID;//Convert.ToDecimal(Session["CheckId"].ToString());
                chkModel.EMAIL_ID = Session["email"].ToString();
                chkModel.TOOL_ORDERED = Session["tool"].ToString();
                return PartialView("_Presentation",chkModel);
            }
            return RedirectToAction("Welcome", "LBC");
        }

        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers)]
        public ActionResult Presentation(FormCollection frm)
        {
            if (Session["StarsIdProfile"] != null)
            {
                CheckpointBL _chkBL = new CheckpointBL();
                CheckpointModel chkpPModel = new CheckpointModel();
                chkpPModel = _chkBL.GetActiveCheckpoint();
               
                CheckpointCompletedModel chkModel = new CheckpointCompletedModel();
                chkModel.STARS_ID = frm["STARS_ID"];
                chkModel.PA_CODE = frm["PA_CODE"];
                chkModel.CHECKPOINT_ID = chkpPModel.CHECKPOINT_ID;//Convert.ToDecimal(frm["CHECKPOINT_ID"]);
                chkModel.TOOL_ORDERED = frm["TOOL_ORDERED"];
                chkModel.EMAIL_ID = frm["EMAIL_ID"];
                chkModel.CREATED_BY = Session["w_user"].ToString().ToUpper();
                chkModel.CREATED_DATE = DateTime.Now;

                if (!(_chkBL.CheckCheckpointCompleted(chkModel.PA_CODE, chkModel.CHECKPOINT_ID)))
                {
                    _chkBL.SaveCheckpointOrder(chkModel);
                    EmailHelper.SendCheckpointEmail(chkModel.EMAIL_ID, chkModel.TOOL_ORDERED);
                    CheckpointReportBL _chkrBL = new CheckpointReportBL();
                    CheckpointReportModel chkrmodel = new CheckpointReportModel();
                    chkrmodel = _chkrBL.GetCheckpointInfoByStarsId(chkModel.STARS_ID);
                    //removed this as part of request: 20262 - 0024482 Dashboard Updates - 4.7.15
                    //EmailHelper.SendCheckpointEmailAdmin(chkrmodel);

                    return Json(new
                    {
                        redirectUrl = Url.Action("Completed", "Toolkit"),
                        isRedirect = true
                    });
                }
                return Json(new
                {
                    redirectUrl = Url.Action("Completed", "Toolkit"),
                    isRedirect = true
                });
            }
            return RedirectToAction("Welcome", "LBC");
        }
        #endregion

        [AuthorizedAttributeHelper(LBC_Role.LBCDealers)]
        public ActionResult Completed()
        {
            CheckpointBL _chkBL = new CheckpointBL();
            CheckpointModel chkModel = new CheckpointModel();
            chkModel = _chkBL.GetActiveCheckpoint();
            return PartialView("_Completed", chkModel);
        }

        [AuthorizedAttributeHelper(LBC_Role.LBCDealers)]
        public ActionResult Clicks(string file)
        {
            string filename = "";
            int Position = file.LastIndexOf("/");
            filename = file.Substring(Position + 1);
            CheckpointBL _chkBL = new CheckpointBL();
            CheckpointTracking chkModel = new CheckpointTracking();
            CheckpointModel chkpPModel = new CheckpointModel();
            chkpPModel = _chkBL.GetActiveCheckpoint();
            chkModel.CHECKPOINT_ID = chkpPModel.CHECKPOINT_ID;// Convert.ToDecimal(Session["CheckId"].ToString());
            chkModel.CREATED_DATE = DateTime.Now;
            chkModel.DOWNLOAD_TIME = DateTime.Now;
            chkModel.CREATED_BY = Session["w_user"].ToString().ToUpper();
            chkModel.DOC_URL = file;
            chkModel.FILE_NAME = filename;
            chkModel.PA_CODE = Session["w_pacode"].ToString();
            chkModel.STARS_ID = Session["StarsIdProfile"].ToString();
           
            _chkBL.SaveCheckpointTracking(chkModel);
            //saveClick();
            return Json(new
            {
                //redirectUrl = Url.Action("Presentation", "Toolkit"),
                isRedirect = true,
                JsonRequestBehavior.AllowGet
            });
        }
	}
}