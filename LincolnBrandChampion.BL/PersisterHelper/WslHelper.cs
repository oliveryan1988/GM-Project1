using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


using LincolnBrandChampion.BL.PersisterHelper;

namespace LincolnBrandChampion.BL.Helpers
{
   public class WslHelper
    {
       public WslxEntity validateWSL()
       {
           WslxEntity resultModel = new WslxEntity();

           #region bypass
           if (
              System.Configuration.ConfigurationManager.AppSettings["bypassWSL"] != null &&
              System.Configuration.ConfigurationManager.AppSettings["bypassWSL"].ToString().Equals("yes")
             )
           {

               resultModel.WUserType = "ADMIN"; // "SUPPLIER" "DEALER";"ADMIN";
               resultModel.WWSLX = "v-birbi5";//"vabes";// "v-birbi2"//"reyete";//"C-BANKS";--dealer//"v-birbi5"--Vendim
               resultModel.WOrgCode = "DEALER";
               resultModel.WEmp = "?";
               resultModel.WRole = "DEALER"; // "DEALER"
               resultModel.WOrg = "FE46";//"03605"; // "48009", "FE46"--Vendim;"12351"--dealer;
               resultModel.WResult = true;
               return resultModel;
           }
           #endregion

           resultModel.WResult = false;

           CoreWSL.CoreWSL oWSL = new CoreWSL.CoreWSL();

           try
           {
               string errmsg = "Invalid View";
               if (HttpContext.Current.Request.ServerVariables["HTTP_COOKIE"] is object)
               {
                   errmsg = oWSL.CoreWSL_Validate(HttpContext.Current.Request.ServerVariables["HTTP_COOKIE"].ToString());

                   if (errmsg.ToString() == "")
                   {
                       resultModel.WUserType = HttpContext.Current.Session["w_usertype"].ToString().ToUpper().Trim();
                       resultModel.WWSLX = HttpContext.Current.Session["w_user"].ToString().ToUpper().Trim();
                       resultModel.WOrgCode = HttpContext.Current.Session["w_location"].ToString().ToUpper().Trim();
                       resultModel.WEmp = HttpContext.Current.Session["w_role"].ToString().ToUpper().Trim();
                       resultModel.WRole = HttpContext.Current.Session["w_username"].ToString().ToUpper().Trim();
                       resultModel.WOrg = HttpContext.Current.Session["w_pacode"].ToString().ToUpper().Trim();
                   }
               }

               if (errmsg.ToString() == "")
                   resultModel.WResult = true;

               return resultModel;
           }
           catch (Exception ex)
           {
               throw (ex);
           }
           finally
           {
               oWSL.Dispose();
               oWSL = null;
           }
       }
    }
}
