using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using System.IO;

partial class Pages_PrintEmployeeIDCards : System.Web.UI.Page
{
    private SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    private SqlCommand cmd = null;
    private SqlDataAdapter da = null;
    private DataTable dt = null;
    private DataTable dt1 = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //string ValidTill = Request.QueryString[0].ToString();
        string ECodes = Request.QueryString[0].ToString();

        cmd = new SqlCommand("SP_EmployeeIDCard", conn);
        cmd.Parameters.AddWithValue("@ECode", ECodes);
        cmd.Parameters.AddWithValue("@ValidUpto", "2021-01-01");
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        //dt.Rows[0][5] = "123456789012345678901234567890";
        ReportDocument crystalReport = new ReportDocument();
        crystalReport.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEnvelope10;
        crystalReport.Load(Server.MapPath("../cr/EmployeeIDCards_old.rpt"));

        string FileName = "ICard";
        if (dt.Rows.Count == 1)
        {
            FileName = dt.Rows[0]["EmployeeName"].ToString();
        }

        CrystalReportViewer1.Visible = true;
        crystalReport.SetDataSource(dt);
        //CrystalReportViewer1.ReportSource = crystalReport;
        //CrystalReportViewer1.DataBind();
        //CrystalReportViewer1.RefreshReport();
        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, FileName);
        Response.End();
    }
    public Pages_PrintEmployeeIDCards()
    {
        Load += Page_Load;
    }
}