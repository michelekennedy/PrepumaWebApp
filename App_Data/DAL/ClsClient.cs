using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsClient
    {
        public Double ClientID { get; set; }
        public string ClientName { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }
        public string ContractNumber { get; set; }

        /// <summary>
        /// Function to retrieve cilent id, client names fron tblcilent
        /// </summary>
        /// <returns></returns>
        public List<ClsClient> GetClientInfo()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsClient> oClient = (from data in prepumaContext.GetTable<tblclient>() orderby data.ClientName
                                       join contract in prepumaContext.GetTable<tblcontract>()
                                       on data.ClientID equals contract.ClientID
                                       select new ClsClient
                                       {
                                           ClientID = (Double)data.ClientID,
                                           ClientName = data.ClientName,
                                           Updatedby = data.Updatedby,
                                           UpdatedOn = Convert.ToDateTime(data.UpdatedOn),
                                           Createdby = data.Createdby,
                                           CreatedOn = Convert.ToDateTime(data.CreatedOn),
                                           ActiveFlag = (bool)data.ActiveFlag,
                                           ContractNumber = contract.ContractNumber
                                       }).ToList<ClsClient>();
            return oClient;
        }

        public List<ClsClient> GetClientList()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsClient> oClient = (from data in prepumaContext.GetTable<tblclient>()
                                       orderby data.ClientName
                                       
                                       select new ClsClient
                                       {
                                           ClientID = (Double)data.ClientID,
                                           ClientName = data.ClientName,
                                           Updatedby = data.Updatedby,
                                           UpdatedOn = Convert.ToDateTime(data.UpdatedOn),
                                           Createdby = data.Createdby,
                                           CreatedOn = Convert.ToDateTime(data.CreatedOn),
                                           ActiveFlag = (bool)data.ActiveFlag
                                           
                                       }).ToList<ClsClient>();
            return oClient;
        }

        public static ClsClient GetClient(Double iDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsClient oClient = (from data in prepumaContext.GetTable<tblclient>()
                                 where data.ClientID == iDatabaseKey

                                 select new ClsClient
                                 {
                                     ClientID = data.ClientID,
                                     ClientName = data.ClientName,
                                     Updatedby = data.Updatedby,
                                     UpdatedOn = Convert.ToDateTime(data.UpdatedOn),
                                     Createdby = data.Createdby,
                                     CreatedOn = data.CreatedOn,
                                     ActiveFlag = (bool)data.ActiveFlag
                                  }).SingleOrDefault<ClsClient>();

            //add this if it's needed
            //if (oClient != null)
                //oClient.Contracts = clsContract.GetContracts(oClient.ClientId);

            return oClient;
        }

        public static ClsClient GetClient(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsClient oClient = (from data in prepumaContext.GetTable<tblclient>()
                                 where data.ClientName == sDatabaseKey

                                 select new ClsClient
                                 {
                                     ClientID = data.ClientID,
                                     ClientName = data.ClientName,
                                     Updatedby = data.Updatedby,
                                     UpdatedOn = Convert.ToDateTime(data.UpdatedOn),
                                     Createdby = data.Createdby,
                                     CreatedOn = data.CreatedOn,
                                     ActiveFlag = (bool)data.ActiveFlag
                                 }).SingleOrDefault<ClsClient>();

            //add this if it's needed
            //if (oClient != null)
            //oClient.Contracts = clsContract.GetContracts(oClient.ClientId);

            return oClient;
        }

        public static ClsClient GetClientbyContractNumber(string contractNumber)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsClient oClient = (from data in prepumaContext.GetTable<tblclient>()
                                 join contract in prepumaContext.GetTable<tblcontract>()
                                 on data.ClientID equals contract.ClientID
                                 where contract.ContractNumber == contractNumber
                                 select new ClsClient
                                 {
                                     ClientID = data.ClientID,
                                     ClientName = data.ClientName,
                                     Updatedby = data.Updatedby,
                                     UpdatedOn = Convert.ToDateTime(data.UpdatedOn),
                                     Createdby = data.Createdby,
                                     CreatedOn = data.CreatedOn,
                                     ActiveFlag = (bool)data.ActiveFlag
                                 }).SingleOrDefault<ClsClient>();

            //add this if it's needed
            //if (oClient != null)
            //oClient.Contracts = clsContract.GetContracts(oClient.ClientId);

            return oClient;
        }

        //********************************************************************
        /// <summary> Function to update or insert a client </summary>
        /// <param name="oNewData"> client object</param>
        public static void UpdateClient(ClsClient oNewData)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            ClsClient oExisting = null;

            if (oNewData.ClientID > 0)
                oExisting = GetClient(oNewData.ClientID);
            
            if (oExisting != null)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in prepumaContext.GetTable<tblclient>()
                    where qdata.ClientID == oExisting.ClientID
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblclient updRow in query)
                {
                    updRow.ClientName = oNewData.ClientName;
                    updRow.Updatedby = oNewData.Updatedby;
                    updRow.ActiveFlag = oNewData.ActiveFlag;
                    
                }

                // Submit the changes to the database. 
                prepumaContext.SubmitChanges();

                oNewData.ClientID = oExisting.ClientID;
            }
            else
            {
                if (oNewData.ClientID <= 0)
                {
                    try
                    {
                        var iClientID = (from qdata in prepumaContext.GetTable<tblclient>()
                                             select qdata.ClientID).Max();
                        oNewData.ClientID= iClientID + 1;
                    }
                    catch
                    {
                        oNewData.ClientID = DateTime.Now.Year;
                    }
                }

                tblclient oNewRow = new tblclient()
                {
                    ClientID = oNewData.ClientID,
                    ClientName = oNewData.ClientName,
                    Updatedby = oNewData.Updatedby,
                    Createdby = oNewData.Createdby,
                    ActiveFlag = oNewData.ActiveFlag
                };

                // Add the new object to the contracts collection.
                prepumaContext.GetTable<tblclient>().InsertOnSubmit(oNewRow);
                // Submit the changes to the database. 
                prepumaContext.SubmitChanges();

                oNewData.ClientID = oNewRow.ClientID;
            }
        }

    }


}