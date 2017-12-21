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
    public class UserRepository
    {
        public UserModel GetUserBy(string wslxId)
        {
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _user = (from us in context.LBC_USERS
                                 where us.USR_WSLX_ID.ToUpper() == wslxId.ToUpper()
                                 select us).FirstOrDefault();

                    return MapModelFromLBC_USERS(_user);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            
            }

        }

        private UserModel MapModelFromLBC_USERS(LBC_USERS user)
        {
            UserModel model = new UserModel();
            if (user != null)
            {
                model.CREATED_BY = user.CREATED_BY;
                model.CREATED_DATE = user.CREATED_DATE;
                //model.LBC_USER_ROLE = new UserRolModel(){} 
                model.UPDATE_DATE = user.UPDATE_DATE;
                model.UPDATED_BY = user.UPDATED_BY;
                model.USR_FIRST_NAME = user.USR_FIRST_NAME;
                model.USR_LAST_NAME = user.USR_LAST_NAME;
                model.USR_ROLE_ID = user.USR_ROLE_ID;
                model.USR_STATUS = user.USR_STATUS;
                model.USR_WSLX_ID = user.USR_WSLX_ID;

            }
            return model;
        }
    }

    public class UserRoleRepository
    {
 
    }
}
