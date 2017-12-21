using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Oracle.DataAccess.Client;

namespace LincolnBrandChampion.DL.Helpers
{
    class ConnectionHelper
    {
        public static string getConnectionString()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.FileDSN))
                throw new Exception("Please add 'FileDSN' value inside 'appSettings' in your Web.Config.");

            string enviromentDSN = WebConfigurationManager.AppSettings["DSNConnection"];
            string tarDSN =  Properties.Settings.Default.FileDSN;


            return ConvertDSNToConnStr(tarDSN); //, fileEntity
        }

        private static string ConvertDSNToConnStr(string tarDSN) //, string fileEntity
        {
            string fileEntity = "Data.LBCDataedmx";

            string UID = "";
            string PWD = "";
            string SERVER = "";


            if (File.Exists(tarDSN))
            {
                StreamReader tmpSR = File.OpenText(tarDSN);
                string tmpLine = "";


                while (!tmpSR.EndOfStream)
                {
                    tmpLine = tmpSR.ReadLine();
                    string[] tmpLineSplit = tmpLine.Split('=');

                    if (tmpLineSplit.Length == 2)
                    {
                        switch (tmpLineSplit[0])
                        {
                            case "UID":
                                UID = tmpLineSplit[1];
                                break;
                            case "PWD":
                                PWD = tmpLineSplit[1];
                                break;
                            case "SERVER":
                                SERVER = tmpLineSplit[1];
                                break;

                        }
                    }
                }
            }
            OracleConnectionStringBuilder sqlBuilder = new OracleConnectionStringBuilder();

            //if (UID.Equals("Integrated Security"))
            //{
            //    sqlBuilder.IntegratedSecurity = true;
            //}
            //else
            //{
            sqlBuilder.UserID = UID;
            sqlBuilder.Password = PWD;
            //  }
            sqlBuilder.DataSource = SERVER;
            //sqlBuilder.UserID = UID;
            //sqlBuilder.Password = PWD;
            //sqlBuilder.InitialCatalog = DATABASE;
            sqlBuilder.PersistSecurityInfo = true;
            //sqlBuilder.Unicode = true;
            // sqlBuilder.MultipleActiveResultSets = true;
            //sqlBuilder.ApplicationName = "EntityFramework";

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Metadata = String.Format(@"res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", fileEntity);
            entityBuilder.Provider = "Oracle.DataAccess.Client";
            entityBuilder.ProviderConnectionString = sqlBuilder.ToString().Replace("Persist Security Info=True", "PERSIST SECURITY INFO=True");

            return entityBuilder.ToString();
        }
    }
}
