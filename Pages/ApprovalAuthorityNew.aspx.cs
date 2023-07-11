using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Pages_ApprovalAuthorityNew : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDropDowns();
            FillGrid();
        }
    }

    protected void FillDropDowns()
    {
        ddlDepartment.Items.Clear();
        ListItem item = new ListItem("Select Department", "-1");
        ListItem item1 = new ListItem("All", "0");
        ddlDepartment.Items.Add(item);
        ddlDepartment.Items.Add(item1);

        cmd = new SqlCommand("SP_Web_Leave", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDepartment.DataSource = dt;
        ddlDepartment.DataTextField = "departmentname";
        ddlDepartment.DataValueField = "departmentcode";
        ddlDepartment.DataBind();
        dt.Dispose();

        ddlBranch.Items.Clear();
        item = new ListItem("Select Branch", "-1");
        item1 = new ListItem("All", "0");
        ddlBranch.Items.Add(item);
        ddlBranch.Items.Add(item1);

        cmd = new SqlCommand("SP_Web_Leave", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlBranch.DataSource = dt;
        ddlBranch.DataTextField = "branchname";
        ddlBranch.DataValueField = "Branchid";
        ddlBranch.DataBind();
        dt.Dispose();

        ddlRecommending.Items.Clear();
        item = new ListItem("Select Recommending Officer", "0");
        ddlRecommending.Items.Add(item);

        cmd = new SqlCommand("SP_Web_Leave", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlRecommending.DataSource = dt;
        ddlRecommending.DataTextField = "employeename";
        ddlRecommending.DataValueField = "employeecode";
        ddlRecommending.DataBind();
        dt.Dispose();

        ddlLevel.Items.Clear();
        item = new ListItem("Select Level", "0");
        ddlLevel.Items.Add(item);

        cmd = new SqlCommand("SP_Web_Leave", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlLevel.DataSource = dt;
        ddlLevel.DataTextField = "LevelName";
        ddlLevel.DataValueField = "LevelID";
        ddlLevel.DataBind();
        dt.Dispose();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //if (ddlDepartment.SelectedItem.Value == "-1")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //    dlglbl.Text = "Please Select Department";
        //    return;
        //}
        //else if (ddlBranch.SelectedItem.Value == "-1")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //    dlglbl.Text = "Please Select Branch";
        //    return;
        //}
        FillGrid();

    }

    protected void FillGrid()
    {
        string Criteria = " and 1=1";
        if (ddlDepartment.SelectedItem.Value != "-1" && ddlDepartment.SelectedItem.Value != "0")
        {
            Criteria += " and EM.DepartmentCode=" + ddlDepartment.SelectedItem.Value;
        }
        if (ddlBranch.SelectedItem.Value != "-1" && ddlBranch.SelectedItem.Value != "0")
        {
            Criteria += " and EM.BranchID=" + ddlBranch.SelectedItem.Value;
        }
        cmd = new SqlCommand("SP_SetApprovalAuthority", conn);
        cmd.Parameters.AddWithValue("@Criteria", Criteria);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        GVAuthority.DataSource = dt;
        GVAuthority.DataBind();

        lblRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " <span class='hidden-xs'>Record(s) Found</span>]";
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int pr = 0;
        foreach (GridViewRow row in GVAuthority.Rows)
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
            dlglbl.Text = "No record is selected to update";
            return;
        }

        Popup1(true);
    }
    protected void btnNo_Click(object sender, EventArgs e)
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
    protected void btnYes1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GVAuthority.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    conn.Open();
                    cmd = new SqlCommand("SP_Web_Leave", conn);
                    cmd.Parameters.AddWithValue("@calltype", 5);
                    cmd.Parameters.AddWithValue("@ApprovalAuthorityEmployeeCode", ddlRecommending.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@LevelID", ddlLevel.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@IsFinalAuthority", ddlIsFinalAuthority.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@ECode", row.Cells[5].Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Record Updated Successfully!!!";

        FillGrid();

        Popup1(false);
    }
    protected void btnNo1_Click(object sender, EventArgs e)
    {
        Popup1(false);
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
    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeader = (CheckBox)GVAuthority.HeaderRow.FindControl("chkHeader");

        foreach (GridViewRow row in GVAuthority.Rows)
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
    protected void GVAuthority_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.Header)
        {
            e.Row.Cells[5].Visible = false;
        }
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Visible = false;
            foreach (TableCell cell in e.Row.Cells)
            {
                if(cell.Text.Contains("(F)"))
                {
                    cell.BackColor = System.Drawing.Color.LightGreen;
                }
            }
        }
    }
}