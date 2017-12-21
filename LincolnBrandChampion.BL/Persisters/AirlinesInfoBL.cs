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
    public class AirlinesInfoBL
    {
        public List<AirlinesInfoModel> GetAll()
        {
            AirlinesInfoRepository _air = new AirlinesInfoRepository();
            return _air.GetAllAirlines();
        }
        public string getTerminal(string terminal)
        {
            AirlinesInfoRepository _air = new AirlinesInfoRepository();
            return _air.getTerminal(terminal);
        }

    }
}
