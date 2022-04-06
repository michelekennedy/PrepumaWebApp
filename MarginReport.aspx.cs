using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


namespace PrepumaWebApp
{
    public partial class MarginReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userName"] != null && Session["appName"] != null)
                {

                    loadMarginDates();
                    if ((string)Session["userRole"] != "Admin" && (string)Session["userRole"] != "ITAdmin")
                    {
                        btnMarginUpdate.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
          

        }
        protected void loadMarginDates()
        {
            try
            {
                
                SqlConnection cnn;
                String strConnString = ConfigurationManager.ConnectionStrings["PrepumaSQLConnectionString"].ConnectionString;
                cnn = new SqlConnection(strConnString);

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                try
                {
                    cmd = new SqlCommand("sp_GetFiscalMonths", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                }
                catch (Exception ex)
                {
                    string errMsg = ex.Message.ToString();
                }
                finally
                {
                    cnn.Close();
                }
                //fill in drop down
                DateTime date = DateTime.Now;
                DateTime CurrentMonth = new DateTime(date.Year, date.Month, 1);
                RadDropDownDate.DataSource = dt;
                String selectDate = CurrentMonth.ToString("yyyy-MM-dd");
                RadDropDownDate.SelectedValue = selectDate;
                RadDropDownDate.DataBind();
               

            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
           
        }
        protected void btnMarginUpdate_Click(object sender, EventArgs e)
        {
            windowManager.RadConfirm("Do you want to update the Margin Report?", "ConfirmCallbackFn", 300, 150, null, "Please Confirm");
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fiscalMonth = Convert.ToDateTime(RadDropDownDate.SelectedValue);
                SqlConnection cnn;
                String strConnString = ConfigurationManager.ConnectionStrings["PrepumaSQLConnectionString"].ConnectionString;
                cnn = new SqlConnection(strConnString);

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                try
                {
                    cmd = new SqlCommand("sp_MarginUpdate", cnn);
                    cmd.Parameters.Add(new SqlParameter("@fiscalmonth", fiscalMonth));
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                }
                catch (Exception ex)
                {
                    string errMsg = ex.Message.ToString();
                }
                finally
                {
                    cnn.Close();
                }
                //update grid with Submitted Date filled in
                rgMargin.DataSource= dt;
                rgMargin.DataBind();
                
                showtheRightButtons(fiscalMonth);
               
            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
        }
        protected void btnMarginReport_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fiscalMonth = Convert.ToDateTime(RadDropDownDate.SelectedValue);


                ViewReportOnly(fiscalMonth, true);

            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }

        }

       
        protected void btnMarginPrepare_Click(object sender, EventArgs e)
        {
            DateTime fiscalMonth = Convert.ToDateTime(RadDropDownDate.SelectedValue);
            bool isYTD = false;
            isYTD = ckYTD.Checked;
            


            SqlConnection cnn;
            String strConnString = ConfigurationManager.ConnectionStrings["PrepumaSQLConnectionString"].ConnectionString;
            cnn = new SqlConnection(strConnString);

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("sp_getMarginReport", cnn);
                cmd.Parameters.Add(new SqlParameter("@fiscalmonth", fiscalMonth));
                cmd.Parameters.Add(new SqlParameter("@YTD", (ckYTD.Checked ? "1" : "0")));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                rgMargin.DataSource = dt;
                rgMargin.DataBind();
                

            }
            catch (Exception ex)
            {
                string errMsg = ex.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }

            

        }
             

         

        protected void rgMargin_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            string selecteddate = RadDropDownDate.SelectedValue;
            if (selecteddate == "")
            {
                selecteddate = DateTime.Now.ToString();
            }
            DateTime fiscalMonth = Convert.ToDateTime(selecteddate);

            ViewReportOnly(fiscalMonth);
            showtheRightButtons(fiscalMonth);
        }


        protected void ViewReportOnly(DateTime fiscalMonth, bool isBind = false)
        {
            SqlConnection cnn;
            String strConnString = ConfigurationManager.ConnectionStrings["PrepumaSQLConnectionString"].ConnectionString;
            cnn = new SqlConnection(strConnString);

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
             bool isYTD = false;
            isYTD = ckYTD.Checked;
            try
            {
                cmd = new SqlCommand("sp_getMarginReport", cnn);
                cmd.Parameters.Add(new SqlParameter("@fiscalmonth", fiscalMonth));
                cmd.Parameters.Add(new SqlParameter("@YTD", (ckYTD.Checked ? "1" : "0")));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                rgMargin.DataSource = dt;
                if (isBind == true)
                {
                    rgMargin.DataBind();
                }
               
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }

            
           
        }

        protected void RadDropDownDate_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
           
            string selecteddate = RadDropDownDate.SelectedValue;
            DateTime fiscalMonth = Convert.ToDateTime(selecteddate);
            showtheRightButtons(fiscalMonth);            
           
        }

        protected void showtheRightButtons(DateTime fiscalMonth)
        {
            try
            {
                bool finalized = false;
                finalized = isReportFinalized(fiscalMonth);
                if (finalized == true)
                {
                    
                    btnMarginPrepare.Visible = false;
                    btnMarginReport.Visible = true;
                    btnMarginUpdate.Visible = false;
                    lblFinalized.Text = "Margin Report Has Been Finalized for " + fiscalMonth.ToString("MM-dd-yyyy"); ;

                }
                else
                {
                    btnMarginPrepare.Visible = true;
                    btnMarginReport.Visible = false;
                    btnMarginUpdate.Visible = true;
                    lblFinalized.Text = "";
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected bool isReportFinalized(DateTime fiscalMonth)
        {
            //Detemine if this Margin Report has been finalized
            bool finalized = false;
            SqlConnection cnn;
            String strConnString = ConfigurationManager.ConnectionStrings["PrepumaSQLConnectionString"].ConnectionString;
            cnn = new SqlConnection(strConnString);

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("sp_MarginSubmittedRows", cnn);
                cmd.Parameters.Add(new SqlParameter("@fiscalmonth", fiscalMonth));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    finalized = true;
                }
                

            }
            catch (Exception ex)
            {
                string errMsg = ex.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }
            return finalized;
        }

        protected void rgMargin_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgMargin.ExportSettings.FileName = "MarginReport";
                rgMargin.ExportSettings.IgnorePaging = true;
                rgMargin.ExportSettings.ExportOnlyData = true;
                rgMargin.ExportSettings.OpenInNewWindow = true;
                rgMargin.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                //rgMargin.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;
            }

          
        }
    }
}