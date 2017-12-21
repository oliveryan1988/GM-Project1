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
    public class GuestBL
    {
        public bool CheckGuestRegistrationBy(string wslxId)
        {
            GuestRepository _registration = new GuestRepository();
            return _registration.CheckGuestRegistrationBy(wslxId);
        }
        public bool CheckGuestSessionBy(string wslxId)
        {
            GuestRepository _registration = new GuestRepository();
            return _registration.CheckGuestSessionBy(wslxId);
        }
        public List<GuestRegistrationModel> GetGuestRegListBy(string wslxId)
        {
            GuestRepository _registration = new GuestRepository();
            return _registration.GetGuestRegListBy(wslxId);
        }

        public GuestRegistrationModel GetGuestRegistrationBy(string wslxId)
        {
            GuestRepository _registration = new GuestRepository();
            return _registration.GetGuestRegistrationBy(wslxId);
        }

        public void SaveGuestRegistration(GuestRegistrationModel model)
        {
            GuestRepository _registration = new GuestRepository();
            _registration.SaveGuestRegistration(model);
        }

        public bool UpdateGuestRegistration(GuestRegistrationModel model)
        {
            GuestRepository _registration = new GuestRepository();
            return _registration.UpdateGuestRegistration(model);
        }

        public List<GuestSessionLookupModel> GetAll(List<decimal> eventIds)
        {
            GuestRepository _guest = new GuestRepository();
            return _guest.GetAll(eventIds);
        }

        public List<GuestSessionLookupModel> GetByWSLX(string wslxId)
        {
            GuestRepository _guest = new GuestRepository();
            return _guest.GetByWSLX(wslxId);
        }
        public List<decimal> GetSessionByWSLX(string wslxId)
        {
            GuestRepository _guest = new GuestRepository();
            return _guest.GetSessionByWSLX(wslxId);
        }

            public bool removeSesssionRegistered(List<decimal> sessionid, string wslxId)
        {
            GuestRepository _registration = new GuestRepository();
            return _registration.removeSesssionRegistered(sessionid,wslxId);
        }
        public void SaveGuestRegistrationSession(GuestSessionModel model)
        {
            GuestRepository _registration = new GuestRepository();
            _registration.SaveGuestRegistrationSession(model);
        }
        public List<GuestSessionLookupModel> GetAllby(List<decimal> eventIds,string wslxId)
        {
            GuestRepository _registration = new GuestRepository();
            return _registration.GetAllby(eventIds, wslxId);
        }
        public List<GuestRegistrationModel> GetListGuestAndRegistrationBy(decimal eventId)
        {
            GuestRepository _registration = new GuestRepository();
            return _registration.GetListGuestAndRegistrationBy(eventId);
        }

        public List<GuestRegistrationModel> GetListGuestAndRegistrationByWslxEvent(List<string> WslxList = null, List<decimal> EventIdList = null)
        {
            GuestRepository _registration = new GuestRepository();
            return _registration.GetListGuestAndRegistrationByWslxEvent(WslxList, EventIdList);
        }
    }
}
