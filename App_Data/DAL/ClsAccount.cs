using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsAccount
    {
        public double? ContractID { get; set; }
        public double? AcctID { get; set; }
        public string Acctnbr { get; set; }
        public string ContractNumber { get; set; }
        public double? ClientID { get; set; }
        public string ClientName { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }


        public List<ClsAccount> GetAccountInfo()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsAccount> oClient = (from account in prepumaContext.GetTable<tblAccount>() 
                                        join contract in prepumaContext.GetTable<tblcontract>() on account.ContractID equals contract.ContractID
                                        join client in prepumaContext.GetTable<tblclient>() on contract.ClientID equals client.ClientID
                                        orderby client.ClientName,contract.ContractNumber,account.Acctnbr
                                       select new ClsAccount
                                       {
                                           AcctID = account.AcctID,
                                           ContractID = account.ContractID,
                                           Acctnbr = account.Acctnbr,
                                           ContractNumber = contract.ContractNumber,
                                           ClientID= contract.ClientID,
                                           ClientName = client.ClientName,
                                           Updatedby = account.Updatedby,
                                           UpdatedOn = Convert.ToDateTime(account.UpdatedOn),
                                           Createdby = account.Createdby,
                                           CreatedOn = Convert.ToDateTime(account.CreatedOn),
                                           ActiveFlag = (bool)account.ActiveFlag
                                       }).ToList<ClsAccount>();
            return oClient;
        }

        public static ClsAccount GetAccount(Double? iDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsAccount oAccount = (from data in prepumaContext.GetTable<tblAccount>()
                                     where data.AcctID == iDatabaseKey

                                     select new ClsAccount
                                     {
                                         AcctID = data.AcctID,
                                         Acctnbr = data.Acctnbr,
                                         ContractID = data.ContractID,
                                         Updatedby = data.Updatedby,
                                         UpdatedOn = Convert.ToDateTime(data.UpdatedOn),
                                         Createdby = data.Createdby,
                                         CreatedOn = Convert.ToDateTime(data.CreatedOn),
                                         ActiveFlag = (bool)data.ActiveFlag
                                     }).SingleOrDefault<ClsAccount>();



            return oAccount;
        }

        public static ClsAccount GetAccount(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsAccount oAccount = (from data in prepumaContext.GetTable<tblAccount>()
                                   where data.Acctnbr == sDatabaseKey

                                   select new ClsAccount
                                   {
                                       AcctID = data.AcctID,
                                       Acctnbr = data.Acctnbr,
                                       ContractID = data.ContractID,
                                       Updatedby = data.Updatedby,
                                       UpdatedOn = Convert.ToDateTime(data.UpdatedOn),
                                       Createdby = data.Createdby,
                                       CreatedOn = Convert.ToDateTime(data.CreatedOn),
                                       ActiveFlag = (bool)data.ActiveFlag
                                   }).SingleOrDefault<ClsAccount>();



            return oAccount;
        }

        //********************************************************************
        /// <summary> Function to update or insert an account </summary>
        /// <param name="oNewData"> account object</param>
        public static void UpdateAccount(ClsAccount oNewData)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            ClsAccount oExisting = null;

            if (oNewData.AcctID > 0)
                oExisting = GetAccount(oNewData.AcctID);

            if (oExisting != null)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in prepumaContext.GetTable<tblAccount>()
                    where qdata.AcctID == oExisting.AcctID
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblAccount updRow in query)
                {
                    updRow.Acctnbr = oNewData.Acctnbr.ToString();
                    updRow.ContractID = Convert.ToInt32(oNewData.ContractID);
                    updRow.Updatedby = oNewData.Updatedby.ToString();
                    updRow.ActiveFlag = Convert.ToBoolean(oNewData.ActiveFlag);

                }

                // Submit the changes to the database. 
                prepumaContext.SubmitChanges();

                //oNewData.AcctID = oExisting.AcctID;
            }
            else
            {
                if (oNewData.AcctID <= 0)
                {
                    try
                    {
                        var iAcctID = (from qdata in prepumaContext.GetTable<tblAccount>()
                                           select qdata.AcctID).Max();
                        oNewData.AcctID = iAcctID + 1;
                    }
                    catch
                    {
                        oNewData.AcctID = DateTime.Now.Year;
                    }
                }

                tblAccount oNewRow = new tblAccount()
                {
                    AcctID = Convert.ToDouble(oNewData.AcctID),
                    Acctnbr = oNewData.Acctnbr,
                    ContractID = Convert.ToDouble(oNewData.ContractID),
                    Updatedby = oNewData.Updatedby,
                    Createdby = oNewData.Createdby,
                    ActiveFlag = oNewData.ActiveFlag
                };

                // Add the new object to the contracts collection.
                prepumaContext.GetTable<tblAccount>().InsertOnSubmit(oNewRow);
                // Submit the changes to the database. 
                prepumaContext.SubmitChanges();

                oNewData.AcctID = oNewRow.AcctID;
            }
        }

        /// <summary>
        /// Function to get total number of Accounts in the tblAccounts.
        /// </summary>
        /// <returns number of total accounts>totalAccounts</returns>
        public static int countAccounts()
        {
            int totalAccounts = 0;
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            totalAccounts = prepumaContext.GetTable<tblAccount>().Count();
            return totalAccounts;
        }

        /// <summary>
        /// Function to get un assigned accounts.
        /// It will contain all the accounts those are in tblAccounts not in the Contract Account Classification table.
        /// </summary>
        /// <returns all ContractNumbers not in CAC table></returns>
        /// 
        public static List<string> unAssignedAccounts()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<string> totalUnassignedAcc = prepumaContext.tblAccounts
                                        .Where(x => !prepumaContext.tblContractAccountClassifications.Any(y => y.Acctnbr == x.Acctnbr))
                                        .Select(x => x.Acctnbr).ToList();
            return totalUnassignedAcc;
        }
        public static int numberUnAssignedAccounts()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            
            int totalUnassignedAcc = prepumaContext.tblAccounts
                                        .Where(x => !prepumaContext.tblContractAccountClassifications.Any(y => y.Acctnbr == x.Acctnbr))
                                        .Select(x => x.Acctnbr).ToList().Count();
            return totalUnassignedAcc;
        }

       
        
   }
}