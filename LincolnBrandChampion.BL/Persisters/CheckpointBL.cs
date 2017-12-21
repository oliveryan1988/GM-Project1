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
    public class CheckpointBL
    {
        public bool CheckCheckpointCompleted(string pacode, decimal checkpointId)
        {
            CheckpointRepository _chk = new CheckpointRepository();
            return _chk.CheckCheckpointCompleted(pacode, checkpointId);
        }
        public List<CheckpointModel> GetInactiveCheckpoints()
        {
            CheckpointRepository _chk = new CheckpointRepository();
            return _chk.GetInactiveCheckpoints();
        }
        public CheckpointModel GetActiveCheckpoint()
        {
            CheckpointRepository _chk = new CheckpointRepository();
            return _chk.GetActiveCheckpoint();
        }
        public CheckpointCompletedModel GetUserEmail(string starsId)
        {
            CheckpointRepository _chk = new CheckpointRepository();
            return _chk.GetUserEmail(starsId);
        }
        public void SaveUserEmail(CheckpointCompletedModel model)
        {
            CheckpointRepository _chk = new CheckpointRepository();
            _chk.SaveUserEmail(model);
        }
        public void SaveCheckpointOrder(CheckpointCompletedModel model)
        {
            CheckpointRepository _chk = new CheckpointRepository();
            _chk.SaveCheckpointOrder(model);
        }
        public void SaveCheckpointTracking(CheckpointTracking model)
        {
            CheckpointRepository _chk = new CheckpointRepository();
            _chk.SaveCheckpointTracking(model);
        }
    }
}
