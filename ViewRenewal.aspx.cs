using PrepumaWebApp.App_Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Configuration;

namespace PrepumaWebApp
{
    public partial class ViewRenewal : System.Web.UI.Page
    {
        public Int16 tdsowwidth = 50;
        public Int16 tdrocwidth = 50;
        public Int16 tdexpwidth = 100;
        public Int16 tdcurwidth = 100;
        public Int16 tdnewwidth = 100;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClsContractRenewal ctxt = new ClsContractRenewal();


            if (Request.QueryString["ContractRenewalID"] != null)
            {

                try
                {
                    int idContractRenewal = Convert.ToInt32(Request.QueryString["ContractRenewalID"].Trim());
                    ClsContractRenewal contract = ClsContractRenewal.GetContractRenewal(idContractRenewal);

                    fillInData(contract);
                    hideFields(contract);

                    string ContractNumbers = ClsContractRenewal.GetContractNumbersString(idContractRenewal);
                    lblContractNumbers.Text = ContractNumbers;

                }
                catch (Exception ex)
                {
                    //pnlDanger.Visible = true;
                    //lblDanger.Text = ex.Message.ToString();
                }

            }


        }
        protected void hideFields(ClsContractRenewal contract)
        {
            int idContractType = Convert.ToInt16(contract.idContractRenewalType);
            //New Contract
            int newtypeid = Convert.ToInt16(ConfigurationManager.AppSettings["NewCustomerTypeID"]);
            if (idContractType == newtypeid)
            {
                lblVDA.Visible=true;
                lblVDARevenue.Visible=true;

                lblpctIncreases.Visible=false;
                lblpctCourier.Visible=false;
                lblpctFreight.Visible=false;
                lblpctLTL.Visible = false;
                lblpctPPST.Visible = false;
                lblpctCPC.Visible=false;
                lblpctOther.Visible = false;
                               
                lblCurrent.Visible=false;
                lblSOW.Visible = false;
                lblROC.Visible = false;
                lblNewProfile.Visible=false;

                lblcurMargin.Visible=false;
                lblsowMargin.Visible = false;
                lblrocMargin.Visible = false;
                lblnewMargin.Visible=false;

                lblcurRev.Visible=false;
                lblsowRev.Visible = false;
                lblrocRev.Visible = false;
                lblnewRev.Visible=false;

                lblcurCourier.Visible=false;
                lblsowCourier.Visible = false;
                lblrocCourier.Visible = false;
                lblnewCourier.Visible=false;

                lblcurFF.Visible=false;
                lblsowFF.Visible = false;
                lblrocFF.Visible = false;
                lblnewFF.Visible=false;

                lblcurLTL.Visible=false;
                lblsowLTL.Visible = false;
                lblrocLTL.Visible = false;
                lblnewLTL.Visible=false;

                lblcurPPST.Visible = false;
                lblsowPPST.Visible = false;
                lblrocPPST.Visible = false;
                lblnewPPST.Visible = false;

                lblcurCPC.Visible=false;
                lblsowCPC.Visible = false;
                lblrocCPC.Visible = false;
                lblnewCPC.Visible=false;

                lblcurOther.Visible = false;
                lblsowOther.Visible = false;
                lblrocOther.Visible = false;
                lblnewOther.Visible = false;

                lblcurGrossMargin.Visible=false;
                lblsowGrossMargin.Visible = false;
                lblrocGrossMargin.Visible = false;
                lblnewGrossMargin.Visible=false;

                lblExpected.Text = "Expected";


                tdcurwidth = 2;
                tdnewwidth = 2;
                tdsowwidth = 2;
                tdrocwidth = 2;

            }
            else
            {
                lblVDA.Visible = false;
                lblVDARevenue.Visible = false;

                if (contract.sowFlag == false)
                {
                    lblSOW.Visible = false;
                    lblsowMargin.Visible = false;
                    lblsowMargin.Visible = false;
                    lblsowRev.Visible = false;
                    lblsowCourier.Visible = false;
                    lblsowFF.Visible = false;
                    lblsowLTL.Visible = false;
                    lblsowPPST.Visible = false;
                    lblsowCPC.Visible = false;
                    lblsowOther.Visible = false;
                    lblsowGrossMargin.Visible = false;
                    tdsowwidth = 1;
                }
                if (contract.rateOfChangeFlag == false)
                {
                    lblROC.Visible = false;
                    lblrocMargin.Visible = false;
                    lblrocMargin.Visible = false;
                    lblrocRev.Visible = false;
                    lblrocCourier.Visible = false;
                    lblrocFF.Visible = false;
                    lblrocLTL.Visible = false;
                    lblrocPPST.Visible = false;
                    lblrocCPC.Visible = false;
                    lblrocOther.Visible = false;
                    lblrocGrossMargin.Visible = false;
                    tdrocwidth = 1;
                }
               
            }
           
         }

        protected void fillInData(ClsContractRenewal contract)
        {
           

            lblCustomerName.Text = contract.Relationship + " - " + contract.RenewalType;
            if (contract.modificationReason != null)
            {
                lblReason.Text = contract.modificationReason;
            }

            //Get First Ship Date, if available in PI Sales Incentives table
            try
            {
                
                lblFSD.Text = "unavailable";
                ClsContractRelationship ccr = new ClsContractRelationship();
                DateTime? fsd = ccr.GetFirstShipDate(contract.Relationship);
                if (fsd == null)
                {
                    lblFSD.Text = "unavailable";
                }
                else
                {
                    lblFSD.Text = Convert.ToDateTime(fsd).ToString("MM/dd/yyyy");
                }

            }
            catch (Exception ex)
            {

            }

            List<ClsContractRelationship> lstContracts;
            ClsContractRelationship cr = new ClsContractRelationship();
            lstContracts = cr.GetContractsByRelationship(contract.Relationship);
            string srids = "";
            List<string> sridList = new List<string>();
            string branches = "";
            List<string> branchList = new List<string>();
            bool sridfound;
            bool branchfound;
            foreach (ClsContractRelationship critem in lstContracts)
            {   
                sridfound = false;
                foreach (string sriditem in sridList)
                {
                    if (critem.Territory == sriditem)
                        sridfound = true;
                }
                if (sridfound == false)
                {
                    sridList.Add(critem.Territory);
                    if (srids != "")
                        srids = srids + ", ";
                    srids = srids + critem.Territory;
                }

                //Check Branch
                branchfound = false;
                foreach (string branchitem in branchList)
                {
                    if (critem.Region == branchitem)
                        branchfound = true;
                }
                if (branchfound == false)
                {
                    branchList.Add(critem.Region);
                    if (branches != "")
                        branches = branches + ", ";
                    branches = branches + critem.Region;
                }

                lblSalesInfo.Text = srids + " " + branches;
                //Get Sls Name, if there is a single srid
                if (srids.IndexOf(',') == -1)
                {
                    ClsSalesReps rep = ClsSalesReps.GetSalesReps(srids);
                    if (rep != null)
                        lblslsName.Text = rep.SalesRep;
                }
            }

            lblEffectiveDate.Text = Convert.ToDateTime(contract.effectiveDate).ToString("MM/dd/yyyy");
            lblExpiryDate.Text = Convert.ToDateTime(contract.expiryDate).ToString("MM/dd/yyyy");
            //percetages
            double pctCourier = Convert.ToDouble(contract.pctIncreaseCourier);
            double pctFreight = Convert.ToDouble(contract.pctIncreaseFreight);
            double pctLTL = Convert.ToDouble(contract.pctIncreaseLTL);
            double pctPPST = Convert.ToDouble(contract.pctIncreasePPST);
            double pctCPC = Convert.ToDouble(contract.pctIncreaseCPC);
            double pctOther = Convert.ToDouble(contract.pctIncreaseOther);
            
            if (pctCourier != 0)
               lblpctCourier.Text = pctCourier + "%";
            if (pctFreight != 0)
               lblpctFreight.Text = pctFreight + "%";
            if (pctLTL != 0)
               lblpctLTL.Text = pctLTL + "%";
            if (pctPPST != 0)
               lblpctPPST.Text = pctPPST + "%";
            if (pctCPC != 0)
               lblpctCPC.Text = pctCPC + "%";
            if (pctOther != 0)
               lblpctOther.Text = pctOther + "%";
            

            double currMargin = Convert.ToDouble(contract.currMarginPct);
            double sowMargin = Convert.ToDouble(contract.sowMarginPct);
            double rocMargin = Convert.ToDouble(contract.rocMarginPct);
            double expMargin = Convert.ToDouble(contract.expMarginPct);
            double newMargin = Convert.ToDouble(contract.newMarginPct);
            lblcurMargin.Text = currMargin + "%";
            //lblsowMargin.Text = sowMargin + "%";
            //lblrocMargin.Text = rocMargin + "%";
            //lblexpMargin.Text = expMargin + "%";
            lblnewMargin.Text = newMargin + "%";

            decimal currRev = Convert.ToDecimal(contract.currRevenue);
            decimal sowRev = Convert.ToDecimal(contract.sowRevenue);
            decimal rocRev = Convert.ToDecimal(contract.rocRevenue);
            decimal expRev = Convert.ToDecimal(contract.expRevenue);
            decimal newRev = Convert.ToDecimal(contract.newRevenue);
            lblcurRev.Text = string.Format("{0:C0}", currRev);
            lblsowRev.Text = string.Format("{0:C0}", sowRev);
            lblrocRev.Text = string.Format("{0:C0}", rocRev);
            lblexpRev.Text = string.Format("{0:C0}", expRev);
            lblnewRev.Text = string.Format("{0:C0}", newRev);

            decimal currCour = Convert.ToDecimal(contract.currCourier);
            decimal sowCour = Convert.ToDecimal(contract.sowCourier);
            decimal rocCour = Convert.ToDecimal(contract.rocCourier);
            decimal expCour = Convert.ToDecimal(contract.expCourier);
            decimal newCour = Convert.ToDecimal(contract.newCourier);
            lblcurCourier.Text = string.Format("{0:C0}", currCour);
            lblsowCourier.Text = string.Format("{0:C0}", sowCour);
            lblrocCourier.Text = string.Format("{0:C0}", rocCour);
            lblexpCourier.Text = string.Format("{0:C0}", expCour);
            lblnewCourier.Text = string.Format("{0:C0}", newCour);

            decimal currFFWD = Convert.ToDecimal(contract.currFFWD);
            decimal sowFFWD = Convert.ToDecimal(contract.sowFFWD);
            decimal rocFFWD = Convert.ToDecimal(contract.rocFFWD);
            decimal expFFWD = Convert.ToDecimal(contract.expFFWD);
            decimal newFFWD = Convert.ToDecimal(contract.newFFWD);
            lblcurFF.Text = string.Format("{0:C0}", currFFWD);
            lblsowFF.Text = string.Format("{0:C0}", sowFFWD);
            lblrocFF.Text = string.Format("{0:C0}", rocFFWD);
            lblexpFF.Text = string.Format("{0:C0}", expFFWD);
            lblnewFF.Text = string.Format("{0:C0}", newFFWD);

            decimal currLTL = Convert.ToDecimal(contract.currLTL);
            decimal sowLTL = Convert.ToDecimal(contract.sowLTL);
            decimal rocLTL = Convert.ToDecimal(contract.rocLTL);
            decimal expLTL = Convert.ToDecimal(contract.expLTL);
            decimal newLTL = Convert.ToDecimal(contract.newLTL);
            lblcurLTL.Text = string.Format("{0:C0}", currLTL);
            lblsowLTL.Text = string.Format("{0:C0}", sowLTL);
            lblrocLTL.Text = string.Format("{0:C0}", rocLTL);
            lblexpLTL.Text = string.Format("{0:C0}", expLTL);
            lblnewLTL.Text = string.Format("{0:C0}", newLTL);

            decimal currPPST = Convert.ToDecimal(contract.currPPST);
            decimal sowPPST = Convert.ToDecimal(contract.sowPPST);
            decimal rocPPST = Convert.ToDecimal(contract.rocPPST);
            decimal expPPST = Convert.ToDecimal(contract.expPPST);
            decimal newPPST = Convert.ToDecimal(contract.newPPST);
            lblcurPPST.Text = string.Format("{0:C0}", currPPST);
            lblsowPPST.Text = string.Format("{0:C0}", sowPPST);
            lblrocPPST.Text = string.Format("{0:C0}", rocPPST);
            lblexpPPST.Text = string.Format("{0:C0}", expPPST);
            lblnewPPST.Text = string.Format("{0:C0}", newPPST);

            decimal currCPC = Convert.ToDecimal(contract.currCPC);
            decimal sowCPC = Convert.ToDecimal(contract.sowCPC);
            decimal rocCPC = Convert.ToDecimal(contract.rocCPC);
            decimal expCPC = Convert.ToDecimal(contract.expCPC);
            decimal newCPC = Convert.ToDecimal(contract.newCPC);
            lblcurCPC.Text = string.Format("{0:C0}", currCPC);
            lblsowCPC.Text = string.Format("{0:C0}", sowCPC);
            lblrocCPC.Text = string.Format("{0:C0}", rocCPC);
            lblexpCPC.Text = string.Format("{0:C0}", expCPC);
            lblnewCPC.Text = string.Format("{0:C0}", newCPC);

            decimal currOther = Convert.ToDecimal(contract.currOther);
            decimal sowOther = Convert.ToDecimal(contract.sowOther);
            decimal rocOther = Convert.ToDecimal(contract.rocOther);
            decimal expOther = Convert.ToDecimal(contract.expOther);
            decimal newOther = Convert.ToDecimal(contract.newOther);
            lblcurOther.Text = string.Format("{0:C0}", currOther);
            lblsowOther.Text = string.Format("{0:C0}", sowOther);
            lblrocOther.Text = string.Format("{0:C0}", rocOther);
            lblexpOther.Text = string.Format("{0:C0}", expOther);
            lblnewOther.Text = string.Format("{0:C0}", newOther);

            double currTGM = Convert.ToDouble(contract.currTargetGrMarginPct);
            double sowTGM = Convert.ToDouble(contract.sowTargetGrMarginPct);
            double rocTGM = Convert.ToDouble(contract.rocTargetGrMarginPct);
            double expTGM = Convert.ToDouble(contract.expTargetGrMarginPct);
            double newTGM = Convert.ToDouble(contract.newTargetGrMarginPct);
            lblcurGrossMargin.Text = string.Format("{0:C0}", currTGM) + "%";
            lblnewGrossMargin.Text = string.Format("{0:C0}", newTGM) + "%";

            if (contract.otherDescription != "")
            {
                lblOtherDesc.Text = "Other Desc: " + contract.otherDescription;
            }
            
            

            decimal VDA = Convert.ToDecimal(contract.VDACommitment);
            lblVDARevenue.Text = string.Format("{0:C0}", VDA);

            lblAccessorialComments.Text = contract.accessorialComment;

                   


            //Fuel and Beyond Discounts
            bool showFuel = checkFuel(contract);
            if (showFuel == false)
            {
                hideFuel(false);
            }
            else
            {
                if (contract.currAlignPuroInc == true)
                {
                    lblcurFuelAlignment.Text = "Puro Inc.";
                    if (contract.currFuelPI == "")
                        lblcurFuelDiscount.Text = "None";
                    else
                        lblcurFuelDiscount.Text = contract.currFuelPI;
                }
                else
                {
                    lblcurFuelAlignment.Text = "Purolator Intl";
                    if (contract.currFuelPuro == "")
                        lblcurFuelDiscount.Text = "None";
                    else
                        lblcurFuelDiscount.Text = contract.currFuelPuro;
                }
                if (contract.expAlignPuroInc == true)
                {
                    lblexpFuelAlignment.Text = "Puro Inc.";
                    if (contract.expFuelPI == "")
                        lblexpFuelDiscount.Text = "None";
                    else
                        lblexpFuelDiscount.Text = contract.expFuelPI;
                }
                else
                {
                    lblexpFuelAlignment.Text = "Purolator Intl";
                    if (contract.expFuelPuro == "")
                        lblexpFuelDiscount.Text = "None";
                    else
                        lblexpFuelDiscount.Text = contract.expFuelPuro;
                }
                if (contract.currBeyondOriginDisc == true)
                    lblBeyondOriginDiscount.Text = "Yes";
                else
                    lblBeyondOriginDiscount.Text = "No";
                if (contract.currBeyondDestDisc == true)
                    lblBeyondDestDiscount.Text = "Yes";
                else
                    lblBeyondDestDiscount.Text = "No";
            }
            



            bool showAccessorials = checkAccessorials(contract);
            if (showAccessorials == false)
            {

                hideAccessorials(false);

            }
            else
            {
                 //GET ACCESSORIALS DATA
                decimal currResiFees = Convert.ToDecimal(contract.currResiFees);
                decimal expResiFees = Convert.ToDecimal(contract.currResiFees); 
                lblcurResiFees.Text = string.Format("{0:$0.00}", currResiFees);
                lblexpResiFees.Text = string.Format("{0:$0.00}", expResiFees);

                decimal currDimsGround = Convert.ToDecimal(contract.currDimsGround);
                decimal expDimsGround = Convert.ToDecimal(contract.currDimsGround);
                lblcurDimsGround.Text = string.Format("{0:$0.00}", currDimsGround);
                lblexpDimsGround.Text = string.Format("{0:$0.00}", expDimsGround);

                decimal currSHFP = Convert.ToDecimal(contract.currSHFP);
                decimal expSHFP = Convert.ToDecimal(contract.expSHFP);
                lblcurshFP.Text = string.Format("{0:$0.00}", currSHFP);
                lblexpshFP.Text = string.Format("{0:$0.00}", expSHFP);

                decimal currDimsAir = Convert.ToDecimal(contract.currDimsAir);
                decimal expDimsAir = Convert.ToDecimal(contract.expDimsAir);
                lblcurDimsAir.Text = string.Format("{0:$0.00}", currDimsAir);
                lblexpDimsAir.Text = string.Format("{0:$0.00}", expDimsAir);

                decimal currSHAH = Convert.ToDecimal(contract.currSHAH);
                decimal expSHAH = Convert.ToDecimal(contract.expSHAH);
                lblcurshAH.Text = string.Format("{0:$0.00}", currSHAH);
                lblexpshAH.Text = string.Format("{0:$0.00}", expSHAH);

                decimal currDGFR = Convert.ToDecimal(contract.currDGFR);
                decimal expDGFR = Convert.ToDecimal(contract.expDGFR);
                lblcurdgFR.Text = string.Format("{0:$0.00}", currDGFR);
                lblexpdgFR.Text = string.Format("{0:$0.00}", expDGFR);

                decimal currSHLP = Convert.ToDecimal(contract.currSHLP);
                decimal expSHLP = Convert.ToDecimal(contract.expSHLP);
                lblcurshLP.Text = string.Format("{0:$0.00}", currSHLP);
                lblexpshLP.Text = string.Format("{0:$0.00}", expSHLP);

                decimal currDGUN3373 = Convert.ToDecimal(contract.currDGUN3373);
                decimal expDGUN3373 = Convert.ToDecimal(contract.expDGUN3373);
                lblcurdgUN3373.Text = string.Format("{0:$0.00}", currDGUN3373);
                lblexpdgUN3373.Text = string.Format("{0:$0.00}", expDGUN3373);

                decimal currSHOML = Convert.ToDecimal(contract.currSHOML);
                decimal expSHOML = Convert.ToDecimal(contract.expSHOML);
                lblcurshOML.Text = string.Format("{0:$0.00}", currSHOML);
                lblexpshOML.Text = string.Format("{0:$0.00}", expSHOML);

                decimal currDGUN1845 = Convert.ToDecimal(contract.currDGUN1845);
                decimal expDGUN1845 = Convert.ToDecimal(contract.expDGUN1845);
                lblcurdgUN1845.Text = string.Format("{0:$0.00}", currDGUN1845);
                lblexpdgUN1845.Text = string.Format("{0:$0.00}", expDGUN1845);

                decimal currSHO = Convert.ToDecimal(contract.currSHO);
                decimal expSHO = Convert.ToDecimal(contract.expSHO);
                lblcurshO.Text = string.Format("{0:$0.00}", currSHO);
                lblexpshO.Text = string.Format("{0:$0.00}", expSHO);

                decimal currDGLT500kg = Convert.ToDecimal(contract.currDGLT500kg);
                decimal expDGLT500kg = Convert.ToDecimal(contract.expDGLT500kg);
                lblcurdgLT500kg.Text = string.Format("{0:$0.00}", currDGLT500kg);
                lblexpdgLT500kg.Text = string.Format("{0:$0.00}", expDGLT500kg);

                decimal currSHRAH = Convert.ToDecimal(contract.currSHRAH);
                decimal expSHRAH = Convert.ToDecimal(contract.expSHRAH);
                lblcurshRAH.Text = string.Format("{0:$0.00}", currSHRAH);
                lblexpshRAH.Text = string.Format("{0:$0.00}", expSHRAH);

                decimal currDGLQ = Convert.ToDecimal(contract.currDGLQ);
                decimal expDGLQ = Convert.ToDecimal(contract.expDGLQ);
                lblcurdgLQ.Text = string.Format("{0:$0.00}", currDGLQ);
                lblexpdgLQ.Text = string.Format("{0:$0.00}", expDGLQ);                             
               
              
            }
           

            //NOTES
            List<ClsRenewalNotes> notesList = ClsRenewalNotes.GetContractRenewalApprovalNotes(contract.idContractRenewal);
            if (notesList.Count > 0)
            {
                string noteString = "";
                noteString = "<table><tr style='background-color:gray'><td style='vertical-align:top;text-decoration:underline'>Date</td><td style='vertical-align:top;text-decoration:underline'>Entered By</td><td style='text-decoration:underline'>Note</td></tr>";
                DateTime notedate;
                string notevalue = "";
                string bgcolor = "";
                int num = 0;

                foreach (ClsRenewalNotes note in notesList)
                {
                    num = num + 1;
                    if (num % 2 == 1)
                    {
                        bgcolor = "lightgray";
                    }
                    else
                    {
                        bgcolor = "white";
                    }
                    notedate = (DateTime)note.CreatedOn;
                    notevalue = note.Note;
                    notevalue = notevalue.Replace("\n", "<br>");
                    noteString = noteString + "<tr style='background-color:" + bgcolor + " !important; -webkit-print-color-adjust: exact'><td style='vertical-align:top;font-family:Calibri;width:75px'>" + notedate.ToString("MM/dd/yyyy") + "</td><td style='vertical-align:top;font-family:Calibri;width:75px'>" + note.CreatedBy + "</td><td style='color:blue;width:590px'>" + notevalue + "</td></tr>";
                }
                noteString = noteString + "</table>";
                lblNotes.Text = noteString;
            }
            else
            {
                lblNotesl.Visible = false;
            }
        }

        protected bool checkAccessorials(ClsContractRenewal contract)
        {
            bool foundAccessorials = false;

            if (contract.currResiFees != 0|| contract.expResiFees != 0)
                foundAccessorials = true;
            if (contract.currDimsGround != 0 || contract.expDimsGround != 0)
                foundAccessorials = true;
            if (contract.currSHFP != 0 || contract.expSHFP != 0)
                foundAccessorials = true;
            if (contract.currDimsAir != 0 || contract.expDimsAir != 0)
                foundAccessorials = true;
            if (contract.currSHAH != 0 || contract.expSHAH != 0)
                foundAccessorials = true;
            if (contract.currDGFR != 0 || contract.expDGFR != 0)
                foundAccessorials = true;
            if (contract.currSHLP != 0 || contract.expSHLP != 0)
                foundAccessorials = true;
            if (contract.currSHLP != 0 || contract.expSHLP != 0)
                foundAccessorials = true;
            if (contract.currDGUN3373 != 0 || contract.expDGUN3373 != 0)
                foundAccessorials = true;
            if (contract.currSHOML != 0 || contract.expSHOML != 0)
                foundAccessorials = true;
            if (contract.currSHO != 0 || contract.expSHO != 0)
                foundAccessorials = true;
            if (contract.currDGLT500kg != 0 || contract.expDGLT500kg != 0)
                foundAccessorials = true;
            if (contract.currSHRAH != 0 || contract.expSHRAH != 0)
                foundAccessorials = true;


                return foundAccessorials;
        }

        protected bool checkFuel(ClsContractRenewal contract)
        {
            bool foundFuel = false;
            if (contract.currAlignPuroInc != false)
                foundFuel = true;
            if (contract.currFuelPI != "" && contract.currFuelPI != null)
                foundFuel = true;
            if (contract.expFuelPI != "" && contract.expFuelPI != null)
                foundFuel = true;
            if (contract.currFuelPuro != "" && contract.currFuelPuro != null)
                foundFuel = true;
            if (contract.currFuelPuro != "" && contract.currFuelPuro != null)
                foundFuel = true;
            if (contract.currBeyondOriginDisc != false)
                foundFuel = true;
            if (contract.currBeyondDestDisc != false)
                foundFuel = true;
            return foundFuel;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            btnCancel.Visible = false;
            btnPrint.Visible = false;
            ClientScript.RegisterClientScriptBlock(this.GetType(), "key", "window.print()", true);

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "key", "window.close()", true);
        }
        protected void btnHide_Click(object sender, EventArgs e)
        {
            hideAccessorials(false);
            hideFuel(false);
            btnShow.Visible = true;
            btnHide.Visible = false;
            btnPrint.Visible = true;
            btnCancel.Visible = true;
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            hideAccessorials(true);
            hideFuel(true);
            btnHide.Visible = true;
            btnShow.Visible = false;
            btnPrint.Visible = true;
            btnCancel.Visible = true;
        }

        protected void hideAccessorials(bool visible)
        {

            //NO ACCESSORIALS - HIDE
            lblaAccessorials.Visible = visible;
            lblaCurrent1.Visible = visible;
            lblaCurrent2.Visible = visible;
            lblaExpected1.Visible = visible;
            lblaExpected2.Visible = visible;
            lblResiFees.Visible = visible;
            lblDimsGround.Visible = visible;
            lblshFP.Visible = visible;
            lblDimsAir.Visible = visible;
            lblshLP.Visible = visible;
            lbldgFR.Visible = visible;
            lblshAH.Visible = visible;
            lbldgUN3373.Visible = visible;
            lblshOML.Visible = visible;
            lbldgUN1845.Visible = visible;
            lblshO.Visible = visible;
            lbldgLT500kg.Visible = visible;
            lblshRAH.Visible = visible;
            lbldgLQ.Visible = visible;

            lblcurResiFees.Visible = visible;
            lblexpResiFees.Visible = visible;
            lblcurDimsGround.Visible = visible;
            lblexpDimsGround.Visible = visible;
            lblcurshFP.Visible = visible;
            lblexpshFP.Visible = visible;
            lblcurDimsAir.Visible = visible;
            lblexpDimsAir.Visible = visible;
            lblcurshAH.Visible = visible;
            lblexpshAH.Visible = visible;
            lblcurdgFR.Visible = visible;
            lblexpdgFR.Visible = visible;
            lblcurshLP.Visible = visible;
            lblexpshLP.Visible = visible;
            lblcurdgUN3373.Visible = visible;
            lblexpdgUN3373.Visible = visible;
            lblcurshOML.Visible = visible;
            lblexpshOML.Visible = visible;
            lblcurdgUN1845.Visible = visible;
            lblexpdgUN1845.Visible = visible;
            lblcurshO.Visible = visible;
            lblexpshO.Visible = visible;
            lblcurdgLT500kg.Visible = visible;
            lblexpdgLT500kg.Visible = visible;
            lblcurshRAH.Visible = visible;
            lblexpshRAH.Visible = visible;
            lblcurdgLQ.Visible = visible;
            lblexpdgLQ.Visible = visible;                        
        }

        protected void hideFuel(bool visible)
        {
            lblCFuelAlign.Visible = visible;
            lblCFuelDisc.Visible = visible;
            lblEFuelAlign.Visible = visible;
            lblEFuelDisc.Visible = visible;
            lblBeyondOrigin.Visible = visible;
            lblBeyondDest.Visible = visible;
            lblcurFuelAlignment.Visible = visible;
            lblcurFuelDiscount.Visible = visible;
            lblexpFuelAlignment.Visible = visible;
            lblexpFuelDiscount.Visible = visible;
            lblBeyondOriginDiscount.Visible = visible;
            lblBeyondDestDiscount.Visible = visible;
        }

        protected string formatDate(DateTime? thisdate)
        {
            string sdt = "";
            if (!String.IsNullOrEmpty(thisdate.ToString()))
            {
                DateTime dt = Convert.ToDateTime(thisdate);
                sdt = dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            return sdt;
        }
    }
}
