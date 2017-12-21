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
    public class StoryCategoryBL
    {
        /// <summary>
        /// Method to get all the StoryCategorys
        /// </summary>
        /// <returns></returns>
        public List<StoryCategoryModel> GetAll()
        {
            StoryCategoryRepository _StoryCategory = new StoryCategoryRepository();
            return _StoryCategory.GetAllCategory();
        }
     
        public StoryCategoryModel GetStoryCategoryModelByID (decimal StoryCategoryId)
        {
            StoryCategoryRepository _StoryCategory = new StoryCategoryRepository();
            return _StoryCategory.GetStoryCategoryModelByID(StoryCategoryId);
        }

        /// <summary>
        /// get the StoryCategory by the id
        /// </summary>
        /// <param name="StoryCategory_id"></param>
        /// <returns></returns>
        public List<StoryCategoryModel> GetStoryCategorysByID(decimal StoryCategory_id)
        {
            StoryCategoryRepository _StoryCategory = new StoryCategoryRepository();
            return _StoryCategory.GetCategoryByID(StoryCategory_id);
        }
    }
}
