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
    public class StoryCategoryRepository
    {
        /// <summary>
        /// Get all the Events
        /// </summary>
        /// <returns></returns>

        public List<StoryCategoryModel> GetAllCategory()
        {
            List<StoryCategoryModel> list = new List<StoryCategoryModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from entity in context.LBC_STORY_CATEGORY
                          select new StoryCategoryModel
                          {
                              SEQ_ID = entity.SEQ_ID,
                              CATEG_NAME = entity.CATEG_NAME,
                          };


                return lst.ToList<StoryCategoryModel>();
            }
        }
        /// <summary>
/// Get the event by ID
/// </summary>
/// <param name="EventId"></param>
/// <returns></returns>
        public List<StoryCategoryModel> GetCategoryByID(Decimal SEQ_ID)
        {
            List<StoryCategoryModel> list = new List<StoryCategoryModel>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from ev in context.LBC_STORY_CATEGORY
                          where ev.SEQ_ID == SEQ_ID
                          select new StoryCategoryModel
                          {
                              CATEG_NAME = ev.CATEG_NAME,

                          };

                return lst.ToList<StoryCategoryModel>();
            }
        }


        public StoryCategoryModel GetStoryCategoryModelByID(decimal SEQ_ID)
        {
            StoryCategoryModel model = new StoryCategoryModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from ev in context.LBC_STORY_CATEGORY
                          where ev.SEQ_ID == SEQ_ID
                          select new StoryCategoryModel
                          {
                              SEQ_ID = ev.SEQ_ID,
                              CATEG_NAME = ev.CATEG_NAME,

                          };

                model = lst.SingleOrDefault();
            }
            return model;
        }
        /// <summary>
        /// Update the event count to check the max;
        /// </summary>
        /// <param name="model"></param>
        public void UpdateEventCount(StoryCategoryModel model)
        {
           // LBC_STORY_CATEGORY _event = new LBC_STORY_CATEGORY();
           // bool flag = false;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _event = (from p in context.LBC_STORY_CATEGORY
                                  where p.SEQ_ID == model.SEQ_ID
                                  select p).FirstOrDefault();

                    _event.CATEG_NAME = model.CATEG_NAME;
                    context.SaveChanges();

                }
            }

            catch(Exception ex)
            {
                throw ex;
            
            }
        }
        /// <summary>
        /// Method to decrease the event count when the they cancel
        /// </summary>
        /// <param name="model"></param>

    }
}
