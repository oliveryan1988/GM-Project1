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
    public class StoryRatingBL
    {
        /// <summary>
        /// Method to get all the StoryRatings
        /// </summary>
        /// <returns></returns>
        public List<StoryRatingModel> GetAll()
        {
            StoryRatingRepository _StoryRating = new StoryRatingRepository();
            return _StoryRating.GetAllRating();
        }
     
        public StoryRatingModel GetStoryRatingModelByID (decimal StoryRatingId)
        {
            StoryRatingRepository _StoryRating = new StoryRatingRepository();
            return _StoryRating.GetStoryRatingModelByID(StoryRatingId);
        }

        /// <summary>
        /// get the StoryRating by the id
        /// </summary>
        /// <param name="StoryRating_id"></param>
        /// <returns></returns>
        public List<StoryRatingModel> GetStoryRatingsByID(decimal StoryRating_id)
        {
            StoryRatingRepository _StoryRating = new StoryRatingRepository();
            return _StoryRating.GetRatingByID(StoryRating_id);
        }
    }
}
