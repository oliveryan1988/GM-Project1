using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LincolnBrandChampion.Model.Models
{
    public class RegistrationModel
    {
        public string STARS_ID { get; set; }
        public string PA_CODE { get; set; }
        public string WSLX_ID { get; set; }
        public decimal EVENT_ID { get; set; }
        public string EVENT_NAME { get; set; }
        public string REGD_STATUS { get; set; }
        [Required(ErrorMessage = "Please indicate how you will be getting to the Immersion Event")]
        public string TRANSPORTATION_MODE { get; set; }
        [Required(ErrorMessage = "Please select the ground transportation that you will require")]
        public string TRANSPORTATION_NEED { get; set; }
        public string ATTENDED { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string NOTES { get; set; }
        public string ADMIN_NOTES { get; set; }
        //Profile Info
        [Required(ErrorMessage = "Please enter a first name, or accept default")]
        public string FIRST_NAME { get; set; }
        [Required(ErrorMessage = "Please enter a last name, or accept default")]
        public string LAST_NAME { get; set; }
        [Required(ErrorMessage = "Please enter your name the way you would like it to appear on your name badge")]
        public string BADGE_NAME { get; set; }
        public string TITLE { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "{0} is not in the correct format")]
        public string EMAIL_ID { get; set; }
        [Required(ErrorMessage = "Please enter a phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PHONE { get; set; }
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:###-###-####}")]
        public string DLR_PHONE { get; set; }        
        public string DLR_NAME { get; set; }
        public string DLR_ADDRESS { get; set; }
        public string DLR_CITY { get; set; }
        public string DLR_STATE { get; set; }
        public string DLR_ZIP { get; set; }
        public string PROFILE_NOTE { get; set; }
        public string PROFILE_TYPE { get; set; }
        public string BIOGRAPHY { get; set; }
        public string DEPARTMENT { get; set; }
        public string SHIRT_SIZE { get; set; }
        [Required(ErrorMessage = "Please indicate if you have any dietary restrictions")]
        public string DIETARY_RESTRICTION { get; set; }
        [Required(ErrorMessage = "Please enter the first three digits of the dealer phone number")]
        [StringLength(3, ErrorMessage = "{0} requires at least {2} characters", MinimumLength = 3)]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Enter first three digits")]
        public string phone1 { get; set; }
        
        [Required(ErrorMessage = "Please enter the next three digits of the dealer phone number")]
        [StringLength(3, ErrorMessage = "{0} requires at least {2} characters", MinimumLength = 3)]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Enter next three digits")]
        public string phone2 { get; set; }
        
        [Required(ErrorMessage = "Please enter the last four digits of the dealer phone number")]
        [StringLength(4, ErrorMessage = "{0} requires at least {2} characters", MinimumLength = 4)]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Enter last four digits")]
        public string phone3 { get; set; }
        
       
        [StringLength(3, ErrorMessage = "{0} requires at least {2} characters", MinimumLength = 3)]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Enter first three digits")]
        public string mobile1 { get; set; }
        
        
        [StringLength(3, ErrorMessage = "{0} requires at least {2} characters", MinimumLength = 3)]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Enter next three digits")]
        public string mobile2 { get; set; }
        
        
        [StringLength(4, ErrorMessage = "{0} requires at least {2} characters", MinimumLength = 4)]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Enter last four digits")]
        public string mobile3 { get; set; }

        public string LBC_CERT { get; set; }

        [Required(ErrorMessage = "Please enter the reason for cancelling")]
        public string CANCEL_REASON { get; set; }
    }
}
