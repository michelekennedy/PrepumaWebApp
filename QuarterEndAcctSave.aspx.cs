using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;
using System.Configuration;
using System.Globalization;
using PI_Application;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrepumaWebApp
{
    public partial class QuarterEndAcctSave : System.Web.UI.Page
    {
        String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userName"] != null && Session["appName"] != null)
                {                 
                    getYears();                   
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
             

        private void getYears()
        {
            Int32 curryear = DateTime.Now.Year;
            Int32 lastyear = DateTime.Now.Year - 1;

            cbxYear.Items.Insert(0, lastyear.ToString());
            cbxYear.Items.Insert(0, curryear.ToString());
            cbxYear.SelectedValue = curryear.ToString();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                int qtr = Convert.ToInt16(cbxQtr.SelectedItem.Value);
                int yr = Convert.ToInt16(cbxYear.Text);
                bool noPrior = CheckPrior(qtr,yr);
                if (noPrior == true)
                {
                    DoSave(qtr, yr);
                    string msg = "Quarter End Account Alignment Save Completed";
                    lblSuccess.Text = msg;
                    pnlsuccess.Visible = true;
                }
                                   

             
            }
            catch (Exception ex)
            {
                pnlDanger.GroupingText = ex.Message;
                pnlDanger.Visible = true;
            }

        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {

            try
            {
                int qtr = Convert.ToInt16(cbxQtr.SelectedItem.Value);
                int yr = Convert.ToInt16(cbxYear.Text);
                RemovePrior(qtr, yr);
                DoSave(qtr, yr);


                string msg = "Quarter End Account Alignment Save Completed";
                lblSuccess.Text = msg;
                pnlsuccess.Visible = true;
                pnlDanger.Visible = false;
                btnContinue.Visible = false;
            }
            catch (Exception ex)
            {
                pnlDanger.GroupingText = ex.Message;
                pnlDanger.Visible = true;

            }

        }

        protected bool CheckPrior(int asofQtr, int asofYear)
        {
            int numRows = 0;
            bool noPrior = true;
            SqlConnection cnn;
            SqlCommand cmd;
            cnn = new SqlConnection(strConnString);

            cnn.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_getCACbyQTRCounts";
            cmd.Connection = cnn;
            cmd.Parameters.Add("@asofQtr", SqlDbType.Int).Value = asofQtr;
            cmd.Parameters.Add("@asofYear", SqlDbType.Int).Value = asofYear;

            SqlDataReader rdr;
            try
            {

                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        numRows = (Int32)rdr["RecCount"];
                    }
                    rdr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();

                //cnn.Close();
            }
            if (numRows > 0)
            {
                lblDanger.Text = "Found " + numRows.ToString()  + " Accounts in the backup table for QTR" + asofQtr.ToString() + " Year " + asofYear.ToString();
                lblDanger.Visible = true;
                pnlDanger.Visible = true;
                noPrior = false;
                btnSubmit.Visible = false;
                btnCancel.Visible = true;
                btnContinue.Visible = true;
            }

            return noPrior;
        }

        protected void RemovePrior(int asofQtr, int asofYear)
        {
            SqlConnection cnn;
            SqlCommand cmd;
            cnn = new SqlConnection(strConnString);

            cnn.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_deleteAccountAlignmentsToBYQTR";
            cmd.Connection = cnn;
            cmd.Parameters.Add("@asofQtr", SqlDbType.Int).Value = asofQtr;
            cmd.Parameters.Add("@asofYear", SqlDbType.Int).Value = asofYear;
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cnn.Close();

        }

        protected void DoSave(int asofQtr, int asofYear)
        {
            SqlConnection cnn;
            SqlCommand cmd;
            cnn = new SqlConnection(strConnString);

            cnn.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_CopyAccountAlignmentsToBYQTR";
            cmd.Connection = cnn;
            cmd.Parameters.Add("@asofQtr", SqlDbType.Int).Value = asofQtr;
            cmd.Parameters.Add("@asofYear", SqlDbType.Int).Value = asofYear;
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cnn.Close();

        }


    }
}