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
    public class StorySubCategoryRepository
    {
        /// <summary>
        /// Get all the Events
        /// </summary>
        /// <returns></returns>

        public List<StorySubCategoryModel> GetAllSubCategory()
        {
            List<StorySubCategoryModel> list = new List<StorySubCategoryModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from entity in context.LBC_STORY_SUBCATEGORY
                          select new StorySubCategoryModel
                          {
                              SEQ_NO = entity.SEQ_NO,
                              SUB_CAT_NAME = entity.SUB_CAT_NAME,
                          };


                return lst.ToList<StorySubCategoryModel>();
            }
        }
        /// <summary>
/// Get the event by ID
/// </summary>
/// <param name="EventId"></param>
/// <returns></returns>
        public List<StorySubCategoryModel> GetSubCategoryByID(Decimal SEQ_NO)
        {
            List<StorySubCategoryModel> list = new List<StorySubCategoryModel>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from ev in context.LBC_STORY_SUBCATEGORY
                          where ev.SEQ_NO == SEQ_NO
                          select new StorySubCategoryModel
                          {
                              SUB_CAT_NAME = ev.SUB_CAT_NAME,

                          };

                return lst.ToList<StorySubCategoryModel>();
            }
        }


        public StorySubCategoryModel GetStorySubCategoryModelByID(decimal SEQ_NO)
        {
            StorySubCategoryModel model = new StorySubCategoryModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from ev in context.LBC_STORY_SUBCATEGORY
                          where ev.SEQ_NO == SEQ_NO
                          select new StorySubCategoryModel
                          {
                              SEQ_NO = ev.SEQ_NO,
                              SUB_CAT_NAME = ev.SUB_CAT_NAME,

                          };

                model = lst.SingleOrDefault();
            }
            return model;
        }
        /// <summary>
        /// Update the event count to check the max;
        /// </summary>
        /// <param name="model"></param>
        public void UpdateEventCount(StorySubCategoryModel model)
        {
           // LBC_STORY_SubCategory _event = new LBC_STORY_SubCategory();
           // bool flag = false;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _event = (from p in context.LBC_STORY_SUBCATEGORY
                                  where p.SEQ_NO == model.SEQ_NO
                                  select p).FirstOrDefault();

                    _event.SUB_CAT_NAME = model.SUB_CAT_NAME;
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
