using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsKeyValuePair
    {

        public string ID { get; set; }
        public string sKey { get; set; }
        public string sValue { get; set; }


        public ClsKeyValuePair()
        {
        }


        public string GetKeyValue(string inKey)
        {
            try
            {


                PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
                ClsKeyValuePair oKeyValue = (from data in prepumaContext.GetTable<tblKeyValuePair>()
                                             where data.sKey == inKey

                                             select new ClsKeyValuePair
                                             {
                                                 sValue = data.sValue

                                             }).SingleOrDefault<ClsKeyValuePair>();
                return oKeyValue.sValue;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string UpdateKeyValue(ClsKeyValuePair inKey)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                ClsKeyValuePair kv = new ClsKeyValuePair();
                string value = kv.GetKeyValue(inKey.sKey);

                if (value != "")
                {
                    // Query the database for the row to be updated. 
                    var query =
                        from qdata in prepumaContext.GetTable<tblKeyValuePair>()
                        where qdata.sKey == inKey.sKey
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblKeyValuePair updRow in query)
                    {
                        
                        updRow.sValue = inKey.sValue;
                    }

                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();

                }
                else
                {
                    errMsg = "No Key Value Pair Found to update for " + "'" + inKey.sKey + "'";
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }



    }
   
}
