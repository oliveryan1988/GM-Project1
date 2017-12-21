using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.DL.Data;
using LincolnBrandChampion.DL.Helpers;

namespace LincolnBrandChampion.DL.Repositories
{
    public class ResourceRepository
    {
        /// <summary>
        /// Save all the clicks for the users when they download a document
        /// </summary>
        /// <param name="model"></param>
        public void SaveResourceTracking(ResourceTracking model)
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                LBC_RESOURCE_TRACKING _resourceTrack = new LBC_RESOURCE_TRACKING()
                {
                    STARS_ID = model.STARS_ID,
                    WSL_ID = model.WSL_ID,
                    PA_CODE = model.PA_CODE,
                    DOC_URL = model.DOC_URL,
                    FILE_NAME = model.FILE_NAME,
                    DOWNLOAD_TIME = model.DOWNLOAD_TIME,
                    SECTION = model.SECTION,
                    CREATED_DATE = model.CREATED_DATE,
                    CREATED_BY = model.CREATED_BY
                };
                context.LBC_RESOURCE_TRACKING.Add(_resourceTrack);
                context.SaveChanges();
            }
        }
    }
}
