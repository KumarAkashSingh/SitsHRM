using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Globalization;
using System.Drawing;
using System.IO;
using ClosedXML.Excel;

public partial class Pages_MasterSheetReport: System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;
    int count = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillYear();
            GVMasterSheetReport.DataSource = FillGrid();
            GVMasterSheetReport.DataBind();

            GVNewEmployeesAdded.DataSource = FillNewEmployeesAdded();
            GVNewEmployeesAdded.DataBind();

            GVEmployeesLeft.DataSource = FillEmployeesLeft();
            GVEmployeesLeft.DataBind();
        }
    }

    private void FillYear()
    {
        ddlYear.Items.Clear();
        int i = DateTime.Now.Year;
        for (int j = i; j > i - 5; j--)
        {
            ListItem item = new ListItem(j.ToString(), j.ToString());
            ddlYear.Items.Add(item);
        }
        if (ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()) != null)
        {
            ddlMonth.ClearSelection();
            ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
        }
    }

    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_PaySlipMasterSheetReport", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@ForMonth", ddlMonth.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@ForYear", ddlYear.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
      
        return dt;
    }

    protected DataTable FillNewEmployeesAdded()
    {
        cmd = new SqlCommand("SP_PaySlipMasterSheetReport", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@ForMonth", ddlMonth.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@ForYear", ddlYear.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        return dt;
    }

    protected DataTable FillEmployeesLeft()
    {
        cmd = new SqlCommand("SP_PaySlipMasterSheetReport", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@ForMonth", ddlMonth.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@ForYear", ddlYear.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        return dt;
    }

    protected void GVMasterSheetReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count.ToString();
            count++;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Popup(false);
    }

    void Popup(bool isDisplay)
    {
        StringBuilder builder = new StringBuilder();
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup();", true);
        }
    }

    public SortDirection direction
    {
        get
        {
            if (ViewState["directionState"] == null)
            {
                ViewState["directionState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["directionState"];
        }
        set
        {
            ViewState["directionState"] = value;
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        GVMasterSheetReport.DataSource = FillGrid();
        GVMasterSheetReport.DataBind();

        GVNewEmployeesAdded.DataSource = FillNewEmployeesAdded();
        GVNewEmployeesAdded.DataBind();

        GVEmployeesLeft.DataSource = FillEmployeesLeft();
        GVEmployeesLeft.DataBind();
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (GVMasterSheetReport.Rows.Count == 0)
        {
            return;
        }

        DataTable datatablePaySlipMaster = new DataTable("dt");
        DataTable datatableNewEmployeesAdded = new DataTable("dt");
        DataTable datatableEmployeesLeft = new DataTable("dt");

        foreach (TableCell cell in GVMasterSheetReport.HeaderRow.Cells)
        {
            datatablePaySlipMaster.Columns.Add(cell.Text);
        }


        foreach (GridViewRow row in GVMasterSheetReport.Rows)
        {
            datatablePaySlipMaster.Rows.Add();
            for (int i = 0; i < row.Cells.Count; i++)
            {
                datatablePaySlipMaster.Rows[row.RowIndex][i] = HttpUtility.HtmlDecode(row.Cells[i].Text);
            }
        }

        if (GVNewEmployeesAdded.Rows.Count >0)
        {
            foreach (TableCell cell in GVNewEmployeesAdded.HeaderRow.Cells)
            {
                datatableNewEmployeesAdded.Columns.Add(cell.Text);
            }


            foreach (GridViewRow row in GVNewEmployeesAdded.Rows)
            {
                datatableNewEmployeesAdded.Rows.Add();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    datatableNewEmployeesAdded.Rows[row.RowIndex][i] = HttpUtility.HtmlDecode(row.Cells[i].Text);
                }
            }
        }

        if (GVEmployeesLeft.Rows.Count > 0)
        {
            foreach (TableCell cell in GVEmployeesLeft.HeaderRow.Cells)
            {
                datatableEmployeesLeft.Columns.Add(cell.Text);
            }


            foreach (GridViewRow row in GVEmployeesLeft.Rows)
            {
                datatableEmployeesLeft.Rows.Add();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    datatableEmployeesLeft.Rows[row.RowIndex][i] = HttpUtility.HtmlDecode(row.Cells[i].Text);
                }
            }
        }

        using (XLWorkbook wb = new XLWorkbook())
        {
            var ws = wb.Worksheets.Add(datatablePaySlipMaster, "Master");
            ws.Row(1).InsertRowsAbove(1);
            ws.Row(1).InsertRowsAbove(1);
            ws.Row(1).InsertRowsAbove(1);
            ws.Cell("A1").Value = "Vidya Bal Mandli";
            ws.Cell("A2").Value = "Vidya Knowledge Park, Baghpat Road, Meerut";
            ws.Cell("A3").Value = "Master Salary Sheet for the Month of : " + ddlMonth.SelectedItem.Text + " - " + ddlYear.SelectedItem.Text;

            ws.Range("A1:L1").Row(1).Merge();
            ws.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            ws.Range("A2:L2").Row(1).Merge();
            ws.Cell("A2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            ws.Range("A3:L3").Row(1).Merge();
            ws.Cell("A3").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);


            if (GVNewEmployeesAdded.Rows.Count > 0)
            {
                var wsEmployeesAdded = wb.Worksheets.Add(datatableNewEmployeesAdded, "New Employees Added");
                wsEmployeesAdded.Row(1).InsertRowsAbove(1);
                wsEmployeesAdded.Row(1).InsertRowsAbove(1);
                wsEmployeesAdded.Row(1).InsertRowsAbove(1);
                wsEmployeesAdded.Cell("A1").Value = "Vidya Bal Mandli";
                wsEmployeesAdded.Cell("A2").Value = "Vidya Knowledge Park, Baghpat Road, Meerut";
                wsEmployeesAdded.Cell("A3").Value = "Master Salary Sheet for the Month of : " + ddlMonth.SelectedItem.Text + " - " + ddlYear.SelectedItem.Text;

                wsEmployeesAdded.Range("A1:F1").Row(1).Merge();
                wsEmployeesAdded.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsEmployeesAdded.Range("A2:F2").Row(1).Merge();
                wsEmployeesAdded.Cell("A2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsEmployeesAdded.Range("A3:F3").Row(1).Merge();
                wsEmployeesAdded.Cell("A3").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            }

            if (GVEmployeesLeft.Rows.Count > 0)
            {
                var wsEmployeesAdded = wb.Worksheets.Add(datatableEmployeesLeft, "Employees Left");
                wsEmployeesAdded.Row(1).InsertRowsAbove(1);
                wsEmployeesAdded.Row(1).InsertRowsAbove(1);
                wsEmployeesAdded.Row(1).InsertRowsAbove(1);
                wsEmployeesAdded.Cell("A1").Value = "Vidya Bal Mandli";
                wsEmployeesAdded.Cell("A2").Value = "Vidya Knowledge Park, Baghpat Road, Meerut";
                wsEmployeesAdded.Cell("A3").Value = "Master Salary Sheet for the Month of : " + ddlMonth.SelectedItem.Text + " - " + ddlYear.SelectedItem.Text;

                wsEmployeesAdded.Range("A1:F1").Row(1).Merge();
                wsEmployeesAdded.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsEmployeesAdded.Range("A2:F2").Row(1).Merge();
                wsEmployeesAdded.Cell("A2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsEmployeesAdded.Range("A3:F3").Row(1).Merge();
                wsEmployeesAdded.Cell("A3").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=MasterPaySlips.xls");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}