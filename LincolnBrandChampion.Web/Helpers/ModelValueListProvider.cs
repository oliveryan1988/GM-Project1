using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LincolnBrandChampion.BL.Persisters;
using LincolnBrandChampion.Model.Models;



namespace LincolnBrandChampion.Web.Helpers
{
    public class ModelValueListProvider : IEnumerable<SelectListItem>
    {
        List<KeyValuePair<string, string>> innerList = new List<KeyValuePair<string, string>>();

        public static readonly ModelValueListProvider DietaryRestrictionsList = new DietaryRestrictionsListProvider();
        public static readonly ModelValueListProvider ShirtSizeList = new ShirtSizeListProvider();
        public static readonly ModelValueListProvider ProfileTypeList = new ProfileTypeListProvider();
        public static readonly ModelValueListProvider TransportationModeList = new TransportationModeListProvider();
        public static readonly ModelValueListProvider TransportationNeedList = new TransportationNeedListProvider();
        public static readonly ModelValueListProvider AirlineList = new AirlineListProvider();
        public static readonly ModelValueListProvider TerminalList = new TerminalListProvider();
        public static readonly ModelValueListProvider EventList = new EventListProvider();
        public static readonly ModelValueListProvider MarketList = new MarketListProvider();
        public static readonly ModelValueListProvider ConnectorList = new ConnectorListProvider();
        public static readonly ModelValueListProvider HotelList = new HotelListProvider();
        public static readonly ModelValueListProvider EventMonthList = new EventListMonthProvider();
        public static readonly ModelValueListProvider CheckpointList = new CheckpointListProvider();
        public static readonly ModelValueListProvider StoryCategotyList = new StoryCategoryListProvider();
        public static readonly ModelValueListProvider StorySubCategotyList = new StorySubCategoryListProvider();
        public static readonly ModelValueListProvider StoryRatingList= new StoryRatingListProvider();
        public static readonly ModelValueListProvider RegistrationStatusList = new RegStatusListProvider();
        //public static readonly ModelValueListProvider AirlineDataList = new AirlineListProvider();
        protected void Added(string value, string text)
        {
            string innerValue = null, innerText = null;

            if (value != null)
                innerValue = value.ToString();
            if (text != null)
                innerText = text.ToString();

            if (innerList.Exists(kvp => kvp.Key == innerValue))
                throw new ArgumentException("Value must be unique", "value");

            innerList.Add(new KeyValuePair<string, string>(innerValue, innerText));
        }
        public IEnumerator<SelectListItem> GetEnumerator()
        {
            return new ModelValueListProviderEnumerator(innerList.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private struct ModelValueListProviderEnumerator : IEnumerator<SelectListItem>
        {
            private IEnumerator<KeyValuePair<string, string>> innerEnumerator;

            public ModelValueListProviderEnumerator(IEnumerator<KeyValuePair<string, string>> enumerator)
            {
                innerEnumerator = enumerator;
            }

            public SelectListItem Current
            {
                get
                {
                    var current = innerEnumerator.Current;
                    return new SelectListItem { Value = current.Key, Text = current.Value };
                }
            }

            public void Dispose()
            {
                try
                {
                    innerEnumerator.Dispose();
                }
                catch (Exception)
                {
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public bool MoveNext()
            {
                return innerEnumerator.MoveNext();
            }

            public void Reset()
            {
                innerEnumerator.Reset();
            }
        }

        private class DietaryRestrictionsListProvider : ModelValueListProvider
        {
            public DietaryRestrictionsListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                Add("", "--SELECT--");
                Add("DR00", "NONE");
                Add("DR01", "VEGETARIAN");
                Add("DR02", "VEGAN");
                Add("DR03", "GLUTEN-FREE");
                Add("DR10", "OTHER");

            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }
        private class ShirtSizeListProvider : ModelValueListProvider
        {
            public ShirtSizeListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                Add("", "--SELECT--");
                Add("S", "SMALL");
                Add("M", "MEDIUM");
                Add("L", "LARGE");
                Add("XL", "EXTRA LARGE	");
                Add("XXL", "EXTRA EXTRA LARGE");


            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }

        private class ProfileTypeListProvider : ModelValueListProvider
        {
            public ProfileTypeListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                Add("", "--SELECT--");
                Add("PRIVATE", "PRIVATE");
                Add("PUBLIC", "PUBLIC");



            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }
        private class TransportationModeListProvider : ModelValueListProvider
        {
            public TransportationModeListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                Add("", "How do you plan to get to the Immersion Event?");
                Add("FLY", "FLY");
                Add("DRIVE", "DRIVE");



            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }

        private class TransportationNeedListProvider : ModelValueListProvider
        {
            public TransportationNeedListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                Add("", "Please select the ground transportation (to and from the hotel) that you will require.");
                Add("NONE", "NONE");
                Add("BOTH", "BOTH ARRIVAL & DEPARTURE");
                Add("ARRIVAL", "ARRIVAL ONLY");
                Add("DEPARTURE", "DEPARTURE ONLY");

            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }

        private class EventListProvider : ModelValueListProvider
        {
            public EventListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                EventBL _event = new EventBL();

                List<EventModel> lst = _event.GetAll();
                Add("", "EVENT");
                for (int i = 0; i < lst.Count; i++)
                {
                    EventModel item = new EventModel()
                    {
                        EVENT_ID = lst.ElementAt(i).EVENT_ID,
                        EVENT_SESSION = lst.ElementAt(i).EVENT_SESSION
                    };
                    Add(item.EVENT_ID.ToString(), item.EVENT_SESSION);
                }


            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }

        private class StoryCategoryListProvider : ModelValueListProvider
        {
            public StoryCategoryListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                StoryCategoryBL _storecategory = new StoryCategoryBL();

                List<StoryCategoryModel> lst = _storecategory.GetAll();
                for (int i = 0; i < lst.Count; i++)
                {
                    StoryCategoryModel item = new StoryCategoryModel()
                    {
                        SEQ_ID = lst.ElementAt(i).SEQ_ID,
                        CATEG_NAME = lst.ElementAt(i).CATEG_NAME 
                    };
                    Add(item.SEQ_ID.ToString(), item.CATEG_NAME);
                }


            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }

        private class StorySubCategoryListProvider : ModelValueListProvider
        {
            public StorySubCategoryListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                StorySubCategoryBL _storesubcategory = new StorySubCategoryBL();

                List<StorySubCategoryModel> lst = _storesubcategory.GetAll();
                for (int i = 0; i < lst.Count; i++)
                {
                    StorySubCategoryModel item = new StorySubCategoryModel()
                    {
                        SEQ_NO = lst.ElementAt(i).SEQ_NO,
                        SUB_CAT_NAME = lst.ElementAt(i).SUB_CAT_NAME
                    };
                    Add(item.SEQ_NO.ToString(), item.SUB_CAT_NAME);
                }


            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }

        private class StoryRatingListProvider : ModelValueListProvider
        {
            public StoryRatingListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                StoryRatingBL _storerating = new StoryRatingBL();

                List<StoryRatingModel> lst = _storerating.GetAll();
                for (int i = 0; i < lst.Count; i++)
                {
                    StoryRatingModel item = new StoryRatingModel()
                    {
                        SEQ_ID = lst.ElementAt(i).SEQ_ID,
                        RATING_NAME = lst.ElementAt(i).RATING_NAME 
                    };
                    Add(item.SEQ_ID.ToString(), item.RATING_NAME);
                }


            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }

        private class EventListMonthProvider : ModelValueListProvider
        {
            public EventListMonthProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                EventBL _event = new EventBL();

                List<string> lst = _event.GetEventListMonth();
               /// Add("ALL", "--ALL--");
                for (int i = 0; i < lst.Count; i++)
                {

                    Add(lst[i], lst[i].Substring(0, lst[i].Length-5));
                  
                }


            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }
        private class AirlineListProvider : ModelValueListProvider
        {
            public AirlineListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                AirlinesInfoBL _air = new AirlinesInfoBL();

                List<AirlinesInfoModel> lst = _air.GetAll();
                Add("", "Please select an airline.");
                for (int i = 0; i < lst.Count; i++)
                {
                    AirlinesInfoModel item = new AirlinesInfoModel()
                    {
                        AIRLINE_CODE = lst.ElementAt(i).AIRLINE_CODE,
                        AIRLINE_NAME = lst.ElementAt(i).AIRLINE_NAME
                    };
                    Add(item.AIRLINE_CODE.ToString(), item.AIRLINE_NAME);
                }


            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }

        private class MarketListProvider : ModelValueListProvider
        {
            public MarketListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                ProfileBL _profile = new ProfileBL();

                List<string> lst = _profile.GetMarketList();
                Add("ALL", "MARKET");
                for (int i = 0; i < lst.Count; i++)
                {

                    Add(lst[i], lst[i]);
                }


            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }
        //private class AirlineListProvider : ModelValueListProvider
        //{
        //    public AirlineListProvider(string defaultText = null)
        //    {
        //        if (!string.IsNullOrEmpty(defaultText))
        //            Add(string.Empty, defaultText);

        //        Add("", "Please select an airline.");
        //        Add("AIRCANADA", "Air Canada");
        //        Add("AIRFRANCE", "Air France");
        //        Add("ALASKAAIRLINES", "Alaska Airlines");
        //        Add("AMERICANAIRLINES", "American Airlines");
        //        Add("DELTA", "Delta Airlines");
        //        Add("FRONTIER", "Frontier");
        //        Add("JETBLUE", "jetBlue");
        //        Add("LUFTHANSA", "Lufthansa");
        //        Add("ROYALJORDANIAN", "Royal Jordanian");
        //        Add("SOUTHWEST", "Southwest");
        //        Add("SPIRIT", "Spirit");
        //        Add("UNITEDAIRLINES", "United Airlines");
        //        Add("USAIRWAYS", "US Airways");
                



        //    }
        //    public void Add(string value, string text)
        //    {
        //        Added(value, text);
        //    }
        //}
        private class TerminalListProvider : ModelValueListProvider
        {
            public TerminalListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                Add("", "Terminal");
                //if (AirlineList.ToString().ToUpper() == "DELTA" || AirlineList.ToString().ToUpper() == "AIRFRANCE")
                //{
                Add("MCNAMARA", "MCNAMARA");
                //}else{
                Add("NORTH", "NORTH");
                //}
                



            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }

        private class ConnectorListProvider : ModelValueListProvider
        {
            public ConnectorListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                Add("", "Select");
                Add("1", "Yes, this is a connecting flight.");
                Add("2", "No, this is a direct flight.");

            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }
        private class HotelListProvider : ModelValueListProvider
        {
            public HotelListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                Add("", "Select");
                Add("Yes", "Yes");
                Add("No", "No");

            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }
        private class CheckpointListProvider : ModelValueListProvider
        {
            public CheckpointListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                Add("", "--ALL--");
                Add("1", "Checkpoint 1");
                Add("2", "Checkpoint 2");
                Add("3", "Checkpoint 3");

            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }
        private class RegStatusListProvider : ModelValueListProvider
        {
            public RegStatusListProvider(string defaultText = null)
            {
                if (!string.IsNullOrEmpty(defaultText))
                    Add(string.Empty, defaultText);

                Add("A", "Active");
                Add("C", "Cancelled");

            }
            public void Add(string value, string text)
            {
                Added(value, text);
            }
        }
    }
}