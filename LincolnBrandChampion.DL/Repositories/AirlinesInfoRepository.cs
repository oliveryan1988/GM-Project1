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
    public class AirlinesInfoRepository
    {
        public List<AirlinesInfoModel> GetAllAirlines()
        {
            List<EventModel> list = new List<EventModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from entity in context.AIRLINES_INFO
                          select new AirlinesInfoModel
                          {
                              AIRLINE_CODE = entity.AIRLINE_CODE,
                              AIRLINE_NAME = entity.AIRLINE_NAME,
                              TERMINAL_CODE = entity.TERMINAL_CODE,
                              TERMINAL_NAME = entity.TERMINAL_NAME
                             

                          };


                return lst.ToList<AirlinesInfoModel>();
            }
        }

        public string getTerminal(string airline)
        {
            string term = "";
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = (from entity in context.AIRLINES_INFO
                          where entity.AIRLINE_CODE == airline
                          select entity.TERMINAL_NAME).FirstOrDefault();


                term= lst.ToString();
            }
            return term;
        }
    }
}
