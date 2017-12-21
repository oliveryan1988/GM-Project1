using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LincolnBrandChampion.Web.Helpers;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.BL.Persisters;
using PagedList;
using System.Configuration;
using System.Drawing;
using ReportManagement;

namespace LincolnBrandChampion.Web.Controllers
{
    public class StoryController : Controller
    {
        
            ProfileBL profile = new ProfileBL();
            StoryBL _storybl = new StoryBL();
            string currentMode = "C"; //E-->Edit; C-->Create New
        //
        // GET: /Story/
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult Index(int? id)
        {
            
            StoryModel story = new StoryModel();
            if (id != null)
            {
                int pID = (int)id;
                story = _storybl.getStoriesBySEQID(pID);
            }
            ViewBag.homeid = "story";
            PrepareProfile(Session["StarsIdProfile"].ToString());
            IPagedList<StoryModel> lstModel = _storybl.GetAll(Session["StarsIdProfile"].ToString(), 1, 1000, "", "");
            story.StoryList = lstModel;
            currentMode = (id == 0 ? "C" : "E");
            ViewBag.ViewMode = false;

            return View(story);
        }

        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult AdminEdit(int? id)
        {
            StoryModel story = new StoryModel();
            if (id != null)
            {
                int pID = (int)id;
                story = _storybl.getStoriesBySEQID(pID);
            }
            ViewBag.homeid = "story";
            //PrepareProfile();
            //IPagedList<StoryModel> lstModel = _storybl.GetAll(Session["StarsIdProfile"].ToString(), 1, 1000, "", "");
            //story.StoryList = lstModel;
            currentMode = (id == 0 ? "C" : "E");

            PrepareProfile(story.STARS_ID);

            return View(story);
        }

        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult AdminEdit(StoryModel model,string save, string category, string subcategory, string reason,string messagebody, int? id)
        {
            StoryModel story = new StoryModel();
            if (id != null)
            {
                int pID = (int)id;
                story = _storybl.getStoriesBySEQID(pID);
            }

            if (save == "CANCEL") { return View(story); }

            ViewBag.homeid = "story";
            
            //string rating =(Request.Form.Get("rating")==null?"0":Request.Form.Get("rating").ToString());

            model.STORY_STATUS = (save == "SAVE STORY >" ? "FINAL":"DRAFT" );
            if (save == "SAVE STORY >" || save==null)
            {
                //model.STORY_CATEGORY = int.Parse(category);
                //model.STORY_SUBCATEGORY = int.Parse(subcategory);
                if (model.STORY_RATING == 0)
                {
                    PrepareProfile(model.STARS_ID);
                    return View(story);
                }
               
            }
            else
            {
                //Return to Draft
                ProfileModel _profile = new ProfileModel();
                ProfileBL _pbl = new ProfileBL();
                _profile = _pbl.GetProfileByStarzId(story.STARS_ID);
                EmailHelper.SendReturnDraftEMail(_profile.EMAIL_ID, reason, messagebody);
            }

            model.UPDATED_BY = System.Web.HttpContext.Current.Session["w_user"].ToString();
            model.UPDATED_DATE = DateTime.Now;
            model.SUBMISION_DATE = model.SUBMISION_DATE;
            model.SEQ_ID = model.SEQ_ID;
            _storybl.UpdateStory(model);
            return RedirectToAction("AdminStories");
        }
        

        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult ViewStory(int id)
        {
            StoryModel story = new StoryModel();
            story = _storybl.getStoriesBySEQID(id);
            ViewBag.homeid = "story";
            PrepareProfile(Session["StarsIdProfile"].ToString());
            IPagedList<StoryModel> lstModel = _storybl.GetAll(Session["StarsIdProfile"].ToString(), 1, 1000, "", "");
            story.StoryList = lstModel;
            ViewBag.ViewMode = true;
            return View(story);
        }
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult MyStories( int? page, string order, string asc)
        {
            if (Convert.ToInt32(Session["ROLE_ID"]) == 5 || Convert.ToInt32(Session["ROLE_ID"]) == 4 || Convert.ToInt32(Session["ROLE_ID"]) == 2)
            {
                ViewBag.homeid = "Story";
            }
            else
            {
                ViewBag.homeid = "home";
            }
            PrepareProfile(Session["StarsIdProfile"].ToString());
            ViewBag.page = page ?? 1;
            int pag = page ?? 1;
            IPagedList<StoryModel> lstModel = _storybl.GetAll(Session["StarsIdProfile"].ToString(), pag, 10, order, asc);
            ViewBag.ViewMode = true;
            return View(lstModel);
        }

        [AuthorizedAttributeHelper(LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult AdminStories(string search, string filter, int? page, string order, string asc)
        {
            ViewBag.homeid = "story";
 
            ViewBag.search = string.IsNullOrEmpty(search) ? "ALL" : search;
            ViewBag.filter = string.IsNullOrEmpty(filter) ? string.Empty : filter;
            ViewBag.page = page ?? 1;
            int pag = page ?? 1;
            Session["cat"] =(Session["cat"]==null? "":Session["cat"]);
            Session["subcat"] = (Session["subcat"] == null ? "" : Session["subcat"]);
            Session["rating"] = (Session["rating"] == null ? "" : Session["rating"]);
            Session["market"] = (Session["market"] == null ? "" : Session["market"]);
            Session["event"] = (Session["event"] == null ? "" : Session["event"]);
            Session["searchname"] = (Session["searchname"] == null ? "" : Session["searchname"]);
            Session["checkall0"] = (Session["checkall0"] == null ? "" : Session["checkall0"].ToString());
            Session["checkall100"] = (Session["checkall100"] == null ? "" : Session["checkall100"].ToString());
            Session["checkall300"] = (Session["checkall300"] == null ? "" : Session["checkall300"].ToString());
            if (Session["fromdate"] == null)
            {
                Session["fromdate"] = DateTime.Parse("01/01/2015").ToString("MM/dd/yyyy");
            }
            if (Session["todate"] == null)
            {
                Session["todate"] = DateTime.Today.ToString("MM/dd/yyyy");
            }

            string sCategory=Session["cat"].ToString() ;
            string sSubCategory=Session["subcat"].ToString();
            string sRating =Session["rating"].ToString() ;

            string sMarket = Session["market"].ToString();
            string sEvent = Session["event"].ToString();
            string SearchName = Session["searchname"].ToString();
            DateTime fromDate = DateTime.Parse(Session["fromdate"].ToString());
            DateTime toDate = DateTime.Parse(Session["todate"].ToString());

            
            IPagedList<StoryModel> lstModel = _storybl.GetAll(sCategory, sSubCategory, sRating, sMarket, sEvent, SearchName, fromDate,toDate, pag, 10, order, asc);

            StorySubCategoryBL _storysubcatbl = new StorySubCategoryBL();
            StorySubCategoryModel _storysubcat;
            StoryRatingBL _storyratingbl = new StoryRatingBL();
            StoryRatingModel _storyrating;

            foreach (StoryModel m in lstModel)
            {
                decimal dsubCategory = (m.STORY_SUBCATEGORY == null ? 0 : (decimal)m.STORY_SUBCATEGORY);
                decimal drating = (m.STORY_RATING == null ? 0 : (decimal)m.STORY_RATING);
                
                _storysubcat = _storysubcatbl.GetStorySubCategoryModelByID(dsubCategory);
                _storyrating = _storyratingbl.GetStoryRatingModelByID(drating);

                m.SubCategory = _storysubcat;
                m.Rating = _storyrating;

            }

            return View(lstModel);
        }

        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult AdminStories(FormCollection frm, int? page, string order, string asc)
        {
            ViewBag.homeid = "story";

            string sCategory = (frm["Category"] == null ? "" : frm["Category"].ToString());

            string sSubCategory = (frm["subCategory"] == null ? "" : frm["subCategory"].ToString());

            string sRating = (frm["Rating"] == null ? "" : frm["Rating"].ToString());

            string sMarket = (frm["market"] == null || frm["market"]=="ALL" ? "" : frm["market"].ToString());
            string sEvent = (frm["event"] == null ? "" : frm["event"].ToString());
            string SearchName = (frm["SearchName"] == null ? "" : frm["SearchName"].ToString());
            DateTime fromDate;
            DateTime toDate;
            if (frm["fromDate"] == null || frm["fromDate"] == "")
            {
                fromDate =  DateTime.Parse("01/01/2015") ;
            }
            else
            {
                fromDate = DateTime.Parse(frm["fromDate"]);
            }
            if (frm["fromDate"] == null || frm["toDate"] == "")
            {
                toDate = DateTime.Today;
            }
            else
            {
                toDate = DateTime.Parse(frm["toDate"]);
            }
            //DateTime toDate = (frm["toDate"] == null && frm["toDate"] == "" ? DateTime.Parse("12/31/9999") : DateTime.Parse(frm["toDate"]));
            Session["checkall0"] = (frm["checkall0"] == null ? "" : frm["checkall0"].ToString());
            Session["checkall100"] = (frm["checkall100"] == null ? "" : frm["checkall100"].ToString());
            Session["checkall300"] = (frm["checkall300"] == null ? "" : frm["checkall300"].ToString());

            Session["cat"] = sCategory;
            Session["subcat"] = sSubCategory;
            Session["rating"] = sRating;
            Session["market"]=sMarket  ;
            Session["event"]= sEvent ;
            Session["searchname"]=SearchName  ;
            Session["fromdate"] = fromDate.ToString("MM/dd/yyyy");
            Session["todate"] = toDate.ToString("MM/dd/yyyy");

            ViewBag.page = page ?? 1;
            int pag = page ?? 1;
            IPagedList<StoryModel> lstModel = _storybl.GetAll(sCategory, sSubCategory, sRating, sMarket, sEvent, SearchName,fromDate,toDate, pag, 10, order, asc);

            StorySubCategoryBL _storysubcatbl = new StorySubCategoryBL();
            StorySubCategoryModel _storysubcat;
            StoryRatingBL _storyratingbl = new StoryRatingBL();
            StoryRatingModel _storyrating;

            foreach (StoryModel m in lstModel)
            {
                decimal dsubCategory = (m.STORY_SUBCATEGORY == null ? 0 : (decimal)m.STORY_SUBCATEGORY);
                decimal drating = (m.STORY_RATING == null ? 0 : (decimal)m.STORY_RATING);

                _storysubcat = _storysubcatbl.GetStorySubCategoryModelByID(dsubCategory);
                _storyrating = _storyratingbl.GetStoryRatingModelByID(drating);

                m.SubCategory = _storysubcat;
                m.Rating = _storyrating;

            }
            return View("AdminStories", lstModel);
        }
        //[HttpPost]
        //[AuthorizedAttributeHelper(LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        //public PartialViewResult SearchStories(string Category, string SubCategory, string Rating,   int? page, string order, string asc)
        //{
        //    ViewBag.homeid = "home";

        //    string sCategory = (Category == null ? "" : Category);

        //    string sSubCategory = (SubCategory == null ? "" : SubCategory);

        //    string sRating = (Rating == null ? "" : Rating); 

        //    string sMarket ="";
        //    string sEvent = "";
        //    string SearchName = "";

        //    Session["cat"] = sCategory;
        //    Session["subcat"] = sSubCategory;
        //    Session["rating"] = sRating;
        //    ViewBag.page = page ?? 1;
        //    int pag = page ?? 1;
        //    IPagedList<StoryModel> lstModel = _storybl.GetAll(sCategory, sSubCategory, sRating, sMarket, sEvent, SearchName, pag, 10, order, asc);
        //    //return View("AdminStories", lstModel);
        //    return PartialView("_AdminStoryList", lstModel);
        //}

        [AuthorizedAttributeHelper(LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public PartialViewResult GetSelections()
        {
            ViewBag.homeid = "Story";

            StoryModel story = new StoryModel();
            
            StoryCategoryBL _StoryCATBL=new StoryCategoryBL();
            List<StoryCategoryModel> _StoreCategoryList = _StoryCATBL.GetAll();

            StorySubCategoryBL _StorySubCATBL = new StorySubCategoryBL();
            List<StorySubCategoryModel> _SubCategoryList = _StorySubCATBL.GetAll();

            StoryRatingBL _StoryRatingBL = new StoryRatingBL();
            List<StoryRatingModel> _StoreRatingList = _StoryRatingBL.GetAll();

            story.CategoryList = _StoreCategoryList;
            story.SubCategoryList = _SubCategoryList;
            story.RatingList = _StoreRatingList;
            return PartialView("_Selections", story); 
        }

        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers,LBC_Role.GenAdmin, LBC_Role.Super_Admin)]
        public ActionResult SaveStoryUser(StoryModel model, string SaveStory, HttpPostedFileBase IMG1, HttpPostedFileBase IMG2, HttpPostedFileBase IMG3)
        {

            ProfileModel tProfile = new ProfileModel();
            model.STARS_ID = Session["StarsIdProfile"].ToString();
            tProfile = profile.GetProfileByStarzId(model.STARS_ID);
            model.PA_CODE = tProfile.PA_CODE; //Session["w_pacode"].ToString();
            model.UPDATED_BY = System.Web.HttpContext.Current.Session["w_user"].ToString();
            model.STORY_STATUS = (SaveStory == "Save DRAFT" ? "DRAFT" : "FINAL");
            model.CREATED_BY = Session["User_Id"].ToString();
            model.CREATED_DATE = DateTime.Now;
            if (IMG1 != null)
            {
                model.IMG_1 = Upload(IMG1, model.PA_CODE);
            }
            if (IMG2 != null)
            {
                model.IMG_2 = Upload(IMG2, model.PA_CODE);
            }
            if (IMG3 != null)
            {
                model.IMG_3 = Upload(IMG3, model.PA_CODE);
            }
            if (SaveStory != "Save DRAFT")
            {
                model.SUBMISION_DATE = DateTime.Now;
                model.STORY_RATING = 0; //default rating
                model.STORY_SUBCATEGORY = 10;//default uncategoiry

            }

                if (model.SEQ_ID == 0) //new story
                {
                    _storybl.SaveStory(model);
                }
                else
                {
                    model.SEQ_ID = model.SEQ_ID;
                    model.UPDATED_DATE = DateTime.Now;
                    _storybl.UpdateStory(model);
                }
                return RedirectToAction("Index");

        }

        public string Upload(HttpPostedFileBase File, string w_pacode) 
        {
            if (File != null && File.ContentLength > 0)
            {
                string filename = " ";
                decimal filesize = 1000048;
                string[] arr = new string[3] { "jpg", "gif", "png" };


                filename = File.FileName;
                int Position = filename.LastIndexOf(".");
                filename = filename.Substring(Position + 1);

                if (File.ContentLength > filesize)
                {
                    return "";

                }
                else if (!arr.Contains(filename.ToLower()))
                {
                    return "";
                }
                else
                {
                    filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid() + "_" + w_pacode + "." +filename;
                    string targetfolder = Server.MapPath(ConfigurationManager.AppSettings["StoryPicPath"] + filename);
                    Bitmap origImage = new Bitmap(File.InputStream);
                    Image img = (Image)origImage;
                    img = ImageResizeHelper.ImageResize(origImage, 680, 383, false, false);
                    Bitmap newImage = new Bitmap(img);
                    //File = ImageResizeHelper.ImageResize(File, 183, 136, false, false);
                    newImage.Save(targetfolder);
                    //File.SaveAs(targetfolder);
                    return filename;
                }
             }
            else
            {
                return "";
            }

        }
        void PrepareProfile(string StartsID)
        {
            ProfileModel pModel=new ProfileModel();
            pModel = profile.GetProfileByStarzId(StartsID);
            ViewBag.PHOTO_PATH = (string.IsNullOrEmpty(pModel.PHOTO_PATH) ? string.Empty : pModel.PHOTO_PATH);
            ViewBag.FIRST_NAME = pModel.FIRST_NAME;
            ViewBag.LAST_NAME = pModel.LAST_NAME;
            ViewBag.DLR_NAME = pModel.DLR_NAME;
            ViewBag.DLR_ADDRESS = pModel.DLR_ADDRESS;
            ViewBag.DLR_CITY = pModel.DLR_CITY;
            ViewBag.DLR_STATE = pModel.DLR_STATE;
            ViewBag.DLR_ZIP = pModel.DLR_ZIP;
            ViewBag.EMAIL_ID = pModel.EMAIL_ID;
            ViewBag.phone1 = pModel.phone1;
            ViewBag.phone2 = pModel.phone2;
            ViewBag.phone3 = pModel.phone3;
            ViewBag.mobile1 = pModel.mobile1;
            ViewBag.mobile2 = pModel.mobile2;
            ViewBag.mobile3 = pModel.mobile3;
        }
        #region registerAdmin
        [AuthorizedAttributeHelper(LBC_Role.GenAdmin, LBC_Role.Super_Admin)]
        public ActionResult AddStoryAdmin(string search)
        {
            ViewBag.homeid = "story";
            RegistrationBL _regbl = new RegistrationBL();
            ViewBag.search = string.IsNullOrEmpty(search) ? string.Empty : search;
            List<ProfileModel> lstModel = _regbl.GetListProfileByPaCode(search);
            return View(lstModel);
        }
        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.GenAdmin, LBC_Role.Super_Admin)]
        public ActionResult AddStoryAdmin(FormCollection frm)
        {

            string id = (frm.GetValue("regLBC").AttemptedValue);

            if (id != null)
            {
                Session["StarsIdProfile"] = id;
            }


            //RegistrationBL _regbl = new RegistrationBL();
            //ViewBag.search = string.IsNullOrEmpty(search) ? string.Empty : search;
            //List<ProfileModel> lstModel = _regbl.GetListProfileByPaCode(search);
            return RedirectToAction("Index");
        }
        // Get the list of the LBC to fill the drop down
        public ActionResult GetLBCList(string PaCode)
        {


            RegistrationBL _regbl = new RegistrationBL();
            ViewBag.search = string.IsNullOrEmpty(PaCode) ? string.Empty : PaCode;
            List<ProfileModel> lstModel = _regbl.GetListProfileByPaCode(PaCode);


            if (lstModel != null && lstModel.Count > 0)
            {
                List<ProfileModel> lst = new List<ProfileModel>();
                for (int i = 0; i < lstModel.Count; i++)
                {
                    ProfileModel item = new ProfileModel()
                    {
                        STARS_ID = lstModel.ElementAt(i).STARS_ID,
                        FIRST_NAME = lstModel.ElementAt(i).FIRST_NAME + " " + lstModel.ElementAt(i).LAST_NAME
                    };
                    lst.Add(item);
                }
                return Json(new
                {
                    error = 0,
                    lstModel = lst,
                }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { error = 1 }, JsonRequestBehavior.AllowGet);

        }


        #endregion
        public ActionResult AdminStoriesExcel(string category,string subcategory,string rating,string market,string events,string searchname)
        {

            StoryBL _storybl = new StoryBL();
            IList<StoryModel> stories = _storybl.GetAll(category, subcategory, rating, market, events, searchname);

            StoryCategoryBL _storycatbl = new StoryCategoryBL();
            StoryCategoryModel _storycat;
            StorySubCategoryBL _storysubcatbl = new StorySubCategoryBL();
            StorySubCategoryModel _storysubcat;
            StoryRatingBL _ratingbl = new StoryRatingBL();
            StoryRatingModel _rating;
            ProfileBL _profilebl = new ProfileBL();
            ProfileModel _profile;

            foreach (StoryModel m in stories )
            {
                decimal dCategory = (m.STORY_CATEGORY == null ? 0 :(decimal)m.STORY_CATEGORY);
                decimal dsubCategory = (m.STORY_SUBCATEGORY == null ? 0 : (decimal)m.STORY_SUBCATEGORY);
                decimal dRating = (m.STORY_RATING == null ? 0 : (decimal)m.STORY_RATING);
               
                _storycat = _storycatbl.GetStoryCategoryModelByID(dCategory);
                _storysubcat = _storysubcatbl.GetStorySubCategoryModelByID(dsubCategory);
                _rating = _ratingbl.GetStoryRatingModelByID(dRating);
                _profile = _profilebl.GetProfileByStarzId(m.STARS_ID);
                m.Category  = _storycat;
                m.SubCategory = _storysubcat;
                m.Rating = _rating;
                m.Profile = _profile;
                

            }
            return PartialView(stories);
        }
	}

}