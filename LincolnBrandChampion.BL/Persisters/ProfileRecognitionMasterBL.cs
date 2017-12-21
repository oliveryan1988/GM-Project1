using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.DL.Repositories;
using LincolnBrandChampion.Model.Models;

namespace LincolnBrandChampion.BL.Persisters
{
    public class ProfileRecognitionMasterBL
    {
        public List<ProfileRecognitionMasterModel> GetAll()
        {
            ProfileRecognitionMasterRpository _profileR = new ProfileRecognitionMasterRpository();
            return _profileR.GetAll(); 
        }
        public bool Save(ProfileRecognitionMasterModel model)
        {
            ProfileRecognitionMasterRpository _profileR = new ProfileRecognitionMasterRpository();
            return _profileR.Save(model);
        }
        public bool Update(ProfileRecognitionMasterModel model)
        {
            ProfileRecognitionMasterRpository _profileR = new ProfileRecognitionMasterRpository();
            return _profileR.Update(model);
        }
        public List<RecognitionModel> GetByStarsId(string starsId)
        {
            ProfileRecognitionRepository _profileR = new ProfileRecognitionRepository();

            return _profileR.GetByStarsId(starsId);

        }
    }
}
