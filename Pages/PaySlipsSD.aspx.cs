using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ClosedXML.Excel;

public partial class Pages_PaySlipsSD : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;
    int count = 1;
    int count1 = 1;
    int counter = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //txtFromDate.Text = DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToShortDateString();
            FillYear();
            FillStatus();
            FillDesignation();
            //FillDesignationType();
            FillDepartment();
            FillEmployeeType();

            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
            Form.DefaultButton = btnSearch.UniqueID;
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

    protected void FillStatus()
    {
        ddlStatus.Items.Clear();
        ListItem item = new ListItem("Select Status", "0");
        ddlStatus.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 87);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlStatus.DataSource = dt;
        ddlStatus.DataTextField = "Status";
        ddlStatus.DataValueField = "Status";
        ddlStatus.DataBind();
        dt.Dispose();
    }
    protected void FillDesignation()
    {
        ddlDesignation.Items.Clear();
        ListItem item = new ListItem("Select Designation", "0");
        ddlDesignation.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 88);
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
    //protected void FillDesignationType()
    //{
    //    ddlDesignationType.Items.Clear();
    //    ListItem item = new ListItem("Select Designation Type", "0");
    //    ddlDesignationType.Items.Add(item);

    //    cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
    //    cmd.Parameters.AddWithValue("@calltype", 881);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    da = new SqlDataAdapter();
    //    da.SelectCommand = cmd;
    //    dt = new DataTable();
    //    da.Fill(dt);
    //    cmd.Dispose();
    //    da.Dispose();

    //    ddlDesignationType.DataSource = dt;
    //    ddlDesignationType.DataTextField = "DesignationTypeName";
    //    ddlDesignationType.DataValueField = "DesignationTypeCode";
    //    ddlDesignationType.DataBind();
    //    dt.Dispose();
    //}
    protected void FillEmployeeType()
    {
        ddlEmployeeType.Items.Clear();
        ListItem item = new ListItem("Select Employee Type", "0");
        ddlEmployeeType.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 191);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlEmployeeType.DataSource = dt;
        ddlEmployeeType.DataTextField = "Type";
        ddlEmployeeType.DataValueField = "EmpTypeCode";
        ddlEmployeeType.DataBind();
        dt.Dispose();
    }
    protected void FillDepartment()
    {
        ddlDepartment.Items.Clear();
        ListItem item = new ListItem("Select Department", "0");
        ddlDepartment.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 89);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDepartment.DataSource = dt;
        ddlDepartment.DataTextField = "DepartmentName";
        ddlDepartment.DataValueField = "DepartmentCode";
        ddlDepartment.DataBind();
        dt.Dispose();
    }

    protected DataTable FillGrid()
    {
        string FilterCriteria = " and 1=1";
        if (txtSearch.Text != "")
        {
            FilterCriteria += " and (EM.EmployeeName like '%" + txtSearch.Text + "%' or EM.EmployeeCode like '%" + txtSearch.Text + "%' or DPM.DepartmentName like '%" + txtSearch.Text + "%' or DSM.DesignationDescription like '%" + txtSearch.Text + "%')";
        }

        if (ddlStatus.SelectedItem.Value != "0")
        {
            FilterCriteria += " and EM.status = '" + ddlStatus.SelectedItem.Text + "'";
        }
        else
        {
            FilterCriteria += " and EM.status = 'A'";
        }

        if (ddlDesignation.SelectedItem.Value != "0")
        {
            FilterCriteria += " and EM.DesignationCode=" + ddlDesignation.SelectedItem.Value;
        }
        //if (ddlDesignationType.SelectedItem.Value != "0")
        //{
        //    FilterCriteria += " and EM.DesignationCode in (Select DesignationCode from DesignationMaster where DesignationTypeCode=" + ddlDesignationType.SelectedItem.Value + ")";
        //}
        if (ddlEmployeeType.SelectedItem.Value != "0")
        {
            FilterCriteria = FilterCriteria + " and EmployeetypeCode=" + ddlEmployeeType.SelectedItem.Value;
        }

        if (ddlDepartment.SelectedItem.Value != "0")
        {
            FilterCriteria += " and EM.DepartmentCode=" + ddlDepartment.SelectedItem.Value;
        }

        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 21);
        cmd.Parameters.AddWithValue("@ForMonth", ddlMonth.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@ForYear", ddlYear.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Criteria", FilterCriteria);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        //GVExport.DataSource = dt;
        //GVExport.DataBind();

        lblRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " <span class='hidden-xs'>Record(s) Found</span>]";
        return dt;
    }
    void Popup(bool isDisplay)
    {
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup();", true);
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Popup(false);
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

        txtSearch.Focus();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeader = (CheckBox)GVDetails.HeaderRow.FindControl("chkHeader");

        foreach (GridViewRow row in GVDetails.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("chkCtrl");
            if (chkHeader.Checked)
            {
                cbx.Checked = true;
            }
            else
            {
                cbx.Checked = false;
            }
        }
    }

    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewSalary")
        {
            string PSNo = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            PSNo = GVDetails.Rows[RowIndex].Cells[2].Text;
            ViewState["PSNo"] = PSNo;
            GVSalary.DataSource = FillGridSalary();
            GVSalary.DataBind();
            Popup5(true);
        }
        else if (e.CommandName == "del")
        {
            string PSNo = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            PSNo = GVDetails.Rows[RowIndex].Cells[2].Text;
            ViewState["PSNo"] = PSNo;
            GVSalary.DataSource = FillGridSalary();
            GVSalary.DataBind();
            Popup1(true);
        }
    }
    protected DataTable FillGridSalary()
    {
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 22);
        cmd.Parameters.AddWithValue("@PaySlipNo", ViewState["PSNo"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        lblSalaryRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " Record(s) Found]";
        return dt;
    }
    protected void GVDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = count.ToString();
            count++;
        }
    }

    void Popup1(bool isDisplay)
    {
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup1();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup1();", true);
        }
    }
    void Popup2(bool isDisplay)
    {
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup2();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup2();", true);
        }
    }
    void Popup5(bool isDisplay)
    {
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup5();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup5();", true);
        }
    }
    protected void btnYes1_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 23);
        cmd.Parameters.AddWithValue("@PaySlipNo", ViewState["PSNo"].ToString());
        cmd.Parameters.AddWithValue("@empcode", Session["empcode"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        int result = cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Record Deleted Successfully";

        ViewState["OpenFlag"] = 0;
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnYes2_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    conn.Open();
                    cmd = new SqlCommand("SP_Payroll", conn);
                    cmd.Parameters.AddWithValue("@calltype", 23);
                    cmd.Parameters.AddWithValue("@PaySlipNo", row.Cells[2].Text);
                    cmd.Parameters.AddWithValue("@empcode", Session["empcode"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Record(s) deleted successfully";
        ViewState["OpenFlag"] = 0;
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnNo1_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }
    protected void btnNo2_Click(object sender, EventArgs e)
    {
        Popup2(true);
    }

    protected void GVSalary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = counter.ToString();
            counter++;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int pr = 0;
        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    pr = pr + 1;
                }
            }
        }

        if (pr == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "No Record is selected to remove";
            ViewState["OpenFlag"] = 0;
            return;
        }

        Popup2(true);
    }

    protected void GVExport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count1.ToString();
            count++;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (GVDetails.Rows.Count == 0)
        {
            return;
        }
        DataTable datatable = new DataTable("dt");

        foreach (TableCell cell in GVDetails.HeaderRow.Cells)
        {
            datatable.Columns.Add(cell.Text);
        }


        foreach (GridViewRow row in GVDetails.Rows)
        {
            datatable.Rows.Add();
            for (int i = 0; i < row.Cells.Count; i++)
            {
                datatable.Rows[row.RowIndex][i] = HttpUtility.HtmlDecode(row.Cells[i].Text);
            }
        }
        datatable.Columns.Remove("Column1");
        datatable.Columns.Remove("Pay Slip Number");
        datatable.Columns.Remove("E_Code");
        datatable.Columns.Remove("Action");
        datatable.Columns.Add("Remarks");

        using (XLWorkbook wb = new XLWorkbook())
        {
            //wb.Worksheets.Add(datatable, "datatable");
            var ws = wb.Worksheets.Add(datatable, "PaySlipReport");
            ws.Row(1).InsertRowsAbove(1);
            ws.Cell("A1").Value = "Salary Sheet for the Month of : " + ddlMonth.SelectedItem.Text + " - " + ddlYear.SelectedItem.Text;

            ws.Range("A1:U1").Row(1).Merge();
            ws.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=PaySlips.xls");
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