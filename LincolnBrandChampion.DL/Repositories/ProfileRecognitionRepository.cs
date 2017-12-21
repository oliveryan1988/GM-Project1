using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LincolnBrandChampion.Model.Models;
using LincolnBrandChampion.DL.Data;
using LincolnBrandChampion.DL.Helpers;

namespace LincolnBrandChampion.DL.Repositories
{
    public class ProfileRecognitionRepository
    {
        public List<RecognitionModel> GetByStarsId(string StarsId)
        {
            List<RecognitionModel> list = new List<RecognitionModel>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from mst in context.LBC_PROFILE_RECOGNITION_MASTER 
                           join  pr in context.LBC_PROFILE_RECOGNITION on  mst.RECOGNITION_ID equals pr.RECOGNITION_ID
                           where pr.STARS_ID == StarsId
                           select new RecognitionModel {
                               STATUS = pr.STATUS,
                               RECOGNITION_DESC = mst.RECOGNITION_DESC,
                               RECOGNITION_TITLE = mst.RECOGNITION_TITLE,
                               CATEGORY = mst.CATEGORY,
                               STARS_ID = pr.STARS_ID

                           };

                 return lst.ToList<RecognitionModel>();
        }
        }

        public List<RecognitionModel> GetAll(string starzId)
        {
            int count = 0;
            List<RecognitionModel> list = new List<RecognitionModel>();
            

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = (from entity in context.LBC_PROFILE_RECOGNITION_MASTER
                           select entity).ToList();

                var lst2 = (from mst in context.LBC_PROFILE_RECOGNITION_MASTER
                            join pr in context.LBC_PROFILE_RECOGNITION on mst.RECOGNITION_ID equals pr.RECOGNITION_ID
                            where pr.STARS_ID == starzId
                            select pr).ToList();


                if (lst2.Count > 0)
                {
                    foreach (LBC_PROFILE_RECOGNITION_MASTER item in lst)
                    {
                        list.Add(new RecognitionModel()
                        {

                            CATEGORY = item.CATEGORY,
                            CREATED_DATE = item.CREATED_DATE,
                            CREATED_BY = item.CREATED_BY,
                            RECOGNITION_DESC = item.RECOGNITION_DESC,
                            RECOGNITION_ID = item.RECOGNITION_ID,
                            RECOGNITION_TITLE = item.RECOGNITION_TITLE,
                            UPDATE_DATE = item.UPDATE_DATE,
                            UPDATED_BY = item.UPDATED_BY,
                            STATUS =lst2[count].STATUS,
                            STARS_ID = starzId



                        });
                        count = count +1;
                    }
                    //foreach (LBC_PROFILE_RECOGNITION item in lst2)
                    //{
                    //    list.Insert(lst2.Count - 1, new RecognitionModel()
                    //    {

                    //        STARS_ID = item.STARS_ID,
                    //        STATUS = item.STATUS,
                           
                    //    });
                    //}

                }
                  
                  else {


                      foreach (LBC_PROFILE_RECOGNITION_MASTER item in lst)
                      {
                          list.Add(new RecognitionModel()
                          {

                              CATEGORY = item.CATEGORY,
                              CREATED_DATE = item.CREATED_DATE,
                              CREATED_BY = item.CREATED_BY,
                              RECOGNITION_DESC = item.RECOGNITION_DESC,
                              RECOGNITION_ID = item.RECOGNITION_ID,
                              RECOGNITION_TITLE = item.RECOGNITION_TITLE,
                              UPDATE_DATE = item.UPDATE_DATE,
                              UPDATED_BY = item.UPDATED_BY,
                              STARS_ID =starzId,
                              STATUS ="N"



                          });
                      }
              
             
                   
                   }



            }

            return list;
        }


        public List<ProfileRecognition> GetRecognitionByStarsId(string StarsId)
        {
            List<ProfileRecognition> list = new List<ProfileRecognition>();

            using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
            {
                var lst = from mst in context.LBC_PROFILE_RECOGNITION_MASTER
                          join pr in context.LBC_PROFILE_RECOGNITION on mst.RECOGNITION_ID equals pr.RECOGNITION_ID
                          where pr.STARS_ID == StarsId
                          select new ProfileRecognition
                          {
                              STATUS = pr.STATUS,
                              RECOGNITION_ID = pr.RECOGNITION_ID,
                              STARS_ID = pr.STARS_ID

                          };

                return lst.ToList<ProfileRecognition>();
            }
        }
        /// <summary>
        /// Method to insert to the Profile recognition
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Save(ProfileRecognition model)
        {
            bool save = true;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    LBC_PROFILE_RECOGNITION _profileM = new LBC_PROFILE_RECOGNITION()
                    {
                       
                        CREATED_DATE = model.CREATED_DATE,
                        CREATED_BY = model.CREATED_BY,
                        RECOGNITION_ID = model.RECOGNITION_ID,
                       STARS_ID = model.STARS_ID,
                       STATUS = model.STATUS
                    };

                    context.LBC_PROFILE_RECOGNITION.Add(_profileM);
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw ex;
                //   save = false;
            }
            return save;
        }

        public bool Update(ProfileRecognition model)
        {
            bool save = true;
            try
            {
                using (LBCData context = new LBCData(ConnectionHelper.getConnectionString()))
                {
                    var _profile = (from p in context.LBC_PROFILE_RECOGNITION
                                    where p.STARS_ID == model.STARS_ID && p.RECOGNITION_ID == model.RECOGNITION_ID
                                    select p).FirstOrDefault();



                    _profile.STATUS = model.STATUS;
                   // _profile.RECOGNITION_ID = model.RECOGNITION_ID;
                    _profile.UPDATE_DATE = model.UPDATE_DATE;
                    _profile.UPDATED_BY = model.UPDATED_BY;

                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw ex;
               // save = false;
            }
            return save;
        }
    }
}
