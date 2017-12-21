using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.DL.Data;
using LincolnBrandChampion.DL.Helpers;
using System.Data.Objects.SqlClient;

namespace LincolnBrandChampion.DL.Repositories
{
    public class CheckpointRepository
    {
        /// <summary>
        /// Check checkpoint orders by pacode, checkpointId
        /// </summary>
        /// <param name="pacode"></param>
        /// <param name="checkpointId"></param>
        /// <returns></returns>
        public bool CheckCheckpointCompleted( string pacode, decimal checkpointId) 
        {
            bool havePaCode = false;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _chkComp = (from cc in context.LBC_CHECKPOINT_COMPLETED
                                where cc.PA_CODE == pacode && cc.CHECKPOINT_ID == checkpointId
                                select cc).FirstOrDefault();
                if (_chkComp != null)
                {
                    havePaCode = true;
                }
            }
            return havePaCode;
        }

        /// <summary>
        /// Get currently inactive checkpoints
        /// </summary>
        /// <returns></returns>
        public List<CheckpointModel> GetInactiveCheckpoints()
        {
            List<CheckpointModel> list = new List<CheckpointModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from entity in context.LBC_CHECKPOINT
                          //where (entity.CHK_START_DATE > DateTime.Now && entity.CHK_END_DATE > DateTime.Now) || (entity.CHK_START_DATE < DateTime.Now && entity.CHK_END_DATE < DateTime.Now)
                          select new CheckpointModel
                          {
                              CHECKPOINT_ID = entity.CHECKPOINT_ID,
                              CHK_DESC = entity.CHK_DESC,
                              CHK_START_DATE = entity.CHK_START_DATE,
                              CHK_END_DATE = entity.CHK_END_DATE,
                              CHK_TITLE = entity.CHK_TITLE,
                              IMAGE_NAME = entity.IMAGE_NAME
                          };
                list = lst.ToList();
                return list;
            }
        }

        /// <summary>
        /// Get active checkpoint
        /// </summary>
        /// <returns></returns>
        public CheckpointModel GetActiveCheckpoint()
        {
            CheckpointModel model = new CheckpointModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var chk = from entity in context.LBC_CHECKPOINT
                          where (entity.CHK_START_DATE < DateTime.Now && entity.CHK_END_DATE > DateTime.Now)
                          select new CheckpointModel
                          {
                              CHECKPOINT_ID = entity.CHECKPOINT_ID,
                              CHK_DESC = entity.CHK_DESC,
                              CHK_START_DATE = entity.CHK_START_DATE,
                              CHK_END_DATE = entity.CHK_END_DATE,
                              CHK_TITLE = entity.CHK_TITLE,
                              IMAGE_NAME = entity.IMAGE_NAME
                          };
                model = chk.SingleOrDefault();
            }
            return model;
        }

        /// <summary>
        /// Get user email by stars id
        /// </summary>
        /// <param name="starsId"></param>
        /// <returns></returns>
        public CheckpointCompletedModel GetUserEmail(string starsId)
        {
            CheckpointCompletedModel model = new CheckpointCompletedModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var email = from entity in context.LBC_PROFILE
                          where (entity.STARS_ID == starsId)
                          select new CheckpointCompletedModel
                          {
                              EMAIL_ID = entity.EMAIL_ID,
                              STARS_ID = entity.STARS_ID,
                              PA_CODE = entity.PA_CODE
                          };
                model = email.SingleOrDefault();
            }
            return model;
        }

        /// <summary>
        /// Method to Update the user email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveUserEmail(CheckpointCompletedModel model)
        {
            bool save = true;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _email = (from p in context.LBC_PROFILE
                                         where p.STARS_ID == model.STARS_ID 
                                         select p).FirstOrDefault();
                    _email.EMAIL_ID = model.EMAIL_ID;
                    _email.UPDATE_DATE = DateTime.Now;
                    _email.UPDATED_BY = model.UPDATED_BY;
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw ex;
                // save = false;
            }
            return save;
        }

        /// <summary>
        /// Saves the user entered checkpoint info.
        /// </summary>
        /// <param name="model"></param>
        public void SaveCheckpointOrder(CheckpointCompletedModel model)
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                LBC_CHECKPOINT_COMPLETED _checkOrder = new LBC_CHECKPOINT_COMPLETED()
                {
                    STARS_ID = model.STARS_ID,
                    PA_CODE = model.PA_CODE,
                    SHIPPING_STATUS = "Submitted",
                    CHECKPOINT_ID = model.CHECKPOINT_ID,
                    EMAIL_ID = model.EMAIL_ID,
                    TOOL_ORDERED = model.TOOL_ORDERED,
                    CREATED_DATE = model.CREATED_DATE,
                    CREATED_BY = model.CREATED_BY
                };
                context.LBC_CHECKPOINT_COMPLETED.Add(_checkOrder);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Get info for checkpoint report, can be filtered by checkpoint id.
        /// </summary>
        /// <param name="checkpointId"></param>
        /// <returns>List of Checkpoint report models</returns>
        public List<CheckpointReportModel> GetCheckpointReportBy(decimal checkpointId)
        {
            List<CheckpointReportModel> list = new List<CheckpointReportModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                if (checkpointId != 0)
                {
                    var lst = from pr in context.LBC_CHECKPOINT_VW
                              where pr.CHECKPOINT_ID == checkpointId
                              select new CheckpointReportModel
                              {
                                  STARS_ID = pr.STARS_ID,
                                  PA_CODE = pr.PA_CODE,
                                  FIRST_NAME = pr.FIRST_NAME,
                                  LAST_NAME = pr.LAST_NAME,
                                  DLR_NAME = pr.DLR_NAME,
                                  DLR_ADDRESS = pr.DLR_ADDRESS,
                                  DLR_CITY = pr.DLR_CITY,
                                  DLR_STATE = pr.DLR_STATE,
                                  DLR_ZIP = pr.DLR_ZIP,
                                  DLR_PHONE = pr.DLR_PHONE,
                                  TOOL_ORDERED = pr.TOOL_ORDERED,
                                  CREATED_DATE = pr.CREATED_DATE,
                                  CHECKPOINT_ID = pr.CHECKPOINT_ID,
                                  EMAIL_ID = pr.EMAIL_ID,
                                  SHIPPING_STATUS = pr.SHIPPING_STATUS,
                                  SELECT_CONTACT_DLR = pr.SELECT_CONTACT_DLR,
                                  Checkpoint1SelfStudyPDF = pr.Checkpoint1SelfStudyPDF,
                                  Checkpoint1SelfStudyPPTX = pr.Checkpoint1SelfStudyPPTX,
                                  CheckPoint1TeamMeetingPDF = pr.CheckPoint1TeamMeetingPDF,
                                  CheckPoint1TeamMeetingPPTX = pr.CheckPoint1TeamMeetingPPTX,
                                  STARS_COMPLETION = pr.Status
                              };
                    return lst.ToList<CheckpointReportModel>();
                }
                else
                {
                    var lst = from pr in context.LBC_CHECKPOINT_VW                              
                              select new CheckpointReportModel
                              {
                                  STARS_ID = pr.STARS_ID,
                                  PA_CODE = pr.PA_CODE,
                                  FIRST_NAME = pr.FIRST_NAME,
                                  LAST_NAME = pr.LAST_NAME,
                                  DLR_NAME = pr.DLR_NAME,
                                  DLR_ADDRESS = pr.DLR_ADDRESS,
                                  DLR_CITY = pr.DLR_CITY,
                                  DLR_STATE = pr.DLR_STATE,
                                  DLR_ZIP = pr.DLR_ZIP,
                                  DLR_PHONE = pr.DLR_PHONE,
                                  TOOL_ORDERED = pr.TOOL_ORDERED,
                                  CREATED_DATE = pr.CREATED_DATE,
                                  CHECKPOINT_ID = pr.CHECKPOINT_ID,
                                  EMAIL_ID = pr.EMAIL_ID,
                                  SHIPPING_STATUS = pr.SHIPPING_STATUS,
                                  SELECT_CONTACT_DLR = pr.SELECT_CONTACT_DLR,
                                  Checkpoint1SelfStudyPDF = pr.Checkpoint1SelfStudyPDF,
                                  Checkpoint1SelfStudyPPTX = pr.Checkpoint1SelfStudyPPTX,
                                  CheckPoint1TeamMeetingPDF = pr.CheckPoint1TeamMeetingPDF,
                                  CheckPoint1TeamMeetingPPTX = pr.CheckPoint1TeamMeetingPPTX,
                                  STARS_COMPLETION = pr.Status
                              };
                    return lst.ToList<CheckpointReportModel>();
                }
            }
        }
        /// <summary>
        /// Get info for checkpoint report, can be filtered by checkpoint id and stars id.
        /// </summary>
        /// <param name="StarsIdList"></param>
        /// <param name="ChkIdList"></param>
        /// <returns>List of Checkpoint report models</returns>
        public List<CheckpointReportModel> GetCheckpointReportByStarsIdList(List<string> StarsIdList = null, List<decimal> ChkIdList = null)
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {

                List<CheckpointReportModel> lst = (from pr in context.LBC_CHECKPOINT_VW
                        select new CheckpointReportModel
                        {
                            STARS_ID = pr.STARS_ID,
                            PA_CODE = pr.PA_CODE,
                            FIRST_NAME = pr.FIRST_NAME,
                            LAST_NAME = pr.LAST_NAME,
                            DLR_NAME = pr.DLR_NAME,
                            DLR_ADDRESS = pr.DLR_ADDRESS,
                            DLR_CITY = pr.DLR_CITY,
                            DLR_STATE = pr.DLR_STATE,
                            DLR_ZIP = pr.DLR_ZIP,
                            DLR_PHONE = pr.DLR_PHONE,
                            TOOL_ORDERED = pr.TOOL_ORDERED,
                            CREATED_DATE = pr.CREATED_DATE,
                            CHECKPOINT_ID = pr.CHECKPOINT_ID,
                            EMAIL_ID = pr.EMAIL_ID,
                            SHIPPING_STATUS = pr.SHIPPING_STATUS,
                            SELECT_CONTACT_DLR = pr.SELECT_CONTACT_DLR,
                            Checkpoint1SelfStudyPDF = pr.Checkpoint1SelfStudyPDF,
                            Checkpoint1SelfStudyPPTX = pr.Checkpoint1SelfStudyPPTX,
                            CheckPoint1TeamMeetingPDF = pr.CheckPoint1TeamMeetingPDF,
                            CheckPoint1TeamMeetingPPTX = pr.CheckPoint1TeamMeetingPPTX,
                            STARS_COMPLETION = pr.Status
                        }).ToList();

                List<CheckpointReportModel> result = new List<CheckpointReportModel>();
                if (StarsIdList != null && ChkIdList != null)
                {
                    for (int i = 0; i < StarsIdList.Count(); i++)
                    {
                        var entry = lst.Where(q => q.STARS_ID == StarsIdList[i] && q.CHECKPOINT_ID == ChkIdList[i]).FirstOrDefault();
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
                return result;
            }

        }
        /// <summary>
        /// Updates the shipping status in the checkpointCompleted entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCheckpointStatus(CheckpointReportModel model)
        {
            bool save = true;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _chk = (from lcc in context.LBC_CHECKPOINT_COMPLETED
                                where lcc.STARS_ID == model.STARS_ID && lcc.CHECKPOINT_ID == model.CHECKPOINT_ID
                                select lcc).FirstOrDefault();

                    _chk.SHIPPING_STATUS = model.SHIPPING_STATUS;
                    _chk.UPDATED_DATE = DateTime.Now;
                    _chk.UPDATED_BY = System.Web.HttpContext.Current.Session["w_user"].ToString();

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                // save = false;
            }
            return save;
        }

        /// <summary>
        /// Get individual rows for editing in the checkpoint report.
        /// </summary>
        /// <param name="StarsId"></param>
        /// <returns>Checkpoint Report Model</returns>
        public CheckpointReportModel GetCheckpointInfoByStarsId(string StarsId)
        {
            CheckpointReportModel model = new CheckpointReportModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from pr in context.LBC_PROFILE
                          join chc in context.LBC_CHECKPOINT_COMPLETED on pr.STARS_ID equals chc.STARS_ID
                          join ch in context.LBC_CHECKPOINT on chc.CHECKPOINT_ID equals ch.CHECKPOINT_ID
                          where pr.STARS_ID == StarsId
                          select new CheckpointReportModel
                          {
                              STARS_ID = pr.STARS_ID,
                              PA_CODE = pr.PA_CODE,
                              FIRST_NAME = pr.FIRST_NAME,
                              LAST_NAME = pr.LAST_NAME,
                              DLR_NAME = pr.DLR_NAME,
                              DLR_ADDRESS = pr.DLR_ADDRESS,
                              DLR_CITY = pr.DLR_CITY,
                              DLR_STATE = pr.DLR_STATE,
                              DLR_ZIP = pr.DLR_ZIP,
                              DLR_PHONE = pr.DLR_PHONE,
                              CHECKPOINT_ID = chc.CHECKPOINT_ID,
                              TOOL_ORDERED = chc.TOOL_ORDERED,
                              EMAIL_ID = chc.EMAIL_ID,
                              SHIPPING_STATUS = chc.SHIPPING_STATUS,
                              CREATED_DATE = chc.CREATED_DATE
                          };

                model = lst.SingleOrDefault();
            }
            return model;
        }
        /// <summary>
        /// Save the checkpoint tracking
        /// </summary>
        /// <param name="model"></param>
        public void SaveCheckpointTracking(CheckpointTracking model)
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                LBC_CHECKPOINT_TRACKING _checkpointTrack = new LBC_CHECKPOINT_TRACKING()
                {
                    STARS_ID = model.STARS_ID,
                    PA_CODE = model.PA_CODE,
                    DOC_URL = model.DOC_URL,
                    FILE_NAME = model.FILE_NAME,
                    DOWNLOAD_TIME = model.DOWNLOAD_TIME,
                    CHECKPOINT_ID = model.CHECKPOINT_ID,
                    CREATED_DATE = model.CREATED_DATE,
                    CREATED_BY = model.CREATED_BY
                };
                context.LBC_CHECKPOINT_TRACKING.Add(_checkpointTrack);
                context.SaveChanges();
            }
        }
}
    }

