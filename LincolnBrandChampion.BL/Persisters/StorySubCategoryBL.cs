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
    public class StorySubCategoryBL
    {
        /// <summary>
        /// Method to get all the StoryCategorys
        /// </summary>
        /// <returns></returns>
        public List<StorySubCategoryModel> GetAll()
        {
            StorySubCategoryRepository _StoryCategory = new StorySubCategoryRepository();
            return _StoryCategory.GetAllSubCategory();
        }
     
        public StorySubCategoryModel GetStorySubCategoryModelByID (decimal StoryCategoryId)
        {
            StorySubCategoryRepository _StoryCategory = new StorySubCategoryRepository();
            return _StoryCategory.GetStorySubCategoryModelByID(StoryCategoryId);
        }

        /// <summary>
        /// get the StoryCategory by the id
        /// </summary>
        /// <param name="StoryCategory_id"></param>
        /// <returns></returns>
        public List<StorySubCategoryModel> GetStoryCategorysByID(decimal StoryCategory_id)
        {
            StorySubCategoryRepository _StoryCategory = new StorySubCategoryRepository();
            return _StoryCategory.GetSubCategoryByID(StoryCategory_id);
        }
    }
}
