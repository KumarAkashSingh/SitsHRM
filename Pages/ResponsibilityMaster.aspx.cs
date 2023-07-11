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

public partial class ResponsibilityMaster : System.Web.UI.Page
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
            FillAssignedBy();
            FillTask();
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
    }
    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_ResponsibilityMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
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
        ddlAssignedBy.SelectedIndex = 0;
        ddlAssignedTo.SelectedIndex = 0;
        ddlTask.SelectedIndex = 0;
        txtremark.Text = "";
        txtDate.Text = "";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlAssignedBy.SelectedIndex==0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select  Assign By');ShowPopup3();", true);
            return;
        }
        if (ddlAssignedTo.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Assign To');ShowPopup3();", true);
            return;
        }
        if (ddlTask.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Task ID');ShowPopup3();", true);
            return;
        }

        if (txtremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Task Remarks');ShowPopup3();", true);
            return;
        }

        if (txtDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Task Date');ShowPopup3();", true);
            return;
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_ResponsibilityMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@TaskAssignby", ddlAssignedBy.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@TaskAssignto", ddlAssignedTo.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Taskid", ddlTask.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Remark", txtremark.Text);
        cmd.Parameters.AddWithValue("@TaskAssignedOn", DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Task Assign Already exist');ShowPopup3();", true);
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Task Assign successfully','success');$('.modal-backdrop').remove();", true);
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string DC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            DC = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["DC"] = DC;
            Popup1(true);
        }
        else if (e.CommandName == "modify")
        {
            string DC = "", OldValue = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            DC = GVDetails.Rows[RowIndex].Cells[1].Text;
            OldValue = GVDetails.Rows[RowIndex].Cells[2].Text;
            ViewState["DC"] = DC;
            ViewState["OldValue"] = OldValue;
            ClearDetails();
            FillAssignedBy();
            FillAssignedTo();
            FillTask();

            if (ddlAssignedBy.Items.FindByText(GVDetails.Rows[RowIndex].Cells[2].Text) != null)
            {
                ddlAssignedBy.ClearSelection();
                ddlAssignedBy.Items.FindByText(GVDetails.Rows[RowIndex].Cells[2].Text).Selected = true;
            }
            if (ddlAssignedTo.Items.FindByText(GVDetails.Rows[RowIndex].Cells[3].Text) != null)
            {
                ddlAssignedTo.ClearSelection();
                ddlAssignedTo.Items.FindByText(GVDetails.Rows[RowIndex].Cells[3].Text).Selected = true;
            }
            if (ddlTask.Items.FindByText(GVDetails.Rows[RowIndex].Cells[4].Text) != null)
            {
                ddlTask.ClearSelection();
                ddlTask.Items.FindByText(GVDetails.Rows[RowIndex].Cells[4].Text).Selected = true;
            }
            txtremark.Text = GVDetails.Rows[RowIndex].Cells[5].Text.Replace("&amp;", "&");
            if (GVDetails.Rows[RowIndex].Cells[6].Text!="")
            {
                txtDate.Text = DateTime.ParseExact(GVDetails.Rows[RowIndex].Cells[6].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            }
            btnAdd.Visible = false;
            btnUpdate.Visible = true;

            ViewState["OldValue"] = GVDetails.Rows[RowIndex].Cells[2].Text.Replace("&amp;", "&");
            Popup3(true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        if (ddlAssignedBy.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Assigned By');ShowPopup();", true);
            return;
        }
        if (ddlAssignedTo.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Assigned To');ShowPopup();", true);
            return;
        }
        if (ddlTask.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Task ID');ShowPopup();", true);
            return;
        }
        if (txtremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Remarks');ShowPopup();", true);
            return;
        }
        if (txtDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Date');ShowPopup();", true);
            return;
        }
        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_ResponsibilityMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@ID", ViewState["DC"].ToString());
        cmd.Parameters.AddWithValue("@TaskAssignby", ddlAssignedBy.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@TaskAssignto", ddlAssignedTo.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Taskid", ddlTask.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Remark", txtremark.Text);
        cmd.Parameters.AddWithValue("@TaskAssignedOn", DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@OldValue", ViewState["OldValue"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Task Assign Updated successfully','success');$('.modal-backdrop').remove();", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Task Assign Already exist');ShowPopup3();", true);
            return;
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_ResponsibilityMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@ID", ViewState["DC"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Task deleted successfully','success');$('.modal-backdrop').remove();", true);
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Task can not be deleted');$('.modal-backdrop').remove();", true);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('No Task is selected to remove');$('.modal-backdrop').remove();", true);
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
                    cmd = new SqlCommand("SP_ResponsibilityMaster", conn);
                    cmd.Parameters.AddWithValue("@calltype", 4);
                    cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
                    cmd.Parameters.AddWithValue("@ID", row.Cells[1].Text);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Some Records can not be removed');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record deleted successfully','success');$('.modal-backdrop').remove();", true);
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


    protected void ddlAssignedBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillAssignedTo();
        Popup3(true);
    }

    protected void FillAssignedBy()
    {
        ddlAssignedBy.Items.Clear();
        ListItem item = new ListItem("Select Assigned by", "0");
        ddlAssignedBy.Items.Add(item);

        cmd = new SqlCommand("SP_ResponsibilityMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlAssignedBy.DataSource = dt;
        ddlAssignedBy.DataTextField = "ddlText";
        ddlAssignedBy.DataValueField = "ddlValue";
        ddlAssignedBy.DataBind();
        dt.Dispose();
    }

    protected void FillAssignedTo()
    {
        ddlAssignedTo.Items.Clear();
        ListItem item = new ListItem("Select Assigned To", "0");
        ddlAssignedTo.Items.Add(item);

        cmd = new SqlCommand("SP_ResponsibilityMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@AssignedBy", ddlAssignedBy.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlAssignedTo.DataSource = dt;
        ddlAssignedTo.DataTextField = "ddlText";
        ddlAssignedTo.DataValueField = "ddlValue";
        ddlAssignedTo.DataBind();
        dt.Dispose();
    }

    protected void FillTask()
    {
        ddlTask.Items.Clear();
        ListItem item = new ListItem("Select Task", "0");
        ddlTask.Items.Add(item);

        cmd = new SqlCommand("SP_TaskMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlTask.DataSource = dt;
        ddlTask.DataTextField = "ddlText";
        ddlTask.DataValueField = "ddlValue";
        ddlTask.DataBind();
        dt.Dispose();
    }
}