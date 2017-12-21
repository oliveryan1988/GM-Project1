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
    public class CheckpointReportBL
    {
        public List<CheckpointReportModel> GetCheckpointReportBy(decimal checkpointId)
        {
            CheckpointRepository _chkReport = new CheckpointRepository();
            return _chkReport.GetCheckpointReportBy(checkpointId);
        }
        
        public CheckpointReportModel GetCheckpointInfoByStarsId(string starsId)
        {
            CheckpointRepository _chk = new CheckpointRepository();
            return _chk.GetCheckpointInfoByStarsId(starsId);
        }

        public List<CheckpointReportModel> GetCheckpointReportByStarsIdList(List<string> StarsIdList = null, List<decimal> ChkIdList = null)
        {
            CheckpointRepository _chkReport = new CheckpointRepository();
            return _chkReport.GetCheckpointReportByStarsIdList(StarsIdList, ChkIdList);
        }
        
        public bool UpdateCheckpointStatus(CheckpointReportModel model)
        {
            CheckpointRepository _chk = new CheckpointRepository();
            return _chk.UpdateCheckpointStatus(model);
        }
    }
}
