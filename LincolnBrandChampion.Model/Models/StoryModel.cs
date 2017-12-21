using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace LincolnBrandChampion.Model.Models
{
    public class StoryModel
    {

        public decimal SEQ_ID { get; set; }
        public string STARS_ID { get; set; }
        public string PA_CODE { get; set; }
        [Required]
        public string STORY_TITLE { get; set; }
        [Required]
        public string STORY_CONTENT { get; set; }
        public string IMG_1 { get; set; }
        public string IMG_2 { get; set; }
        public string IMG_3 { get; set; }
        public string STORY_STATUS { get; set; }
        public Nullable<decimal> STORY_RATING { get; set; }
        public Nullable<decimal> STORY_CATEGORY { get; set; }
        public Nullable<decimal> STORY_SUBCATEGORY { get; set; }
        public Nullable<System.DateTime> SUBMISION_DATE { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }

        public IPagedList<StoryModel> StoryList { get; set; }
        public List<StoryCategoryModel> CategoryList { get; set; }
        public List<StorySubCategoryModel> SubCategoryList { get; set; }
        public List<StoryRatingModel> RatingList { get; set; }

        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string PHOTO_PATH { get; set; }
        public string DLR_NAME { get; set; }
        public ProfileModel Profile { get; set; }
        public StoryCategoryModel Category { get; set; }
        public StorySubCategoryModel SubCategory { get; set; }
        public StoryRatingModel Rating { get; set; }
    }

    public class ListModel
    {
        public StoryModel StoryModel { get; set; }
        public ProfileModel ProfileModel { get; set; }
    }
}
