using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.DL.Repositories;
using LincolnBrandChampion.Model.Models;

namespace LincolnBrandChampion.BL.Persisters
{
    public class LoginTrackingBL
    {
        public static long AddLOGIN_TRACKING(LoginTrackingModel model)
        {
            LoginTrackingRepository _logIn = new LoginTrackingRepository();
            return _logIn.AddLOGIN_TRACKING(model);
        }
    }
}
