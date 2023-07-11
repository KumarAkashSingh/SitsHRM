using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Globalization;

public partial class ApproveManualPunch : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1) ;

            txtDate.Text = DateTime.Now.ToString(firstDayOfMonth.ToString("dd/MM/yyyy") + " - " + lastDayOfMonth.ToString("dd/MM/yyyy"));
            FillDesignation();
            FillDepartment();
            FillBranch();
            FillEmployeeType();
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
    }

    protected void FillDesignation()
    {
        ddlDesignation.Items.Clear();
        ListItem item = new ListItem("Select Designation", "0");
        ddlDesignation.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDesignation.DataSource = dt;
        ddlDesignation.DataTextField = "ddlText";
        ddlDesignation.DataValueField = "ddlValue";
        ddlDesignation.DataBind();
        dt.Dispose();
    }
    protected void FillDepartment()
    {
        ddlDepartment.Items.Clear();
        ListItem item = new ListItem("Select Department", "0");
        ddlDepartment.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDepartment.DataSource = dt;
        ddlDepartment.DataTextField = "ddlText";
        ddlDepartment.DataValueField = "ddlValue";
        ddlDepartment.DataBind();
        dt.Dispose();
    }
    protected void FillBranch()
    {
        ddlBranch.Items.Clear();
        ListItem item = new ListItem("Select Organisation", "0");
        ddlBranch.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlBranch.DataSource = dt;
        ddlBranch.DataTextField = "ddlText";
        ddlBranch.DataValueField = "ddlValue";
        ddlBranch.DataBind();
        dt.Dispose();
    }

    protected void FillEmployeeType()
    {
        ddlEmployeeType.Items.Clear();
        ListItem item = new ListItem("Select Employee Type", "0");
        ddlEmployeeType.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlEmployeeType.DataSource = dt;
        ddlEmployeeType.DataTextField = "ddlText";
        ddlEmployeeType.DataValueField = "ddlValue";
        ddlEmployeeType.DataBind();
        dt.Dispose();
    }

    protected DataTable FillGrid()
    {
        string Criteria = "";
        string FromDate = DateTime.Now.ToString("yyyy-MM-dd");
        string ToDate = DateTime.Now.ToString("yyyy-MM-dd");
        try
        {
            FromDate = DateTime.ParseExact(txtDate.Text.Split('-')[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            ToDate = DateTime.ParseExact(txtDate.Text.Split('-')[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
        }
        catch
        {

        }
        Criteria += "and cast(TransactionTime as date) between '" + FromDate + "' and '" + ToDate + "'";
        if (ddlDesignation.SelectedItem.Value != "0")
        {
            Criteria += " and EM.DesignationCode=" + ddlDesignation.SelectedItem.Value;
        }

        if (ddlDepartment.SelectedItem.Value != "0")
        {
            Criteria +=  " and EM.DepartmentCode=" + ddlDepartment.SelectedItem.Value;
        }

        if (ddlBranch.SelectedItem.Value != "0")
        {
            Criteria += " and EM.BranchID=" + ddlBranch.SelectedItem.Value;
        }

        if (ddlEmployeeType.SelectedItem.Value != "0")
        {
            Criteria += " and EM.EmployeetypeCode=" + ddlEmployeeType.SelectedItem.Value;
        }
        if (ddlPunchStatus.SelectedItem.Value != "0")
        {
            if(ddlPunchStatus.SelectedItem.Value=="1")
            {
                Criteria += " and BD.IsApproved=0";
            }
            if (ddlPunchStatus.SelectedItem.Value == "2")
            {
                Criteria += " and BD.IsApproved=1";
            }
            if (ddlPunchStatus.SelectedItem.Value == "3")
            {
                Criteria += " and BD.IsPartiallyRemoved=1";
            }
        }
        
        cmd = new SqlCommand("SP_ManualPunch", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@Criteria", Criteria);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        lblRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " <span class='hidden-xs'>Record(s) Found</span>]";
        return dt;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
       Popup(false);
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
    protected void GVDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            if(e.Row.Cells[7].Text=="Approved")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            if (e.Row.Cells[7].Text == "Approved Against Late")
            {
                e.Row.BackColor = System.Drawing.Color.Cornsilk;
            }
        }
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

    protected void btnApprovePunch_Click(object sender, EventArgs e)
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
            dlglbl.Text = "No Record is selected to Approve";
            return;
        }

        Popup1(true);
    }

    protected void btnRemove_Click(object sender, EventArgs e)
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
            return;
        }

        Popup2(true);
    }

    protected void btnYesApprove_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    conn.Open();
                    cmd = new SqlCommand("SP_ManualPunch", conn);
                    cmd.Parameters.AddWithValue("@calltype", 9);
                    cmd.Parameters.AddWithValue("@BiometricCode", row.Cells[3].Text);
                    cmd.Parameters.AddWithValue("@PunchDate", DateTime.ParseExact(row.Cells[4].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
                    SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    int result = Convert.ToInt32(parm.Value);
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Record(s) approved successfully";

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void btnYesRemove_Click(object sender, EventArgs e)
    {
        bool flag = false;
        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    conn.Open();
                    cmd = new SqlCommand("SP_ManualPunch", conn);
                    cmd.Parameters.AddWithValue("@calltype", 4);
                    cmd.Parameters.AddWithValue("@BiometricCode", row.Cells[3].Text);
                    cmd.Parameters.AddWithValue("@PunchDate", DateTime.ParseExact(row.Cells[4].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
                    SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    int result = Convert.ToInt32(parm.Value);
                    cmd.Dispose();
                    conn.Close();

                    if (result != 1)
                    {
                        flag = true;
                    }
                }
            }
        }
        if (flag == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Some Records can not be removed. Salary processed..";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Record(s) deleted successfully";
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
}