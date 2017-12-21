using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LincolnBrandChampion.Web.Helpers;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.BL.Persisters;
using System.Configuration;
using System.Drawing;
using ReportManagement;

namespace LincolnBrandChampion.Web.Controllers
{
    public class StoryPdfController : PdfViewController
    {
        [AuthorizedAttributeHelper(LBC_Role.LBCDealers, LBC_Role.MKS, LBC_Role.GenAdmin, LBC_Role.Admin, LBC_Role.Super_Admin)]
        public ActionResult PrintStory(int id)
        {
            StoryBL _storybl = new StoryBL();
            StoryModel _story = new StoryModel();
            _story = _storybl.getStoriesBySEQID(id);

            ProfileModel pModel = new ProfileBL().GetProfileByStarzId(Session["StarsIdProfile"].ToString());
            _story.Profile = pModel;

            return this.ViewPdfLandscape(" ", "PrintStory", _story);
        }
    }
}