using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsContract
    {
        public double? ContractID { get; set; }
        public string ContractName { get; set; }
        public string ContractNumber { get; set; }
        public double? ClientID { get; set; }
        public string contractInfo { get; set; }
        public string ClientName { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }

        public List<ClsContract> GetContractInfo()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsContract> oContract = (from contract in prepumaContext.GetTable<tblcontract>()
                                           join client in prepumaContext.GetTable<tblclient>() on contract.ClientID equals client.ClientID orderby contract.ClientID
                                           orderby client.ClientName,contract.ContractNumber
                                         select new ClsContract
                                       {
                                           
                                           ContractID = contract.ContractID,
                                           ContractNumber = contract.ContractNumber,
                                           ClientID = contract.ClientID,
                                           ContractName = contract.ContractName,
                                           //contractInfo= string.Format("{0}-{1}", data.ContractID, data.ContractName)
                                           contractInfo= string.Format("{0}-{1}", contract.ContractNumber, contract.ContractName),
                                           ClientName = client.ClientName,
                                           Updatedby = contract.Updatedby,
                                           UpdatedOn = Convert.ToDateTime(contract.UpdatedOn),
                                           Createdby = contract.Createdby,
                                           CreatedOn = Convert.ToDateTime(contract.CreatedOn),
                                           ActiveFlag = (bool)contract.ActiveFlag

                                       }).ToList<ClsContract>();
            return oContract;

        }

        public static ClsContract GetContract(Double? iDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsContract oContract = (from contract in prepumaContext.GetTable<tblcontract>()
                                     join client in prepumaContext.GetTable<tblclient>() on contract.ClientID equals client.ClientID
                                     where contract.ContractID == iDatabaseKey

                                     select new ClsContract
                                     {
                                         ContractID = contract.ContractID,
                                         ContractName = contract.ContractName,
                                         ContractNumber = contract.ContractNumber,
                                         ClientID = contract.ClientID,
                                         contractInfo = string.Format("{0}-{1}", contract.ContractNumber, contract.ContractName),
                                         ClientName = client.ClientName,
                                         Updatedby = contract.Updatedby,
                                         UpdatedOn = Convert.ToDateTime(contract.UpdatedOn),
                                         Createdby = contract.Createdby,
                                         CreatedOn = Convert.ToDateTime(contract.CreatedOn),
                                         ActiveFlag = (bool)contract.ActiveFlag
                                     }).SingleOrDefault<ClsContract>();

          

            return oContract;
        }

        public static ClsContract GetContract(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsContract oContract = (from contract in prepumaContext.GetTable<tblcontract>()
                                     join client in prepumaContext.GetTable<tblclient>() on contract.ClientID equals client.ClientID
                                     where contract.ContractNumber == sDatabaseKey

                                     select new ClsContract
                                     {
                                         ContractID = contract.ContractID,
                                         ContractName = contract.ContractName,
                                         ContractNumber = contract.ContractNumber,
                                         ClientID = contract.ClientID,
                                         contractInfo = string.Format("{0}-{1}", contract.ContractNumber, contract.ContractName),
                                         ClientName = client.ClientName,
                                         Updatedby = contract.Updatedby,
                                         UpdatedOn = Convert.ToDateTime(contract.UpdatedOn),
                                         Createdby = contract.Createdby,
                                         CreatedOn = Convert.ToDateTime(contract.CreatedOn),
                                         ActiveFlag = (bool)contract.ActiveFlag
                                     }).SingleOrDefault<ClsContract>();



            return oContract;
        }
        

        //********************************************************************
        /// <summary> Function to update or insert a contract </summary>
        /// <param name="oNewData"> contract object</param>
        public static void UpdateContract(ClsContract oNewData)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            ClsContract oExisting = null;

            if (oNewData.ContractID > 0)
                oExisting = GetContract(oNewData.ContractID);
            
            if (oExisting != null)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in prepumaContext.GetTable<tblcontract>()
                    where qdata.ContractID == oExisting.ContractID
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblcontract updRow in query)
                {
                    updRow.ContractName = oNewData.ContractName;
                    updRow.ContractNumber = oNewData.ContractNumber;
                    updRow.Updatedby = oNewData.Updatedby;
                    updRow.ActiveFlag = oNewData.ActiveFlag;
                    
                }

                // Submit the changes to the database. 
                prepumaContext.SubmitChanges();

                oNewData.ContractID = oExisting.ContractID;
            }
            else
            {
                if (oNewData.ContractID <= 0)
                {
                    try
                    {
                        var iContractID = (from qdata in prepumaContext.GetTable<tblcontract>()
                                             select qdata.ContractID).Max();
                        oNewData.ContractID= iContractID + 1;
                    }
                    catch
                    {
                        oNewData.ContractID = DateTime.Now.Year;
                    }
                }

                tblcontract oNewRow = new tblcontract()
                {
                    ContractID = Convert.ToDouble(oNewData.ContractID),
                    ContractName = oNewData.ContractName,
                    ContractNumber = oNewData.ContractNumber,
                    ClientID = oNewData.ClientID,
                    //Field1= false,
                    SSMA_TimeStamp = Convert.ToString(DateTime.Today),
                    Updatedby = oNewData.Updatedby,
                    Createdby = oNewData.Createdby,
                    ActiveFlag = oNewData.ActiveFlag

                };

                // Add the new object to the contracts collection.
                prepumaContext.GetTable<tblcontract>().InsertOnSubmit(oNewRow);
                // Submit the changes to the database. 
                prepumaContext.SubmitChanges();

                oNewData.ContractID = oNewRow.ContractID;
            }
        }
        /// <summary>
        /// Function to get total number of contracts in the tblcontracts.
        /// </summary>
        /// <returns number of total contracts>totalContracts</returns>
        public static int countContracts()
        {
            int totalContracts = 0;
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            totalContracts = prepumaContext.GetTable<tblcontract>().Count();
            return totalContracts;
        }

        public static List<string> unAssignedContracts()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<string> totalUnassignedCon = prepumaContext.tblcontracts
                                        .Where(x => !prepumaContext.tblContractClassifications.Any(y => y.ContractNumber == x.ContractNumber))
                                        .Select(x => x.ContractNumber).ToList();
            return totalUnassignedCon;
        }

        public static int numberUnAssignedContracts()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            int totalUnassignedCon = prepumaContext.tblcontracts
                                        .Where(x => !prepumaContext.tblContractClassifications.Any(y => y.ContractNumber == x.ContractNumber))
                                        .Select(x => x.ContractNumber).ToList().Count();
            return totalUnassignedCon;
        }
    }


}


