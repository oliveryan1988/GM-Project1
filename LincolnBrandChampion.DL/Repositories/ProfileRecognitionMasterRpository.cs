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
    public class ProfileRecognitionMasterRpository
    {
        public List<ProfileRecognitionMasterModel> GetAll()
        {
            List<ProfileRecognitionMasterModel> list = new List<ProfileRecognitionMasterModel>();
           
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = (from entity in context.LBC_PROFILE_RECOGNITION_MASTER 
                           select entity).ToList();

                foreach (LBC_PROFILE_RECOGNITION_MASTER item in lst)
                {
                    list.Add(new ProfileRecognitionMasterModel()
                    {
                        CATEGORY = item.CATEGORY,
                        CREATED_DATE = item.CREATED_DATE,
                        CREATED_BY = item.CREATED_BY,
                        RECOGNITION_DESC = item.RECOGNITION_DESC,
                        RECOGNITION_ID = item.RECOGNITION_ID,
                        RECOGNITION_TITLE = item.RECOGNITION_TITLE,
                        UPDATE_DATE = item.UPDATE_DATE,
                        UPDATED_BY = item.UPDATED_BY                  
                        

                    });
                }

            }

            return list;
        }

        public bool Save(ProfileRecognitionMasterModel model)
        {
            bool save = true;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    LBC_PROFILE_RECOGNITION_MASTER _profileM = new LBC_PROFILE_RECOGNITION_MASTER()
                    {
                        CATEGORY = model.CATEGORY,
                        CREATED_DATE = model.CREATED_DATE,
                        CREATED_BY = model.CREATED_BY,
                        RECOGNITION_DESC = model.RECOGNITION_DESC,
                        RECOGNITION_ID = model.RECOGNITION_ID,
                        RECOGNITION_TITLE = model.RECOGNITION_TITLE,
                        UPDATE_DATE = model.UPDATE_DATE,
                        UPDATED_BY = model.UPDATED_BY
                    };

                    context.LBC_PROFILE_RECOGNITION_MASTER.Add(_profileM);
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                save = false;
            }
            return save;
        }

        public bool Update(ProfileRecognitionMasterModel model)
        {
            bool save = true;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _profile = (from p in context.LBC_PROFILE_RECOGNITION_MASTER
                                    where p.RECOGNITION_ID == model.RECOGNITION_ID
                                    select p).FirstOrDefault();

                    _profile.CATEGORY = model.CATEGORY;
                    _profile.CREATED_DATE = model.CREATED_DATE;
                    _profile.CREATED_BY = model.CREATED_BY;
                    _profile.RECOGNITION_DESC = model.RECOGNITION_DESC;
                    _profile.RECOGNITION_ID = model.RECOGNITION_ID;
                    _profile.RECOGNITION_TITLE = model.RECOGNITION_TITLE;
                    _profile.UPDATE_DATE = model.UPDATE_DATE;
                    _profile.UPDATED_BY = model.UPDATED_BY;

                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                save = false;
            }
            return save;
        }
    }
}
