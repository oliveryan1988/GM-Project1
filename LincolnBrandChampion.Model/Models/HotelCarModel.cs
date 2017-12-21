using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace LincolnBrandChampion.Model.Models
{
    public class HotelCarModel
    {
        public string STARS_ID { get; set; }
        public decimal EVENT_ID { get; set; }
        [Required(ErrorMessage = "Please enter the hotel confirmation number")]
        public string HOTEL_CONF { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string DEALERSHIP_NAME { get; set; }
        public string PA_CODE { get; set; }
        [Required(ErrorMessage = "Please enter the hotel name")]
        public string HOTEL_NAME { get; set; }
        [Required(ErrorMessage = "Please enter the hotel check-in date")]
        public Nullable<System.DateTime> HOTEL_CHECK_IN { get; set; }
        [Required(ErrorMessage = "Please enter the hotel check-out date")]
        public Nullable<System.DateTime> HOTEL_CHECK_OUT { get; set; }
        [Required(ErrorMessage = "Please enter the car confirmation info")]
        public string CAR_CONFIRM { get; set; }
        public string ADMIN_NOTES { get; set; }
        public string CAR_NOTES { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
    }
}
