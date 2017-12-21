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
    public class ProfileRepository
    {
        public bool HaveStarsIdBy(string wslxId)
        {
            bool haveStarsId = false;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _profile = (from p in context.LBC_PROFILE
                                where p.WSLX_ID == wslxId
                                select p).FirstOrDefault();

                if (_profile != null)
                {
                    haveStarsId = true;
                }

            }
            return haveStarsId;

        }

        public List<string> GetMarketList()
        {
            List<string> lst = new List<string>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
               
                   var list = (from entity in context.LBC_PROFILE
                                //where entity.DLR_REGION == dlrRegion || string.IsNullOrEmpty(entity.DLR_REGION)
                                select entity.DLR_REGION).ToList().Distinct();


                   lst = list.ToList();


            }
            return lst;
        }

        public ProfileModel GetProfileBy(string wslxId)
        {
            ProfileModel model = new ProfileModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _profile = (from p in context.LBC_PROFILE
                                where p.WSLX_ID == wslxId && p.EMP_STATUS_CODE == "A"
                                select p).FirstOrDefault();

                model = MapModelFromLBC_Profile(_profile);
            }
            return model;
        }

        public ProfileModel GetProfileByStarsId(string starsId)
        {
            ProfileModel model = new ProfileModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _profile = (from p in context.LBC_PROFILE
                                where p.STARS_ID == starsId && p.EMP_STATUS_CODE == "A"
                                select p).FirstOrDefault();

                model = MapModelFromLBC_Profile(_profile);
            }
            return model;
        }

        public ProfileModel GetAnyProfileByStarsId(string starsId)
        {
            ProfileModel model = new ProfileModel();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _profile = (from p in context.LBC_PROFILE
                                where p.STARS_ID == starsId
                                select p).FirstOrDefault();

                model = MapModelFromLBC_Profile(_profile);
            }
            return model;
        }

        public void SaveProfileWslxId(ProfileModel model)
        {
            LBC_PROFILE _profile = new LBC_PROFILE();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                _profile = MapLBC_PROFILEFromModel(model);
                _profile.CREATED_BY = _profile.WSLX_ID;
                _profile.CREATED_DATE = DateTime.Now;
                context.LBC_PROFILE.Add(_profile);
                context.SaveChanges();

            }
        }

        public void SaveProfilUploadImage(string photoPath, string starsId)
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _profile = (from p in context.LBC_PROFILE
                                where p.STARS_ID == starsId
                                select p).FirstOrDefault();

                _profile.PHOTO_PATH = photoPath;
                _profile.UPDATED_BY = _profile.WSLX_ID;
                _profile.UPDATE_DATE = DateTime.Now;
                context.SaveChanges();

            }
        }
         public void UpdateProfileByStarsIdAdmin(ProfileModel model)
         {
             using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
             {
                 var _profile = (from p in context.LBC_PROFILE
                                 where p.STARS_ID == model.STARS_ID
                                 select p).FirstOrDefault();
                _profile.EMAIL_ID = model.EMAIL_ID;
                _profile.PHONE = model.PHONE;
                _profile.SHIRT_SIZE = model.SHIRT_SIZE;
                _profile.ADMIN_NOTES = model.ADMIN_NOTES;
                _profile.UPDATED_BY = model.WSLX_ID;
                _profile.UPDATE_DATE = DateTime.Now;

                context.SaveChanges();

            }
        }

        public void UpdateProfileByStarsId(ProfileModel model)
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _profile = (from p in context.LBC_PROFILE
                                where p.STARS_ID == model.STARS_ID
                                select p).FirstOrDefault();

                _profile.BIOGRAPHY = model.BIOGRAPHY;
                //_profile.CREATED_BY = model.CREATED_BY;
                //_profile.CREATED_DATE = model.CREATED_DATE;
                _profile.DEPARTMENT = model.DEPARTMENT;
                _profile.DIETARY_RESTRICTION = model.DIETARY_RESTRICTION;
                _profile.DLR_ADDRESS = model.DLR_ADDRESS;
                _profile.DLR_CITY = model.DLR_CITY;
                //_profile.DLR_NAME = model.DLR_NAME;
                //_profile.DLR_PHONE = model.DLR_PHONE;
                //_profile.DLR_REGION = model.DLR_REGION;
                _profile.DLR_STATE = model.DLR_STATE;
                _profile.DLR_ZIP = model.DLR_ZIP;
                _profile.EMAIL_ID = model.EMAIL_ID;
                //_profile.EMP_STATUS_CODE = model.EMP_STATUS_CODE;
                _profile.FIRST_NAME = model.FIRST_NAME;
                _profile.LAST_NAME = model.LAST_NAME;
                _profile.BADGE_NAME = model.BADGE_NAME;
                //_profile.LBC_CERT = model.LBC_CERT;
                _profile.PA_CODE = model.PA_CODE;
                _profile.PHONE = model.PHONE;
                //_profile.PHOTO_PATH = model.PHOTO_PATH;
                _profile.PROFILE_NOTE = model.PROFILE_NOTE;
                _profile.PROFILE_TYPE = model.PROFILE_TYPE;
                _profile.SHIRT_SIZE = model.SHIRT_SIZE;
                _profile.TITLE = model.TITLE;
                //_profile.STARS_ID = model.STARS_ID;
                //_profile.WSLX_ID = model.WSLX_ID;
                _profile.UPDATED_BY = model.WSLX_ID;
                _profile.UPDATE_DATE = DateTime.Now;

                context.SaveChanges();

            }
        }

        /// <summary>
        /// Method that Provide the Stars Id validation on the pop up
        /// If the user Exist it will update other wise kick them out
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateProfileWslxIdByStarsId(ProfileModel model)
        {
            bool flag = false;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _profile = (from p in context.LBC_PROFILE
                                where p.STARS_ID == model.STARS_ID && p.PA_CODE == model.PA_CODE && p.EMP_STATUS_CODE == "A"
                                select p).FirstOrDefault();

                if (_profile != null)
                {
                    _profile.STARS_ID = model.STARS_ID;
                    _profile.WSLX_ID = model.WSLX_ID;
                    _profile.UPDATED_BY = model.WSLX_ID;
                    _profile.UPDATE_DATE = DateTime.Now;

                    context.SaveChanges();
                    flag = true;
                }

            }
            return flag;
        }

        public void UpdateWSLXByStarsId(ProfileModel model)
        {
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var _profile = (from p in context.LBC_PROFILE
                                where p.STARS_ID == model.STARS_ID
                                select p).FirstOrDefault();

                _profile.WSLX_ID = model.WSLX_ID;
                _profile.UPDATED_BY = model.WSLX_ID;
                _profile.UPDATE_DATE = DateTime.Now;

                context.SaveChanges();

            }
        }


        public IPagedList<ProfileModel> GetAll(string search, string filter, int page, int pageSize, string order = "", string asc = "")
        {
            search = string.IsNullOrEmpty(search) ? string.Empty : search;
            search = search.ToUpper().Equals("ALL") ? string.Empty : search;
            filter = string.IsNullOrEmpty(filter) ? string.Empty : filter;
            List<ProfileModel> _lst = new List<ProfileModel>();
            IEnumerable<ProfileModel> _lstTemp;
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var list = (from entity in context.LBC_PROFILE
                            where entity.FIRST_NAME.StartsWith(search) && entity.EMP_STATUS_CODE =="A"
                            select entity).ToList();
                if (!string.IsNullOrEmpty(filter))
                {
                    list.Clear();
                    list = (from entity in context.LBC_PROFILE
                            where entity.EMP_STATUS_CODE == "A" && (entity.FIRST_NAME.ToLower().Contains(filter.ToLower()) || entity.LAST_NAME.ToLower().Contains(filter.ToLower()))
                            select entity).ToList();

                }

                foreach (LBC_PROFILE item in list)
                {
                    _lst.Add(MapModelFromLBC_Profile(item));
                }
            }
            _lstTemp = _lst.AsQueryable();
            return _lstTemp.ToPagedList<ProfileModel>(page, pageSize);

        }
       
        public List<ProfileModel> GetListProfileBy(string dlrRegion)
        {
            List<ProfileModel> lst = new List<ProfileModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                if (string.IsNullOrEmpty(dlrRegion))
                {
                    var list = (from entity in context.LBC_PROFILE
                                 where entity.EMP_STATUS_CODE =="A"
                                //where entity.DLR_REGION == dlrRegion || string.IsNullOrEmpty(entity.DLR_REGION)
                                select entity).ToList();
                    foreach (LBC_PROFILE item in list)
                    {
                        lst.Add(MapModelFromLBC_Profile(item));
                    }
                }
                else
                {
                    var list = (from entity in context.LBC_PROFILE
                                where entity.DLR_REGION == dlrRegion && entity.EMP_STATUS_CODE =="A"
                                select entity).ToList();
                    foreach (LBC_PROFILE item in list)
                    {
                        lst.Add(MapModelFromLBC_Profile(item));
                    }
                }

            }
            return lst;
        }

        public List<ProfileModel> GetListProfileByStarsId(List<string> StarsIdList = null)
        {
            List<ProfileModel> lst = new List<ProfileModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var list = (from entity in context.LBC_PROFILE
                            where entity.EMP_STATUS_CODE == "A"
                            select entity).ToList();

                if (StarsIdList != null)
                {
                    for (int i = 0; i < StarsIdList.Count(); i++)
                    {
                        var entry = list.Where(q => q.STARS_ID == StarsIdList[i]).FirstOrDefault();
                        if (entry != null)
                        {
                            lst.Add(MapModelFromLBC_Profile(entry));
                        }
                    }
                }
                else
                {
                    foreach (LBC_PROFILE item in list)
                    {
                        lst.Add(MapModelFromLBC_Profile(item));
                    }
                }              

            }
            return lst;
        }

        public List<ProfileModel> GetListProfileAndRecognitionStatusBy(string dlrRegion)
        {
            List<ProfileModel> lst = new List<ProfileModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                if (string.IsNullOrEmpty(dlrRegion))
                {
                    var list = (from entity in context.LBC_PROFILE
                                where entity.EMP_STATUS_CODE == "A"
                                //where entity.DLR_REGION == dlrRegion || string.IsNullOrEmpty(entity.DLR_REGION)
                                select entity).ToList();
                    foreach (LBC_PROFILE item in list)
                    {
                        lst.Add(MapModelFromLBC_Profile(item));
                    }
                }
                else
                {
                    var list = (from entity in context.LBC_PROFILE
                                where entity.DLR_REGION == dlrRegion
                                && entity.EMP_STATUS_CODE == "A"
                                select entity).ToList();
                    foreach (LBC_PROFILE item in list)
                    {
                        lst.Add(MapModelFromLBC_Profile(item));
                    }
                }

            }

            foreach (ProfileModel item in lst)
            {
                ProfileRecognitionRepository _recognition = new ProfileRecognitionRepository();
                item.recognitionList = _recognition.GetAll(item.STARS_ID);
            }

            return lst;
        }

        public List<ProfileModel> GetListProfileAndRecognitionStatusBy(List<string> StarsIdList = null)
        {
            List<ProfileModel> lst = new List<ProfileModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var list = (from entity in context.LBC_PROFILE
                            where entity.EMP_STATUS_CODE == "A"
                            select entity).ToList();

                if (StarsIdList != null)
                {
                    for (int i = 0; i < StarsIdList.Count(); i++)
                    {
                        var entry = list.Where(q => q.STARS_ID == StarsIdList[i]).FirstOrDefault();
                        if (entry != null)
                        {
                            lst.Add(MapModelFromLBC_Profile(entry));
                        }
                    }
                }
                else
                {
                    foreach (LBC_PROFILE item in list)
                    {
                        lst.Add(MapModelFromLBC_Profile(item));
                    }
                }
            }

            foreach (ProfileModel item in lst)
            {
                ProfileRecognitionRepository _recognition = new ProfileRecognitionRepository();
                item.recognitionList = _recognition.GetAll(item.STARS_ID);
            }

            return lst;
        }

        public List<ProfileModel> GetListProfileByPaCode(string paCode)
        {
            List<ProfileModel> lst = new List<ProfileModel>();
            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                if (string.IsNullOrEmpty(paCode))
                {
                    var list = (from entity in context.LBC_PROFILE
                                where entity.EMP_STATUS_CODE == "A"
                                select entity).ToList();
                    foreach (LBC_PROFILE item in list)
                    {
                        lst.Add(MapModelFromLBC_Profile(item));
                    }
                }
                else
                {
                    var list = (from entity in context.LBC_PROFILE
                                where entity.PA_CODE == paCode &&  entity.EMP_STATUS_CODE == "A"
                                select entity).ToList();
                    foreach (LBC_PROFILE item in list)
                    {
                        lst.Add(MapModelFromLBC_Profile(item));
                    }
                }

            }
            return lst;
        }
     
        private LBC_PROFILE MapLBC_PROFILEFromModel(ProfileModel entity)
        {
            LBC_PROFILE model = new LBC_PROFILE();
            if (entity != null)
            {
                model.ADMIN_NOTES = entity.ADMIN_NOTES;
                model.BIOGRAPHY = entity.BIOGRAPHY;
                model.CREATED_BY = entity.CREATED_BY;
                model.CREATED_DATE = entity.CREATED_DATE;
                model.DEPARTMENT = entity.DEPARTMENT;
                model.DIETARY_RESTRICTION = entity.DIETARY_RESTRICTION;
                model.DLR_ADDRESS = entity.DLR_ADDRESS;
                model.DLR_CITY = entity.DLR_CITY;
                model.DLR_NAME = entity.DLR_NAME;
                model.DLR_PHONE = entity.DLR_PHONE;
                model.DLR_REGION = entity.DLR_REGION;
                model.DLR_STATE = entity.DLR_STATE;
                model.DLR_ZIP = entity.DLR_ZIP;
                model.EMAIL_ID = entity.EMAIL_ID;
                model.EMP_STATUS_CODE = entity.EMP_STATUS_CODE;
                model.FIRST_NAME = entity.FIRST_NAME;
                model.LAST_NAME = entity.LAST_NAME;
                model.LBC_CERT = entity.LBC_CERT;
                model.PA_CODE = entity.PA_CODE;
                model.PHONE = entity.PHONE;
                model.PHOTO_PATH = entity.PHOTO_PATH;
                model.PROFILE_NOTE = entity.PROFILE_NOTE;
                model.PROFILE_TYPE = entity.PROFILE_TYPE;
                model.SHIRT_SIZE = entity.SHIRT_SIZE;
                model.STARS_ID = entity.STARS_ID;
                model.TITLE = entity.TITLE;
                model.UPDATE_DATE = entity.UPDATE_DATE;
                model.UPDATED_BY = entity.UPDATED_BY;
                model.WSLX_ID = entity.WSLX_ID;
            }

            return model;

        }

        private ProfileModel MapModelFromLBC_Profile(LBC_PROFILE entity)
        {
            ProfileModel model = new ProfileModel();
            if (entity != null)
            {
                model.ADMIN_NOTES = entity.ADMIN_NOTES;
                model.BIOGRAPHY = entity.BIOGRAPHY;
                model.CREATED_BY = entity.CREATED_BY;
                model.CREATED_DATE = entity.CREATED_DATE;
                model.DEPARTMENT = entity.DEPARTMENT;
                model.DIETARY_RESTRICTION = entity.DIETARY_RESTRICTION;
                model.DLR_ADDRESS = entity.DLR_ADDRESS;
                model.DLR_CITY = entity.DLR_CITY;
                model.DLR_NAME = entity.DLR_NAME;
                model.DLR_PHONE = entity.DLR_PHONE;
                model.DLR_REGION = entity.DLR_REGION;
                model.DLR_STATE = entity.DLR_STATE;
                model.DLR_ZIP = entity.DLR_ZIP;
                model.EMAIL_ID = entity.EMAIL_ID;
                model.EMP_STATUS_CODE = entity.EMP_STATUS_CODE;
                model.FIRST_NAME = entity.FIRST_NAME;
                model.LAST_NAME = entity.LAST_NAME;
                model.LBC_CERT = entity.LBC_CERT;
                model.PA_CODE = entity.PA_CODE;
                model.PHONE = entity.PHONE;
                model.PHOTO_PATH = entity.PHOTO_PATH;
                model.PROFILE_NOTE = entity.PROFILE_NOTE;
                model.PROFILE_TYPE = entity.PROFILE_TYPE;
                model.SHIRT_SIZE = entity.SHIRT_SIZE;
                model.STARS_ID = entity.STARS_ID;
                model.TITLE = entity.TITLE;
                model.UPDATE_DATE = entity.UPDATE_DATE;
                model.UPDATED_BY = entity.UPDATED_BY;
                model.WSLX_ID = entity.WSLX_ID;
                model.ZONE = entity.ZONE;
                model.SALESCODE = entity.SALESCODE;
                model.DEPT_INFO = entity.DEPT_INFO;
                ////ToDo: Add entity ref to dealer type s/c in entity once model is updated.
                model.DEALER_TYPE = entity.SELECT_CONTACT_DLR;

                model.haveProfileWslxId = string.IsNullOrEmpty(entity.WSLX_ID) ? false : true;

            }

            return model;
        }
    }
}
