using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LincolnBrandChampion.BL.Helpers;
using LincolnBrandChampion.BL.PersisterHelper;
using LincolnBrandChampion.BL.Persisters;
using LincolnBrandChampion.Model.Models;


namespace LincolnBrandChampion.Web.Helpers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class WSLxAuthorizeFilter : FilterAttribute, IAuthorizationFilter
    {
        
        public void OnAuthorization(AuthorizationContext filterContext)
        {

            try
            {
                if (filterContext.HttpContext.Request.Url.LocalPath.ToLower().EndsWith("/httperrors/noauthorized"))
                    return;

                WslxEntity curWSLXModel = new WslHelper().validateWSL();

                if (curWSLXModel.WResult)
                {
                    string w_ma = "N";
                    string w_ford = "N";
                    string w_special_user = "N";

                    if (curWSLXModel.WOrg.ToUpper() == "FE46" || curWSLXModel.WOrg.ToUpper() == "99999" || curWSLXModel.WOrg.ToUpper() == "FE46F" || curWSLXModel.WWSLX.ToUpper() == "DBROTHE7")
                        w_ma = "Y";

                    if (curWSLXModel.WOrg.ToUpper() == "MKS" || curWSLXModel.WOrg.ToUpper() == "FOE" || curWSLXModel.WRole.ToUpper() == "GENMGR" || curWSLXModel.WOrg.ToUpper() == "BMFXA" || curWSLXModel.WOrg.ToUpper() == "GHFUA")
                        w_ford = "Y";

                    if (curWSLXModel.WWSLX.ToUpper() == "DBROTHE7" || curWSLXModel.WWSLX.ToUpper() == "J-TELEHA" || curWSLXModel.WWSLX.ToUpper() == "S-DOUG22")
                        w_special_user = "Y";

                    HttpContext.Current.Session["WSLXID"] = curWSLXModel.WWSLX;
                    HttpContext.Current.Session["PA_CODE"] = curWSLXModel.WOrg;
                    HttpContext.Current.Session["UserName"] = curWSLXModel.WRole;
                    HttpContext.Current.Session["UserType"] = curWSLXModel.WUserType == null ? string.Empty : curWSLXModel.WUserType;
                    HttpContext.Current.Session["Title"] = string.Empty;
                    HttpContext.Current.Session["Email"] = string.Empty;
                    HttpContext.Current.Session["SuperDealerCode"] = string.Empty;

                    if (curWSLXModel.WRole.ToLower().Equals("dealer"))
                    {
                        UsuarioBL _usuarioBl = new UsuarioBL();
                        UserModel model = _usuarioBl.GetUserBy(curWSLXModel.WWSLX);
                        if (model != null)
                        {
                            ProfileBL _profile = new ProfileBL();
                            if (_profile.GetStarsIdBy(model.USR_WSLX_ID))                            
                            {
                                HttpContext.Current.Session["starsId"] = string.Empty;
                            }
                            

                        }
                        else
                        {
                            filterContext.Result = new RedirectResult("~/HttpErrors/NoAuthorized", false);
                        }
                    }

                   
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/HttpErrors/NoAuthorized", false);
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/HttpErrors/NoAuthorized", false);
            }
        }
    }


}