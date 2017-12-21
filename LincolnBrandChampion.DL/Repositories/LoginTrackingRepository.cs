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
    public class LoginTrackingRepository
    {
        public  long AddLOGIN_TRACKING(LoginTrackingModel model)
        {
            try
            {
                LBC_LOGIN_TRACKING _login = new LBC_LOGIN_TRACKING();
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    model.SEQ_ID = GetNextSeqLoginTracking();
                    _login = MapLBCLogInTrackingFromModel(model);
                    context.LBC_LOGIN_TRACKING.Add(_login);
                    context.SaveChanges();
                    
                }
                return 1;
            }
            catch (Exception ex)
            {
                var exdata = ex.Data.ToString();
                return 0;
            }
        }

        private decimal GetNextSeqLoginTracking()
        {
            decimal seq = -1;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _seq = (from s in context.LBC_LOGIN_TRACKING
                            select s.SEQ_ID).Max()+1;

                seq = _seq != null ? _seq : seq;
            }
            return seq;
        }

        private LBC_LOGIN_TRACKING MapLBCLogInTrackingFromModel(LoginTrackingModel model)
        {
            LBC_LOGIN_TRACKING entity = new LBC_LOGIN_TRACKING();
            if (model != null)
            {
                entity.ACI = model.ACI;
                entity.CREATE_DATE = model.CREATE_DATE;
                entity.CREATED_BY = model.CREATED_BY;
                entity.EMPCODE = model.EMPCODE;
                entity.LOGIN_DATE = model.LOGIN_DATE;
                entity.LOGIN_TIME = model.LOGIN_TIME;
                entity.MRROLE = model.MRROLE;
                entity.ORG = model.ORG;
                entity.ORGCODE = model.ORGCODE;
                entity.SEQ_ID = model.SEQ_ID;
                entity.SITE = model.SITE;
                entity.UPDATE_DATE = model.UPDATE_DATE;
                entity.UPDATED_BY = model.UPDATED_BY;
                entity.USERID = model.USERID;


            }
            return entity;
        }
    }
}
