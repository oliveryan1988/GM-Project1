using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LincolnBrandChampion.Web.Helpers;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.BL.Persisters;
using PagedList;
using System.IO;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace LincolnBrandChampion.Web.Controllers
{
    public class BrandChampionController : Controller
    {
        //
        // GET: /BrandChampion/
        ProfileBL _profileBl = new ProfileBL();

        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult LBCProfile(string ID = null)
        {
            if (Convert.ToInt32(Session["ROLE_ID"]) == 5 || Convert.ToInt32(Session["ROLE_ID"]) == 4 || Convert.ToInt32(Session["ROLE_ID"]) == 2)
            {
                ViewBag.homeid = "profile";
            }
            else
            {
                ViewBag.homeid = "home";
            }
            ProfileModel model = new ProfileModel();
            ProfileBL _profile = new ProfileBL();
            ProfileRecognitionMasterBL _recognition = new ProfileRecognitionMasterBL();
            ProfileRecognitionBL _recog = new ProfileRecognitionBL();



            var _id = System.Web.HttpContext.Current.Session["w_user"].ToString();

            if (ID == null || Convert.ToInt32(Session["ROLE_ID"]) == 1)
            {
                model = _profile.GetProfileBy(_id);
                model.recognitionList = _recog.GetAll(Session["StarsIdProfile"].ToString()); 
                //model.recognitionList = _recognition.GetByStarsId(Session["StarsIdProfile"].ToString());
            }
            else
            {
                model = _profile.GetProfileByStarzId(ID);
                model.recognitionList = _recog.GetAll(ID); 
                //model.recognitionList = _recognition.GetByStarsId(ID);

            }
            if ( model != null){
                if (!String.IsNullOrWhiteSpace(model.DLR_PHONE))
                {
                    model.phone1 = model.DLR_PHONE.Substring(0, 3);
                    model.phone2 = model.DLR_PHONE.Substring(3, 3);
                    model.phone3 = model.DLR_PHONE.Substring(6, 4);
                }
                
                if (!String.IsNullOrWhiteSpace(model.PHONE) && model.PHONE.Length == 10)
                {
                    model.mobile1 = model.PHONE.Substring(0, 3);
                    model.mobile2 = model.PHONE.Substring(3, 3);
                    model.mobile3 = model.PHONE.Substring(6, 4);
                }
                else
                {   model.mobile1 = null;
                    model.mobile2 = null;
                    model.mobile3 = null;
                    model.PHONE = null;
                }
            }

           // model.recognitionMasterList = _recognition.GetAll();
           // model.recognitionList = _recog.GetAll(starzId); 
            return View(model);

        }

       


        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers)]
        public ActionResult Upload(HttpPostedFileBase File, string STARS_ID, string w_pacode)
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


                }
                else if (!arr.Contains(filename.ToLower()))
                {

                }
                else
                {

                    ProfileBL profile = new ProfileBL();
                    filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid() + "_" + w_pacode + "." + filename;
                    string targetfolder = Server.MapPath(ConfigurationManager.AppSettings["UploadPath"] + filename);
                    Bitmap origImage = new Bitmap(File.InputStream);
                    Image img = (Image)origImage;
                    img = ImageResizeHelper.ImageResize(origImage, 200, 200, false, false);
                    Bitmap newImage = new Bitmap(img);
                    //File = ImageResizeHelper.ImageResize(File, 183, 136, false, false);
                    newImage.Save(targetfolder);
                    //File.SaveAs(targetfolder);
                    profile.SaveProfilUploadImage(filename, STARS_ID);


                }

            }
            return RedirectToAction("LBCProfile", "BrandChampion");
        }
        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers)]
            public ActionResult SaveProfileUser(ProfileModel model) 
        { 
            ProfileBL profile = new ProfileBL();

            if (!string.IsNullOrEmpty(model.mobile1) && !string.IsNullOrEmpty(model.mobile2) && !string.IsNullOrEmpty(model.mobile3))
            {
                if (model.mobile1.Length + model.mobile2.Length + model.mobile3.Length == 10)
                {
                    model.PHONE = model.mobile1 + model.mobile2 + model.mobile3;
                }
                else
                { model.PHONE = null; }
            }
            else
            {
                model.PHONE = null;
                model.mobile1 = null;
                model.mobile2 = null;
                model.mobile3 = null;
            }
            model.UPDATED_BY = System.Web.HttpContext.Current.Session["w_user"].ToString(); 
            profile.UpdateProfileByStarsId(model); 
            return RedirectToAction("LBCProfile");
        }

        [HttpPost]
        [AuthorizedAttributeHelper(LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult SaveProfileUserAdmin(ProfileModel model, FormCollection frm)
        {
            ProfileBL profile = new ProfileBL();
            ProfileRecognitionBL _recog = new ProfileRecognitionBL();
            ProfileRecognition _modelpr = new ProfileRecognition();
            ProfileRecognitionMasterBL _recogmaster = new ProfileRecognitionMasterBL();

            if (!string.IsNullOrEmpty(model.mobile1) && !string.IsNullOrEmpty(model.mobile2) && !string.IsNullOrEmpty(model.mobile3))
            {
                if (model.mobile1.Length + model.mobile2.Length + model.mobile3.Length == 10)
                {
                    model.PHONE = model.mobile1 + model.mobile2 + model.mobile3;
                }
                else
                { model.PHONE = null; }
            }
            else
            {
                model.PHONE = null;
                model.mobile1 = null;
                model.mobile2 = null;
                model.mobile3 = null;
            }
            model.UPDATED_BY = System.Web.HttpContext.Current.Session["w_user"].ToString();
            profile.UpdateProfileByStarsIdAdmin(model);

            List<ProfileRecognition> lst = new List<ProfileRecognition>();
            lst = _recog.GetRecognitionByStarsId(model.STARS_ID);
            List<ProfileRecognitionMasterModel> lstmaster = new List<ProfileRecognitionMasterModel>();
            lstmaster = _recogmaster.GetAll();

            if (lst.Count > 0)
            {
                for (int i = 1; i <= lstmaster.Count; i++)
                {

                    _modelpr.STATUS =  frm.GetValue(""+i+"").AttemptedValue;//frm[""+i+""].ToString().GEt
                    _modelpr.UPDATE_DATE = DateTime.Now;
                    _modelpr.UPDATED_BY = model.WSLX_ID;
                    _modelpr.STARS_ID = model.STARS_ID;
                    _modelpr.RECOGNITION_ID = i;
                    _recog.Update(_modelpr);
                   
                }

            }
            else
            {
                for (int i = 1; i <= lstmaster.Count; i++)
                {

                    _modelpr.STATUS = frm.GetValue("" + i + "").AttemptedValue;
                    _modelpr.CREATED_DATE = DateTime.Now;
                    _modelpr.STARS_ID = model.STARS_ID;
                    _modelpr.RECOGNITION_ID = i;
                    _modelpr.CREATED_BY =model.WSLX_ID;
                    _recog.Save(_modelpr);
                }
            }

        //    String i = frm["1"].ToString();
            return RedirectToAction("LBCProfile", new { ID = model.STARS_ID });
        }
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
      public ActionResult MakeConnections(string search, string filter, int? page, string order, string asc)
        {
            if (Convert.ToInt32(Session["ROLE_ID"]) == 5 || Convert.ToInt32(Session["ROLE_ID"]) == 4 || Convert.ToInt32(Session["ROLE_ID"]) == 2)
            {
                ViewBag.homeid = "make-connections";
            }
            else
            {
                ViewBag.homeid = "home";
            }
            ViewBag.search = string.IsNullOrEmpty(search) ? "ALL" : search;
            ViewBag.filter = string.IsNullOrEmpty(filter) ? string.Empty : filter;
            ViewBag.page = page ?? 1;
            int pag = page ?? 1;
            IPagedList<ProfileModel> lstModel = _profileBl.GetAll(search, filter, pag, 10, order, asc);
            return View(lstModel);
        }
    }
}