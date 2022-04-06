using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsContractRenewal
    {
        public int idContractRenewal { get; set; }
        public string ContractNumber { get; set; }
        public string ContractName { get; set; }
        public int? idContractRenewalType { get; set; }
        public DateTime? effectiveDate { get; set; }
        public DateTime? expiryDate { get; set; }
        public double? pctIncreaseCourier { get; set; }
        public double? pctIncreaseFreight { get; set; }
        public double? pctIncreaseLTL { get; set; }
        public double? pctIncreasePPST { get; set; }
        public double? pctIncreaseCPC { get; set; }
        public double? pctIncreaseOther { get; set; }

        public bool? sowFlag { get; set; }
        public bool? rateOfChangeFlag { get; set; }
        public string modificationReason { get; set; }

        public double? currMarginPct { get; set; }
        public decimal? currRevenue { get; set; }
        public decimal? currCourier { get; set; }
        public decimal? currFFWD { get; set; }
        public decimal? currLTL { get; set; }
        public decimal? currPPST { get; set; }
        public decimal? currCPC { get; set; }
        public decimal? currOther { get; set; }
        public double? currTargetGrMarginPct { get; set; }

        public double? sowMarginPct { get; set; }
        public decimal? sowRevenue { get; set; }
        public decimal? sowCourier { get; set; }
        public decimal? sowFFWD { get; set; }
        public decimal? sowLTL { get; set; }
        public decimal? sowPPST { get; set; }
        public decimal? sowCPC { get; set; }
        public decimal? sowOther { get; set; }
        public double? sowTargetGrMarginPct { get; set; }

        public double? rocMarginPct { get; set; }
        public decimal? rocRevenue { get; set; }
        public decimal? rocCourier { get; set; }
        public decimal? rocFFWD { get; set; }
        public decimal? rocLTL { get; set; }
        public decimal? rocPPST { get; set; }
        public decimal? rocCPC { get; set; }
        public decimal? rocOther { get; set; }
        public double? rocTargetGrMarginPct { get; set; }

        public double? expMarginPct { get; set; }
        public decimal? expRevenue { get; set; }
        public decimal? expCourier { get; set; }
        public decimal? expFFWD { get; set; }
        public decimal? expLTL { get; set; }
        public decimal? expPPST { get; set; }
        public decimal? expCPC { get; set; }
        public decimal? expOther { get; set; }
        public double? expTargetGrMarginPct { get; set; }

        public double? newMarginPct { get; set; }
        public decimal? newRevenue { get; set; }
        public decimal? newCourier { get; set; }
        public decimal? newFFWD { get; set; }
        public decimal? newLTL { get; set; }
        public decimal? newPPST { get; set; }
        public decimal? newCPC { get; set; }
        public decimal? newOther { get; set; }
        public double? newTargetGrMarginPct { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? ActiveFlag { get; set; }

        public string Relationship { get; set; }
        public string Branch { get; set; }
        public string SRID { get; set; }
        public string ContractNumbers { get; set; }
        public List<string> ContractList { get; set; }
        public string currentlyRoutedTo { get; set; }
        public string RenewalType { get; set; }
        public string SRIDName { get; set; }
        public string SRIDPlusName { get; set; }
        public string note { get; set; }
        public int idContractRenewalRouting { get; set; }
        public decimal? VDACommitment { get; set; }
        public string otherDescription { get; set; }
        public string loginID { get; set; }

        public decimal? currDimsAir { get; set; }
        public decimal? expDimsAir { get; set; }
        public decimal? currDimsGround { get; set; }
        public decimal? expDimsGround { get; set; }
        public decimal? currResiFees { get; set; }
        public decimal? expResiFees { get; set; }
        public bool? currAlignPuroInc { get; set; }
        public bool? expAlignPuroInc { get; set; }
        public string currFuelPI { get; set; }
        public string expFuelPI { get; set; }
        public string currFuelPuro { get; set; }
        public string expFuelPuro { get; set; }

        public decimal? currDGFR { get; set; }
        public string currDGFRtype { get; set; }
        public decimal? expDGFR { get; set; }
        public string expDGFRtype { get; set; }
        public decimal? currDGUN3373 { get; set; }
        public string currDGUN3373type { get; set; }
        public decimal? expDGUN3373 { get; set; }
        public string expDGUN3373type { get; set; }
        public decimal? currDGUN1845 { get; set; }
        public string currDGUN1845type { get; set; }
        public decimal? expDGUN1845 { get; set; }
        public string expDGUN1845type { get; set; }
        public decimal? currDGLT500kg { get; set; }
        public string currDGLT500kgtype { get; set; }
        public decimal? expDGLT500kg { get; set; }
        public string expDGLT500kgtype { get; set; }
        public decimal? currDGLQ { get; set; }
        public string currDGLQtype { get; set; }
        public decimal? expDGLQ { get; set; }
        public string expDGLQtype { get; set; }

        public decimal? currSHFP { get; set; }
        public decimal? expSHFP { get; set; }
        public decimal? currSHAH { get; set; }
        public decimal? expSHAH { get; set; }
        public decimal? currSHLP { get; set; }
        public decimal? expSHLP { get; set; }
        public decimal? currSHOML { get; set; }
        public decimal? expSHOML { get; set; }
        public decimal? currSHO { get; set; }
        public decimal? expSHO { get; set; }
        public decimal? currSHRAH { get; set; }
        public decimal? expSHRAH { get; set; }

        public bool? currBeyondOriginDisc { get; set; }
        public bool? expBeyondOriginDisc { get; set; }
        public bool? currBeyondDestDisc { get; set; }
        public bool? expBeyondDestDisc { get; set; }

        public string accessorialComment { get; set; }
        
        public static List<ClsContractRenewal> GetRenewalTypeListDetail()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
           

            List<ClsContractRenewal> oRenewalList = (from data in prepumaContext.GetTable<tblContractRenewal>()
                                                    join det in  prepumaContext.GetTable<tblContractRenewalDetail>() on data.idContractRenewal equals det.idContractRenewal
                                                    join cc in prepumaContext.GetTable<tblContractClassification>() on det.ContractNumber equals cc.ContractNumber
                                                    join type in prepumaContext.GetTable<tblContractRenewalType>() on data.idContractRenewalType equals type.idContractRenewalType
                                                    join routed in prepumaContext.GetTable<tblContractRenewalRouting>() on data.idContractRenewalRouting equals routed.idContractRenewalRouting
                                                    join sls in prepumaContext.GetTable<vw_SalesRep>() on cc.Territory equals sls.SalesRepID
                                                    where data.ActiveFlag == true || data.ActiveFlag == null
                                                    orderby cc.RelationshipName,cc.ContractNumber

                                                  select new ClsContractRenewal
                                              {
                                                  idContractRenewal = Convert.ToInt16(data.idContractRenewal),
                                                  ContractNumber = det.ContractNumber,
                                                  idContractRenewalType = Convert.ToInt16(data.idContractRenewalType),
                                                  effectiveDate = Convert.ToDateTime(data.effectiveDate),
                                                  expiryDate = Convert.ToDateTime(data.expiryDate),

                                                  pctIncreaseCourier = Convert.ToDouble(data.pctIncreaseCourier),
                                                  pctIncreaseFreight = Convert.ToDouble(data.pctIncreaseFreight),
                                                  pctIncreaseLTL = Convert.ToDouble(data.pctIncreaseLTL),
                                                  pctIncreasePPST = Convert.ToDouble(data.pctIncreasePPST),
                                                  pctIncreaseCPC = Convert.ToDouble(data.pctIncreaseCPC),
                                                  pctIncreaseOther = Convert.ToDouble(data.pctIncreaseOther),

                                                  sowFlag = Convert.ToBoolean(data.sowFlag),
                                                  rateOfChangeFlag = Convert.ToBoolean(data.rateOfChangeFlag),
                                                  modificationReason = data.modificationReason,

                                                  currMarginPct = Convert.ToDouble(data.currMarginPct),
                                                  currRevenue = Convert.ToDecimal(data.currRevenue),
                                                  currCourier = Convert.ToDecimal(data.currCourier),
                                                  currFFWD = Convert.ToDecimal(data.currFFWD),
                                                  currLTL = Convert.ToDecimal(data.currLTL),
                                                  currPPST = Convert.ToDecimal(data.currPPST),
                                                  currCPC = Convert.ToDecimal(data.currCPC),
                                                  currOther = Convert.ToDecimal(data.currOther),

                                                  sowMarginPct = Convert.ToDouble(data.sowMarginPct),
                                                  sowRevenue = Convert.ToDecimal(data.sowRevenue),
                                                  sowCourier = Convert.ToDecimal(data.sowCourier),
                                                  sowFFWD = Convert.ToDecimal(data.sowFFWD),
                                                  sowLTL = Convert.ToDecimal(data.sowLTL),
                                                  sowPPST = Convert.ToDecimal(data.sowPPST),
                                                  sowCPC = Convert.ToDecimal(data.sowCPC),
                                                  sowOther = Convert.ToDecimal(data.sowOther),

                                                  rocMarginPct = Convert.ToDouble(data.rocMarginPct),
                                                  rocRevenue = Convert.ToDecimal(data.rocRevenue),
                                                  rocCourier = Convert.ToDecimal(data.rocCourier),
                                                  rocFFWD = Convert.ToDecimal(data.rocFFWD),
                                                  rocLTL = Convert.ToDecimal(data.rocLTL),
                                                  rocPPST = Convert.ToDecimal(data.rocPPST),
                                                  rocCPC = Convert.ToDecimal(data.rocCPC),
                                                  rocOther = Convert.ToDecimal(data.rocOther),

                                                  expMarginPct = Convert.ToDouble(data.expMarginPct),
                                                  expRevenue = Convert.ToDecimal(data.expRevenue),
                                                  expCourier = Convert.ToDecimal(data.expCourier),
                                                  expFFWD = Convert.ToDecimal(data.expFFWD),
                                                  expLTL = Convert.ToDecimal(data.expLTL),
                                                  expPPST = Convert.ToDecimal(data.expPPST),
                                                  expCPC = Convert.ToDecimal(data.expCPC),
                                                  expOther = Convert.ToDecimal(data.expOther),

                                                  newMarginPct = Convert.ToDouble(data.newMarginPct),
                                                  newRevenue = Convert.ToDecimal(data.newRevenue),
                                                  newCourier = Convert.ToDecimal(data.newCourier),
                                                  newFFWD = Convert.ToDecimal(data.newFFWD),
                                                  newLTL = Convert.ToDecimal(data.newLTL),
                                                  newPPST = Convert.ToDecimal(data.newPPST),
                                                  newCPC = Convert.ToDecimal(data.newCPC),
                                                  newOther = Convert.ToDecimal(data.newOther),

                                                  Relationship = data.RelationshipName,
                                                  Branch = cc.Region,
                                                  SRID = cc.Territory,
                                                  RenewalType = type.ContractRenewalType,
         
                                                  UpdatedBy = data.UpdatedBy,
                                                  UpdatedOn = data.UpdatedOn,
                                                  CreatedBy = data.CreatedBy,
                                                  CreatedOn = data.CreatedOn,
                                                  ActiveFlag = (bool)data.ActiveFlag,
                                                  idContractRenewalRouting = (data.idContractRenewalRouting == null) ? 0 : Convert.ToInt16(data.idContractRenewalRouting),
                                                  currentlyRoutedTo = routed.RoutingName,
                                                  loginID = routed.LoginID,
                                                  SRIDName = sls.SalesRep,
                                                  SRIDPlusName = cc.Territory + " - " + sls.SalesRep,

                                                  currTargetGrMarginPct = data.currTargetGrMarginPct,
                                                  expTargetGrMarginPct = data.expTargetGrMarginPct,
                                                  newTargetGrMarginPct = data.newTargetGrMarginPct,

                                                  VDACommitment = Convert.ToDecimal(data.VDACommitment),

                                                  currDimsAir = Convert.ToDecimal(data.currDimsAir),
                                                  expDimsAir = Convert.ToDecimal(data.expDimsAir),
                                                  currDimsGround = Convert.ToDecimal(data.currDimsGround),
                                                  expDimsGround = Convert.ToDecimal(data.expDimsGround),
                                                  currResiFees = Convert.ToDecimal(data.currResiFees),
                                                  expResiFees = Convert.ToDecimal(data.expResiFees),

                                                  currAlignPuroInc = Convert.ToBoolean(data.currAlignPuroInc),
                                                  expAlignPuroInc = Convert.ToBoolean(data.expAlignPuroInc),
                                                  currFuelPI = data.currFuelPI,
                                                  expFuelPI = data.expFuelPI,
                                                  currFuelPuro = data.currFuelPuro,
                                                  expFuelPuro = data.expFuelPuro,

                                                  currDGFR = Convert.ToDecimal(data.currDGFR),
                                                  currDGFRtype = data.currDGFRtype,
                                                  expDGFR = Convert.ToDecimal(data.expDGFR),
                                                  expDGFRtype = data.expDGFRtype,
                                                  currDGUN3373 = Convert.ToDecimal(data.currDGUN3373),
                                                  currDGUN3373type = data.currDGUN3373type,
                                                  expDGUN3373 = Convert.ToDecimal(data.expDGUN3373),
                                                  expDGUN3373type = data.expDGUN3373type,
                                                  currDGUN1845 = Convert.ToDecimal(data.currDGUN3373),
                                                  currDGUN1845type = data.currDGUN1845type,
                                                  expDGUN1845 = Convert.ToDecimal(data.expDGUN1845),
                                                  expDGUN1845type = data.expDGUN1845type,
                                                  currDGLT500kg = Convert.ToDecimal(data.currDGLT500kg),
                                                  currDGLT500kgtype = data.currDGLT500kgtype,
                                                  expDGLT500kg = Convert.ToDecimal(data.expDGLT500kg),
                                                  expDGLT500kgtype = data.expDGLT500kgtype,
                                                  currDGLQ = Convert.ToDecimal(data.currDGLQ),
                                                  currDGLQtype = data.currDGLQtype,
                                                  expDGLQ = Convert.ToDecimal(data.expDGLQ),
                                                  expDGLQtype = data.expDGLQtype,

                                                  currSHFP = Convert.ToDecimal(data.currSHFP),
                                                  expSHFP = Convert.ToDecimal(data.expSHFP),
                                                  currSHAH = Convert.ToDecimal(data.currSHAH),
                                                  expSHAH = Convert.ToDecimal(data.expSHAH),
                                                  currSHLP = Convert.ToDecimal(data.currSHLP),
                                                  expSHLP = Convert.ToDecimal(data.expSHLP),
                                                  currSHOML = Convert.ToDecimal(data.currSHOML),
                                                  expSHOML = Convert.ToDecimal(data.expSHOML),
                                                  currSHO = Convert.ToDecimal(data.currSHO),
                                                  expSHO = Convert.ToDecimal(data.expSHO),
                                                  currSHRAH = Convert.ToDecimal(data.currSHRAH),
                                                  expSHRAH = Convert.ToDecimal(data.expSHRAH),

                                                  currBeyondOriginDisc = Convert.ToBoolean(data.currBeyondOriginDisc),
                                                  expBeyondOriginDisc = Convert.ToBoolean(data.expBeyondOriginDisc),
                                                  currBeyondDestDisc = Convert.ToBoolean(data.currBeyondDestDisc),
                                                  expBeyondDestDisc = Convert.ToBoolean(data.expBeyondDestDisc),

                                                  accessorialComment = data.accessorialComment
                                                  
                                                  

                                              }).ToList<ClsContractRenewal>();

            return oRenewalList;
        }

        public static  ClsContractRenewal GetContractRenewal(int ContractRenewalID)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();


            ClsContractRenewal oRenewalList = (from data in prepumaContext.GetTable<tblContractRenewal>()
                                                     join type in prepumaContext.GetTable<tblContractRenewalType>() on data.idContractRenewalType equals type.idContractRenewalType into d2
                                                     from d3 in d2.DefaultIfEmpty()
                                                     join routed in prepumaContext.GetTable<tblContractRenewalRouting>() on data.idContractRenewalRouting equals routed.idContractRenewalRouting into r2
                                                     from r3 in r2.DefaultIfEmpty()
                                                     where data.idContractRenewal == ContractRenewalID

                                                     select new ClsContractRenewal
                                                     {
                                                         idContractRenewal = Convert.ToInt16(data.idContractRenewal),
                                                         idContractRenewalType = Convert.ToInt16(data.idContractRenewalType),
                                                         effectiveDate = Convert.ToDateTime(data.effectiveDate),
                                                         expiryDate = Convert.ToDateTime(data.expiryDate),

                                                         pctIncreaseCourier = (data.pctIncreaseCourier == null) ? 0 : Convert.ToDouble(data.pctIncreaseCourier),
                                                         pctIncreaseFreight = (data.pctIncreaseFreight == null) ? 0 : Convert.ToDouble(data.pctIncreaseFreight),                                                        
                                                         pctIncreaseLTL = (data.pctIncreaseLTL == null) ? 0 : Convert.ToDouble(data.pctIncreaseLTL),
                                                         pctIncreasePPST = (data.pctIncreasePPST == null) ? 0 :Convert.ToDouble(data.pctIncreasePPST),
                                                         pctIncreaseCPC = (data.pctIncreaseCPC == null) ? 0 : Convert.ToDouble(data.pctIncreaseCPC),
                                                         pctIncreaseOther = (data.pctIncreaseOther == null) ? 0 : Convert.ToDouble(data.pctIncreaseOther),

                                                         sowFlag = (data.sowFlag == null) ? false : Convert.ToBoolean(data.sowFlag),
                                                         rateOfChangeFlag = (data.rateOfChangeFlag == null) ? false : Convert.ToBoolean(data.rateOfChangeFlag),
                                                         modificationReason = data.modificationReason,

                                                         currMarginPct = (data.currMarginPct == null) ? 0 : Convert.ToDouble(data.currMarginPct),
                                                         currRevenue = (data.currRevenue == null) ? 0 : Convert.ToDecimal(data.currRevenue),
                                                         currCourier = (data.currCourier == null) ? 0 : Convert.ToDecimal(data.currCourier),
                                                         currFFWD = (data.currFFWD == null) ? 0 : Convert.ToDecimal(data.currFFWD),
                                                         currLTL = (data.currLTL == null) ? 0 : Convert.ToDecimal(data.currLTL),
                                                         currPPST = (data.currPPST == null) ? 0 : Convert.ToDecimal(data.currPPST),
                                                         currCPC = (data.currCPC == null) ? 0 : Convert.ToDecimal(data.currCPC),
                                                         currOther = (data.currOther == null) ? 0 : Convert.ToDecimal(data.currOther),

                                                         sowMarginPct = (data.sowMarginPct == null) ? 0 : Convert.ToDouble(data.sowMarginPct),
                                                         sowRevenue = (data.sowRevenue == null) ? 0 : Convert.ToDecimal(data.sowRevenue),
                                                         sowCourier = (data.sowCourier) == null ? 0 : Convert.ToDecimal(data.sowCourier),
                                                         sowFFWD = (data.sowFFWD == null) ? 0 : Convert.ToDecimal(data.sowFFWD),
                                                         sowLTL = (data.sowLTL == null) ? 0 : Convert.ToDecimal(data.sowLTL),
                                                         sowPPST = (data.sowPPST == null) ? 0 : Convert.ToDecimal(data.sowPPST),
                                                         sowCPC = (data.sowCPC == null) ? 0 : Convert.ToDecimal(data.sowCPC),
                                                         sowOther = (data.sowOther == null) ? 0 : Convert.ToDecimal(data.sowOther),

                                                         rocMarginPct = (data.rocMarginPct == null) ? 0 : Convert.ToDouble(data.rocMarginPct),
                                                         rocRevenue = (data.rocRevenue == null) ? 0 : Convert.ToDecimal(data.rocRevenue),
                                                         rocCourier = (data.rocCourier) == null ? 0 : Convert.ToDecimal(data.rocCourier),
                                                         rocFFWD = (data.rocFFWD == null) ? 0 : Convert.ToDecimal(data.rocFFWD),
                                                         rocLTL = (data.rocLTL == null) ? 0 : Convert.ToDecimal(data.rocLTL),
                                                         rocPPST = (data.rocPPST == null) ? 0 : Convert.ToDecimal(data.rocPPST),
                                                         rocCPC = (data.rocCPC == null) ? 0 : Convert.ToDecimal(data.rocCPC),
                                                         rocOther = (data.rocOther == null) ? 0 : Convert.ToDecimal(data.rocOther),

                                                         expMarginPct = (data.expMarginPct == null) ? 0 : Convert.ToDouble(data.expMarginPct),
                                                         expRevenue = (data.expRevenue == null) ? 0 : Convert.ToDecimal(data.expRevenue),
                                                         expCourier = (data.expCourier) == null ? 0 : Convert.ToDecimal(data.expCourier),
                                                         expFFWD = (data.expFFWD == null) ? 0 : Convert.ToDecimal(data.expFFWD),
                                                         expLTL = (data.expLTL == null) ? 0 : Convert.ToDecimal(data.expLTL),
                                                         expPPST = (data.expPPST == null) ? 0 : Convert.ToDecimal(data.expPPST),
                                                         expCPC = (data.expCPC == null) ? 0 : Convert.ToDecimal(data.expCPC),
                                                         expOther = (data.expOther == null) ? 0 : Convert.ToDecimal(data.expOther),

                                                         newMarginPct = (data.newMarginPct == null) ? 0 : Convert.ToDouble(data.newMarginPct),
                                                         newRevenue = (data.newRevenue == null) ? 0 : Convert.ToDecimal(data.newRevenue),
                                                         newCourier = (data.newCourier == null) ? 0 : Convert.ToDecimal(data.newCourier),
                                                         newFFWD = (data.newFFWD == null) ? 0 : Convert.ToDecimal(data.newFFWD),
                                                         newLTL = (data.newLTL == null) ? 0 : Convert.ToDecimal(data.newLTL),
                                                         newPPST = (data.newPPST == null) ? 0 : Convert.ToDecimal(data.newPPST),
                                                         newCPC = (data.newCPC == null) ? 0 : Convert.ToDecimal(data.newCPC),
                                                         newOther = (data.newOther == null) ? 0 : Convert.ToDecimal(data.newOther),

                                                         Relationship = data.RelationshipName,
                                                         RenewalType = d3.ContractRenewalType,

                                                         UpdatedBy = data.UpdatedBy,
                                                         UpdatedOn = data.UpdatedOn,
                                                         CreatedBy = data.CreatedBy,
                                                         CreatedOn = data.CreatedOn,
                                                         ActiveFlag = (bool)data.ActiveFlag,
                                                         idContractRenewalRouting = (data.idContractRenewalRouting == null) ? 0 : Convert.ToInt16(data.idContractRenewalRouting),
                                                         currentlyRoutedTo = r3.RoutingName,
                                                         loginID = r3.LoginID,
                                                         currTargetGrMarginPct = (data.currTargetGrMarginPct == null) ? 0 : data.currTargetGrMarginPct,
                                                         expTargetGrMarginPct = (data.expTargetGrMarginPct == null) ? 0 : data.expTargetGrMarginPct,
                                                         newTargetGrMarginPct = (data.newTargetGrMarginPct == null) ? 0 : data.newTargetGrMarginPct,
                                                         sowTargetGrMarginPct = (data.sowTargetGrMarginPct == null) ? 0 : data.sowTargetGrMarginPct,
                                                         rocTargetGrMarginPct = (data.rocTargetGrMarginPct == null) ? 0 : data.rocTargetGrMarginPct,

                                                         VDACommitment = (data.VDACommitment == null) ? 0 : Convert.ToDecimal(data.VDACommitment),
                                                         otherDescription = data.otherDescription,

                                                         currDimsAir = (data.currDimsAir == null) ? 0 : Convert.ToDecimal(data.currDimsAir),
                                                         expDimsAir = (data.expDimsAir == null) ? 0 : Convert.ToDecimal(data.expDimsAir),
                                                         currDimsGround = (data.currDimsGround == null) ? 0 : Convert.ToDecimal(data.currDimsGround),
                                                         expDimsGround = (data.expDimsGround == null) ? 0 : Convert.ToDecimal(data.expDimsGround),
                                                         currResiFees = (data.currResiFees == null) ? 0 : Convert.ToDecimal(data.currResiFees),
                                                         expResiFees = (data.expResiFees == null) ? 0 : Convert.ToDecimal(data.expResiFees),

                                                         currAlignPuroInc = (data.currAlignPuroInc == null) ? false : Convert.ToBoolean(data.currAlignPuroInc),
                                                         expAlignPuroInc = (data.expAlignPuroInc == null) ? false : Convert.ToBoolean(data.expAlignPuroInc),
                                                         currFuelPI = (data.currFuelPI == null) ? "" : data.currFuelPI,
                                                         expFuelPI = (data.expFuelPI == null) ? "" : data.expFuelPI,
                                                         currFuelPuro = (data.currFuelPuro == null) ? "" : data.currFuelPuro,
                                                         expFuelPuro = (data.expFuelPuro == null) ? "" : data.expFuelPuro,

                                                         currDGFR = (data.currDGFR == null) ? 0 : Convert.ToDecimal(data.currDGFR),
                                                         currDGFRtype = data.currDGFRtype,
                                                         expDGFR = (data.expDGFR == null) ? 0 : Convert.ToDecimal(data.expDGFR),
                                                         expDGFRtype = data.expDGFRtype,
                                                         currDGUN3373 = (data.currDGUN3373 == null) ? 0 : Convert.ToDecimal(data.currDGUN3373),
                                                         currDGUN3373type = data.currDGUN3373type,
                                                         expDGUN3373 = (data.expDGUN3373 == null) ? 0 : Convert.ToDecimal(data.expDGUN3373),
                                                         expDGUN3373type = data.expDGUN3373type,
                                                         currDGUN1845 = (data.currDGUN1845 == null) ? 0 : Convert.ToDecimal(data.currDGUN1845),
                                                         currDGUN1845type = data.currDGUN1845type,
                                                         expDGUN1845 = (data.expDGUN1845 == null) ? 0 : Convert.ToDecimal(data.expDGUN1845),
                                                         expDGUN1845type = data.expDGUN1845type,
                                                         currDGLT500kg = (data.currDGLT500kg == null) ? 0 : Convert.ToDecimal(data.currDGLT500kg),
                                                         currDGLT500kgtype = data.currDGLT500kgtype,
                                                         expDGLT500kg = (data.expDGLT500kg == null) ? 0 : Convert.ToDecimal(data.expDGLT500kg),
                                                         expDGLT500kgtype = data.expDGLT500kgtype,
                                                         currDGLQ = (data.currDGLQ == null) ? 0 : Convert.ToDecimal(data.currDGLQ),
                                                         currDGLQtype = data.currDGLQtype,
                                                         expDGLQ = (data.expDGLQ == null) ? 0 : Convert.ToDecimal(data.expDGLQ),
                                                         expDGLQtype = data.expDGLQtype,

                                                         currSHFP = (data.currSHFP == null) ? 0 : Convert.ToDecimal(data.currSHFP),
                                                         expSHFP = (data.expSHFP == null) ? 0 : Convert.ToDecimal(data.expSHFP),
                                                         currSHAH = (data.currSHAH == null) ? 0 : Convert.ToDecimal(data.currSHAH),
                                                         expSHAH = (data.expSHAH == null) ? 0 : Convert.ToDecimal(data.expSHAH),
                                                         currSHLP = (data.currSHLP == null) ? 0 : Convert.ToDecimal(data.currSHLP),
                                                         expSHLP = (data.expSHLP == null) ? 0 : Convert.ToDecimal(data.expSHLP),
                                                         currSHOML = (data.currSHOML == null) ? 0 : Convert.ToDecimal(data.currSHOML),
                                                         expSHOML = (data.expSHOML == null) ? 0 : Convert.ToDecimal(data.expSHOML),
                                                         currSHO = (data.currSHO == null) ? 0 : Convert.ToDecimal(data.currSHO),
                                                         expSHO = (data.expSHO == null) ? 0 : Convert.ToDecimal(data.expSHO),
                                                         currSHRAH = (data.currSHRAH == null) ? 0 : Convert.ToDecimal(data.currSHRAH),
                                                         expSHRAH = (data.expSHRAH == null) ? 0 : Convert.ToDecimal(data.expSHRAH),

                                                         currBeyondOriginDisc = (data.currBeyondOriginDisc == null) ? false : Convert.ToBoolean(data.currBeyondOriginDisc),
                                                         expBeyondOriginDisc = (data.expBeyondOriginDisc == null) ? false : Convert.ToBoolean(data.expBeyondOriginDisc),
                                                         currBeyondDestDisc = (data.currBeyondDestDisc == null) ? false : Convert.ToBoolean(data.currBeyondDestDisc),
                                                         expBeyondDestDisc = (data.expBeyondDestDisc == null) ? false : Convert.ToBoolean(data.expBeyondDestDisc),

                                                         accessorialComment = data.accessorialComment
                                                        

                                                     }).FirstOrDefault();

            return oRenewalList;
        }

        public static List<ClsContractRenewal> GetContractRenewalList()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();


            List<ClsContractRenewal> oRenewalList = (from data in prepumaContext.GetTable<tblContractRenewal>()
                                                     join type in prepumaContext.GetTable<tblContractRenewalType>() on data.idContractRenewalType equals type.idContractRenewalType into d2
                                                     from d3 in d2.DefaultIfEmpty()
                                                     join routed in prepumaContext.GetTable<tblContractRenewalRouting>() on data.idContractRenewalRouting equals routed.idContractRenewalRouting into r2
                                                     from r3 in r2.DefaultIfEmpty()
                                                     where data.ActiveFlag == true || data.ActiveFlag == null
                                                     orderby data.RelationshipName,data.effectiveDate

                                                     select new ClsContractRenewal
                                                     {
                                                         idContractRenewal = Convert.ToInt16(data.idContractRenewal),
                                                         idContractRenewalType = Convert.ToInt16(data.idContractRenewalType),
                                                         effectiveDate = Convert.ToDateTime(data.effectiveDate),
                                                         expiryDate = Convert.ToDateTime(data.expiryDate),

                                                         pctIncreaseCourier = Convert.ToDouble(data.pctIncreaseCourier),
                                                         pctIncreaseFreight = Convert.ToDouble(data.pctIncreaseFreight),
                                                         pctIncreaseLTL = Convert.ToDouble(data.pctIncreaseLTL),
                                                         pctIncreasePPST = Convert.ToDouble(data.pctIncreasePPST),
                                                         pctIncreaseCPC = Convert.ToDouble(data.pctIncreaseCPC),
                                                         pctIncreaseOther = Convert.ToDouble(data.pctIncreaseOther),

                                                         sowFlag = Convert.ToBoolean(data.sowFlag),
                                                         rateOfChangeFlag = Convert.ToBoolean(data.rateOfChangeFlag),
                                                         modificationReason = data.modificationReason,

                                                         currMarginPct = Convert.ToDouble(data.currMarginPct),
                                                         currRevenue = Convert.ToDecimal(data.currRevenue),
                                                         currCourier = Convert.ToDecimal(data.currCourier),
                                                         currFFWD = Convert.ToDecimal(data.currFFWD),
                                                         currLTL = Convert.ToDecimal(data.currLTL),
                                                         currPPST = Convert.ToDecimal(data.currPPST),
                                                         currCPC = Convert.ToDecimal(data.currCPC),
                                                         currOther = Convert.ToDecimal(data.currOther),

                                                         sowMarginPct = Convert.ToDouble(data.sowMarginPct),
                                                         sowRevenue = Convert.ToDecimal(data.sowRevenue),
                                                         sowCourier = Convert.ToDecimal(data.sowCourier),
                                                         sowFFWD = Convert.ToDecimal(data.sowFFWD),
                                                         sowLTL = Convert.ToDecimal(data.sowLTL),
                                                         sowPPST = Convert.ToDecimal(data.sowPPST),
                                                         sowCPC = Convert.ToDecimal(data.sowCPC),
                                                         sowOther = Convert.ToDecimal(data.sowOther),

                                                         rocMarginPct = Convert.ToDouble(data.rocMarginPct),
                                                         rocRevenue = Convert.ToDecimal(data.rocRevenue),
                                                         rocCourier = Convert.ToDecimal(data.rocCourier),
                                                         rocFFWD = Convert.ToDecimal(data.rocFFWD),
                                                         rocLTL = Convert.ToDecimal(data.rocLTL),
                                                         rocPPST = Convert.ToDecimal(data.rocPPST),
                                                         rocCPC = Convert.ToDecimal(data.rocCPC),
                                                         rocOther = Convert.ToDecimal(data.rocOther),

                                                         expMarginPct = Convert.ToDouble(data.expMarginPct),
                                                         expRevenue = Convert.ToDecimal(data.expRevenue),
                                                         expCourier = Convert.ToDecimal(data.expCourier),
                                                         expFFWD = Convert.ToDecimal(data.expFFWD),
                                                         expLTL = Convert.ToDecimal(data.expLTL),
                                                         expPPST = Convert.ToDecimal(data.expPPST),
                                                         expCPC = Convert.ToDecimal(data.expCPC),
                                                         expOther = Convert.ToDecimal(data.expOther),

                                                         newMarginPct = Convert.ToDouble(data.newMarginPct),
                                                         newRevenue = Convert.ToDecimal(data.newRevenue),
                                                         newCourier = Convert.ToDecimal(data.newCourier),
                                                         newFFWD = Convert.ToDecimal(data.newFFWD),
                                                         newLTL = Convert.ToDecimal(data.newLTL),
                                                         newPPST = Convert.ToDecimal(data.newPPST),
                                                         newCPC = Convert.ToDecimal(data.newCPC),
                                                         newOther = Convert.ToDecimal(data.newOther),
     
                                                         UpdatedBy = data.UpdatedBy,
                                                         UpdatedOn = data.UpdatedOn,
                                                         CreatedBy = data.CreatedBy,
                                                         CreatedOn = data.CreatedOn,
                                                         ActiveFlag = (bool)data.ActiveFlag,
                                                         idContractRenewalRouting = (data.idContractRenewalRouting == null) ? 0 : Convert.ToInt16(data.idContractRenewalRouting),
                                                         currentlyRoutedTo = r3.RoutingName,
                                                         loginID = r3.LoginID,
                                                         Relationship = data.RelationshipName,
                                                         RenewalType = d3.ContractRenewalType,
               

                                                         currTargetGrMarginPct = data.currTargetGrMarginPct,
                                                         expTargetGrMarginPct = data.expTargetGrMarginPct,
                                                         newTargetGrMarginPct = data.newTargetGrMarginPct,

                                                         VDACommitment = Convert.ToDecimal(data.VDACommitment),

                                                         currDimsAir = Convert.ToDecimal(data.currDimsAir),
                                                         expDimsAir = Convert.ToDecimal(data.expDimsAir),
                                                         currDimsGround = Convert.ToDecimal(data.currDimsGround),
                                                         expDimsGround = Convert.ToDecimal(data.expDimsGround),
                                                         currResiFees = Convert.ToDecimal(data.currResiFees),
                                                         expResiFees = Convert.ToDecimal(data.expResiFees),

                                                         currAlignPuroInc = Convert.ToBoolean(data.currAlignPuroInc),
                                                         expAlignPuroInc = Convert.ToBoolean(data.expAlignPuroInc),
                                                         currFuelPI = data.currFuelPI,
                                                         expFuelPI = data.expFuelPI,
                                                         currFuelPuro = data.currFuelPuro,
                                                         expFuelPuro = data.expFuelPuro,

                                                         currDGFR = Convert.ToDecimal(data.currDGFR),
                                                         currDGFRtype = data.currDGFRtype,
                                                         expDGFR = Convert.ToDecimal(data.expDGFR),
                                                         expDGFRtype = data.expDGFRtype,
                                                         currDGUN3373 = Convert.ToDecimal(data.currDGUN3373),
                                                         currDGUN3373type = data.currDGUN3373type,
                                                         expDGUN3373 = Convert.ToDecimal(data.expDGUN3373),
                                                         expDGUN3373type = data.expDGUN3373type,
                                                         currDGUN1845 = Convert.ToDecimal(data.currDGUN1845),
                                                         currDGUN1845type = data.currDGUN1845type,
                                                         expDGUN1845 = Convert.ToDecimal(data.expDGUN1845),
                                                         expDGUN1845type = data.expDGUN1845type,
                                                         currDGLT500kg = Convert.ToDecimal(data.currDGLT500kg),
                                                         currDGLT500kgtype = data.currDGLT500kgtype,
                                                         expDGLT500kg = Convert.ToDecimal(data.expDGLT500kg),
                                                         expDGLT500kgtype = data.expDGLT500kgtype,
                                                         currDGLQ = Convert.ToDecimal(data.currDGLQ),
                                                         currDGLQtype = data.currDGLQtype,
                                                         expDGLQ = Convert.ToDecimal(data.expDGLQ),
                                                         expDGLQtype = data.expDGLQtype,

                                                         currSHFP = Convert.ToDecimal(data.currSHFP),
                                                         expSHFP = Convert.ToDecimal(data.expSHFP),
                                                         currSHAH = Convert.ToDecimal(data.currSHAH),
                                                         expSHAH = Convert.ToDecimal(data.expSHAH),
                                                         currSHLP = Convert.ToDecimal(data.currSHLP),
                                                         expSHLP = Convert.ToDecimal(data.expSHLP),
                                                         currSHOML = Convert.ToDecimal(data.currSHOML),
                                                         expSHOML = Convert.ToDecimal(data.expSHOML),
                                                         currSHO = Convert.ToDecimal(data.currSHO),
                                                         expSHO = Convert.ToDecimal(data.expSHO),
                                                         currSHRAH = Convert.ToDecimal(data.currSHRAH),
                                                         expSHRAH = Convert.ToDecimal(data.expSHRAH),

                                                         currBeyondOriginDisc = Convert.ToBoolean(data.currBeyondOriginDisc),
                                                         expBeyondOriginDisc = Convert.ToBoolean(data.expBeyondOriginDisc),
                                                         currBeyondDestDisc = Convert.ToBoolean(data.currBeyondDestDisc),
                                                         expBeyondDestDisc = Convert.ToBoolean(data.expBeyondDestDisc),

                                                         accessorialComment = data.accessorialComment
                                                         
                                                     }).ToList<ClsContractRenewal>();

            return oRenewalList;
        }

        public static List<ClsContractRenewal> GetContractRenewalList(string routedTo)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();


            List<ClsContractRenewal> oRenewalList = (from data in prepumaContext.GetTable<tblContractRenewal>()
                                                     join type in prepumaContext.GetTable<tblContractRenewalType>() on data.idContractRenewalType equals type.idContractRenewalType into d2
                                                     from d3 in d2.DefaultIfEmpty()
                                                     join routed in prepumaContext.GetTable<tblContractRenewalRouting>() on data.idContractRenewalRouting equals routed.idContractRenewalRouting into r2
                                                     from r3 in r2.DefaultIfEmpty()
                                                     where (data.ActiveFlag == true || data.ActiveFlag == null) && r3.LoginID == routedTo
                                                     orderby data.RelationshipName, data.effectiveDate

                                                     select new ClsContractRenewal
                                                     {
                                                         idContractRenewal = Convert.ToInt16(data.idContractRenewal),
                                                         idContractRenewalType = Convert.ToInt16(data.idContractRenewalType),
                                                         effectiveDate = Convert.ToDateTime(data.effectiveDate),
                                                         expiryDate = Convert.ToDateTime(data.expiryDate),

                                                         pctIncreaseCourier = Convert.ToDouble(data.pctIncreaseCourier),
                                                         pctIncreaseFreight = Convert.ToDouble(data.pctIncreaseFreight),
                                                         pctIncreaseLTL = Convert.ToDouble(data.pctIncreaseLTL),
                                                         pctIncreasePPST = Convert.ToDouble(data.pctIncreasePPST),
                                                         pctIncreaseCPC = Convert.ToDouble(data.pctIncreaseCPC),
                                                         pctIncreaseOther = Convert.ToDouble(data.pctIncreaseOther),

                                                         sowFlag = Convert.ToBoolean(data.sowFlag),
                                                         rateOfChangeFlag = Convert.ToBoolean(data.rateOfChangeFlag),
                                                         modificationReason = data.modificationReason,

                                                         currMarginPct = Convert.ToDouble(data.currMarginPct),
                                                         currRevenue = Convert.ToDecimal(data.currRevenue),
                                                         currCourier = Convert.ToDecimal(data.currCourier),
                                                         currFFWD = Convert.ToDecimal(data.currFFWD),
                                                         currLTL = Convert.ToDecimal(data.currLTL),
                                                         currPPST = Convert.ToDecimal(data.currPPST),
                                                         currCPC = Convert.ToDecimal(data.currCPC),
                                                         currOther = Convert.ToDecimal(data.currOther),

                                                         sowMarginPct = Convert.ToDouble(data.sowMarginPct),
                                                         sowRevenue = Convert.ToDecimal(data.sowRevenue),
                                                         sowCourier = Convert.ToDecimal(data.sowCourier),
                                                         sowFFWD = Convert.ToDecimal(data.sowFFWD),
                                                         sowLTL = Convert.ToDecimal(data.sowLTL),
                                                         sowPPST = Convert.ToDecimal(data.sowPPST),
                                                         sowCPC = Convert.ToDecimal(data.sowCPC),
                                                         sowOther = Convert.ToDecimal(data.sowOther),

                                                         rocMarginPct = Convert.ToDouble(data.rocMarginPct),
                                                         rocRevenue = Convert.ToDecimal(data.rocRevenue),
                                                         rocCourier = Convert.ToDecimal(data.rocCourier),
                                                         rocFFWD = Convert.ToDecimal(data.rocFFWD),
                                                         rocLTL = Convert.ToDecimal(data.rocLTL),
                                                         rocPPST = Convert.ToDecimal(data.rocPPST),
                                                         rocCPC = Convert.ToDecimal(data.rocCPC),
                                                         rocOther = Convert.ToDecimal(data.rocOther),

                                                         expMarginPct = Convert.ToDouble(data.expMarginPct),
                                                         expRevenue = Convert.ToDecimal(data.expRevenue),
                                                         expCourier = Convert.ToDecimal(data.expCourier),
                                                         expFFWD = Convert.ToDecimal(data.expFFWD),
                                                         expLTL = Convert.ToDecimal(data.expLTL),
                                                         expPPST = Convert.ToDecimal(data.expPPST),
                                                         expCPC = Convert.ToDecimal(data.expCPC),
                                                         expOther = Convert.ToDecimal(data.expOther),

                                                         newMarginPct = Convert.ToDouble(data.newMarginPct),
                                                         newRevenue = Convert.ToDecimal(data.newRevenue),
                                                         newCourier = Convert.ToDecimal(data.newCourier),
                                                         newFFWD = Convert.ToDecimal(data.newFFWD),
                                                         newLTL = Convert.ToDecimal(data.newLTL),
                                                         newPPST = Convert.ToDecimal(data.newPPST),
                                                         newCPC = Convert.ToDecimal(data.newCPC),
                                                         newOther = Convert.ToDecimal(data.newOther),

                                                         UpdatedBy = data.UpdatedBy,
                                                         UpdatedOn = data.UpdatedOn,
                                                         CreatedBy = data.CreatedBy,
                                                         CreatedOn = data.CreatedOn,
                                                         ActiveFlag = (bool)data.ActiveFlag,
                                                         idContractRenewalRouting = (data.idContractRenewalRouting == null) ? 0 : Convert.ToInt16(data.idContractRenewalRouting),
                                                         currentlyRoutedTo = r3.RoutingName,
                                                         loginID = r3.LoginID,
                                                         Relationship = data.RelationshipName,
                                                         RenewalType = d3.ContractRenewalType,


                                                         currTargetGrMarginPct = data.currTargetGrMarginPct,
                                                         expTargetGrMarginPct = data.expTargetGrMarginPct,
                                                         newTargetGrMarginPct = data.newTargetGrMarginPct,

                                                         VDACommitment = Convert.ToDecimal(data.VDACommitment),

                                                         currDimsAir = Convert.ToDecimal(data.currDimsAir),
                                                         expDimsAir = Convert.ToDecimal(data.expDimsAir),
                                                         currDimsGround = Convert.ToDecimal(data.currDimsGround),
                                                         expDimsGround = Convert.ToDecimal(data.expDimsGround),
                                                         currResiFees = Convert.ToDecimal(data.currResiFees),
                                                         expResiFees = Convert.ToDecimal(data.expResiFees),

                                                         currAlignPuroInc = Convert.ToBoolean(data.currAlignPuroInc),
                                                         expAlignPuroInc = Convert.ToBoolean(data.expAlignPuroInc),
                                                         currFuelPI = data.currFuelPI,
                                                         expFuelPI = data.expFuelPI,
                                                         currFuelPuro = data.currFuelPuro,
                                                         expFuelPuro = data.expFuelPuro,

                                                         currDGFR = Convert.ToDecimal(data.currDGFR),
                                                         currDGFRtype = data.currDGFRtype,
                                                         expDGFR = Convert.ToDecimal(data.expDGFR),
                                                         expDGFRtype = data.expDGFRtype,
                                                         currDGUN3373 = Convert.ToDecimal(data.currDGUN3373),
                                                         currDGUN3373type = data.currDGUN3373type,
                                                         expDGUN3373 = Convert.ToDecimal(data.expDGUN3373),
                                                         expDGUN3373type = data.expDGUN3373type,
                                                         currDGUN1845 = Convert.ToDecimal(data.currDGUN1845),
                                                         currDGUN1845type = data.currDGUN1845type,
                                                         expDGUN1845 = Convert.ToDecimal(data.expDGUN1845),
                                                         expDGUN1845type = data.expDGUN1845type,
                                                         currDGLT500kg = Convert.ToDecimal(data.currDGLT500kg),
                                                         currDGLT500kgtype = data.currDGLT500kgtype,
                                                         expDGLT500kg = Convert.ToDecimal(data.expDGLT500kg),
                                                         expDGLT500kgtype = data.expDGLT500kgtype,
                                                         currDGLQ = Convert.ToDecimal(data.currDGLQ),
                                                         currDGLQtype = data.currDGLQtype,
                                                         expDGLQ = Convert.ToDecimal(data.expDGLQ),
                                                         expDGLQtype = data.expDGLQtype,

                                                         currSHFP = Convert.ToDecimal(data.currSHFP),
                                                         expSHFP = Convert.ToDecimal(data.expSHFP),
                                                         currSHAH = Convert.ToDecimal(data.currSHAH),
                                                         expSHAH = Convert.ToDecimal(data.expSHAH),
                                                         currSHLP = Convert.ToDecimal(data.currSHLP),
                                                         expSHLP = Convert.ToDecimal(data.expSHLP),
                                                         currSHOML = Convert.ToDecimal(data.currSHOML),
                                                         expSHOML = Convert.ToDecimal(data.expSHOML),
                                                         currSHO = Convert.ToDecimal(data.currSHO),
                                                         expSHO = Convert.ToDecimal(data.expSHO),
                                                         currSHRAH = Convert.ToDecimal(data.currSHRAH),
                                                         expSHRAH = Convert.ToDecimal(data.expSHRAH),

                                                         currBeyondOriginDisc = Convert.ToBoolean(data.currBeyondOriginDisc),
                                                         expBeyondOriginDisc = Convert.ToBoolean(data.expBeyondOriginDisc),
                                                         currBeyondDestDisc = Convert.ToBoolean(data.currBeyondDestDisc),
                                                         expBeyondDestDisc = Convert.ToBoolean(data.expBeyondDestDisc),

                                                         accessorialComment = data.accessorialComment
                                                         

                                                     }).ToList<ClsContractRenewal>();

            return oRenewalList;
        }

        public static void UpdateContractRenewal(ClsContractRenewal oNewData,List<string> oNewDetail,ClsRenewalNotes oNewNotes)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            ClsContractRenewal oExisting = null;
            Int32 ContractRenewalID = 0;

            if (oNewData.idContractRenewal > 0)
                oExisting = GetContractRenewal(oNewData.idContractRenewal);

            if (oExisting != null)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in prepumaContext.GetTable<tblContractRenewal>()
                    where qdata.idContractRenewal == oExisting.idContractRenewal
                    select qdata;


                foreach (tblContractRenewal updRow in query)
                {
                    //Populate updRow
                    updRow.idContractRenewalType = oNewData.idContractRenewalType;
                    updRow.RelationshipName = oNewData.Relationship;
                    updRow.effectiveDate = oNewData.effectiveDate;
                    updRow.expiryDate = oNewData.expiryDate;
                    updRow.pctIncreaseCourier = oNewData.pctIncreaseCourier;
                    updRow.pctIncreaseFreight = oNewData.pctIncreaseFreight;
                    updRow.pctIncreaseLTL = oNewData.pctIncreaseLTL;
                    updRow.pctIncreasePPST = oNewData.pctIncreasePPST;
                    updRow.pctIncreaseCPC = oNewData.pctIncreaseCPC;
                    updRow.idContractRenewalRouting = oNewData.idContractRenewalRouting;

                    updRow.sowFlag = oNewData.sowFlag;
                    updRow.rateOfChangeFlag =  oNewData.rateOfChangeFlag;
                    updRow.modificationReason =  oNewData.modificationReason;

                    updRow.currMarginPct =  oNewData.currMarginPct;
                    updRow.currRevenue =  oNewData.currRevenue;
                    updRow.currCourier =  oNewData.currCourier;
                    updRow.currFFWD =  oNewData.currFFWD;
                    updRow.currLTL =  oNewData.currLTL;
                    updRow.currPPST =  oNewData.currPPST;
                    updRow.currCPC =  oNewData.currCPC;
                    updRow.currOther =  oNewData.currOther;

                    updRow.sowMarginPct = oNewData.sowMarginPct;
                    updRow.sowRevenue = oNewData.sowRevenue;
                    updRow.sowCourier = oNewData.sowCourier;
                    updRow.sowFFWD = oNewData.sowFFWD;
                    updRow.sowLTL = oNewData.sowLTL;
                    updRow.sowPPST = oNewData.sowPPST;
                    updRow.sowCPC = oNewData.sowCPC;
                    updRow.sowOther = oNewData.sowOther;

                    updRow.rocMarginPct = oNewData.rocMarginPct;
                    updRow.rocRevenue = oNewData.rocRevenue;
                    updRow.rocCourier = oNewData.rocCourier;
                    updRow.rocFFWD = oNewData.rocFFWD;
                    updRow.rocLTL = oNewData.rocLTL;
                    updRow.rocPPST = oNewData.rocPPST;
                    updRow.rocCPC = oNewData.rocCPC;
                    updRow.rocOther = oNewData.rocOther;

                    updRow.expMarginPct = oNewData.expMarginPct;
                    updRow.expRevenue = oNewData.expRevenue;
                    updRow.expCourier = oNewData.expCourier;
                    updRow.expFFWD = oNewData.expFFWD;
                    updRow.expLTL = oNewData.expLTL;
                    updRow.expPPST = oNewData.expPPST;
                    updRow.expCPC = oNewData.expCPC;
                    updRow.expOther = oNewData.expOther;

                    updRow.newMarginPct = oNewData.newMarginPct;
                    updRow.newRevenue = oNewData.newRevenue;
                    updRow.newCourier = oNewData.newCourier;
                    updRow.newFFWD = oNewData.newFFWD;
                    updRow.newLTL = oNewData.newLTL;
                    updRow.newPPST = oNewData.newPPST;
                    updRow.newCPC = oNewData.newCPC;
                    updRow.newOther = oNewData.newOther;
                    updRow.otherDescription = oNewData.otherDescription;

                    updRow.currTargetGrMarginPct = oNewData.currTargetGrMarginPct;
                    updRow.expTargetGrMarginPct = oNewData.expTargetGrMarginPct;
                    updRow.newTargetGrMarginPct = oNewData.newTargetGrMarginPct;

                    updRow.VDACommitment = oNewData.VDACommitment;

                    updRow.currDimsAir = oNewData.currDimsAir;
                    updRow.expDimsAir = oNewData.expDimsAir;
                    updRow.currDimsGround = oNewData.currDimsGround;
                    updRow.expDimsGround = oNewData.expDimsGround;
                    updRow.currResiFees = oNewData.currResiFees;
                    updRow.expResiFees = oNewData.expResiFees;

                    updRow.currAlignPuroInc = oNewData.currAlignPuroInc;
                    updRow.expAlignPuroInc = oNewData.expAlignPuroInc;
                    updRow.currFuelPI = oNewData.currFuelPI;
                    updRow.expFuelPI = oNewData.expFuelPI;
                    updRow.currFuelPuro = oNewData.currFuelPuro;
                    updRow.expFuelPuro = oNewData.expFuelPuro;

                    updRow.currDGFR = oNewData.currDGFR;
                    updRow.currDGFRtype = oNewData.currDGFRtype;
                    updRow.expDGFR = oNewData.expDGFR;
                    updRow.expDGFRtype = oNewData.expDGFRtype;
                    updRow.currDGUN3373 = oNewData.currDGUN3373;
                    updRow.currDGUN3373type = oNewData.currDGUN3373type;
                    updRow.expDGUN3373 = oNewData.expDGUN3373;
                    updRow.expDGUN3373type = oNewData.expDGUN3373type;
                    updRow.currDGUN1845 = oNewData.currDGUN1845;
                    updRow.currDGUN1845type = oNewData.currDGUN1845type;
                    updRow.expDGUN1845 = oNewData.expDGUN1845;
                    updRow.expDGUN1845type = oNewData.expDGUN1845type;
                    updRow.currDGLT500kg = oNewData.currDGLT500kg;
                    updRow.currDGLT500kgtype = oNewData.currDGLT500kgtype;
                    updRow.expDGLT500kg = oNewData.expDGLT500kg;
                    updRow.expDGLT500kgtype = oNewData.expDGLT500kgtype;
                    updRow.currDGLQ = oNewData.currDGLQ;
                    updRow.currDGLQtype = oNewData.currDGLQtype;
                    updRow.expDGLQ = oNewData.expDGLQ;
                    updRow.expDGLQtype = oNewData.expDGLQtype;

                    updRow.currSHFP = oNewData.currSHFP;
                    updRow.expSHFP = oNewData.expSHFP;
                    updRow.currSHAH = oNewData.currSHAH;
                    updRow.expSHAH = oNewData.expSHAH;
                    updRow.currSHLP = oNewData.currSHLP;
                    updRow.expSHLP = oNewData.expSHLP;
                    updRow.currSHOML = oNewData.currSHOML;
                    updRow.expSHOML = oNewData.expSHOML;
                    updRow.currSHO = oNewData.currSHO;
                    updRow.expSHO = oNewData.expSHO;
                    updRow.currSHRAH = oNewData.currSHRAH;
                    updRow.expSHRAH = oNewData.expSHRAH;

                    updRow.accessorialComment = oNewData.accessorialComment;

                    updRow.currBeyondOriginDisc = oNewData.currBeyondOriginDisc;
                    updRow.expBeyondOriginDisc = oNewData.expBeyondOriginDisc;
                    updRow.currBeyondDestDisc = oNewData.currBeyondDestDisc;
                    updRow.expBeyondDestDisc = oNewData.expBeyondDestDisc;
                    
                    updRow.UpdatedBy = oNewNotes.UpdatedBy;
                    updRow.UpdatedOn = oNewNotes.UpdatedOn;
                }

                // Submit the changes to the database. 
                prepumaContext.SubmitChanges();
                oNewData.idContractRenewal = oExisting.idContractRenewal;
                ContractRenewalID = oNewData.idContractRenewal;
            }
            else
            {

                tblContractRenewal oNewRow = new tblContractRenewal()
                {
                    idContractRenewalType = oNewData.idContractRenewalType,
                    RelationshipName = oNewData.Relationship,
                    effectiveDate = oNewData.effectiveDate,
                    expiryDate = oNewData.expiryDate,
                    pctIncreaseCourier = oNewData.pctIncreaseCourier,
                    pctIncreaseFreight = oNewData.pctIncreaseFreight,
                    pctIncreaseLTL = oNewData.pctIncreaseLTL,
                    pctIncreasePPST = oNewData.pctIncreasePPST,
                    pctIncreaseCPC = oNewData.pctIncreaseCPC,
                    idContractRenewalRouting = oNewData.idContractRenewalRouting,

                    sowFlag = oNewData.sowFlag,
                    rateOfChangeFlag =  oNewData.rateOfChangeFlag,
                    modificationReason =  oNewData.modificationReason,

                    currMarginPct =  oNewData.currMarginPct,
                    currRevenue =  oNewData.currRevenue,
                    currCourier =  oNewData.currCourier,
                    currFFWD =  oNewData.currFFWD,
                    currLTL =  oNewData.currLTL,
                    currPPST =  oNewData.currPPST,
                    currCPC =  oNewData.currCPC,
                    currOther =  oNewData.currOther,

                    sowMarginPct = oNewData.sowMarginPct,
                    sowRevenue = oNewData.sowRevenue,
                    sowCourier = oNewData.sowCourier,
                    sowFFWD = oNewData.sowFFWD,
                    sowLTL = oNewData.sowLTL,
                    sowPPST = oNewData.sowPPST,
                    sowCPC = oNewData.sowCPC,
                    sowOther = oNewData.sowOther,

                    rocMarginPct = oNewData.rocMarginPct,
                    rocRevenue = oNewData.rocRevenue,
                    rocCourier = oNewData.rocCourier,
                    rocFFWD = oNewData.rocFFWD,
                    rocLTL = oNewData.rocLTL,
                    rocPPST = oNewData.rocPPST,
                    rocCPC = oNewData.rocCPC,
                    rocOther = oNewData.rocOther,

                    expMarginPct = oNewData.expMarginPct,
                    expRevenue = oNewData.expRevenue,
                    expCourier = oNewData.expCourier,
                    expFFWD = oNewData.expFFWD,
                    expLTL = oNewData.expLTL,
                    expPPST = oNewData.expPPST,
                    expCPC = oNewData.expCPC,
                    expOther = oNewData.expOther,

                    newMarginPct = oNewData.newMarginPct,
                    newRevenue = oNewData.newRevenue,
                    newCourier = oNewData.newCourier,
                    newFFWD = oNewData.newFFWD,
                    newLTL = oNewData.newLTL,
                    newPPST = oNewData.newPPST,
                    newCPC = oNewData.newCPC,
                    newOther = oNewData.newOther,

                    currTargetGrMarginPct = oNewData.currTargetGrMarginPct,
                    expTargetGrMarginPct = oNewData.expTargetGrMarginPct,
                    newTargetGrMarginPct = oNewData.newTargetGrMarginPct,

                    VDACommitment = oNewData.VDACommitment,

                    currDimsAir = oNewData.currDimsAir,
                    expDimsAir = oNewData.expDimsAir,
                    currDimsGround = oNewData.currDimsGround,
                    expDimsGround = oNewData.expDimsGround,
                    currResiFees = oNewData.currResiFees,
                    expResiFees = oNewData.expResiFees,

                    currAlignPuroInc = oNewData.currAlignPuroInc,
                    expAlignPuroInc = oNewData.expAlignPuroInc,
                    currFuelPI = oNewData.currFuelPI,
                    expFuelPI = oNewData.expFuelPI,
                    currFuelPuro = oNewData.currFuelPuro,
                    expFuelPuro = oNewData.expFuelPuro,

                    currDGFR = oNewData.currDGFR,
                    currDGFRtype = oNewData.currDGFRtype,
                    expDGFR = oNewData.expDGFR,
                    expDGFRtype = oNewData.expDGFRtype,
                    currDGUN3373 = oNewData.currDGUN3373,
                    currDGUN3373type = oNewData.currDGUN3373type,
                    expDGUN3373 = oNewData.expDGUN3373,
                    expDGUN3373type = oNewData.expDGUN3373type,
                    currDGUN1845 = oNewData.currDGUN1845,
                    currDGUN1845type = oNewData.currDGUN1845type,
                    expDGUN1845 = oNewData.expDGUN1845,
                    expDGUN1845type = oNewData.expDGUN1845type,
                    currDGLT500kg = oNewData.currDGLT500kg,
                    currDGLT500kgtype = oNewData.currDGLT500kgtype,
                    expDGLT500kg = oNewData.expDGLT500kg,
                    expDGLT500kgtype = oNewData.expDGLT500kgtype,
                    currDGLQ = oNewData.currDGLQ,
                    currDGLQtype = oNewData.currDGLQtype,
                    expDGLQ = oNewData.expDGLQ,
                    expDGLQtype = oNewData.expDGLQtype,

                    currSHFP = oNewData.currSHFP,
                    expSHFP = oNewData.expSHFP,
                    currSHAH = oNewData.currSHAH,
                    expSHAH = oNewData.expSHAH,
                    currSHLP = oNewData.currSHLP,
                    expSHLP = oNewData.expSHLP,
                    currSHOML = oNewData.currSHOML,
                    expSHOML = oNewData.expSHOML,
                    currSHO = oNewData.currSHO,
                    expSHO = oNewData.expSHO,
                    currSHRAH = oNewData.currSHRAH,
                    expSHRAH = oNewData.expSHRAH,

                    accessorialComment = oNewData.accessorialComment,

                    currBeyondOriginDisc = oNewData.currBeyondOriginDisc,
                    expBeyondOriginDisc = oNewData.expBeyondOriginDisc,
                    currBeyondDestDisc = oNewData.currBeyondDestDisc,
                    expBeyondDestDisc = oNewData.expBeyondDestDisc,
                    
                    CreatedBy = oNewNotes.CreatedBy,
                    CreatedOn = oNewNotes.CreatedOn,
                    UpdatedBy = oNewNotes.UpdatedBy,
                    UpdatedOn = oNewNotes.UpdatedOn,
                    //ActiveFlag = oNewNotes.ActiveFlag
                };

                // Add the new object to the contracts collection.
                prepumaContext.GetTable<tblContractRenewal>().InsertOnSubmit(oNewRow);
                prepumaContext.SubmitChanges();

                oNewData.idContractRenewal = oNewRow.idContractRenewal;
                ContractRenewalID = oNewData.idContractRenewal;
            }

            //remove contract numbers if any are already existing
            try
            {
                String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
                SqlConnection cnn;
                SqlCommand cmd;
                cnn = new SqlConnection(strConnString);
                cnn.Open();
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Delete_tblContractRenewalDetail";
                cmd.Parameters.Add("@idContractRenewal", SqlDbType.Int).Value = ContractRenewalID;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
               
            }
                     
                      
            //Add selected contracts
            foreach (string contractnumber in oNewDetail) 
            {
                tblContractRenewalDetail newDetail = new tblContractRenewalDetail()
                {
                    idContractRenewal = ContractRenewalID,
                    ContractNumber = contractnumber
                };
                // Add the new object 
                prepumaContext.GetTable<tblContractRenewalDetail>().InsertOnSubmit(newDetail);               
            }
            prepumaContext.SubmitChanges();

            //Check for Note
            if (oNewNotes.Note != "")
            {
                //insert new note
                tblContractRenewalNote oNewNote = new tblContractRenewalNote()
                {
                    idContractRenewal = ContractRenewalID,
                    Note = oNewNotes.Note,
                    NoteType = oNewNotes.NoteType,
                    CreatedBy = oNewNotes.CreatedBy,
                    CreatedOn = oNewNotes.CreatedOn,
                    UpdatedBy = oNewNotes.UpdatedBy,
                    UpdatedOn = oNewNotes.UpdatedOn,
                    ActiveFlag = oNewNotes.ActiveFlag
                };

                // Add the new object 
                prepumaContext.GetTable<tblContractRenewalNote>().InsertOnSubmit(oNewNote);
                prepumaContext.SubmitChanges();
            }
            

        }

        public static void DeactivateContract(int idContractRenewal, string userid)
        {
           
               ClsContractRenewal oExisting = GetContractRenewal(idContractRenewal);
              

            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            if (oExisting != null)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in prepumaContext.GetTable<tblContractRenewal>()
                    where qdata.idContractRenewal == oExisting.idContractRenewal
                    select qdata;


                foreach (tblContractRenewal updRow in query)
                {
                    updRow.ActiveFlag = false;
                    updRow.UpdatedBy = userid;
                    updRow.UpdatedOn = DateTime.Now;
                }

                // Submit the changes to the database. 
                prepumaContext.SubmitChanges();
            }
                
        }


        public static List<string> GetContractNumbers(int renewalID)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsContractRenewal> contractNumbers = (from data in prepumaContext.GetTable<tblContractRenewalDetail>()
                                                    where data.idContractRenewal == renewalID
                                                    orderby data.ContractNumber
                                                    select new ClsContractRenewal
                                                    {
                                                        ContractNumber = data.ContractNumber
                                                    }).ToList<ClsContractRenewal>();
            
            
            var contractList = from c in contractNumbers
                               select c.ContractNumber;
            List<string> returnList = contractList.ToList();
            return returnList;
        }

        public static string GetContractNumbersString(int renewalID)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsContractRenewal> contractNumbers = (from data in prepumaContext.GetTable<tblContractRenewalDetail>()
                                                        where data.idContractRenewal == renewalID
                                                        orderby data.ContractNumber
                                                        select new ClsContractRenewal
                                                        {
                                                            ContractNumber = data.ContractNumber
                                                        }).ToList<ClsContractRenewal>();


            var contractList = from c in contractNumbers
                               select c.ContractNumber;
            List<string> returnList = contractList.ToList();
            string contractString = "";
            foreach (string c in returnList)
            {
                if (contractString != "")
                    contractString = contractString + ", ";
                contractString = contractString + c;
            }

            return contractString;
        }


       

    }


}
     
