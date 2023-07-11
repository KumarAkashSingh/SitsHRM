using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;


public partial class Pages_LeaveCategoryTransaction : System.Web.UI.Page
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
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();

        }
    }

    protected void FillLeave()
    {
        ddlLeave.Items.Clear();
        ListItem item = new ListItem("Select Leave", "0");
        ddlLeave.Items.Add(item);

        cmd = new SqlCommand("SP_Web_DesignationLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlLeave.DataSource = dt;
        ddlLeave.DataTextField = "LeaveDescription";
        ddlLeave.DataValueField = "LeaveCode";
        ddlLeave.DataBind();
        dt.Dispose();

    }

    protected void FillDesignation()
    {
        ddlDesignation.Items.Clear();
        ListItem item = new ListItem("Select Designation", "0");
        ddlDesignation.Items.Add(item);

        cmd = new SqlCommand("SP_Web_DesignationLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
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

    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_Web_DesignationLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
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

    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modify")
        {
            string DC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            DC = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["DC"] = DC;
            lblDesignation.Text = GVDetails.Rows[RowIndex].Cells[4].Text;
            GVDetails2.DataSource = FillGrid2();
            GVDetails2.DataBind();
            FillLeave2();
            Popup4(true);
        }
        else if (e.CommandName == "del")
        {
            string DC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            DC = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["DC"] = DC;
            Popup1(true);
        }
    }

    protected DataTable FillGrid2()
    {
        cmd = new SqlCommand("SP_Web_DesignationLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@DesignationCode", ViewState["DC"].ToString());
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

    protected void FillLeave2()
    {
        ddlLeave2.Items.Clear();
        ListItem item = new ListItem("Select Leave", "0");
        ddlLeave2.Items.Add(item);

        cmd = new SqlCommand("SP_Web_DesignationLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.Parameters.AddWithValue("@DesignationCode", ViewState["DC"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlLeave2.DataSource = dt;
        ddlLeave2.DataTextField = "LeaveDescription";
        ddlLeave2.DataValueField = "LeaveCode";
        ddlLeave2.DataBind();
        dt.Dispose();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        FillLeave();
        FillDesignation();
        ddlIsLeaveCountFix.SelectedIndex = 0;
        txtFixedCount.Text = "";
        txtWorkingDaysCalculation.Text = "";
        Popup3(true);
    }

    protected void GVDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVDetails.PageIndex = e.NewPageIndex;
        if (Session["SortedView"] != null)
        {
            GVDetails.DataSource = Session["SortedView"];
            GVDetails.DataBind();
        }
        else
        {
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
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

    protected void GVDetails_Sorting(object sender, GridViewSortEventArgs e)
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
        //
        GVDetails.DataSource = sortedView;
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('No Record is selected to remove');$('.modal-backdrop').remove();", true);
            dlglbl.Text = "No Record is selected to remove";
            return;
        }

        Popup2(true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int pr = 0;
        foreach (ListItem item in ddlDesignation.Items)
        {
            if (item.Selected)
            {
                pr = 1;
                break;
            }
        }
        if (pr == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Designation');ShowPopup3();", true);
        }
        if (ddlLeave.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Leave');ShowPopup3();", true);
        }
        if (ddlIsLeaveCountFix.SelectedItem.Value == "1" && txtFixedCount.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Fixed Count');ShowPopup3();", true);
        }
        if (ddlIsLeaveCountFix.SelectedItem.Value == "0")
        {
            txtFixedCount.Text = "0";
        }
        if (txtWorkingDaysCalculation.Text == "")
        {
            txtWorkingDaysCalculation.Text = "0";
        }
        pr = 0;
        foreach (ListItem item in ddlDesignation.Items)
        {
            if (item.Selected)
            {
                conn.Open();
                cmd = new SqlCommand("SP_Web_DesignationLeave", conn);
                cmd.Parameters.AddWithValue("@calltype", 6);
                cmd.Parameters.AddWithValue("@DesignationCode", item.Value);
                cmd.Parameters.AddWithValue("@LeaveCode", ddlLeave.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@IsLeaveCountFixed", ddlIsLeaveCountFix.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@FixedCount", txtFixedCount.Text);
                cmd.Parameters.AddWithValue("@WorkingDaysCalculation", txtWorkingDaysCalculation.Text);
                cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
                SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
                parm.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parm);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                int result = Convert.ToInt32(parm.Value);
                cmd.Dispose();
                conn.Close();

                if (result == 1)
                {
                    pr = 1;
                }
            }
        }
        if (pr == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Some record already exist');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record Updated successfully','success');$('.modal-backdrop').remove();", true);
        }
        ViewState["OpenFlag"] = 0;
        Popup(true);
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void btnSave2_Click(object sender, EventArgs e)
    {
        if (ddlLeave2.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Leave');ShowPopup4();", true);
        }
        if (ddlIsLeaveCountFix2.SelectedItem.Value == "1" && txtFixedCount2.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Leave Count');ShowPopup4();", true);
        }
        if (ddlIsLeaveCountFix2.SelectedItem.Value == "0")
        {
            txtFixedCount2.Text = "0";
        }
        if (txtWorkingDaysCalculation2.Text == "")
        {
            txtWorkingDaysCalculation2.Text = "0";
        }
        conn.Open();
        cmd = new SqlCommand("SP_Web_DesignationLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@DesignationCode", ViewState["DC"].ToString());
        cmd.Parameters.AddWithValue("@LeaveCode", ddlLeave2.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@IsLeaveCountFixed", ddlIsLeaveCountFix2.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@FixedCount", txtFixedCount2.Text);
        cmd.Parameters.AddWithValue("@WorkingDaysCalculation", txtWorkingDaysCalculation2.Text);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
        parm.Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(parm);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        int result = Convert.ToInt32(parm.Value);
        cmd.Dispose();
        conn.Close();

        if (result == 0)
        {
            Popup(true);
            dlglbl.Text = "Record Already exist";
            ViewState["OpenFlag"] = -2;
            return;
        }
        Popup4(true);
        ViewState["OpenFlag"] = 0;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Leave details updated successfully','success');$('.modal-backdrop').remove();", true);
        GVDetails2.DataSource = FillGrid2();
        GVDetails2.DataBind();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ViewState["OpenFlag"] != null && ViewState["OpenFlag"].ToString() == "-1")
        {
            Popup1(true);
        }
        else if (ViewState["OpenFlag"] != null && ViewState["OpenFlag"].ToString() == "-2")
        {
            Popup4(true);
        }
        else
        {
            Popup(false);
        }
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        //Delete Single Designation 
        conn.Open();
        cmd = new SqlCommand("SP_Web_DesignationLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
        cmd.Parameters.AddWithValue("@DesignationCode", ViewState["DC"].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ViewState["OpenFlag"] = 0;
        Popup(true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record deleted succesfully','success');$('.modal-backdrop').remove();", true);
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }

    protected void btnYes2_Click(object sender, EventArgs e)
    {
        //Delete Multiple  Designation Together
        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    conn.Open();
                    cmd = new SqlCommand("SP_Web_DesignationLeave", conn);
                    cmd.Parameters.AddWithValue("@calltype", 7);
                    cmd.Parameters.AddWithValue("@DesignationCode", row.Cells[1].Text);
                    cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
        ViewState["OpenFlag"] = 0;
        Popup(true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record deleted succesfully','success');$('.modal-backdrop').remove();", true);
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void btnNo2_Click(object sender, EventArgs e)
    {
        Popup2(false);
    }

    protected void btnYes3_Click(object sender, EventArgs e)
    {
        //Delete Single Designation single Leave
        conn.Open();
        cmd = new SqlCommand("SP_Web_DesignationLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 8);
        cmd.Parameters.AddWithValue("@DesignationCode", ViewState["DC"].ToString());
        cmd.Parameters.AddWithValue("@LeaveCode", ViewState["LC"].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        FillLeave2();

        GVDetails2.DataSource = FillGrid2();
        GVDetails2.DataBind();

        Popup4(true);
    }

    protected void btnNo3_Click(object sender, EventArgs e)
    {
        Popup5(false);
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

    void Popup4(bool isDisplay)
    {
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup4();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup4();", true);
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


    protected void GVDetails2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string LC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            LC = GVDetails2.Rows[RowIndex].Cells[0].Text;
            ViewState["LC"] = LC;
            Popup5(true);
        }
    }
}