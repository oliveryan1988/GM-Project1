using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.DL.Repositories;
using LincolnBrandChampion.DL.Properties;
using LincolnBrandChampion.Model.Models;
using PagedList;
namespace LincolnBrandChampion.BL.Persisters
{

    public class StoryBL
    {

        StoryRepository _story = new StoryRepository();
        /// <summary>
        /// Method to get the stories by stars Id 
        /// </summary>
        /// <param name="starsId"></param>
        /// <returns></returns>
        public List<StoryModel> getStoriesByStarsId(string starsId)
        {
          //  StoryRepository _story = new StoryRepository();
            return _story.getStoriesByStarsId(starsId);
        }
        public StoryModel getStoriesBySEQID(int seqid)
        {
            //  StoryRepository _story = new StoryRepository();
            return _story.getStoriesBySEQID(seqid);
        }
        /// <summary>
        /// Method to get all the stories with Status final
        /// </summary>
        /// <returns></returns>
        public List<StoryModel> getAllStories()
        {
            //  StoryRepository _story = new StoryRepository();
            return _story.getAllStories();
        }
        public IPagedList<StoryModel> GetAll(string stars_id,  int page, int pageSize, string order = "", string asc = "")
        {
            StoryRepository _story = new StoryRepository();
            return _story.GetAll(stars_id, page, pageSize, order, asc);
        }
        public IPagedList<StoryModel> GetAll(string category, string subcategory, string rating, string market, string events, string search, DateTime fromdate,DateTime todate, int page, int pageSize, string order = "", string asc = "")
        {
            StoryRepository _story = new StoryRepository();
            return _story.GetAll(category,subcategory,rating,market,events,search, fromdate,todate, page, pageSize, order, asc);
        }
        public IList<StoryModel> GetAll(string category, string subcategory, string rating, string market, string events, string search,  string order = "", string asc = "")
        {
            StoryRepository _story = new StoryRepository();
            return _story.GetAll(category, subcategory, rating, market, events, search,  order, asc);
        }
        /// <summary>
        /// SAve the story
        /// </summary>
        /// <param name="model"></param>
        public void SaveStory(StoryModel model)
        {
            _story.SaveStory(model);
        }
        /// <summary>
        /// Method to update the story
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateStory(StoryModel model)
        {
            
            return _story.UpdateStory(model);
        }

    }
}
