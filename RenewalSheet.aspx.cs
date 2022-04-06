﻿using PI_Application;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;
using Telerik.Web.UI.Calendar;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Reflection;
using System.Data.SqlClient;

namespace PrepumaWebApp
{
    public partial class RenewalSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userName"] != null && Session["appName"] != null)
                {

                    loadDates();
                    initializeData();

                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }


    protected void loadDates()
    {
        
        int year = DateTime.Now.Year;
        DateTime firstDay = new DateTime(year, 1, 1);
        DateTime lastDay = new DateTime(year, 12, 31);

        dpInvoiceDate1.SelectedDate = firstDay;
        dpInvoiceDate2.SelectedDate = lastDay;
    }
   


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SetReportParameters();
        loadData();

    }

    private void SetReportParameters()
    {
        string datetype = "EffectiveDate";
        DateTime beginDate = (DateTime)dpInvoiceDate1.SelectedDate;
        DateTime endDate = (DateTime)dpInvoiceDate2.SelectedDate;


        ReportParameter[] Params = new ReportParameter[3];
        Params[0] = new ReportParameter("DateType", datetype);
        Params[1] = new ReportParameter("FromDate", beginDate.ToString());
        Params[2] = new ReportParameter("ToDate", endDate.ToString());
        
        this.ReportViewer1.LocalReport.SetParameters(Params);
        ReportViewer1.LocalReport.DisplayName = "RenewalSheet";
        ReportViewer1.LocalReport.Refresh();
    }



    protected void initializeData()
    {

        DataTable dt1 = new DataTable();

        try
        {


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rptData1 = new ReportDataSource("DataSet1", dt1);         
            

            LocalReport r = new LocalReport();
            r.ReportPath = Server.MapPath("~/ClientLogDetail.rdlc");
            r.DataSources.Add(rptData1);
           
            ReportViewer1.LocalReport.DataSources.Add(rptData1);
            
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ClientLogDetail.rdlc");
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.Visible = true;


        }
        catch (Exception ex)
        {
            string errMsg = ex.Message.ToString();
            lblDanger.Text = errMsg;
            pnlDanger.Visible = true;
        }

    }

    protected void loadData()
    {
        DataTable dt1 = getDataTable();
        
        try
        {            

            ReportViewer1.Visible = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rptData1 = new ReportDataSource("DataSet1", dt1);
           
                      
            ReportViewer1.LocalReport.DataSources.Add(rptData1);
            
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ClientLogDetail.rdlc");
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.Visible = true;

        }
        catch (Exception ex)
        {
            string errMsg = ex.Message.ToString();
            lblDanger.Text = errMsg;
            pnlDanger.Visible = true;
        }
       
    }

     protected void btnExport_Click(object sender, EventArgs e)
    {

       
        DataTable dt1 = getDataTable();
        ExportTableData(dt1, "ClientLogDetail");        


    }

    public DataTable getDataTable()
     {
         DataTable dt1 = new DataTable();

         
         DateTime beginDate = (DateTime)dpInvoiceDate1.SelectedDate;
         DateTime endDate = (DateTime)dpInvoiceDate2.SelectedDate;
        
        
         SqlConnection cnn;
         String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
         cnn = new SqlConnection(strConnString);

         SqlCommand cmd = new SqlCommand();
         SqlDataAdapter da = new SqlDataAdapter();
         
        
         try
         {
             //CURRENT YEAR
             cmd = new SqlCommand("sp_getRenewalSheet", cnn);
             cmd.Parameters.Add(new SqlParameter("@beginString", beginDate.ToString()));
             cmd.Parameters.Add(new SqlParameter("@endString", endDate.ToString()));
             cmd.CommandTimeout = 10800;
             cmd.CommandType = CommandType.StoredProcedure;
             da.SelectCommand = cmd;
             da.Fill(dt1);
                         

         }
         catch (Exception ex)
         {
             string errMsg = ex.Message.ToString();
             lblDanger.Text = errMsg;
             pnlDanger.Visible = true;
         }
         finally
         {
             cnn.Close();
         }
         return dt1;
     }

    public void ExportTableData(DataTable dt1, string filename)
    {
        try
        {
            

            SetReportParameters();
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rptData1 = new ReportDataSource("DataSet1", dt1);

            ReportViewer1.LocalReport.DataSources.Add(rptData1);

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ClientLogDetail.rdlc");
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.Visible = false;

            string FileName = "ClientLogDetail";
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;



            try
            {
                byte[] bytes = ReportViewer1.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                // Now that you have all the bytes representing the  report, buffer it and send it to the client.
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName + "." + extension);
                Response.BinaryWrite(bytes);
                // create the file
                // send it to the client to download
                Response.Flush();

            }
            catch (Exception ex)
            {
                string errMsg = ex.Message.ToString();
                lblDanger.Text = errMsg;
                pnlDanger.Visible = true;
            }


        }
        catch (Exception ex)
        {
            string errMsg = ex.Message.ToString();
            lblDanger.Text = errMsg;
            pnlDanger.Visible = true;
        }
    }
  }
}