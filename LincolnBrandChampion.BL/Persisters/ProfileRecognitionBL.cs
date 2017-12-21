using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.DL.Repositories;
using LincolnBrandChampion.Model.Models;

namespace LincolnBrandChampion.BL.Persisters
{
    public class ProfileRecognitionBL
    {
        /// <summary>
        /// get all the data using the starz Id as parameter
        /// </summary>
        /// <param name="starzId"></param>
        /// <returns> list of recognitions</returns>
        public List<RecognitionModel> GetAll(string starzId)
        {
            ProfileRecognitionRepository _profileR = new ProfileRecognitionRepository();
            return _profileR.GetAll(starzId);
        }
        /// <summary>
        /// Check if the Recognition is there
        /// </summary>
        /// <param name="starzId"></param>
        /// <returns></returns>
        public List<ProfileRecognition> GetRecognitionByStarsId(string starzId)
        {
            ProfileRecognitionRepository _profileR = new ProfileRecognitionRepository();
            return _profileR.GetRecognitionByStarsId(starzId);
        }
        /// <summary>
        /// Save the data first time (insert)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Save(ProfileRecognition model)
        {
            ProfileRecognitionRepository _profileR = new ProfileRecognitionRepository();
            return _profileR.Save(model);
        }
        /// <summary>
        /// Update the data when the entry already exist
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(ProfileRecognition model)
        {
            ProfileRecognitionRepository _profileR = new ProfileRecognitionRepository();
            return _profileR.Update(model);
        }
    }
}
