using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.DL.Data;
using LincolnBrandChampion.DL.Helpers;
using PagedList;

namespace LincolnBrandChampion.DL.Repositories
{
    public class StoryRepository
    {
        /// <summary>
        /// Method to retrive the story based on the stars Id
        /// </summary>
        /// <param name="stars_id"></param>
        /// <returns></returns>
        public List<StoryModel> getStoriesByStarsId(string stars_id)
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from sq in context.LBC_STORY
                          where sq.STARS_ID == stars_id
                          select new StoryModel
                          {
                              SEQ_ID = sq.SEQ_ID,
                              STARS_ID = sq.STARS_ID,
                              PA_CODE = sq.PA_CODE,
                              STORY_TITLE = sq.STORY_TITLE,
                              STORY_CONTENT = sq.STORY_CONTENT,
                              STORY_RATING = sq.STORY_RATING,
                              STORY_STATUS = sq.STORY_STATUS,
                              STORY_CATEGORY = sq.STORY_CATEGORY,
                              STORY_SUBCATEGORY = sq.STORY_SUBCATEGORY,
                              IMG_1 = sq.IMG_1,
                              IMG_2 = sq.IMG_2,
                              IMG_3 = sq.IMG_3,
                              CREATED_DATE = sq.CREATED_DATE,
                              CREATED_BY = sq.CREATED_BY,
                              UPDATED_BY = sq.UPDATED_BY,
                              UPDATED_DATE = sq.UPDATED_DATE,
                              SUBMISION_DATE = sq.SUBMISION_DATE
                          };

                return lst.ToList<StoryModel>();
            }
        }
        public StoryModel getStoriesBySEQID(int seqid)
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from sq in context.LBC_STORY
                          where sq.SEQ_ID == seqid
                          select new  StoryModel
                          {
                              SEQ_ID = sq.SEQ_ID,
                              STARS_ID = sq.STARS_ID,
                              PA_CODE = sq.PA_CODE,
                              STORY_TITLE = sq.STORY_TITLE,
                              STORY_CONTENT = sq.STORY_CONTENT,
                              STORY_RATING = sq.STORY_RATING,
                              STORY_STATUS = sq.STORY_STATUS,
                              STORY_CATEGORY = sq.STORY_CATEGORY,
                              STORY_SUBCATEGORY = sq.STORY_SUBCATEGORY,
                              IMG_1 = sq.IMG_1,
                              IMG_2 = sq.IMG_2,
                              IMG_3 = sq.IMG_3,
                              CREATED_DATE = sq.CREATED_DATE,
                              CREATED_BY = sq.CREATED_BY,
                              UPDATED_BY = sq.UPDATED_BY,
                              UPDATED_DATE = sq.UPDATED_DATE,
                              SUBMISION_DATE = sq.SUBMISION_DATE

                          }
                          ;

                return lst.SingleOrDefault()                   ;
            }
        }
        /// <summary>
        /// Method to get all the stories for the admin where the status is Final!
        /// </summary>
        /// <returns></returns>
        public List<StoryModel> getAllStories()
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from sq in context.LBC_STORY
                          where sq.STORY_STATUS == "FINAL"
                          select new StoryModel
                          {
                            SEQ_ID = sq.SEQ_ID,
                            STARS_ID = sq.STARS_ID,
                            PA_CODE = sq.PA_CODE,
                            STORY_TITLE = sq.STORY_TITLE,
                            STORY_CONTENT = sq.STORY_CONTENT,
                            STORY_RATING = sq.STORY_RATING,
                            STORY_STATUS = sq.STORY_STATUS,
                            STORY_CATEGORY = sq.STORY_CATEGORY,
                            STORY_SUBCATEGORY = sq.STORY_SUBCATEGORY,
                            IMG_1 = sq.IMG_1,
                            IMG_2 = sq.IMG_2,
                            IMG_3 = sq.IMG_3,
                            CREATED_DATE = sq.CREATED_DATE,
                            CREATED_BY = sq.CREATED_BY,
                            UPDATED_BY = sq.UPDATED_BY,
                            UPDATED_DATE = sq.UPDATED_DATE,
                            SUBMISION_DATE = sq.SUBMISION_DATE
                          };

                return lst.ToList<StoryModel>();
            }
        }
        /// <summary>
        /// For Dealer Viewer
        /// </summary>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="order"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public IPagedList<StoryModel> GetAll(string stars_id, int page, int pageSize, string order = "", string asc = "")
        {
            stars_id = string.IsNullOrEmpty(stars_id) ? string.Empty : stars_id;
            stars_id = stars_id.ToUpper().Equals("ALL") ? string.Empty : stars_id;
            List<StoryModel> _lst = new List<StoryModel>();
            IEnumerable<StoryModel> _lstTemp;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                //search
                var  list = (from entity in context.LBC_STORY
                            join pf in context.LBC_PROFILE
                            on entity.STARS_ID equals pf.STARS_ID
                             where pf.STARS_ID == stars_id && pf.EMP_STATUS_CODE == "A"
                             orderby entity.STORY_STATUS,entity.CREATED_DATE
                             select new { entity, pf.PHOTO_PATH, pf.FIRST_NAME, pf.LAST_NAME, pf.DLR_NAME } into s
                            select s).ToList();

                //List<StoryModel> _last = (List<StoryModel>)list;

                foreach (var item in list)
                {
                    //_lst.Add(item);
                    StoryModel _sm = new StoryModel();
                    
                    _sm.SEQ_ID = item.entity.SEQ_ID;
                    _sm.STARS_ID = item.entity.STARS_ID;
                    _sm.PA_CODE = item.entity.PA_CODE;
                    _sm.STORY_TITLE = item.entity.STORY_TITLE;
                    _sm.STORY_CONTENT = item.entity.STORY_CONTENT;
                    _sm.STORY_RATING = item.entity.STORY_RATING;
                    _sm.STORY_STATUS = item.entity.STORY_STATUS;
                    _sm.STORY_CATEGORY = item.entity.STORY_CATEGORY;
                    _sm.STORY_SUBCATEGORY = item.entity.STORY_SUBCATEGORY;
                    _sm.IMG_1 = item.entity.IMG_1;
                    _sm.IMG_2 = item.entity.IMG_2;
                    _sm.IMG_3 = item.entity.IMG_3;
                    _sm.CREATED_DATE = item.entity.CREATED_DATE;
                    _sm.CREATED_BY = item.entity.CREATED_BY;
                    _sm.UPDATED_BY = item.entity.UPDATED_BY;
                    _sm.UPDATED_DATE = item.entity.UPDATED_DATE;
                    _sm.SUBMISION_DATE = item.entity.SUBMISION_DATE;
                    _sm.PHOTO_PATH=item.PHOTO_PATH;
                    _sm.FIRST_NAME=item.FIRST_NAME;
                    _sm.LAST_NAME =item.LAST_NAME;
                    _sm.DLR_NAME = item.DLR_NAME;
                    _lst.Add(_sm);
                }
            }
            _lstTemp = _lst.AsQueryable();
            return _lstTemp.ToPagedList<StoryModel>(page, pageSize);

        }
        /// <summary>
        /// For Admin Viewer
        /// </summary>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="order"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public IPagedList<StoryModel> GetAll(string category, string subcategory, string rating, string market, string events, string search, DateTime fromdate,DateTime todate, int page, int pageSize, string order = "", string asc = "")
        {
            search = string.IsNullOrEmpty(search) ? string.Empty : search;
            search = search.ToUpper().Equals("ALL") ? string.Empty : search;
            market = market.ToUpper().Equals("ALL") ? string.Empty : market;
            events = events.ToUpper().Equals("ALL") ? string.Empty : events;

            List<int> dCAT = new List<int> { 0 };
            if (!string.IsNullOrEmpty(category))
            {
                dCAT = new List<string>(category.Split(',')).ConvertAll(i => int.Parse(i));
            }
            List<int> dSubCAT = new List<int> { 0 };
            if (!string.IsNullOrEmpty(subcategory))
            {
                dSubCAT = new List<string>(subcategory.Split(',')).ConvertAll(i => int.Parse(i));
            }
            List<int> dRating =new List<int> { 0 };
            if (!string.IsNullOrEmpty(rating))
            {
                dRating = new List<string>(rating.Split(',')).ConvertAll(i => int.Parse(i));
            }

            List<int> dEvent = new List<int> { 0 };
            if (!string.IsNullOrEmpty(events))
            {
                dEvent = new List<string>(events.Split(',')).ConvertAll(i => int.Parse(i));
            }

            List<StoryModel> _lst = new List<StoryModel>();
            IEnumerable<StoryModel> _lstTemp;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                //search
                var list = (from entity in context.LBC_STORY
                            join pf in context.LBC_PROFILE
                            on entity.STARS_ID equals pf.STARS_ID into cT
                            from ct1 in cT.DefaultIfEmpty()
                            join rg in context.LBC_REGISTRATION.Where(r=>r.REGD_STATUS == "A")
                            on ct1.STARS_ID equals rg.STARS_ID into aT  
                            from at1 in aT.DefaultIfEmpty()
                            join evs in context.LBC_EVENT
                            on at1.EVENT_ID equals evs.EVENT_ID  into bT
                            from bt1 in bT.DefaultIfEmpty()
                            where ct1.FIRST_NAME.StartsWith(search) && ct1.EMP_STATUS_CODE == "A" //&& at1.REGD_STATUS == "A"
                            && entity.STORY_STATUS == "FINAL"
                            select new
                            {
                                entity = entity,
                                PHOTO_PATH = ct1.PHOTO_PATH,
                                FIRST_NAME = ct1.FIRST_NAME,
                                LAST_NAME = ct1.LAST_NAME,
                                DLR_NAME = ct1.DLR_NAME,
                                EVENT_ID = (Decimal?)at1.EVENT_ID ?? 0,
                                DLR_REGION = ct1.DLR_REGION
                            } into s
                            select s);

                if (category != "") { list = list.Where(p => dCAT.Contains((int)p.entity.STORY_CATEGORY)); }
                if (subcategory != "") { list = list.Where(p => dSubCAT.Contains((int)p.entity.STORY_SUBCATEGORY)); }
                if (rating != "") { list = list.Where(p => dRating.Contains((int)p.entity.STORY_RATING)); }
                if (market != "") { list = list.Where(p => p.DLR_REGION == market); }
                if (events != "") { list = list.Where(p => dEvent.Contains((int)p.EVENT_ID)); }
                list = list.Where(p => p.entity.CREATED_DATE >=fromdate && p.entity.CREATED_DATE<=todate);

                list.ToList();
                
                foreach (var item in list)
                {
                    //_lst.Add(item);
                    StoryModel _sm = new StoryModel();
                    _sm.SEQ_ID = item.entity.SEQ_ID;
                    _sm.STARS_ID = item.entity.STARS_ID;
                    _sm.PA_CODE = item.entity.PA_CODE;
                    _sm.STORY_TITLE = item.entity.STORY_TITLE;
                    _sm.STORY_CONTENT = item.entity.STORY_CONTENT;
                    _sm.STORY_RATING = item.entity.STORY_RATING;
                    _sm.STORY_STATUS = item.entity.STORY_STATUS;
                    _sm.STORY_CATEGORY = item.entity.STORY_CATEGORY;
                    _sm.STORY_SUBCATEGORY = item.entity.STORY_SUBCATEGORY;
                    _sm.IMG_1 = item.entity.IMG_1;
                    _sm.IMG_2 = item.entity.IMG_2;
                    _sm.IMG_3 = item.entity.IMG_3;
                    _sm.CREATED_DATE = item.entity.CREATED_DATE;
                    _sm.CREATED_BY = item.entity.CREATED_BY;
                    _sm.UPDATED_BY = item.entity.UPDATED_BY;
                    _sm.UPDATED_DATE = item.entity.UPDATED_DATE;
                    _sm.SUBMISION_DATE = item.entity.SUBMISION_DATE;
                    _sm.PHOTO_PATH = item.PHOTO_PATH;
                    _sm.FIRST_NAME = item.FIRST_NAME;
                    _sm.LAST_NAME = item.LAST_NAME;
                    _sm.DLR_NAME = item.DLR_NAME;
                    _lst.Add(_sm);
                }
            }
            _lstTemp = _lst.AsQueryable();
            return _lstTemp.ToPagedList<StoryModel>(page, pageSize);

        }
        public IList<StoryModel> GetAll(string category, string subcategory, string rating, string market, string events, string search, string order = "", string asc = "")
        {
            search = string.IsNullOrEmpty(search) ? string.Empty : search;
            search = search.ToUpper().Equals("ALL") ? string.Empty : search;
            market = market.ToUpper().Equals("ALL") ? string.Empty : market;
            events = events.ToUpper().Equals("ALL") ? string.Empty : events;

            List<int> dCAT = new List<int> { 0 };
            if (!string.IsNullOrEmpty(category))
            {
                dCAT = new List<string>(category.Split(',')).ConvertAll(i => int.Parse(i));
            }
            List<int> dSubCAT = new List<int> { 0 };
            if (!string.IsNullOrEmpty(subcategory))
            {
                dSubCAT = new List<string>(subcategory.Split(',')).ConvertAll(i => int.Parse(i));
            }
            List<int> dRating = new List<int> { 0 };
            if (!string.IsNullOrEmpty(rating))
            {
                dRating = new List<string>(rating.Split(',')).ConvertAll(i => int.Parse(i));
            }

            List<int> dEvent = new List<int> { 0 };
            if (!string.IsNullOrEmpty(events))
            {
                dEvent = new List<string>(events.Split(',')).ConvertAll(i => int.Parse(i));
            }

            List<StoryModel> _lst = new List<StoryModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                //search
                var list = (from entity in context.LBC_STORY
                            join pf in context.LBC_PROFILE
                            on entity.STARS_ID equals pf.STARS_ID into cT
                            from ct1 in cT.DefaultIfEmpty()
                            join rg in context.LBC_REGISTRATION.Where(r=>r.REGD_STATUS == "A")
                            on ct1.STARS_ID equals rg.STARS_ID into aT
                            from at1 in aT.DefaultIfEmpty()
                            join evs in context.LBC_EVENT
                            on at1.EVENT_ID equals evs.EVENT_ID into bT
                            from bt1 in bT.DefaultIfEmpty()
                            where ct1.FIRST_NAME.StartsWith(search) && ct1.EMP_STATUS_CODE == "A" 
                            && entity.STORY_STATUS == "FINAL"
                            select new { entity = entity, PHOTO_PATH = ct1.PHOTO_PATH, FIRST_NAME = ct1.FIRST_NAME, LAST_NAME = ct1.LAST_NAME, DLR_NAME = ct1.DLR_NAME, 
                                EVENT_ID = (Decimal?)at1.EVENT_ID ?? 0, DLR_REGION = ct1.DLR_REGION } into s
                            select s);

                if (category != "") { list = list.Where(p => dCAT.Contains((int)p.entity.STORY_CATEGORY)); }
                if (subcategory != "") { list = list.Where(p => dSubCAT.Contains((int)p.entity.STORY_SUBCATEGORY)); }
                if (rating != "") { list = list.Where(p => dRating.Contains((int)p.entity.STORY_RATING)); }
                if (market != "") { list = list.Where(p => p.DLR_REGION == market); }
                if (events != "") { list = list.Where(p => dEvent.Contains((int)p.EVENT_ID)); }


                list.ToList();

                foreach (var item in list)
                {
                    //_lst.Add(item);
                    StoryModel _sm = new StoryModel();
                    _sm.SEQ_ID = item.entity.SEQ_ID;
                    _sm.STARS_ID = item.entity.STARS_ID;
                    _sm.PA_CODE = item.entity.PA_CODE;
                    _sm.STORY_TITLE = item.entity.STORY_TITLE;
                    _sm.STORY_CONTENT = item.entity.STORY_CONTENT;
                    _sm.STORY_RATING = item.entity.STORY_RATING;
                    _sm.STORY_STATUS = item.entity.STORY_STATUS;
                    _sm.STORY_CATEGORY = item.entity.STORY_CATEGORY;
                    _sm.STORY_SUBCATEGORY = item.entity.STORY_SUBCATEGORY;
                    _sm.IMG_1 = item.entity.IMG_1;
                    _sm.IMG_2 = item.entity.IMG_2;
                    _sm.IMG_3 = item.entity.IMG_3;
                    _sm.CREATED_DATE = item.entity.CREATED_DATE;
                    _sm.CREATED_BY = item.entity.CREATED_BY;
                    _sm.UPDATED_BY = item.entity.UPDATED_BY;
                    _sm.UPDATED_DATE = item.entity.UPDATED_DATE;
                    _sm.SUBMISION_DATE = item.entity.SUBMISION_DATE;
                    _sm.PHOTO_PATH = item.PHOTO_PATH;
                    _sm.FIRST_NAME = item.FIRST_NAME;
                    _sm.LAST_NAME = item.LAST_NAME;
                    _sm.DLR_NAME = item.DLR_NAME;
                    _lst.Add(_sm);
                }
            }
            
            return _lst;

        }
        private StoryModel MapModelFromLBC_Story(StoryModel  entity)
        {
            StoryModel model = new StoryModel();

            if (entity != null)
            {
                model = (StoryModel)entity;
                //model.SEQ_ID = entity.SEQ_ID;
                //model.STARS_ID = entity.STARS_ID;
                //model.PA_CODE = entity.PA_CODE;
                //model.STORY_TITLE = entity.STORY_TITLE;
                //model.STORY_CONTENT = entity.STORY_CONTENT;
                //model.STORY_RATING = entity.STORY_RATING;
                //model.STORY_STATUS = entity.STORY_STATUS;
                //model.STORY_CATEGORY = entity.STORY_CATEGORY;
                //model.STORY_SUBCATEGORY = entity.STORY_SUBCATEGORY;
                //model.IMG_1 = entity.IMG_1;
                //model.IMG_2 = entity.IMG_2;
                //model.IMG_3 = entity.IMG_3;
                //model.CREATED_DATE = entity.CREATED_DATE;
                //model.CREATED_BY = entity.CREATED_BY;
                //model.UPDATED_BY = entity.UPDATED_BY;
                //model.UPDATED_DATE = entity.UPDATED_DATE;
                //model.SUBMISION_DATE = entity.SUBMISION_DATE;
                
            }

            return model;
        }

        /// <summary>
        /// Method to add the Story
        /// </summary>
        /// <param name="model"></param>
        public void SaveStory( StoryModel model)
        {
         
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                LBC_STORY _story = new LBC_STORY()
                {

                    STARS_ID = model.STARS_ID,
                    PA_CODE = model.PA_CODE,
                    STORY_TITLE= model.STORY_TITLE,
                    STORY_CONTENT= model.STORY_CONTENT,
                    STORY_RATING = model.STORY_RATING,
                    STORY_STATUS = model.STORY_STATUS,
                    STORY_CATEGORY = model.STORY_CATEGORY,
                    STORY_SUBCATEGORY = model.STORY_SUBCATEGORY,
                    SUBMISION_DATE = model.SUBMISION_DATE,
                    IMG_1=model.IMG_1,
                    IMG_2=model.IMG_2,
                    IMG_3=model.IMG_3,
                    CREATED_DATE = model.CREATED_DATE,
                    CREATED_BY = model.CREATED_BY
                    
                };

                context.LBC_STORY.Add(_story);
                context.SaveChanges();


            }
        }

        /// <summary>
        /// Method to Update the Registration Table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateStory(StoryModel model)
        {
            bool save = true;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _story = (from p in context.LBC_STORY
                                    where p.SEQ_ID == model.SEQ_ID
                                    select p).FirstOrDefault();


                                     
                    _story.STORY_TITLE = model.STORY_TITLE;
                    _story.STORY_CONTENT = model.STORY_CONTENT;
                    _story.STORY_RATING = model.STORY_RATING;
                    _story.STORY_CATEGORY = model.STORY_CATEGORY;
                    _story.STORY_SUBCATEGORY = model.STORY_SUBCATEGORY;
                    _story.SUBMISION_DATE = model.SUBMISION_DATE;
                    _story.STORY_STATUS = model.STORY_STATUS;
                    _story.UPDATED_DATE = model.UPDATED_DATE;
                    _story.UPDATED_BY = model.UPDATED_BY;
                    _story.IMG_1=model.IMG_1;
                    _story.IMG_2=model.IMG_2;
                    _story.IMG_3 = model.IMG_3;                    


                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw ex;
                // save = false;
            }
            return save;
        }
    }
        
}
