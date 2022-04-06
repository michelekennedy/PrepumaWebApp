using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class AccountRevenueCheck : System.Web.UI.Page
{

    String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCheck_Click(object sender, System.EventArgs e)
    {

        pnlWarning.Visible = false;
        pnlSuccess.Visible = false;
        pnlDanger.Visible = false;

        string acctnbr = AcctNbr.Text;
       
        SqlConnection cnn;
        String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
        cnn = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_GetAcctRevDates", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AcctNbr", SqlDbType.VarChar).Value = acctnbr;
            cmd.CommandTimeout = 10800;
            da.SelectCommand = cmd;

            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ContractNbr.Text = row["ContractNumber"].ToString();
                    MinRevDate.Text = row["MinFiscalMonth"].ToString();
                    MaxRevDate.Text = row["MaxFiscalMonth"].ToString();
                }
            }
            else
            {
                ContractNbr.Text = "No Records Found";
                MinRevDate.Text = "";
                MaxRevDate.Text = "";
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

 }