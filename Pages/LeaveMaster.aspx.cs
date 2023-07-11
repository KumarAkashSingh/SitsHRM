using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class Pages_LeaveMaster : System.Web.UI.Page
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

    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 13);
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
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearDetails();
        btnAdd.Visible = true;
        btnUpdate.Visible = false;
        Popup3(true);
    }
    protected void ClearDetails()
    {
        txtLeaveName.Text = "";
        txtAliasName.Text = "";
        txtLeaves.Text = "0";
        txtDaysBack.Text = "0";
        txtDaysFuture.Text = "0";
        txtDueAfter.Text = "0";
        txtAtATime.Text = "0"; 
        txtDeduction.Text = "0";
        ddlStatus.SelectedIndex = 0;
        ddlLimited.SelectedIndex = 0;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtLeaveName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Add Leave Name";
            ViewState["OpenFlag"] = -1;
            return;
        }
        ViewState["OpenFlag"] = 0;
        if (txtLeaves.Text == "")
        {
            txtLeaves.Text = "0";
        }

        if (txtDaysBack.Text == "")
        {
            txtDaysBack.Text = "0";
        }

        if (txtDaysFuture.Text == "")
        {
            txtDaysFuture.Text = "0";
        }

        if (txtDueAfter.Text == "")
        {
            txtDueAfter.Text = "0";
        }

        if (txtAtATime.Text == "")
        {
            txtAtATime.Text = "0";
        }

        if (txtDeduction.Text == "")
        {
            txtDeduction.Text = "0";
        }
        
        conn.Open();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 14);
        cmd.Parameters.AddWithValue("@LeaveDescription", txtLeaveName.Text);
        cmd.Parameters.AddWithValue("@TotalLeaves", txtLeaves.Text);
        cmd.Parameters.AddWithValue("@Remarks", txtAliasName.Text);
        cmd.Parameters.AddWithValue("@frac",txtDeduction.Text);
        cmd.Parameters.AddWithValue("@lpd",txtDueAfter.Text);
        cmd.Parameters.AddWithValue("@backdays", txtDaysBack.Text);
        cmd.Parameters.AddWithValue("@futuredays", txtDaysFuture.Text);
        cmd.Parameters.AddWithValue("@limited", ddlLimited.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@lstatus", ddlStatus.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@atatime", txtAtATime.Text);
        cmd.Parameters.AddWithValue("@skipdays", "1");
        cmd.Parameters.AddWithValue("@MonthLimit", txtMonthLimit.Text);
        cmd.Parameters.AddWithValue("@RequestLeave", ddlRequestLeave.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@WithdrawLimit", txtWithDrawLimit.Text);
        cmd.Parameters.AddWithValue("@CarryForward", ddlCF.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@empcode", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Leave Name Already Exist";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Leave Name Added successfully";
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string LC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            LC = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["LC"] = LC;
            Popup1(true);
        }
        else if (e.CommandName == "modify")
        {
            string LC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            LC = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["LC"] = LC;
            ClearDetails();

            txtLeaveName.Text = GVDetails.Rows[RowIndex].Cells[2].Text;
            txtAliasName.Text = GVDetails.Rows[RowIndex].Cells[3].Text;
            txtLeaves.Text = GVDetails.Rows[RowIndex].Cells[4].Text;
            txtDaysBack.Text = GVDetails.Rows[RowIndex].Cells[7].Text;
            txtDaysFuture.Text = GVDetails.Rows[RowIndex].Cells[8].Text;
            txtDueAfter.Text = GVDetails.Rows[RowIndex].Cells[5].Text;
            txtAtATime.Text = GVDetails.Rows[RowIndex].Cells[11].Text;
            txtMonthLimit.Text = GVDetails.Rows[RowIndex].Cells[12].Text;
            txtWithDrawLimit.Text = GVDetails.Rows[RowIndex].Cells[14].Text;
            txtDeduction.Text = GVDetails.Rows[RowIndex].Cells[6].Text;
            ddlStatus.ClearSelection();
            ddlStatus.Items.FindByText(GVDetails.Rows[RowIndex].Cells[10].Text).Selected = true;
            ddlLimited.ClearSelection();
            ddlLimited.Items.FindByText(GVDetails.Rows[RowIndex].Cells[9].Text).Selected = true;
            ddlRequestLeave.ClearSelection();
            ddlRequestLeave.Items.FindByText(GVDetails.Rows[RowIndex].Cells[13].Text).Selected = true;
            ddlCF.ClearSelection();
            ddlCF.Items.FindByText(GVDetails.Rows[RowIndex].Cells[15].Text).Selected = true;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;

            ViewState["OldValue"] = GVDetails.Rows[RowIndex].Cells[2].Text;

            Popup3(true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtLeaveName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Add Leave Name";
            ViewState["OpenFlag"] = -1;
            return;
        }
        ViewState["OpenFlag"] = 0;

        if (txtLeaves.Text == "")
        {
            txtLeaves.Text = "0";
        }

        if (txtDaysBack.Text == "")
        {
            txtDaysBack.Text = "0";
        }

        if (txtDaysFuture.Text == "")
        {
            txtDaysFuture.Text = "0";
        }

        if (txtDueAfter.Text == "")
        {
            txtDueAfter.Text = "0";
        }

        if (txtAtATime.Text == "")
        {
            txtAtATime.Text = "0";
        }

        if (txtDeduction.Text == "")
        {
            txtDeduction.Text = "0";
        }

        conn.Open();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 15);
        cmd.Parameters.AddWithValue("@LeaveDescription", txtLeaveName.Text);
        cmd.Parameters.AddWithValue("@TotalLeaves", txtLeaves.Text);
        cmd.Parameters.AddWithValue("@Remarks", txtAliasName.Text);
        cmd.Parameters.AddWithValue("@frac", txtDeduction.Text);
        cmd.Parameters.AddWithValue("@lpd", txtDueAfter.Text);
        cmd.Parameters.AddWithValue("@backdays", txtDaysBack.Text);
        cmd.Parameters.AddWithValue("@futuredays", txtDaysFuture.Text);
        cmd.Parameters.AddWithValue("@limited", ddlLimited.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@lstatus", ddlStatus.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@atatime", txtAtATime.Text);
        cmd.Parameters.AddWithValue("@MonthLimit", txtMonthLimit.Text);
        cmd.Parameters.AddWithValue("@RequestLeave", ddlRequestLeave.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@WithdrawLimit", txtWithDrawLimit.Text);
        cmd.Parameters.AddWithValue("@CarryForward", ddlCF.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@LeaveCode", ViewState["LC"].ToString());
        cmd.Parameters.AddWithValue("@OldValue", ViewState["OldValue"].ToString());
        cmd.Parameters.AddWithValue("@empcode", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Leave Description Already exist";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Leave Details Updated successfully";
        }

        ViewState["OpenFlag"] = 0;
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 16);
        cmd.Parameters.AddWithValue("@LeaveCode", ViewState["LC"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Record deleted successfully";

            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Record cannot be deleted";
        }

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ViewState["OpenFlag"] != null && ViewState["OpenFlag"].ToString() == "-1")
        {
            Popup3(true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup();", true);
        }
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "No Record is selected to remove";
            return;
        }

        Popup2(true);
    }
    protected void btnYes2_Click(object sender, EventArgs e)
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
                    cmd = new SqlCommand("SP_Web_Masters", conn);
                    cmd.Parameters.AddWithValue("@calltype", 16);
                    cmd.Parameters.AddWithValue("@LeaveCode", row.Cells[1].Text);
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
                        flag = true;
                    }
                }
            }
        }

        if (flag == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Some Records can not be removed";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Record deleted successfully";
        }
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnNo2_Click(object sender, EventArgs e)
    {
        Popup2(false);
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "RemoveBackDrop();", true);
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

    }
    
}