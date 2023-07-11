using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using ClosedXML.Excel;
using System.Drawing;
public partial class Pages_AttendanceAnalysis : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy");
            //FillBranch();
            FillDepartment();
            FillDesignation();
        }
    }

    //public void FillBranch()
    //{
    //    ddlBranch.Items.Clear();
    //    ListItem item = new ListItem("Select College", "0");
    //    ddlBranch.Items.Add(item);

    //    cmd = new SqlCommand("SP_Web_BioMetricAttendance", conn);
    //    cmd.Parameters.AddWithValue("@calltype", 8);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    da = new SqlDataAdapter();
    //    da.SelectCommand = cmd;
    //    dt = new DataTable();
    //    da.Fill(dt);
    //    cmd.Dispose();
    //    da.Dispose();

    //    ddlBranch.DataSource = dt;
    //    ddlBranch.DataTextField = "BranchName";
    //    ddlBranch.DataValueField = "BranchID";
    //    ddlBranch.DataBind();
    //    dt.Dispose();
    //}
    public void FillDepartment()
    {
        ddlDept.Items.Clear();
        ListItem item = new ListItem("Select Department", "0");
        ddlDept.Items.Add(item);

        cmd = new SqlCommand("SP_Web_BioMetricAttendance", conn);
        cmd.Parameters.AddWithValue("@calltype", 9);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDept.DataSource = dt;
        ddlDept.DataTextField = "DepartmentName";
        ddlDept.DataValueField = "DepartmentCode";
        ddlDept.DataBind();
        dt.Dispose();
    }
    public void FillDesignation()
    {
        ddlDesignation.Items.Clear();
        ListItem item = new ListItem("Select Designation", "0");
        ddlDesignation.Items.Add(item);

        cmd = new SqlCommand("SP_Web_BioMetricAttendance", conn);
        cmd.Parameters.AddWithValue("@calltype", 10);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDesignation.DataSource = dt;
        ddlDesignation.DataTextField = "DesignationDescription";
        ddlDesignation.DataValueField = "DesignationCode";
        ddlDesignation.DataBind();
        dt.Dispose();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        GVReports.DataSource = FillGrid();
        GVReports.DataBind();
        dt.Dispose();
    }
    public DataTable FillGrid()
    {
        string SDate = "", EDate = "";
        string Criteria = " and 1=1";
        SDate = DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).Year + "-" + DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).Month + "-01";
        if (DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).Month == 12)
        {
            EDate = DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).Year + 1 + "-Jan-01";
        }
        else
        {
            EDate = DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).Year + "-" + ((DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).Month) + 1) + "-01";
        }
        EDate = Convert.ToDateTime(EDate).AddDays(-1).ToString();
        ViewState["SDate"] = SDate;
        ViewState["EDate"] = EDate;

        if (ddlReports.SelectedItem.Value == "1")
        {
            cmd = new SqlCommand("SP_Salarydays", conn);
            cmd.Parameters.AddWithValue("@MONTH", DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).Month);
            cmd.Parameters.AddWithValue("@YEAR", DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).Year);
            cmd.Parameters.AddWithValue("@FD", ViewState["SDate"].ToString());
            cmd.Parameters.AddWithValue("@TD", ViewState["EDate"].ToString());
            //if (ddlBranch.SelectedItem.Value != "0")
            //{
            //    Criteria += " and em.BranchID=" + ddlBranch.SelectedItem.Value + "";
            //}
            if (ddlDept.SelectedItem.Value != "0")
            {
                Criteria += " and dm.departmentcode=" + ddlDept.SelectedItem.Value + "";
            }
            if (ddlDesignation.SelectedItem.Value != "0")
            {
                Criteria += " and dg.designationcode=" + ddlDesignation.SelectedItem.Value + "";
            }
            cmd.Parameters.AddWithValue("@Criteria", Criteria);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();
        }
        else if (ddlReports.SelectedItem.Value == "2")
        {
            cmd = new SqlCommand("SP_DisplayWithTimeNew2", conn);
            cmd.Parameters.AddWithValue("@MONTH", DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).Month);
            cmd.Parameters.AddWithValue("@YEAR", DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).Year);
            cmd.Parameters.AddWithValue("@FD", ViewState["SDate"].ToString());
            cmd.Parameters.AddWithValue("@TD", ViewState["EDate"].ToString());
            //if (ddlBranch.SelectedItem.Value != "0")
            //{
            //    Criteria += " and em.BranchID=" + ddlBranch.SelectedItem.Value + "";
            //}
            if (ddlDept.SelectedItem.Value != "0")
            {
                Criteria += " and dm.departmentcode=" + ddlDept.SelectedItem.Value + "";
            }
            if (ddlDesignation.SelectedItem.Value != "0")
            {
                Criteria += " and dg.designationcode=" + ddlDesignation.SelectedItem.Value + "";
            }
            cmd.Parameters.AddWithValue("@Criteria", Criteria);
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();
        }
        else if (ddlReports.SelectedItem.Value == "3")
        {
            cmd = new SqlCommand("SP_MissPunch", conn);
            cmd.Parameters.AddWithValue("@MONTH", DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).Month);
            cmd.Parameters.AddWithValue("@YEAR", DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).Year);
            //if (ddlBranch.SelectedItem.Value != "0")
            //{
            //    Criteria += " and EmployeeMaster.BranchID=" + ddlBranch.SelectedItem.Value + "";
            //}
            if (ddlDept.SelectedItem.Value != "0")
            {
                Criteria += " and DepartmentMaster.departmentcode=" + ddlDept.SelectedItem.Value + "";
            }
            if (ddlDesignation.SelectedItem.Value != "0")
            {
                Criteria += " and DesignationMaster.designationcode=" + ddlDesignation.SelectedItem.Value + "";
            }
            cmd.Parameters.AddWithValue("@Criteria", Criteria);
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();
        }
        else if (ddlReports.SelectedItem.Value == "4")
        {
            cmd = new SqlCommand("SP_MonthlyReport", conn);
            cmd.Parameters.AddWithValue("@FD", ViewState["SDate"].ToString());
            cmd.Parameters.AddWithValue("@TD", ViewState["EDate"].ToString());
            //if (ddlBranch.SelectedItem.Value != "0")
            //{
            //    Criteria += " and em.BranchID=" + ddlBranch.SelectedItem.Value + "";
            //}
            if (ddlDept.SelectedItem.Value != "0")
            {
                Criteria += " and dm.departmentcode=" + ddlDept.SelectedItem.Value + "";
            }
            if (ddlDesignation.SelectedItem.Value != "0")
            {
                Criteria += " and dg.designationcode=" + ddlDesignation.SelectedItem.Value + "";
            }
            cmd.Parameters.AddWithValue("@Criteria", Criteria);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();
        }
        return dt;
    }
    protected void GVReports_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVReports.PageIndex = e.NewPageIndex;
        if (Session["SortedView"] != null)
        {
            GVReports.DataSource = Session["SortedView"];
            GVReports.DataBind();
        }
        else
        {
            GVReports.DataSource = FillGrid();
            GVReports.DataBind();
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
    protected void GVReports_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (direction == SortDirection.Ascending)
        {
            direction = SortDirection.Descending;
            sortingDirection = "Desc";

        }
        else
        {
            direction = SortDirection.Ascending;
            sortingDirection = "Asc";

        }
        DataView sortedView = new DataView(FillGrid());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        
        GVReports.DataSource = sortedView;
        GVReports.DataBind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (GVReports.Rows.Count == 0)
        {
            return;
        }
        DataTable datatable = new DataTable("dt");

        foreach (TableCell cell in GVReports.HeaderRow.Cells)
        {
            datatable.Columns.Add(cell.Text);
        }


        foreach (GridViewRow row in GVReports.Rows)
        {
            datatable.Rows.Add();
            for (int i = 0; i < row.Cells.Count; i++)
            {
                datatable.Rows[row.RowIndex][i] = HttpUtility.HtmlDecode(row.Cells[i].Text);
            }
        }

        using (XLWorkbook wb = new XLWorkbook())
        {
            //wb.Worksheets.Add(datatable, "datatable");
            var ws = wb.Worksheets.Add(datatable, "Attendance Analysis");
            ws.Row(1).InsertRowsAbove(1);
            ws.Cell("A1").Value = "Attendance Analysis for the Month of : " + DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).ToString("MMM") + " - " + DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("YYYY");

            ws.Range("A1:U1").Row(1).Merge();
            ws.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=AttendanceAnalysis.xls");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        //string attachment = "attachment; filename=AttendanceAnalysis.xls";
        //Response.ClearContent();
        //Response.AddHeader("content-disposition", attachment);
        //Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //if (GVReports.Rows.Count > 0)
        //{
        //    GVReports.RenderControl(htw);
        //}
        //Response.Write(sw.ToString());
        //Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void GVReports_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            //Manual Punch & Automatic Punch
            int MP = 0;
            int AP = 0;
            for(int i=0;i<e.Row.Cells.Count;i++)
            {
                if (e.Row.Cells[i].Text.Contains("Auto Punch"))
                {
                    AP++;
                }
                else if(e.Row.Cells[i].Text.Contains("Self Manual Punch"))
                {
                    MP++;
                }
                if (e.Row.Cells[i].Text.Contains("Red"))
                {
                    string hex = "#F89406";
                    e.Row.Cells[i].BackColor = ColorTranslator.FromHtml(hex);
                    e.Row.Cells[i].ForeColor = Color.White;
                    e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("Red", "");
                }
                if (e.Row.Cells[i].Text.Contains("Pink"))
                {
                    //string hex = "#F32406";
                    //e.Row.Cells[i].BackColor = ColorTranslator.FromHtml(hex);
                    e.Row.Cells[i].BackColor = Color.Brown;
                    e.Row.Cells[i].ForeColor = Color.White;
                    e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("Pink", "");
                }
                if (e.Row.Cells[i].Text.Contains("Missed Punch") || (e.Row.Cells[i].Text.Contains("A") && e.Row.Cells[i].Text.Length==1))
                {
                    e.Row.Cells[i].BackColor = Color.Red;
                    e.Row.Cells[i].ForeColor = Color.White;
                }
                if (e.Row.Cells[i].Text.Contains("Late"))
                {
                    string hex = "#afd201";
                    e.Row.Cells[i].BackColor = ColorTranslator.FromHtml(hex);
                    e.Row.Cells[i].ForeColor = Color.White;
                }
            }
            e.Row.Cells[3].Text = "Auto Punch : " + AP.ToString() + "<br>Manual Punch : " + MP.ToString();
        }
    }
}