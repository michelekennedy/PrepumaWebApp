using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using PrepumaWebApp.App_Data.DAL;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;
using System.Configuration;

namespace PrepumaWebApp
{
    public partial class EditContractRenewal : System.Web.UI.UserControl
    {
        public Int16 tdsowwidth = 10;
        public Int16 tdrocwidth = 10;
        public Int16 tdexpwidth = 10;
        public Int16 tdcurwidth = 10;
        public Int16 tdnewwidth = 10;
        public bool doFileUploadFlag = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                getRevDates();
                getSAPDates();
                
            }
               
            
        }

        protected void getRevDates()
        {            
            DateTime maxDate = getMaxDate();
            DateTime displayEndDate = maxDate.AddMonths(1).AddDays(-1);
            DateTime beginDate = maxDate.AddMonths(-11);

            string beginstring = beginDate.ToString("MM/dd/yyyy");
            String endstring = displayEndDate.ToString("MM/dd/yyyy");
            lbldaterange.Text = beginstring + " - " + endstring;
        }


        protected DateTime getMaxDate()
        {
           
            DataTable dt1 = new DataTable();
            SqlConnection cnn;
            String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
            cnn = new SqlConnection(strConnString);
            DateTime maxDate = DateTime.Now;

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                cmd = new SqlCommand("sp_GetMarginTrendMaxDate", cnn);
                cmd.CommandTimeout = 10800;
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt1);
                maxDate = Convert.ToDateTime(dt1.Rows[0][0]);      

            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
            return maxDate;
        }

        protected void getSAPDates()
        {
            DateTime maxDate = getMaxSAPDate();
           
            string asOfstring = maxDate.ToString("MM/dd/yyyy");
            lblAccessorialDates.Text = asOfstring;
        }

        protected DateTime getMaxSAPDate()
        {

            DataTable dt1 = new DataTable();
            SqlConnection cnn;
            String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
            cnn = new SqlConnection(strConnString);
            DateTime maxDate = DateTime.Now;

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            //CHANGE to SAP when we get data
            try
            {
                cmd = new SqlCommand("sp_GetMaxSAPDate", cnn);
                cmd.CommandTimeout = 10800;
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt1);
                maxDate = Convert.ToDateTime(dt1.Rows[0][0]);

            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
            return maxDate;
        }

        protected DataTable getRevenue(string contractlist,string begindate,string enddate)
        {

            DataTable dt1 = new DataTable();
            SqlConnection cnn;
            String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
            cnn = new SqlConnection(strConnString);
            
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                cmd = new SqlCommand("sp_GetMarginActualRev", cnn);
                cmd.CommandTimeout = 10800;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ContractIDs", SqlDbType.VarChar).Value = contractlist;
                cmd.Parameters.Add("@beginstring", SqlDbType.VarChar).Value = begindate;
                cmd.Parameters.Add("@endstring", SqlDbType.VarChar).Value = enddate;
                da.SelectCommand = cmd;
                da.Fill(dt1);
                

            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
            return dt1;
        }

        protected DataTable getGrossTargetMargin(string type,decimal courier,decimal ffw, decimal ltl, decimal ppst, decimal cpc, decimal other)
        {

            DataTable dt1 = new DataTable();
            SqlConnection cnn;
            String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
            cnn = new SqlConnection(strConnString);

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                cmd = new SqlCommand("sp_GetGrossTargetMargin", cnn);
                cmd.CommandTimeout = 10800;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = type;
                cmd.Parameters.Add("@courier", SqlDbType.Money).Value = courier;
                cmd.Parameters.Add("@ffw", SqlDbType.Money).Value = ffw;
                cmd.Parameters.Add("@ltl", SqlDbType.Money).Value = ltl;
                cmd.Parameters.Add("@ppst", SqlDbType.Money).Value = ppst;
                cmd.Parameters.Add("@cpc", SqlDbType.Money).Value = cpc;
                cmd.Parameters.Add("@other", SqlDbType.Money).Value = other;
                da.SelectCommand = cmd;
                da.Fill(dt1);


            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
            return dt1;
        }

        protected DataTable getMargin(string contractlist, string begindate, string enddate)
        {

            DataTable dt1 = new DataTable();
            SqlConnection cnn;
            String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
            cnn = new SqlConnection(strConnString);

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                cmd = new SqlCommand("sp_GetMarginActualPct", cnn);
                cmd.CommandTimeout = 10800;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ContractIDs", SqlDbType.VarChar).Value = contractlist;
                cmd.Parameters.Add("@beginstring", SqlDbType.VarChar).Value = begindate;
                cmd.Parameters.Add("@endstring", SqlDbType.VarChar).Value = enddate;
                da.SelectCommand = cmd;
                da.Fill(dt1);


            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
            return dt1;
        }

       
     
        protected void btnAddOther_Click(object sender, EventArgs e)
        {
            showHideOther(true);
                      
        }

        protected void courierpct_TextChanged(object sender, EventArgs e)
        {
            decimal curCourier = 0;
            if (txtcurCourierN.Text != "" && txtcurCourierN.Text != null)
                  curCourier = Convert.ToDecimal(txtcurCourierN.Text);
            decimal pctCourier = 0;
            if (txtpctCourierN.Text != "" && txtpctCourierN.Text != null)
                  pctCourier = Convert.ToDecimal(txtpctCourierN.Text);
            decimal expCourier = curCourier * pctCourier / 100;
            txtexpCourierN.Text = expCourier.ToString();
            txtexpCourierN.ForeColor = System.Drawing.Color.ForestGreen;
            calcNewProfile();
            calcRevenueTotals();

        }
        protected void ffpct_TextChanged(object sender, EventArgs e)
        {
            decimal curFF = 0;
            if (txtcurFFN.Text != "" && txtcurFFN.Text != null)
                 curFF = Convert.ToDecimal(txtcurFFN.Text);
            decimal pctFF = 0;
            if (txtpctFFN.Text != "" && txtpctFFN.Text != null)
                 pctFF = Convert.ToDecimal(txtpctFFN.Text);
            decimal expFF = curFF * pctFF / 100;
            txtexpFFN.Text = expFF.ToString();
            txtexpFFN.ForeColor = System.Drawing.Color.ForestGreen;
            calcNewProfile();
            calcRevenueTotals();
        }
        protected void ltlpct_TextChanged(object sender, EventArgs e)
        {
            decimal curLTL = 0;
            if (txtcurLTLN.Text != "" && txtcurLTLN != null)
                 curLTL = Convert.ToDecimal(txtcurLTLN.Text);
            decimal pctLTL = 0;
            if (txtpctLTLN.Text != "" && txtpctLTLN.Text != null)
                 pctLTL = Convert.ToDecimal(txtpctLTLN.Text);
            decimal expLTL = curLTL * pctLTL / 100;
            txtexpLTLN.Text = expLTL.ToString();
            txtexpLTLN.ForeColor = System.Drawing.Color.ForestGreen;
            calcNewProfile();
            calcRevenueTotals();
        }
        protected void ppstpct_TextChanged(object sender, EventArgs e)
        {
            decimal curPPST = 0;
            if (txtcurPPSTN.Text != "" && txtcurPPSTN.Text != null)
                 curPPST = Convert.ToDecimal(txtcurPPSTN.Text);
            decimal pctPPST = 0;
            if (txtpctPPSTN.Text != "" && txtpctPPSTN.Text != null)
                pctPPST = Convert.ToDecimal(txtpctPPSTN.Text);
            decimal expPPST = curPPST * pctPPST / 100;
            txtexpPPSTN.Text = expPPST.ToString();
            txtexpPPSTN.ForeColor = System.Drawing.Color.ForestGreen;
            calcNewProfile();
            calcRevenueTotals();
        }
        protected void cpcpct_TextChanged(object sender, EventArgs e)
        {
            decimal curCPC = 0;
            if (txtcurCPCN.Text != "" && txtcurCPCN.Text != null)
                curCPC = Convert.ToDecimal(txtcurCPCN.Text);
            decimal pctCPC = 0;
            if (txtpctPPSTN.Text != "" && txtpctPPSTN.Text != null)
                pctCPC = Convert.ToDecimal(txtpctCPCN.Text);
            decimal expCPC = curCPC * pctCPC / 100;
            txtexpCPCN.Text = expCPC.ToString();
            txtexpCPCN.ForeColor = System.Drawing.Color.ForestGreen;
            calcNewProfile();
            calcRevenueTotals();
        }
        protected void otherpct_TextChanged(object sender, EventArgs e)
        {
            decimal curOther = 0;
            if (txtcurOtherN.Text != "" && txtcurOtherN.Text != null)
                curOther = Convert.ToDecimal(txtcurOtherN.Text);
            decimal pctOther = 0;
            if (txtpctOtherN.Text != "" && txtpctOtherN.Text != null)
                pctOther = Convert.ToDecimal(txtpctOtherN.Text);
            decimal expOther = curOther * pctOther / 100;
            txtexpOtherN.Text = expOther.ToString();
            txtexpOtherN.ForeColor = System.Drawing.Color.ForestGreen;
            calcNewProfile();
            calcRevenueTotals();
        }

        protected void btnDefault_Click(object sender, EventArgs e)
        {
            calcNewProfile();
            calcRevenueTotals();

        }

        //protected void radioFuel_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string fueltype = radioFuel.SelectedValue.ToLower();
        //    if(fueltype == "fuelpi")
        //    {
        //        txtcurFuelPIS.Enabled = true;
        //        txtexpFuelPIS.Enabled = true;
        //        txtcurFuelPuroS.Enabled = false;
        //        txtexpFuelPuroS.Enabled = false;
        //    }
        //    else
        //    {
        //        txtcurFuelPIS.Enabled = false;
        //        txtexpFuelPIS.Enabled = false;
        //        txtcurFuelPuroS.Enabled = true;
        //        txtexpFuelPuroS.Enabled = true;
        //    }
        //}

        protected void btnAddNote_Click(object sender, EventArgs e)
        {
            saveNote();
        }

        protected void saveNote()
        {
            try
            {

                string warningtxt = "";
                //bool newNoteflag = true;
                ClsRenewalNotes noteObj = new ClsRenewalNotes();
                noteObj = populateNoteObj();
                //int newID;
                //Check for required values
                if (noteObj.Note == "")
                {
                    warningtxt =  "Notes value is Empty";
                }
                
 
                if (warningtxt != "")
                {
       
                    lblErrorMessage.Text = warningtxt;
                }
                else
                {                   
          
                    ClsRenewalNotes.SaveNote(noteObj);
                    getNotes();
                    rgNotesGrid.DataBind();
                    txtNotes.Text = "";
                    
                    lblErrorMessage.Text = "";
                   
                }
            }

            catch (Exception ex)
            {
                //lblWarning.Text = ex.Message;
                //lblWarning.Visible = true;
                //pnlwarning.Visible = true;
            }
        }

        protected ClsRenewalNotes populateNoteObj()
        {
            //If Notes have been entered, return true so notes can be saved
            ClsRenewalNotes note = new ClsRenewalNotes();
            note.Note = txtNotes.Text;
            //RadioButtonList rblist1 = rblist1 as RadioButtonList;
            note.NoteType = rblist1.SelectedValue.ToLower();
            //RadButton cbxIncludeNote = userControl.FindControl("cbxIncludeNote") as RadButton;
            //note.includeFlag = cbxIncludeNote.Checked;
            note.idContractRenewal = Convert.ToInt16(ContractRenewalID.Value);
            note.CreatedBy = (string)Session["userName"];
            note.CreatedOn = Convert.ToDateTime(DateTime.Now);
            note.UpdatedBy = (string)Session["userName"];
            note.UpdatedOn = Convert.ToDateTime(DateTime.Now);
            note.ActiveFlag = true;

            return note;
        }


        protected void btnGetRevenue_Click(object sender, EventArgs e)
        {
            //Get last fiscal month in tblMarginTrend
            DateTime endDate = getMaxDate();

            //Subtract 11 months
            DateTime beginDate = new DateTime();
            beginDate = endDate.AddMonths(-11);

            //Get string of contract numbers in single quotes
            string contractlist = "";
            //foreach (RadComboBoxItem rcbItem in cbxContracts.Items)
            //{
            //    if (rcbItem.Checked == true)
            //    {
            //        if (contractlist != "")
            //            contractlist = contractlist + ",";
            //        contractlist = contractlist + "'" + rcbItem.Text + "'";
            //    }
            //}
            contractlist = getContractList();
            
            if (contractlist != "")
            {
                //Get revenue from tblMarginTrend
                DataTable dt = getRevenue(contractlist, beginDate.ToString(), endDate.ToString());
                DataTable dt2 = getMargin(contractlist, beginDate.ToString(), endDate.ToString());

                String svctype = "";
                string amt = "0";
                //Blank out prior data
                txtcurCourierN.Text = amt;
                txtcurFFN.Text = amt;
                txtcurLTLN.Text = amt;
                txtcurPPSTN.Text = amt;
                txtcurCPCN.Text = amt;
                txtcurCourierN.ForeColor = System.Drawing.Color.ForestGreen;
                txtcurFFN.ForeColor = System.Drawing.Color.ForestGreen;
                txtcurLTLN.ForeColor = System.Drawing.Color.ForestGreen;
                txtcurPPSTN.ForeColor = System.Drawing.Color.ForestGreen;
                txtcurCPCN.ForeColor = System.Drawing.Color.ForestGreen;
                txtcurMarginN.Text = amt;
                txtcurMarginN.ForeColor = System.Drawing.Color.ForestGreen;
                txtcurOtherN.Text = amt;
                txtcurOtherN.ForeColor = System.Drawing.Color.ForestGreen;

                decimal otheramt = 0;
                decimal decimalamt = 0;
                string otherDesc = "";

                //Display revenue
                foreach (DataRow row in dt.Rows)
                {
                    svctype = row["ServiceType"].ToString();
                    amt = row["revenue"].ToString();
                    Decimal.TryParse(amt, out decimalamt);
               

                    switch (svctype)
                    {
                        case "COURIER":
                            txtcurCourierN.Text = amt;
                            break;
                        case "FREIGHT FORWARDING":
                            txtcurFFN.Text = amt;
                            break;
                        case "LTLC":
                            txtcurLTLN.Text = amt;
                            break;
                        case "PUROPOST":
                            txtcurPPSTN.Text = amt;
                            break;
                        case "CPC":
                            txtcurCPCN.Text = amt;
                            break;
                        case "OTHER":
                            otheramt = otheramt + decimalamt;
                             if (otherDesc != "")
                                otherDesc = otherDesc + ", ";
                            otherDesc = otherDesc + "Other Revenue";
                            break;
                        //All other goes into OTHER category                        
                        default:
                            if (otherDesc != "")
                                otherDesc = otherDesc + ", ";
                            otherDesc = otherDesc + svctype;
                            otheramt = otheramt + decimalamt;
                            break;
                    }                    

                }
                txtcurOtherN.Text = otheramt.ToString();
                txtOtherDesc.Text = otherDesc;
                if (otheramt != 0)
                {
                    showHideOther(true);
                }
                //Margin Pct
                DataRow marginrow = dt2.Rows[0];
                amt = marginrow["marginpct"].ToString();
                txtcurMarginN.Text = amt;

                //Recalc Values
                calcExpectedRevenue();
                calcNewProfile();
                calcRevenueTotals();


            }

            

        }

       

        protected void getCurrGrossTargetMargin()
        {
            //Get Parameters
            //Need to figure out qtr and year logic later
            //Get Qtr and Year from Renewal Date
            //Int16 qtr;
            //Int16 year;
            string grMarginPct = "";
            try
            {
                String type = cbxRenewalTypes.SelectedItem.Text;
                Decimal courier = Convert.ToDecimal(txtcurCourierN.Text);
                Decimal ffw = Convert.ToDecimal(txtcurFFN.Text);
                Decimal ltl = Convert.ToDecimal(txtcurLTLN.Text);
                Decimal ppst = Convert.ToDecimal(txtcurPPSTN.Text);
                Decimal cpc = Convert.ToDecimal(txtcurCPCN.Text);
                Decimal other = Convert.ToDecimal(txtcurOtherN.Text);

                DataTable MarginTable = getGrossTargetMargin(type, courier, ffw, ltl, ppst, cpc, other);
                
                foreach (DataRow rowv in MarginTable.Rows)
                {
                    grMarginPct = rowv["TargetMargin"].ToString();
                }
            }
            catch (Exception ex)
            {

            }   
           
            txtcurTargetGrMarginPctN.Text = grMarginPct;
            txtcurTargetGrMarginPctN.ForeColor = System.Drawing.Color.ForestGreen;
        }

        protected void getExpGrossTargetMargin()
        {
            string grMarginPct = "";
            try
            {

                String type = cbxRenewalTypes.SelectedItem.Text;
                Decimal courier = Convert.ToDecimal(txtexpCourierN.Text);
                Decimal ffw = Convert.ToDecimal(txtexpFFN.Text);
                Decimal ltl = Convert.ToDecimal(txtexpLTLN.Text);
                Decimal ppst = Convert.ToDecimal(txtexpPPSTN.Text);
                Decimal cpc = Convert.ToDecimal(txtexpCPCN.Text);
                Decimal other = Convert.ToDecimal(txtcurOtherN.Text);

                DataTable MarginTable = getGrossTargetMargin(type, courier, ffw, ltl, ppst, cpc, other);
                
                foreach (DataRow rowv in MarginTable.Rows)
                {
                    grMarginPct = rowv["TargetMargin"].ToString();

                }

            }
            catch (Exception ex)
            {

            }   

            //txtexpTargetGrMarginPctN.Text = grMarginPct;
        }

        protected void getNewGrossTargetMargin()
        {
            string grMarginPct = "";
            try
            {
                String type = cbxRenewalTypes.SelectedItem.Text;
                Decimal courier = Convert.ToDecimal(txtcurCourierN.Text) + Convert.ToDecimal(txtexpCourierN.Text);
                Decimal ffw = Convert.ToDecimal(txtcurFFN.Text) + Convert.ToDecimal(txtexpFFN.Text);
                Decimal ltl = Convert.ToDecimal(txtcurLTLN.Text) + Convert.ToDecimal(txtexpLTLN.Text);
                Decimal ppst = Convert.ToDecimal(txtcurPPSTN.Text) + Convert.ToDecimal(txtexpPPSTN.Text);
                Decimal cpc = Convert.ToDecimal(txtcurCPCN.Text) + Convert.ToDecimal(txtexpCPCN.Text);
                Decimal other = Convert.ToDecimal(txtcurOtherN.Text);

                DataTable MarginTable = getGrossTargetMargin(type, courier, ffw, ltl, ppst, cpc, other);
                
                foreach (DataRow rowv in MarginTable.Rows)
                {
                    grMarginPct = rowv["TargetMargin"].ToString();

                }
            }
            catch (Exception ex)
            {
                
            }            
            txtnewTargetGrMarginPctN.Text = grMarginPct;
            txtnewTargetGrMarginPctN.ForeColor = System.Drawing.Color.ForestGreen;
        }

        protected string getContractList()
        {
            //Get string of contract numbers in single quotes
            string contractlist = "";
            foreach (RadComboBoxItem rcbItem in cbxContracts.Items)
            {
                if (rcbItem.Checked == true)
                {
                    if (contractlist != "")
                        contractlist = contractlist + ",";
                    contractlist = contractlist + "'" + rcbItem.Text + "'";
                }
            }
            return contractlist;
        }

        protected void btnGetAccessorials_Click(object sender, EventArgs e)
        {
           

            try {

                //Blank out prior data
                string amt = "0";

                txtcurResiFees.Text = amt;
                txtcurshFP.Text = amt;
                txtcurshAH.Text = amt;
                txtcurshLP.Text = amt;
                txtcurshOML.Text = amt;
                txtcurshO.Text = amt;
                txtcurshRAH.Text = amt;
                txtcurDimsGround.Text = amt;
                txtcurDimsAir.Text = amt;
                txtcurdgFR.Text = amt;
                txtcurdgUN3373.Text = amt;
                txtcurdgUN1845.Text = amt;
                txtcurdgLT500kg.Text = amt;
                txtcurdgLQ.Text = amt;

                txtexpResiFees.Text = amt;
                txtexpshFP.Text = amt;
                txtexpshAH.Text = amt;
                txtexpshLP.Text = amt;
                txtexpshOML.Text = amt;
                txtexpshO.Text = amt;
                txtexpshRAH.Text = amt;
                txtexpDimsGround.Text = amt;
                txtexpDimsAir.Text = amt;
                txtexpdgFR.Text = amt;
                txtexpdgUN3373.Text = amt;
                txtexpdgUN1845.Text = amt;
                txtexpdgLT500kg.Text = amt;
                txtexpdgLQ.Text = amt;

                bool isInfoAvail = true;
                //find out if SAP info is the same for all selected contracts

                DataTable dt1 = new DataTable();

                //Get string of contract numbers in single quotes
                string contractlist = getContractList();
                if (contractlist != "")
                {
                    dt1 = getSAPInfo(contractlist);

                    if (dt1.Rows.Count == 0)
                    {
                        isInfoAvail = false;
                    }

                    if (dt1.Rows.Count > 1)
                    {
                        //if more than one row after grouping, then don't display any info
                        //lblNoAccessorialInfo.Text = "Accessorials Differ by Contract";
                        isInfoAvail = false;
                    }

                } else
                {
                    isInfoAvail = false;
                }
              

            if (isInfoAvail == false)
            {
                lblNoAccessorialInfo.Visible = true;
            }
            else
            {
                lblNoAccessorialInfo.Visible = false;

                   

                    txtcurResiFees.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurshFP.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurshAH.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurshLP.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurshOML.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurshO.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurshRAH.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurDimsGround.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurDimsAir.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurdgFR.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurdgUN3373.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurdgUN1845.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurdgLT500kg.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurdgLQ.ForeColor = System.Drawing.Color.ForestGreen;                    
                    txtcurFuelPIS.ForeColor = System.Drawing.Color.ForestGreen;
                    txtcurFuelPuroS.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpFuelPIS.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpFuelPuroS.ForeColor = System.Drawing.Color.ForestGreen;

                    //Display Current Accessorials
                    foreach (DataRow row in dt1.Rows)
                    {
                       //display all the SAP values
                        txtcurResiFees.Text = row["ResiFee"].ToString();
                        txtcurshFP.Text = row["SH05"].ToString();
                        txtcurshAH.Text = row["SH06"].ToString();
                        txtcurshLP.Text = row["SH07"].ToString();
                        txtcurshOML.Text = row["SH08"].ToString();
                        txtcurshO.Text = row["SH09"].ToString();
                        txtcurshRAH.Text = row["SH10"].ToString();
                        txtcurDimsGround.Text = row["SAPGroundDim"].ToString();
                        txtcurDimsAir.Text = row["SAPAirDim"].ToString();
                        txtcurdgFR.Text = row["DG01"].ToString();
                        cbxperc1.SelectedValue = row["DG01type"].ToString().ToLower();
                        txtcurdgUN3373.Text = row["DG02"].ToString();
                        cbxperc2.SelectedValue = row["DG02type"].ToString().ToLower();
                        txtcurdgUN1845.Text = row["DG03"].ToString();
                        cbxperc3.SelectedValue = row["DG03type"].ToString().ToLower();
                        txtcurdgLT500kg.Text = row["DG04"].ToString();
                        cbxperc4.SelectedValue = row["DG04type"].ToString().ToLower();
                        txtcurdgLQ.Text = row["DG05"].ToString();
                        cbxperc5.SelectedValue = row["DG05type"].ToString().ToLower();
                        bool puroIncMirror = Convert.ToBoolean(row["PuroIncMirror"]);
                        if (puroIncMirror == true)
                        {
                            radioCurrFuel.SelectedValue = "fuelpuroinc";
                            txtcurFuelPuroS.Text = row["CourierFSCDisc"].ToString();
                        }                            
                        else
                        {
                            radioCurrFuel.SelectedValue = "fuelpi";
                            txtcurFuelPIS.Text = row["CourierFSCDisc"].ToString();
                        }                         
                        
                        bool beyondOriginDisc = Convert.ToBoolean(row["BeyondOriginDisc"]);
                        if (beyondOriginDisc == true)
                            radioBeyondOrigin.SelectedValue = "yes";
                        else
                            radioBeyondOrigin.SelectedValue = "no";
                        bool beyondDestDisc = Convert.ToBoolean(row["BeyondDestDisc"]);
                        if (beyondDestDisc == true)
                            radioBeyondDest.SelectedValue = "yes";
                        else
                            radioBeyondDest.SelectedValue = "no";
                    }

                    //FILL IN EXPECTED ALSO
                   
                    txtexpResiFees.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpshFP.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpshAH.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpshLP.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpshOML.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpshO.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpshRAH.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpDimsGround.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpDimsAir.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpdgFR.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpdgUN3373.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpdgUN1845.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpdgLT500kg.ForeColor = System.Drawing.Color.ForestGreen;
                    txtexpdgLQ.ForeColor = System.Drawing.Color.ForestGreen;

                                    
                    foreach (DataRow row in dt1.Rows)
                    {
                        //display all the SAP values
                        txtexpResiFees.Text = row["ResiFee"].ToString();
                        txtexpshFP.Text = row["SH05"].ToString();
                        txtexpshAH.Text = row["SH06"].ToString();
                        txtexpshLP.Text = row["SH07"].ToString();
                        txtexpshOML.Text = row["SH08"].ToString();
                        txtexpshO.Text = row["SH09"].ToString();
                        txtexpshRAH.Text = row["SH10"].ToString();
                        txtexpDimsGround.Text = row["SAPGroundDim"].ToString();
                        txtexpDimsAir.Text = row["SAPAirDim"].ToString();
                        txtexpdgFR.Text = row["DG01"].ToString();
                        cbxpere1.SelectedValue = row["DG01type"].ToString().ToLower();
                        txtexpdgUN3373.Text = row["DG02"].ToString();
                        cbxpere2.SelectedValue = row["DG02type"].ToString().ToLower();
                        txtexpdgUN1845.Text = row["DG03"].ToString();
                        cbxpere3.SelectedValue = row["DG03type"].ToString().ToLower();
                        txtexpdgLT500kg.Text = row["DG04"].ToString();
                        cbxpere4.SelectedValue = row["DG04type"].ToString().ToLower();
                        txtexpdgLQ.Text = row["DG05"].ToString();
                        cbxpere5.SelectedValue = row["DG05type"].ToString().ToLower();
                        bool puroIncMirror = Convert.ToBoolean(row["PuroIncMirror"]);
                        if (puroIncMirror == true)
                        {
                            radioExpFuel.SelectedValue = "fuelpuroinc";
                            txtexpFuelPuroS.Text = row["CourierFSCDisc"].ToString();
                        }
                        else
                        {
                            radioExpFuel.SelectedValue = "fuelpi";
                            txtexpFuelPIS.Text = row["CourierFSCDisc"].ToString();
                        }              
                    }
                                
                }
            }

            catch (Exception ex)
            {
                //lblNoAccessorialInfo.Visible = true;
            }
            
        }

        protected DataTable getSAPInfo(string contractlist)
        {                        
            DataTable dt1 = new DataTable();
            try
            {
                

                SqlConnection cnn;
                String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
                cnn = new SqlConnection(strConnString);

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
               
                    cmd = new SqlCommand("sp_GetSAPData", cnn);
                    cmd.CommandTimeout = 10800;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@contractlist", SqlDbType.VarChar).Value = contractlist;
                    da.SelectCommand = cmd;
                    da.Fill(dt1);


            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
            return dt1;
        }

        protected void cbxRelationshipName_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                string relationship = cbxRelationshipName.SelectedValue;
                cbxContracts.Items.Clear();
                getContractsByRelationship(relationship);
                getTerritoryByContract();
                getFirstShipDate();
               

            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
        }

        protected void cbxRenewalTypes_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                checkType();
                //Check if New Contract
                if (cbxRenewalTypes.SelectedValue == "5")
                {
                    lblNewContract.Visible = true;
                    lblNewContract1.Visible = true;
                    txtVDACommitment.Visible = true;
                    btnGetRevenue.Visible = false;
                    lbldaterange.Visible = false;

                    //hide columns
                    showHide(false);
                    tdcurwidth = 1;
                    tdnewwidth = 1;
                    showHideSOW(false);
                    tdsowwidth = 1;
                    showHideROC(false);
                    tdrocwidth = 1;
                    showHideOther(false);
                }
                else
                {
                    lblNewContract.Visible = false;
                    lblNewContract1.Visible = false;
                    txtVDACommitment.Visible = false;
                    btnGetRevenue.Visible = true;
                    lbldaterange.Visible = true;

                    //hide other fields
                    showHide(true);
                    tdcurwidth = 10;
                    tdnewwidth = 10;
                    showHideSOW(false);
                    tdsowwidth = 10;
                    sowFlag.Checked = false;
                    showHideROC(false);
                    tdrocwidth = 10;
                    rocFlag.Checked = false;
                }
                if (Session["userRole"].ToString().ToLower() != "pricing")
                {
                    btnGetRevenue.Visible = false;
                    lblGetRevenue.Visible = false;
                    lbldaterange.Visible = false;

                }


            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
        }

        protected void cbxRouteTo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                sendEmailFlag.Checked = true;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
        }

        protected void showHide(bool visibleFlag)
        {
            lblPctIncreases.Visible = visibleFlag;

            txtpctCourierN.Visible = visibleFlag;
            lblpctIncCourier.Visible = visibleFlag;
            txtpctFFN.Visible = visibleFlag;
            lblPctIncFF.Visible = visibleFlag;
            txtpctLTLN.Visible = visibleFlag;
            lblpctIncLTL.Visible = visibleFlag;
            txtpctPPSTN.Visible = visibleFlag;
            lblpctIncPPST.Visible = visibleFlag;
            txtpctCPCN.Visible = visibleFlag;
            lblPctIncCPC.Visible = visibleFlag;         

            lblCurrent.Visible = visibleFlag;
            lblNewProfile.Visible = visibleFlag;

            txtcurMarginN.Visible = visibleFlag;
            txtnewMarginN.Visible = visibleFlag;
            //txtcurRevenueN.Visible = visibleFlag;
            lblcurRevenue.Visible = visibleFlag;
            lblnewRevenue.Visible = visibleFlag;
            txtcurCourierN.Visible = visibleFlag;
            //txtnewCourierN.Visible = visibleFlag;
            lblnewCourier.Visible = visibleFlag;
            txtcurFFN.Visible = visibleFlag;
            lblnewFF.Visible = visibleFlag;
            txtcurLTLN.Visible = visibleFlag;
            lblnewLTL.Visible = visibleFlag;
            txtcurPPSTN.Visible = visibleFlag;
            lblnewPPST.Visible = visibleFlag;
            txtcurCPCN.Visible = visibleFlag;
            lblnewCPC.Visible = visibleFlag;
           
            if (visibleFlag == true)
            {
                if (AddOther.Visible == false)
                {
                    txtcurOtherN.Visible = true;
                    lblnewOther.Visible = true;
                    txtpctOtherN.Visible = true;
                    lblpctIncOther.Visible = true;
                }
            }
            else
            {
                txtcurOtherN.Visible = visibleFlag;
                lblnewOther.Visible = visibleFlag;
                txtpctOtherN.Visible = visibleFlag;
                lblpctIncOther.Visible = visibleFlag;
            }

            txtcurTargetGrMarginPctN.Visible = visibleFlag;
            txtnewTargetGrMarginPctN.Visible = visibleFlag;

            lblsowFlag.Visible = visibleFlag;
            sowFlag.Visible = visibleFlag;
            lblrocFlag.Visible = visibleFlag;
            rocFlag.Visible = visibleFlag;

        }

        
        protected void checkType()
        {

            int selectedValue = Convert.ToInt16(cbxRenewalTypes.SelectedValue);
            int newtypeid = Convert.ToInt16(ConfigurationManager.AppSettings["NewCustomerTypeID"]);
            if (selectedValue == newtypeid)
            {
                lblExpected.Text = "Expected";
            }
            else
            {
                lblExpected.Text = "Increase";
            }
        }

        protected void sow_check_changed(Object sender, EventArgs e)
        {

            bool sowValue = sowFlag.Checked;
            showHideSOW(sowValue);           

        }

        protected void roc_check_changed(Object sender, EventArgs e)
        {

            bool rocValue = rocFlag.Checked;
            showHideROC(rocValue);
            

        }

        protected void showHideSOW(bool visibleFlag)
        {
            lblSOW.Visible = visibleFlag;
            //txtsowMarginN.Visible = visibleFlag;
            lblsowRevenue.Visible = visibleFlag;
            txtsowCourierN.Visible = visibleFlag;
            txtsowFFN.Visible = visibleFlag;
            txtsowLTLN.Visible = visibleFlag;
            txtsowPPSTN.Visible = visibleFlag;
            txtsowCPCN.Visible = visibleFlag;
            if (visibleFlag == true)
            {
                if (AddOther.Visible == false)
                {
                    txtsowOtherN.Visible = true;
                }               
            }
            else
            {
                txtsowOtherN.Visible = visibleFlag;
            }
           
            //txtsowTargetGrMarginPctN.Visible = visibleFlag; 

        }

        protected void showHideROC(bool visibleFlag)
        {
            lblROC.Visible = visibleFlag;
            //txtrocMarginN.Visible = visibleFlag;
            lblrocRevenue.Visible = visibleFlag;
            txtrocCourierN.Visible = visibleFlag;
            txtrocFFN.Visible = visibleFlag;
            txtrocLTLN.Visible = visibleFlag;
            txtrocPPSTN.Visible = visibleFlag;
            txtrocCPCN.Visible = visibleFlag;
            if (visibleFlag == true)
            {
                if (AddOther.Visible == false)
                {
                    txtrocOtherN.Visible = true;
                }                
            }
            else
            {
                txtrocOtherN.Visible = visibleFlag;
            }
            //txtrocTargetGrMarginPctN.Visible = visibleFlag;         
        }

        protected void showHideOther(bool visibleFlag)
        {

            lblAnOther.Visible = visibleFlag;
            txtexpOtherN.Visible = visibleFlag;
           
            if (cbxRenewalTypes.SelectedValue != "5")
            {
                txtcurOtherN.Visible = visibleFlag;
                lblnewOther.Visible = visibleFlag;
                lblpctIncOther.Visible = visibleFlag;
                txtpctOtherN.Visible = visibleFlag;
                if (sowFlag.Checked == visibleFlag)
                {
                    txtsowOtherN.Visible = visibleFlag;
                }
                else
                {
                    txtsowOtherN.Visible = !visibleFlag;
                }

                if (rocFlag.Checked == visibleFlag)
                {
                    txtrocOtherN.Visible = visibleFlag;
                }
                else
                {
                    txtrocOtherN.Visible = !visibleFlag;
                }               
            }           
            
            lblOtherDesc.Visible = visibleFlag;
            txtOtherDesc.Visible = visibleFlag;
            AddOther.Visible = !visibleFlag;

        }


        protected void date1_Changed(object sender, System.EventArgs e)
        {
            DateTime efdate = Convert.ToDateTime(rdpEffectiveDate.SelectedDate);
            DateTime newdate = efdate.AddYears(1);
            newdate = newdate.AddDays(-1);
            rdpExpiryDate.SelectedDate = newdate;
        }

        protected void cbxContracts_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                getTerritoryByContract();


            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
        }

        protected void getContractsByRelationship(string relationship)
        {
            try
            {
                cbxContracts.Items.Clear();
                List<ClsContractRelationship> lstContracts;
                ClsContractRelationship cr = new ClsContractRelationship();
                lstContracts = cr.GetContractsByRelationship(relationship);
                cbxContracts.DataSource = lstContracts;

                cbxContracts.DataTextField = "ContractNumber";
                cbxContracts.DataValueField = "ContractNumber";
                cbxContracts.DataBind();

                foreach (RadComboBoxItem rcbItem in cbxContracts.Items)
                    rcbItem.Checked = true;



            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
        }

        protected void getFirstShipDate()
        {
            try
            {
                lblFSD.Text = "unavailable";
                string relationship = cbxRelationshipName.Text;
                ClsContractRelationship cr = new ClsContractRelationship();
                DateTime? fsd = cr.GetFirstShipDate(relationship);
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
        }

        //protected decimal getCurrRevTotal()
        //{
        //    decimal curTotal = 0;
        //    if (txtcurCourierN.Text != "")
        //        curTotal = curTotal + Convert.ToDecimal(txtcurCourierN.Text);
        //    if (txtcurFFN.Text != "")
        //        curTotal = curTotal + Convert.ToDecimal(txtcurFFN.Text);
        //    if (txtcurLTLN.Text != "")
        //        curTotal = curTotal + Convert.ToDecimal(txtcurLTLN.Text);
        //    if (txtcurPPSTN.Text != "")
        //        curTotal = curTotal + Convert.ToDecimal(txtcurPPSTN.Text);
        //    if (txtcurCPCN.Text != "")
        //        curTotal = curTotal + Convert.ToDecimal(txtcurCPCN.Text);
        //    if (txtcurOtherN.Text != "")
        //        curTotal = curTotal + Convert.ToDecimal(txtcurOtherN.Text);
        //    return curTotal;

        //}

        //protected decimal getNewRevTotal()
        //{
        //    decimal newTotal = 0;
        //    if (txtnewCourierN.Text != "")
        //        expTotal = expTotal + Convert.ToDecimal(txtexpCourierN.Text);
        //    if (txtexpFFN.Text != "")
        //        expTotal = expTotal + Convert.ToDecimal(txtexpFFN.Text);
        //    if (txtexpLTLN.Text != "")
        //        expTotal = expTotal + Convert.ToDecimal(txtexpLTLN.Text);
        //    if (txtexpPPSTN.Text != "")
        //        expTotal = expTotal + Convert.ToDecimal(txtexpPPSTN.Text);
        //    if (txtexpCPCN.Text != "")
        //        expTotal = expTotal + Convert.ToDecimal(txtexpCPCN.Text);
        //    if (txtexpOtherN.Text != "")
        //        expTotal = expTotal + Convert.ToDecimal(txtexpOtherN.Text);
        //    return newTotal;

        //}

        protected void calcRevenueTotals()
        {
            try
            {
                decimal curTotal = 0;
                if (txtcurCourierN.Text != "")
                    curTotal = curTotal + Convert.ToDecimal(txtcurCourierN.Text);
                if (txtcurFFN.Text != "")
                    curTotal = curTotal + Convert.ToDecimal(txtcurFFN.Text);
                if (txtcurLTLN.Text != "")
                    curTotal = curTotal + Convert.ToDecimal(txtcurLTLN.Text);
                if (txtcurPPSTN.Text != "")
                    curTotal = curTotal + Convert.ToDecimal(txtcurPPSTN.Text);
                if (txtcurCPCN.Text != "")
                    curTotal = curTotal + Convert.ToDecimal(txtcurCPCN.Text);
                if (txtcurOtherN.Text != "")
                    curTotal = curTotal + Convert.ToDecimal(txtcurOtherN.Text);              
                lblcurRevenue.Text = curTotal.ToString("$###,###,##0");


                decimal sowTotal = 0;
                if (txtsowCourierN.Text != "")
                  sowTotal = sowTotal + Convert.ToDecimal(txtsowCourierN.Text);
                if (txtsowFFN.Text != "")
                  sowTotal = sowTotal + Convert.ToDecimal(txtsowFFN.Text);
                if (txtsowLTLN.Text != "")
                  sowTotal = sowTotal + Convert.ToDecimal(txtsowLTLN.Text);
                if (txtsowPPSTN.Text != "")
                  sowTotal = sowTotal + Convert.ToDecimal(txtsowPPSTN.Text);
                if (txtsowCPCN.Text != "")
                  sowTotal = sowTotal + Convert.ToDecimal(txtsowCPCN.Text);
                if (txtsowOtherN.Text != "")
                  sowTotal = sowTotal + Convert.ToDecimal(txtsowOtherN.Text);
                if (sowFlag.Checked == true)
                   lblsowRevenue.Text = sowTotal.ToString("$###,###,##0");

                decimal rocTotal = 0;
                if (txtrocCourierN.Text != "")
                  rocTotal = rocTotal + Convert.ToDecimal(txtrocCourierN.Text);
                if (txtrocFFN.Text != "")
                  rocTotal = rocTotal + Convert.ToDecimal(txtrocFFN.Text);
                if (txtrocLTLN.Text != "")
                  rocTotal = rocTotal + Convert.ToDecimal(txtrocLTLN.Text);
                if (txtrocPPSTN.Text != "")
                  rocTotal = rocTotal + Convert.ToDecimal(txtrocPPSTN.Text);
                if (txtrocCPCN.Text != "")
                  rocTotal = rocTotal + Convert.ToDecimal(txtrocCPCN.Text);
                if (txtrocOtherN.Text != "")
                  rocTotal = rocTotal + Convert.ToDecimal(txtrocOtherN.Text);
                if (rocFlag.Checked == true)
                   lblrocRevenue.Text = rocTotal.ToString("$###,###,##0");

                decimal expTotal = 0;
                if (txtexpCourierN.Text != "")
                    expTotal = expTotal + Convert.ToDecimal(txtexpCourierN.Text);
                if (txtexpFFN.Text != "")
                    expTotal = expTotal + Convert.ToDecimal(txtexpFFN.Text);
                if (txtexpLTLN.Text != "")
                    expTotal = expTotal + Convert.ToDecimal(txtexpLTLN.Text);
                if (txtexpPPSTN.Text != "")
                    expTotal = expTotal + Convert.ToDecimal(txtexpPPSTN.Text);
                if (txtexpCPCN.Text != "")
                    expTotal = expTotal + Convert.ToDecimal(txtexpCPCN.Text);
                if (txtexpOtherN.Text != "")
                    expTotal = expTotal + Convert.ToDecimal(txtexpOtherN.Text);
                lblexpRevenue.Text = expTotal.ToString("$###,###,##0");

                decimal newTotal = 0;
                decimal newvalue = 0;
                if (lblnewCourier.Text != "")
                {
                    newvalue = getlblDecimalValue(lblnewCourier.Text);
                    newTotal = newTotal + Convert.ToDecimal(newvalue);
                }                    
                if (lblnewFF.Text != "")
                {
                    newvalue = getlblDecimalValue(lblnewFF.Text);
                    newTotal = newTotal + Convert.ToDecimal(newvalue);
                }                 
                if (lblnewLTL.Text != "")
                {
                    newvalue = getlblDecimalValue(lblnewLTL.Text);
                    newTotal = newTotal + Convert.ToDecimal(newvalue);
                }                  
                if (lblnewPPST.Text != "")
                {
                    newvalue = getlblDecimalValue(lblnewPPST.Text);
                    newTotal = newTotal + Convert.ToDecimal(newvalue);
                }                  
                if (lblnewCPC.Text != "")
                {
                    newvalue = getlblDecimalValue(lblnewCPC.Text);
                    newTotal = newTotal + Convert.ToDecimal(newvalue);
                }                  
                if (lblnewOther.Text != "")
                {
                    newvalue = getlblDecimalValue(lblnewOther.Text);
                    newTotal = newTotal + Convert.ToDecimal(newvalue);
                }            
                lblnewRevenue.Text = newTotal.ToString("$###,###,##0");

                //New Margin Pct
                try
                {
                    Double CurrRevenue = Convert.ToDouble(curTotal);
                    Double CurrMarginPct = Convert.ToDouble(txtcurMarginN.Text) / 100;
                    Double CurrExpense = (CurrRevenue * CurrMarginPct) - CurrRevenue;
                    Double NewRevenue = Convert.ToDouble(newTotal);
                    Double NewExpense = CurrExpense * 1.02;
                    Double newMarginPct = (NewExpense + NewRevenue) / NewRevenue * 100;
                    //string newMarginDisplay = newMarginPct.ToString("###.#") + " %";
                    txtnewMarginN.Text = newMarginPct.ToString("###.0");
                    txtnewMarginN.ForeColor = System.Drawing.Color.ForestGreen;
                }
                catch (Exception ex)
                {

                }   

                //Target Gross Margin %
                getCurrGrossTargetMargin();
                getExpGrossTargetMargin();
                getNewGrossTargetMargin();


            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
        }

        private decimal getlblDecimalValue(String labelText)
        {
            decimal returnval = 0;

            string txtNumericValue = labelText;
            if (txtNumericValue != "")
            {
                txtNumericValue = txtNumericValue.Replace("$", "");
                txtNumericValue = txtNumericValue.Replace(",", "");
                if (txtNumericValue == "")
                    txtNumericValue="0";
                returnval = Convert.ToDecimal(txtNumericValue);
            }


            return returnval;
        }

        protected void calcNewProfile()
        {
            try
            {
                decimal newCourier = 0;
                if (txtcurCourierN.Text != "")
                  newCourier = newCourier + Convert.ToDecimal(txtcurCourierN.Text);
                if (txtexpCourierN.Text != "")
                   newCourier = newCourier + Convert.ToDecimal(txtexpCourierN.Text);
                if (txtsowCourierN.Text != "")
                   newCourier = newCourier + Convert.ToDecimal(txtsowCourierN.Text);
                if (txtrocCourierN.Text != "")
                   newCourier = newCourier + Convert.ToDecimal(txtrocCourierN.Text);
                lblnewCourier.Text = newCourier.ToString("$###,###,##0");

                decimal newFF = 0;
                if (txtcurFFN.Text != "")
                  newFF = newFF + Convert.ToDecimal(txtcurFFN.Text);
                if (txtexpFFN.Text != "")
                  newFF = newFF + Convert.ToDecimal(txtexpFFN.Text);
                if (txtsowFFN.Text != "")
                  newFF = newFF + Convert.ToDecimal(txtsowFFN.Text);
                if (txtrocFFN.Text != "")
                  newFF = newFF + Convert.ToDecimal(txtrocFFN.Text);
                lblnewFF.Text = newFF.ToString("$###,###,##0");
              

                decimal newLTL = 0;
                if (txtcurLTLN.Text != "")
                  newLTL = newLTL + Convert.ToDecimal(txtcurLTLN.Text);
                if (txtexpLTLN.Text != "")
                  newLTL = newLTL + Convert.ToDecimal(txtexpLTLN.Text);
                if (txtsowLTLN.Text != "")
                  newLTL = newLTL + Convert.ToDecimal(txtsowLTLN.Text);
                if (txtrocLTLN.Text != "")
                  newLTL = newLTL + Convert.ToDecimal(txtrocLTLN.Text);
                lblnewLTL.Text = newLTL.ToString("$###,###,##0");

                decimal newPPST = 0;
                if (txtcurPPSTN.Text != "")
                  newPPST = newPPST + Convert.ToDecimal(txtcurPPSTN.Text);
                if (txtexpPPSTN.Text != "")
                   newPPST = newPPST + Convert.ToDecimal(txtexpPPSTN.Text);
                if (txtsowPPSTN.Text != "")
                  newPPST = newPPST + Convert.ToDecimal(txtsowPPSTN.Text);
                if (txtrocPPSTN.Text != "")
                  newPPST = newPPST + Convert.ToDecimal(txtrocPPSTN.Text);
                lblnewPPST.Text = newPPST.ToString("$###,###,##0");

                decimal newCPC = 0;
                if (txtcurCPCN.Text != "")
                  newCPC = newCPC + Convert.ToDecimal(txtcurCPCN.Text);
                if (txtexpCPCN.Text != "")
                  newCPC = newCPC + Convert.ToDecimal(txtexpCPCN.Text);
                if (txtsowCPCN.Text != "")
                  newCPC = newCPC + Convert.ToDecimal(txtsowCPCN.Text);
                if (txtrocCPCN.Text != "")
                  newCPC = newCPC + Convert.ToDecimal(txtrocCPCN.Text);
                lblnewCPC.Text = newCPC.ToString("$###,###,##0");

                decimal newOther = 0;
                if (txtcurOtherN.Text != "")
                    newOther = newOther + Convert.ToDecimal(txtcurOtherN.Text);
                if (txtexpOtherN.Text != "")
                    newOther = newOther + Convert.ToDecimal(txtexpOtherN.Text);
                if (txtsowOtherN.Text != "")
                    newOther = newOther + Convert.ToDecimal(txtsowOtherN.Text);
                if (txtrocOtherN.Text != "")
                    newOther = newOther + Convert.ToDecimal(txtrocOtherN.Text);
                lblnewOther.Text = newOther.ToString("$###,###,##0");
                

            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
        }

        protected void calcExpectedRevenue()
        {
            try
            {
                if (txtpctCourierN.Text != "" && txtcurCourierN.Text != "")
                {
                    decimal curCourier = Convert.ToDecimal(txtcurCourierN.Text);
                    decimal pctCourier = Convert.ToDecimal(txtpctCourierN.Text);
                    decimal expCourier = curCourier * pctCourier / 100;
                    txtexpCourierN.Text = expCourier.ToString();
                    txtexpCourierN.ForeColor = System.Drawing.Color.ForestGreen;
                }                

                if (txtcurFFN.Text != "" && txtpctFFN.Text != "")
                {
                    decimal curFF = Convert.ToDecimal(txtcurFFN.Text);
                    decimal pctFF = Convert.ToDecimal(txtpctFFN.Text);
                    decimal expFF = curFF * pctFF / 100;
                    txtexpFFN.Text = expFF.ToString();
                    txtexpFFN.ForeColor = System.Drawing.Color.ForestGreen;
                }
                
                if (txtcurLTLN.Text != "" && txtpctLTLN.Text != "")
                {
                    decimal curLTL = Convert.ToDecimal(txtcurLTLN.Text);
                    decimal pctLTL = Convert.ToDecimal(txtpctLTLN.Text);
                    decimal expLTL = curLTL * pctLTL / 100;
                    txtexpLTLN.Text = expLTL.ToString();
                    txtexpLTLN.ForeColor = System.Drawing.Color.ForestGreen;
                }                

                if (txtcurPPSTN.Text != "" && txtpctPPSTN.Text != "")
                {
                    decimal curPPST = Convert.ToDecimal(txtcurPPSTN.Text);
                    decimal pctPPST = Convert.ToDecimal(txtpctPPSTN.Text);
                    decimal expPPST = curPPST * pctPPST / 100;
                    txtexpPPSTN.Text = expPPST.ToString();
                    txtexpPPSTN.ForeColor = System.Drawing.Color.ForestGreen;
                }
               
                if (txtcurCPCN.Text != "" && txtpctCPCN.Text != "")
                {
                    decimal curCPC = Convert.ToDecimal(txtcurCPCN.Text);
                    decimal pctCPC = Convert.ToDecimal(txtpctCPCN.Text);
                    decimal expCPC = curCPC * pctCPC / 100;
                    txtexpCPCN.Text = expCPC.ToString();
                    txtexpCPCN.ForeColor = System.Drawing.Color.ForestGreen;
                }

                if (txtcurOtherN.Text != "" && txtpctOtherN.Text != "")
                {
                    decimal curOther = Convert.ToDecimal(txtcurOtherN.Text);
                    decimal pctOther = Convert.ToDecimal(txtpctOtherN.Text);
                    decimal expOther = curOther * pctOther / 100;
                    txtexpOtherN.Text = expOther.ToString();
                    txtexpOtherN.ForeColor = System.Drawing.Color.ForestGreen;
                }        
               
               
            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
        }

        
        protected void getTerritoryByContract()
        {
            try
            {
                string relationship = cbxRelationshipName.Text;
                List<ClsContractRelationship> lstContracts;
                ClsContractRelationship cr = new ClsContractRelationship();
                lstContracts = cr.GetContractsByRelationship(relationship);
                List<string> selectedContracts = new List<string>();


                foreach (RadComboBoxItem rcbItem in cbxContracts.Items)
                {
                    if (rcbItem.Checked == true)
                        selectedContracts.Add(rcbItem.Value);
                }


                //Get SRID and Branch
                string srids = "";
                List<string> sridList = new List<string>();
                string branches = "";
                List<string> branchList = new List<string>();
                bool sridfound;
                bool branchfound;
                foreach (ClsContractRelationship critem in lstContracts)
                {
                    string checkedvalue = selectedContracts.FirstOrDefault(x => x == critem.ContractNumber);
                    if (checkedvalue != null)
                    {

                        //Check SRID
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
                        lblSRID.Text = srids;
                        lblSRID.ForeColor = System.Drawing.Color.Blue;
                        lblBranch.Text = branches;
                        lblBranch.ForeColor = System.Drawing.Color.Blue;

                        //Get Sls Name, if there is a single srid
                        if (srids.IndexOf(',') == -1)
                        {
                            ClsSalesReps rep = ClsSalesReps.GetSalesReps(srids);
                            lblslsName.Text = rep.SalesRep;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
        }

       
        protected void rgNotesGrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {

            GridDataItem item = (GridDataItem)e.Item;
            string noteID = item.OwnerTableView.DataKeyValues[item.ItemIndex]["idContractRenewalNotes"].ToString();
            Int16 idNote = Convert.ToInt16(noteID);
            string thisuser = Session["userName"].ToString();
            ClsRenewalNotes thisnote = ClsRenewalNotes.GetNote(idNote);
            if (thisnote.CreatedBy == thisuser)
            {
                //do the delete
                if (idNote != 0)
                {
                    ClsRenewalNotes.DeactivateNote(idNote, thisuser);
                }

                lblErrorMessage.Text = "";
                getNotes();
                rgNotesGrid.DataBind();
                
            }
            else
            {
                //do not allow
                lblErrorMessage.Text = "Cannot Delete Notes That You Did Not Originally Enter";
                getNotes();
            }
            

        }

        protected void rgNotesGrid_UpdateCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void rgNotesGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //GridDataItem item = (GridDataItem)e.Item;
            //string ContractRenewalID = item.OwnerTableView.DataKeyValues[item.ItemIndex]["idContractRenewal"].ToString();

            //Int16 idContractRewewal = Convert.ToInt16(ContractRenewalID);
            //string contractID = ContractRenewalID.Value;
            //int idContractRenewalID = Convert.ToInt16(contractID);
            //List<ClsRenewalNotes> notesList = ClsRenewalNotes.GetContractRenewalNotes(idContractRenewalID);
            //rgNotesGrid.DataSource = notesList;
        }

        protected void rgNotesGrid_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            getNotes();
        }


        protected void rgNotesGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridPagerItem)
                {
                    GridPagerItem pager = (GridPagerItem)e.Item;
                    Label lbl = (Label)pager.FindControl("ChangePageSizeLabel");
                    lbl.Visible = false;

                    RadComboBox combo = (RadComboBox)pager.FindControl("PageSizeComboBox");
                    combo.Visible = false;
                }
                //Disable delete button for notes entered by other users
                string thisuser = Session["userName"].ToString();
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    if (item != null)
                    {
                        // Disable delete   
                        TableCell cell = ((GridDataItem)e.Item)["DeleteLink"];
                        ImageButton deleteButton = (ImageButton)cell.Controls[0];
                        string userval = ((GridDataItem)e.Item)["CreatedBy"].Text;
                        if (userval != thisuser)
                        {
                            deleteButton.Visible = false;
                            deleteButton.Enabled = false;
                            cell.Text = "&nbsp;";
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
           


        }
        

        
        protected void getNotes()
        {
            string contractID = ContractRenewalID.Value;
            int idContractRenewalID = Convert.ToInt16(contractID);
            List<ClsRenewalNotes> notesList = ClsRenewalNotes.GetContractRenewalNotes(idContractRenewalID);
            rgNotesGrid.DataSource = notesList;
        }


        //FIle Uploads
        protected void rgUpload_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem fileitem = (GridDataItem)e.Item;
                    HyperLink fhLink = (HyperLink)fileitem["FilePath"].Controls[0];
                    fhLink.ForeColor = System.Drawing.Color.Blue;
                    ClsFileUpload row = (ClsFileUpload)fileitem.DataItem;
                    fhLink.Attributes["onclick"] = "OpenFile('" + row.FilePath + "');";

                }

            }
            catch (Exception ex)
            {
                //lblWarning.Text = ex.Message;
                //lblWarning.Visible = true;
                //pnlwarning.Visible = true;
            }
        }

        //protected void rgUpload_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    getUploads();

        //}

        protected void rgUpload_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            getUploads();
        }

        protected void getUploads()
        {
            string contractID = ContractRenewalID.Value;
            int idContractRenewalID = Convert.ToInt16(contractID);
            ClsFileUpload fup = new ClsFileUpload();
            List<ClsFileUpload> alluploads = fup.GetFileList(idContractRenewalID);
            rgUpload.DataSource = alluploads;
            //rgUpload.Visible = true;
        }

        protected void rgUpload_DeleteCommand(object sender, GridCommandEventArgs e)
        {

            GridDataItem item = (GridDataItem)e.Item;
            string fileID = item.OwnerTableView.DataKeyValues[item.ItemIndex]["idContractRenewalUpload"].ToString();
            Int16 idFile = Convert.ToInt16(fileID);
            ClsFileUpload fup = new ClsFileUpload();
            fup.deActivateFileUpload(idFile);
            getUploads();
        }

        protected void rgUpload_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                //int rownum = e.Item.ItemIndex;
                //ClsFileUpload fileup = new ClsFileUpload();
                //GridEditFormItem item = (GridEditFormItem)e.Item;
                //TableCell cell = item["idContractRenewalUpload"];
                //string strFileUploadId = cell.Text;
                //int fileUploadID = Convert.ToInt32(strFileUploadId);
                //ClsFileUpload objFileUpload = fileup.GetFileUpload(fileUploadID);

                ////Make changes and submit
                //UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

                //RadTextBox updatedDesc = (RadTextBox)userControl.FindControl("txtDescription");
                //objFileUpload.Description = txtFileDescription.Text;


                //objFileUpload.UpdatedBy = (string)Session["userName"];
                //objFileUpload.UpdatedOn = Convert.ToDateTime(DateTime.Now);
                ////objFileUpload.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;
                // //objFileUpload.ActiveFlag = 

                //String msg = fileup.UpdateFileUpload(objFileUpload);
                //rgUpload.Rebind();

            }
            catch (Exception ex)
            {
                //pnlDanger.Visible = true;
                //lblDanger.Text = ex.Message.ToString();
                e.Canceled = true;
            }
        }

        protected void rgUpload_InsertCommand(object sender, GridCommandEventArgs e)
        {
        }



        protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {

            try
            {
                //btnSaveUpload.Enabled = true;
                if (doFileUploadFlag == true)
                        doFileSave();
            }

            catch (Exception ex)
            {

            }

        }

        protected void btnSaveUpload_Click(object sender, EventArgs e)
        {
            try
            {
                //doFileSave();
                //string errmsg;
                //int fileID;
                //string FilePath = ConfigurationManager.AppSettings["FileUploadPath"].ToString();
                //string contractID = ContractRenewalID.Value;
                //int idContractRenewalID = Convert.ToInt16(contractID);

                //foreach (UploadedFile f in RadAsyncUpload2.UploadedFiles)
                //{
                //    string fileName = f.GetName();
                //    //string title = f.GetFieldValue("txtDescription");
                //    ClsFileUpload filedata = new ClsFileUpload();
                //    filedata.idContractRenewal = idContractRenewalID;
                //    filedata.FilePath = FilePath + fileName;
                //    filedata.Description = txtFileDescription.Text;
                //    filedata.ActiveFlag = true;
                //    filedata.CreatedBy = (string)(Session["userName"]);
                //    filedata.CreatedOn = Convert.ToDateTime(DateTime.Now);
                //    filedata.UploadDate = Convert.ToDateTime(DateTime.Now);

                //    errmsg = filedata.InsertFileUpload(filedata, out fileID);
                //    if (errmsg != "")
                //    {
                //        //pnlDanger.Visible = true;
                //        //lblDanger.Text = errmsg;
                //    }

                //    getUploads();
                //    rgUpload.Rebind();
                //    btnSaveUpload.Enabled = false;
                //}


            }
            catch (Exception ex)
            {
                //pnlDanger.Visible = true;
                //lblDanger.Text = ex.Message.ToString();
            }
        }

        protected void doFileSave()
        {
            try
            {
                string errmsg;
                int fileID;
                string FilePath = ConfigurationManager.AppSettings["FileUploadPath"].ToString();
                string contractID = ContractRenewalID.Value;
                int idContractRenewalID = Convert.ToInt16(contractID);

                if (idContractRenewalID > 0)
                {
                    foreach (UploadedFile f in RadAsyncUpload2.UploadedFiles)
                    {
                        string fileName = f.GetName();
                        //string title = f.GetFieldValue("txtDescription");
                        ClsFileUpload filedata = new ClsFileUpload();
                        filedata.idContractRenewal = idContractRenewalID;
                        filedata.FilePath = FilePath + fileName;
                        filedata.Description = txtFileDescription.Text;
                        filedata.ActiveFlag = true;
                        filedata.CreatedBy = (string)(Session["userName"]);
                        filedata.CreatedOn = Convert.ToDateTime(DateTime.Now);
                        filedata.UploadDate = Convert.ToDateTime(DateTime.Now);

                        errmsg = filedata.InsertFileUpload(filedata, out fileID);
                        if (errmsg != "")
                        {
                            //pnlDanger.Visible = true;
                            //lblDanger.Text = errmsg;
                        }

                        getUploads();
                        rgUpload.Rebind();
                        doFileUploadFlag = false;
                        //btnSaveUpload.Enabled = false;
                    }
                }
               


            }
            catch (Exception ex)
            {
                //pnlDanger.Visible = true;
                //lblDanger.Text = ex.Message.ToString();
            }
        }

        //protected void txtFileDescription_TextChanged(object sender, EventArgs e)
        //{
        //    btnSaveUpload.Enabled = true;
        //}
    
    }
}