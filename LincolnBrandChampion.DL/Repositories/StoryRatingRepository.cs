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
    public class StoryRatingRepository
    {
        /// <summary>
        /// Get all the Events
        /// </summary>
        /// <returns></returns>

        public List<StoryRatingModel> GetAllRating()
        {
            List<StoryRatingModel> list = new List<StoryRatingModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from entity in context.LBC_STORY_RATING
                          select new StoryRatingModel
                          {
                              SEQ_ID = entity.SEQ_ID,
                              RATING_NAME = entity.RATING_NAME,
                          };


                return lst.ToList<StoryRatingModel>();
            }
        }
        /// <summary>
/// Get the event by ID
/// </summary>
/// <param name="EventId"></param>
/// <returns></returns>
        public List<StoryRatingModel> GetRatingByID(Decimal SEQ_ID)
        {
            List<StoryRatingModel> list = new List<StoryRatingModel>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from ev in context.LBC_STORY_RATING
                          where ev.SEQ_ID == SEQ_ID
                          select new StoryRatingModel
                          {
                              RATING_NAME = ev.RATING_NAME,

                          };

                return lst.ToList<StoryRatingModel>();
            }
        }


        public StoryRatingModel GetStoryRatingModelByID(decimal SEQ_ID)
        {
            StoryRatingModel model = new StoryRatingModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from ev in context.LBC_STORY_RATING
                          where ev.SEQ_ID == SEQ_ID
                          select new StoryRatingModel
                          {
                              SEQ_ID = ev.SEQ_ID,
                              RATING_NAME = ev.RATING_NAME,

                          };

                model = lst.SingleOrDefault();
            }
            return model;
        }
        /// <summary>
        /// Update the event count to check the max;
        /// </summary>
        /// <param name="model"></param>
        public void UpdateEventCount(StoryRatingModel model)
        {
           // LBC_STORY_RATING _event = new LBC_STORY_RATING();
           // bool flag = false;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _event = (from p in context.LBC_STORY_RATING
                                  where p.SEQ_ID == model.SEQ_ID
                                  select p).FirstOrDefault();

                    _event.RATING_NAME = model.RATING_NAME;
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
