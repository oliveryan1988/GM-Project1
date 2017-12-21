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
    public class EventBL
    {
        /// <summary>
        /// Method to get all the events
        /// </summary>
        /// <returns></returns>
        public List<EventModel> GetAll()
        {
            EventRepository _event = new EventRepository();
            return _event.GetAllEvents();
        }
        public List<EventModel> GetAll(string starsid)
        {
            EventRepository _event = new EventRepository();
            return _event.GetAllEvents(starsid);
        }

        public List<string> GetEventListMonth()
        {
            EventRepository _event = new  EventRepository();
            return _event.GetEventListMonth();
        }
     
        public EventModel GetEventModelByID (decimal EventId)
        {
            EventRepository _event = new EventRepository();
            return _event.GetEventModelByID(EventId);
        }

        /// <summary>
        /// get the event by the id
        /// </summary>
        /// <param name="event_id"></param>
        /// <returns></returns>
        public List<EventModel> GetEventsByID(decimal event_id)
        {
            EventRepository _event = new EventRepository();
            return _event.GetEventsByID(event_id);
        }
        public List<decimal> GetEventModelByMonYear(string month, decimal EventId)
        {
            EventRepository _event = new EventRepository();
            return _event.GetEventModelByMonYear(month, EventId);
        }
        
        /// <summary>
        /// Update event count after each registration
        /// </summary>
        /// <param name="model"></param>
        public void UpdateEventCount(EventModel model)
        {
            EventRepository _event = new EventRepository();
            _event.UpdateEventCount(model);
        
        }
        /// <summary>
        /// Decrease the Count when the event is cancel
        /// </summary>
        /// <param name="model"></param>
        public void UpdateEventCountDecrease(EventModel model)
        {
            EventRepository _event = new EventRepository();
            _event.UpdateEventCountDecrease(model);

        }
    }
}
