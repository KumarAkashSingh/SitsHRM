using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
public partial class Pages_SetWorkingHours : System.Web.UI.Page
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
            FillStatus();
            FillDesignation();
            FillDepartment();
            FillBranch();

            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();

        }
    }
    protected void FillStatus()
    {
        ddlStatus.Items.Clear();
        ListItem item = new ListItem("Select Status", "0");
        ddlStatus.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
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
        ListItem item = new ListItem("Select Branch", "0");
        ddlBranch.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
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
    protected DataTable FillGrid()
    {
        string FilterCriteria = "";

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 11);
        
        if (ddlStatus.SelectedItem.Value != "0")
        {
            FilterCriteria = " a.status = '" + ddlStatus.SelectedItem.Text + "'";
        }
        else
        {
            FilterCriteria = "a.status='A'";
        }

        if (ddlDesignation.SelectedItem.Value != "0")
        {
            FilterCriteria = FilterCriteria + " and a.DesignationCode=" + ddlDesignation.SelectedItem.Value;
        }

        if (ddlDepartment.SelectedItem.Value != "0")
        {
            FilterCriteria = FilterCriteria + " and a.DepartmentCode=" + ddlDepartment.SelectedItem.Value;
        }

        if (ddlBranch.SelectedItem.Value != "0")
        {
            FilterCriteria = FilterCriteria + " and a.BranchID=" + ddlBranch.SelectedItem.Value;
        }

        cmd.Parameters.AddWithValue("@Status", FilterCriteria);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        lblRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " Record(s) Found]";
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ViewState["OpenFlag"]!=null && (ViewState["OpenFlag"].ToString() == "-1" || ViewState["OpenFlag"].ToString() == "-2"))
        {
            Popup3(true);
        }
        else
        {
            Popup(false);
        }

    }
    protected void btnYes1_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Web_Leave", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@empcode", ViewState["EC"].ToString());
        cmd.Parameters.AddWithValue("@e_code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        int result = cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Record deleted successfully";

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
        ViewState["OpenFlag"] = "0";
    }
    protected void btnYes2_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Web_Leave", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
        cmd.Parameters.AddWithValue("@recno", ViewState["RN"].ToString());
        cmd.Parameters.AddWithValue("@e_code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        int result = cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Record deleted successfully";
        GVTimings.DataSource = FillGridTimings();
        GVTimings.DataBind();
        ViewState["OpenFlag"] = "-2";
    }
    protected void btnNo1_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }
    protected void btnNo2_Click(object sender, EventArgs e)
    {
        Popup2(false);
    }
    
    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[3].Text;
            ViewState["EC"] = EC;
            Popup1(true);
        }
        else if (e.CommandName == "modify")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[3].Text;
            ViewState["EC"] = EC;
            btnSave.Visible = false;
            toppanel.Visible = false;
            bottompanel.Visible = true;
            GVTimings.DataSource = FillGridTimings();
            GVTimings.DataBind();
            Popup3(true);
            GVTimings.Enabled = true;
        }
        else if (e.CommandName == "view")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[3].Text;
            ViewState["EC"] = EC;
            btnSave.Visible = false;
            toppanel.Visible = false;
            bottompanel.Visible = true;
            GVTimings.DataSource = FillGridTimings();
            GVTimings.DataBind();
            Popup3(true);
            GVTimings.Enabled = false;
        }
        else if (e.CommandName == "viewphoto")
        {
            string FC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            FC = GVDetails.Rows[RowIndex].Cells[3].Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", ("<script>openPopup('ViewImage.aspx?Code=" + FC + "&Type=E')</script>"), false);
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

    }
    protected void btnApplyStructure_Click(object sender, EventArgs e)
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
                    break;
                }
            }
        }

        if (pr == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "No Record is selected to Update";
            ViewState["OpenFlag"] = "0";
            return;
        }

        btnSave.Visible = true;
        radDays.ClearSelection();
        Popup3(true);
        toppanel.Visible = true;
        bottompanel.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (TimeFrom.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Choose From Time";
            ViewState["OpenFlag"] = "-1";
            return;
        }
        else if (TimeTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Choose To Time";
            ViewState["OpenFlag"] = "-1";
            return;
        }
        else if (BreakFrom.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Choose Break From Time";
            ViewState["OpenFlag"] = "-1";
            return;
        }
        else if (BreakTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Choose Break To Time";
            ViewState["OpenFlag"] = "-1";
            return;
        }
        else if (TotalHours.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Enter Total Hours";
            ViewState["OpenFlag"] = "-1";
            return;
        }
        if (Remarks.Text == "")
            Remarks.Text = "-";

        ViewState["OpenFlag"] = "0";
        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    string E_Code = row.Cells[3].Text;
                    foreach (ListItem item in radDays.Items)
                    {
                        if (item.Selected)
                        {
                            conn.Open();
                            cmd = new SqlCommand("SP_Web_Leave", conn);
                            //cmd.Parameters.AddWithValue("@calltype", 91);
                            cmd.Parameters.AddWithValue("@calltype", 8);
                            cmd.Parameters.AddWithValue("@ECode", E_Code);
                            cmd.Parameters.AddWithValue("@DayofWeek", item.Value);
                            cmd.Parameters.AddWithValue("@InTime", TimeFrom.Text);
                            cmd.Parameters.AddWithValue("@OutTime", TimeTo.Text);
                            cmd.Parameters.AddWithValue("@Ltimefrom", BreakFrom.Text);
                            cmd.Parameters.AddWithValue("@Ltimeto", BreakTo.Text);
                            cmd.Parameters.AddWithValue("@TotalHrs", TotalHours.Text);
                            cmd.Parameters.AddWithValue("@remarks", Remarks.Text);
                            cmd.Parameters.AddWithValue("@IsFlexible", chkIsFlexible.Checked);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }
                }
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Working Timings Created";
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
    protected void btnCloseMain_Click(object sender, EventArgs e)
    {
        Popup3(false);
    }
    void Popup3(bool isDisplay)
    {
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup3();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup3();", true);
        }
    }
    protected DataTable FillGridTimings()
    {
        cmd = new SqlCommand("SP_Web_Leave", conn);
        cmd.Parameters.AddWithValue("@calltype", 9);
        cmd.Parameters.AddWithValue("@ECode", ViewState["EC"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        return dt;
    }
    protected void GVTimings_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string RN = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            RN = GVTimings.Rows[RowIndex].Cells[0].Text;
            ViewState["RN"] = RN;
            Popup2(true);
        }
    }
   
}