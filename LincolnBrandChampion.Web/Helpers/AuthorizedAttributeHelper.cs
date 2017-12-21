using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using LincolnBrandChampion.BL.Helpers;
using LincolnBrandChampion.BL.PersisterHelper;
using LincolnBrandChampion.BL.Persisters;
using LincolnBrandChampion.Model.Models;
using System.Web.Security;


namespace LincolnBrandChampion.Web.Helpers
{

    public static class LBC_Role
    {

        public const int Super_Admin = 5;
        public const int Admin = 4;
        public const int GenAdmin = 3;
        public const int MKS = 2;
        public const int LBCDealers = 1;
    }
    public class AuthorizedAttributeHelper : FilterAttribute, IAuthorizationFilter
    {

        private readonly int[] access_levels;
        public AuthorizedAttributeHelper(params int[] _access_levels)
        {
            access_levels = _access_levels;
        }

        public AuthorizedAttributeHelper()
        {
            access_levels = null;
        }

        string errorcode = "N/A";

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                bool isAlreadyLogedIn = HttpContext.Current.Session["w_user"] != null;
                //HttpContext.Current.Session["showVideo"] = false;
                //Validate the WSL Cookie
                WslxEntity curWSLXModel = new WslHelper().validateWSL();
                HttpContext.Current.Session["userId"] = curWSLXModel.WWSLX;

                UsuarioBL _usuarioBl = new UsuarioBL();
                UserModel model = _usuarioBl.GetUserBy(curWSLXModel.WWSLX);

                if (curWSLXModel.WResult)
                {
                    if (HttpContext.Current.Session["ROLE_ID"] == null)
                    {
                        ReviewUserData(model);
                    }

                    if (!isAlreadyLogedIn)
                    {
                        LoginTrackingModel LOGIN_TRACKING = new LoginTrackingModel
                        {
                            CREATE_DATE = DateTime.Now,
                            CREATED_BY = curWSLXModel.WWSLX,
                            LOGIN_DATE = DateTime.Now,
                            LOGIN_TIME = DateTime.Now.Hour,
                            USERID = curWSLXModel.WWSLX,
                            ACI = curWSLXModel.WUserType,
                            SITE = (HttpContext.Current.Session["w_sitecode"] != null) ? HttpContext.Current.Session["w_sitecode"].ToString().ToUpper().Trim() : "",
                            ORGCODE = (HttpContext.Current.Session["w_location"] != null) ? HttpContext.Current.Session["w_location"].ToString().ToUpper().Trim() : "",
                            EMPCODE = (HttpContext.Current.Session["w_role"] != null) ? HttpContext.Current.Session["w_role"].ToString().ToUpper().Trim() : "",
                            MRROLE = (HttpContext.Current.Session["w_username"] != null) ? HttpContext.Current.Session["w_username"].ToString().ToUpper().Trim() : "",
                            ORG = (HttpContext.Current.Session["w_pacode"] != null) ? HttpContext.Current.Session["w_pacode"].ToString().ToUpper().Trim() : ""
                        };
                        LoginTrackingBL.AddLOGIN_TRACKING(LOGIN_TRACKING);
                        //HttpContext.Current.Session["showVideo"] = true;
                    }
                    HttpContext.Current.Session["w_user"] = curWSLXModel.WWSLX;                 
                    
                    HttpContext.Current.Session["w_pacode"] = curWSLXModel.WOrg;
                    //this will need to change accordingly based on the client request.
                    HttpContext.Current.Session["User_Id"] = curWSLXModel.WWSLX;

                    if (curWSLXModel.WUserType.ToString().ToUpper().Trim() == "DEALER")
                    {
                        ProfileBL _profile = new ProfileBL();
                        ProfileModel profileModel = new ProfileModel();
                        profileModel = _profile.GetProfileBy(curWSLXModel.WWSLX);
                         HttpContext.Current.Session["ShowPopUpS"] =null;

                        if (model.USR_WSLX_ID != null)
                        {
                            HttpContext.Current.Session["ROLE_ID"] = Convert.ToString(LBC_Role.LBCDealers);
                            model.USR_ROLE_ID = Convert.ToDecimal(LBC_Role.LBCDealers);
                            HttpContext.Current.Session["UserName"] = curWSLXModel.WRole;
                            
                            if (!profileModel.haveProfileWslxId)
                            {
                                HttpContext.Current.Session["ShowPopUpS"] = true;
                            }
                            else
                            {
                                HttpContext.Current.Session["StarsIdProfile"] = profileModel.STARS_ID;

                            }
                            HttpContext.Current.Session["User_Id"] = profileModel.FIRST_NAME != null ? (profileModel.FIRST_NAME + " " + profileModel.LAST_NAME) : curWSLXModel.WWSLX;
                        }
                        else if (profileModel.WSLX_ID == null)
                        {
                            HttpContext.Current.Session["ShowPopUpS"] = true;
                            HttpContext.Current.Session["ROLE_ID"] = Convert.ToString(LBC_Role.LBCDealers);
                            model.USR_ROLE_ID = LBC_Role.LBCDealers;
                           // filterContext.Result = new RedirectResult("~/LBC/Welcome", true);
                        }
                        else
                        {
                            HttpContext.Current.Session["StarsIdProfile"] = profileModel.STARS_ID;
                            HttpContext.Current.Session["User_Id"] = profileModel.FIRST_NAME != null ? (profileModel.FIRST_NAME + " " + profileModel.LAST_NAME) : curWSLXModel.WWSLX;
                            HttpContext.Current.Session["ROLE_ID"] = Convert.ToString(LBC_Role.LBCDealers);
                            model.USR_ROLE_ID = LBC_Role.LBCDealers;
                           
                        }
                    }
                    else if ((curWSLXModel.WOrg.ToString().ToUpper().Trim() == "MKS" || curWSLXModel.WOrg.ToString().ToUpper().Trim() == "FNAMR") && model.USR_WSLX_ID == null)
                    {
                        HttpContext.Current.Session["ROLE_ID"] = Convert.ToString(LBC_Role.MKS);
                        model.USR_ROLE_ID = Convert.ToDecimal(LBC_Role.MKS);
                       
                        HttpContext.Current.Session["User_Id"] = curWSLXModel.WWSLX;
                        // this is a Lincoln Empoyee Role
                    }
                    else{
                        if (model != null && model.USR_WSLX_ID != null) 
            { 
                HttpContext.Current.Session["ROLE_ID"] = model.USR_ROLE_ID; 
               // HttpContext.Current.Session["User_Id"] = model.USR_WSLX_ID; 
                HttpContext.Current.Session["User_Id"] = model.USR_FIRST_NAME + " " + model.USR_LAST_NAME;  
            }

                        // This Case we will check from the database Admin table
                        // var model = new LoginModel();
                        //model.Permission = new CommonRepository().GetPermission(wslId);
                        //if (model.Permission != null)
                        //{
                        //   return;
                        //}
                        //else
                        //{
                        //   errorcode = wslId + " is not Registered";
                        //  HttpContext.Current.Session["errorcode"] = errorcode;
                        // filterContext.Result = new RedirectResult("~/Home/Error?id=" + wslId);
                        //}
                    }
                }
                else
                {
                    HttpContext.Current.Session["ViewType"] = "ViewNoAccess";
                    
                    filterContext.Result = new RedirectResult("~/HttpErrors/NoAuthorized", false);
                }

                if (access_levels != null)
                {
                    bool permitAccess = false;
                    foreach (int access_level in access_levels)
                    {
                        if (model.USR_ROLE_ID == access_level)
                        {
                            permitAccess = true;
                            break;
                        }
                    }

                    if (!permitAccess)
                    {
                        filterContext.Result = new RedirectResult("~/HttpErrors/NoAuthorized", false);
                    }
                }
            }
            catch (Exception ex)
            {
                errorcode = ex.Message;
                HttpContext.Current.Session["User_Id"] ="";
                HttpContext.Current.Session["UserName"] = "";
                HttpContext.Current.Session["errorcode"] = errorcode + " " + ex.Message;
                filterContext.Result = new RedirectResult("~/HttpErrors/Http404", false);
                throw ex;
            }
        }

        public void ReviewUserData(UserModel model)
        {
            if (model != null && model.USR_WSLX_ID != null)
            {
                HttpContext.Current.Session["ROLE_ID"] = model.USR_ROLE_ID;
              //HttpContext.Current.Session["User_Id"] = model.USR_WSLX_ID;
                 HttpContext.Current.Session["User_Id"] = model.USR_FIRST_NAME + " " + model.USR_LAST_NAME;
              // HttpContext.Current.Session["User_Id"] = model.USR_FIRST_NAME != null ? (model.USR_FIRST_NAME + " " + model.USR_LAST_NAME) : model.USR_WSLX_ID;

            }
            else
            {
                HttpContext.Current.Session["ROLE_ID"] = null;
                HttpContext.Current.Session["User_Id"] = "";

            }
        }

    }


}