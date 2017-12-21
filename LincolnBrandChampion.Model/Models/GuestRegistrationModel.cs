using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LincolnBrandChampion.Model.Models
{
    public class GuestRegistrationModel
    {
        public string WSLX_ID { get; set; }
        [Required(ErrorMessage = "Please enter your first name.")]
        public string FIRST_NAME { get; set; }
        [Required(ErrorMessage = "Please enter your last name.")]
        public string LAST_NAME { get; set; }
        [Required(ErrorMessage = "Company name is required.")]
        public string COMPANY_NAME { get; set; }
        public string TITLE { get; set; }
        public string DEPARTMENT { get; set; }
        [Required(ErrorMessage = "Email address is required.")]
        public string EMAIL_ID { get; set; }
        public string PHONE { get; set; }
        [Required(ErrorMessage = "Please indicate if you have any dietary restrictions.")]
        public string DIETARY_RESTRICTION { get; set; }
        public string PROFILE_NOTE { get; set; }
        [Required(ErrorMessage = "Please indicate if you will be requiring hotel accommodations.")]
        public string HOTEL_REQUIRED { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public virtual ICollection<GuestSessionModel> LBC_GUEST_SESSION { get; set; }
        [Required(ErrorMessage = "Enter first three digits")]
        [StringLength(3, ErrorMessage = "{0} requires at least {2} characters", MinimumLength = 3)]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Enter first three digits")]
        public string phone1 { get; set; }
        [Required(ErrorMessage = "Enter next three digits")]
        [StringLength(3, ErrorMessage = "{0} requires at least {2} characters", MinimumLength = 3)]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Enter next three digits")]
        public string phone2 { get; set; }
        [Required(ErrorMessage = "Enter last four digits")]
        [StringLength(4, ErrorMessage = "{0} requires at least {2} characters", MinimumLength = 4)]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Enter last four digits")]
        public string phone3 { get; set; }
        public decimal SSSION_ID { get; set; }
        public string Session_Name { get; set; }
        public List<GuestSessionModel> registrationList { get; set; }
        public List<GuestSessionLookupModel> lookupList { get; set; }
        public string WAVE { get; set; }
        public decimal EVENT_ID { get; set; }
    }
}
