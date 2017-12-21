using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace LincolnBrandChampion.Web.Helpers
{
    public static class ReportFormatHelper
    {
        public static string FormatPhoneNumber(string phone)
        {
            double dPhone;
            string result = "";
            if(!String.IsNullOrEmpty(phone) && phone.Trim().Count() == 10 && Double.TryParse(phone, out dPhone))
            {
                result = String.Format("{0:###-###-####}", dPhone);
            }
            return result;
        }

        public static string FormatAirMethod(string method)
        {
            string result = "";
            switch (method.ToUpper())
            {
                case "AIR":
                    result = "Yes";
                    break;
                case "DRIVE":
                    result = "No";
                    break;
                default:
                    result = "No";
                    break;
            }
            return result;
        }

        public static string FormatDriveMethod(string method)
        {
            string result = "";
            switch (method.ToUpper())
            {
                case "AIR":
                    result = "No";
                    break;
                case "DRIVE":
                    result = "Yes";
                    break;
                default:
                    result = "No";
                    break;
            }
            return result;
        }

        public static string FormatAttended(string attended)
        {
            string result = "";
            if (!String.IsNullOrEmpty(attended))
            {
                switch (attended.ToUpper())
                {
                    case "Y":
                        result = "Yes";
                        break;
                    case "C":
                        result = "Canceled";
                        break;
                    default:
                        result = "No";
                        break;
                }
            }
            else
            {
                result = "No";
            }
            return result;
        }

        public static string FormatToProper(string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.Trim().ToLower());
            }
            else
            {
                return String.Empty;
            }
        }

        public static string FormatToCaps(string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                return str.Trim().ToUpper();
            }
            else
            {
                return String.Empty;
            }
        }

        public static string FormatDate(Nullable<DateTime> date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("MMMM-dd/yyyy").Trim();
            }
            else
            {
                return String.Empty;
            }
        }

        public static string FormatTime(string time)
        {
            if (!String.IsNullOrEmpty(time))
            {
                return time.Trim();
            }
            else
            {
                return String.Empty;
            }
        }

        public static string[] SplitBadgeName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return new string[] {String.Empty, String.Empty};
            }
            string[] result = new string[2];
            string[] names = name.Split(' ');
            string firstName = names[0];
            string lastName = names[names.Count() - 1];

            if (!String.IsNullOrEmpty(firstName))
            {
                result[0] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(firstName.Trim().ToLower());
            }

            if (!String.IsNullOrEmpty(lastName))
            {
                result[1] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lastName.Trim().ToLower());
            }

            return result;
        }

        public static string FormatBadgeName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return String.Empty;
            }

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.Trim().ToLower());
        }

        public static string FormatState(string shortName)
        {
            string state = "";
            switch(shortName)
            {
                case "AL":  state = "Alabama"; break;
                case "AK":  state = "Alaska"; break;
                case "AZ":  state = "Arizona"; break;
                case "AR":  state = "Arkansas"; break;
                case "CA":  state = "California"; break;
                case "CO":  state = "Colorado"; break;
                case "CT":  state = "Connecticut"; break;
                case "DE":  state = "Delaware"; break;
                case "FL":  state = "Florida"; break;
                case "GA":  state = "Georgia"; break;
                case "HI":  state = "Hawaii"; break;
                case "ID":  state = "Idaho"; break;
                case "IL":  state = "Illinois"; break;
                case "IN":  state = "Indiana"; break;
                case "IA":  state = "Iowa"; break;
                case "KS":  state = "Kansas"; break;
                case "KY":  state = "Kentucky"; break;
                case "LA":  state = "Louisiana"; break;
                case "ME":  state = "Maine"; break;
                case "MD":  state = "Maryland"; break;
                case "MA":  state = "Massachusetts"; break;
                case "MI":  state = "Michigan"; break;
                case "MN":  state = "Minnesota"; break;
                case "MS":  state = "Mississippi"; break;
                case "MO":  state = "Missouri"; break;
                case "MT":  state = "Montana"; break;
                case "NE":  state = "Nebraska"; break;
                case "NV":  state = "Nevada"; break;
                case "NH":  state = "New Hampshire"; break;
                case "NJ":  state = "New Jersey"; break;
                case "NM":  state = "New Mexico"; break;
                case "NY":  state = "New York"; break;
                case "NC":  state = "North Carolina"; break;
                case "ND":  state = " North Dakota"; break;
                case "OH":  state = "Ohio"; break;
                case "OK":  state = "Oklahoma"; break;
                case "OR":  state = "Oregon"; break;
                case "PA":  state = "Pennsylvania"; break;
                case "RI":  state = "Rhode Island"; break;
                case "SC":  state = "South Carolina"; break;
                case "SD":  state = "South Dakota"; break;
                case "TN":  state = "Tennessee"; break;
                case "TX":  state = "Texas"; break;
                case "UT":  state = "Utah"; break;
                case "VT":  state = "Vermont"; break;
                case "VA":  state = "Virginia"; break;
                case "WA":  state = "Washington"; break;
                case "WV":  state = "West Virginia"; break;
                case "WI":  state = "Wisconsin"; break;
                case "WY":  state = "Wyoming"; break;
            }

            return state;
        }

        public static string FormatEmail(string email)
        {
            if (!String.IsNullOrEmpty(email))
            {
                return char.ToUpper(email[0]) + email.Substring(1).ToLower();
            }
            else
            {
                return String.Empty;
            }
        }

        public static string FormatConfirmNo(string confirmNo)
        {
            if (!String.IsNullOrEmpty(confirmNo))
            {
                return confirmNo.Trim();
            }
            else
            {
                return String.Empty;
            }
        }

        public static string FormatTrim(string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                return str.Trim();
            }
            else
            {
                return String.Empty;
            }
        }
    }
}