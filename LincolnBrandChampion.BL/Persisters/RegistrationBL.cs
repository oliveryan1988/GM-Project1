using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.DL.Repositories;
using LincolnBrandChampion.DL.Properties;
using LincolnBrandChampion.Model.Models;


namespace LincolnBrandChampion.BL.Persisters
{
    public class RegistrationBL
    {

        public List<RegistrationModel> GetRegByStarsId(string starsId)
        {
            RegistrationRepository _regProfile = new RegistrationRepository();
            return _regProfile.GetRegByStarsId(starsId);
        }

        public RegistrationModel GetRegistrationByStarsId(string starsId)
        {
            RegistrationRepository _registration = new RegistrationRepository();
            return _registration.GetRegistrationByStarsId(starsId);
        }

        /// <summary>
        /// Retriving the registration based on the star ID
        /// </summary>
        /// <param name="starsId">string starsid</param>
        /// <returns>Registration Model </returns>
        public RegistrationModel GetRegistrationByStarsEventId(string starsId, decimal eventid)
        {
            RegistrationRepository _registration = new RegistrationRepository();
            return _registration.GetRegistrationByStarsEventId(starsId, eventid);
        }

        /// <summary>
        /// Method to Save a REgistratiob
        /// </summary>
        /// <param name="model"></param>
        public void SaveRegistration(RegistrationModel model)
        {
            RegistrationRepository _registration = new RegistrationRepository();
            _registration.SaveRegistration(model);
        }

        /// <summary>
        /// Update the registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateRegistraion(RegistrationModel model)
        {
            RegistrationRepository _registration = new RegistrationRepository();
            return _registration.UpdateRegistration(model);
        }

        public bool CheckRegistrationBy(string starsId)
        {
            RegistrationRepository _registration = new RegistrationRepository();
            return _registration.CheckRegistrationBy(starsId);
        }

        public List<ProfileModel> GetListProfileByPaCode(string paCode)
        {
            ProfileRepository _profile = new ProfileRepository();
            return _profile.GetListProfileByPaCode(paCode);
        }

        public bool CheckIsSelectBy(string starsId)
        {
            RegistrationRepository _registration = new RegistrationRepository();
            return _registration.CheckIsSelectBy(starsId);
        }
    }
}
