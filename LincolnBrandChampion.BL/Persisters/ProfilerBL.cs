using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.DL.Repositories;
using LincolnBrandChampion.DL.Properties;
using LincolnBrandChampion.Model.Models;
using PagedList;

namespace LincolnBrandChampion.BL.Persisters
{
    public class ProfileBL
    {
        public bool GetStarsIdBy(string wslxId)
        {
            ProfileRepository _profile = new ProfileRepository();
            return _profile.HaveStarsIdBy(wslxId);

        }

        public List<string> GetMarketList()
        {
            ProfileRepository _profile = new ProfileRepository();
            return _profile.GetMarketList();
        }

        public ProfileModel GetProfileBy(string wslxId)
        {
            ProfileRepository _profile = new ProfileRepository();

            return _profile.GetProfileBy(wslxId);

        }

        public ProfileModel GetProfileByStarzId(string StarzId)
        {
            ProfileRepository _profile = new ProfileRepository();

            return _profile.GetProfileByStarsId(StarzId);

        }

        public ProfileModel GetAnyProfileByStarsId(string StarzId)
        {
            ProfileRepository _profile = new ProfileRepository();

            return _profile.GetAnyProfileByStarsId(StarzId);

        }

        public void SaveProfileWslxId(ProfileModel model)

        {
            ProfileRepository _profile = new ProfileRepository();
            _profile.SaveProfileWslxId(model);
        }

        public void SaveProfilUploadImage(string photoPath, string starsId)
        {
            ProfileRepository _profile = new ProfileRepository();
            _profile.SaveProfilUploadImage(photoPath, starsId);
        }

        public void UpdateProfileByStarsIdAdmin(ProfileModel model)
        {
            ProfileRepository _profile = new ProfileRepository();
            _profile.UpdateProfileByStarsIdAdmin(model);
        }

        public void UpdateWSLXByStarsId(ProfileModel model)
        {
            ProfileRepository _profile = new ProfileRepository();
            _profile.UpdateWSLXByStarsId(model);
        }

        public void UpdateProfileByStarsId(ProfileModel model)
        {
            ProfileRepository _profile = new ProfileRepository();
            _profile.UpdateProfileByStarsId(model);
        }

        public IPagedList<ProfileModel> GetAll(string search, string filter, int page, int pageSize, string order = "", string asc = "")
        {
            ProfileRepository _profile = new ProfileRepository();
            return _profile.GetAll(search,filter,page,pageSize,order,asc);
        }

        public List<ProfileModel> GetListProfileBy(string dlrRegion)
        {
            ProfileRepository _profile = new ProfileRepository();
            return _profile.GetListProfileBy(dlrRegion);
        }

        public List<ProfileModel> GetListProfileByStarsId(List<string> StarsIdList = null)
        {
            ProfileRepository _profile = new ProfileRepository();
            return _profile.GetListProfileByStarsId(StarsIdList);
        }

        public List<ProfileModel> GetListProfileAndRecognitionStatusBy(string dlrRegion)
        {
            ProfileRepository _profile = new ProfileRepository();
            return _profile.GetListProfileAndRecognitionStatusBy(dlrRegion);
        }

        public List<ProfileModel> GetListProfileAndRecognitionStatusBy(List<string> StarsIdList = null)
        {
            ProfileRepository _profile = new ProfileRepository();
            return _profile.GetListProfileAndRecognitionStatusBy(StarsIdList);
        }

        public bool UpdateProfileWslxIdByStarsId(ProfileModel model)
        {
            ProfileRepository _profile = new ProfileRepository();
            return _profile.UpdateProfileWslxIdByStarsId(model);
        }
    }
}
