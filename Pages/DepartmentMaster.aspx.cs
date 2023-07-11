using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class Pages_DepartmentMaster : System.Web.UI.Page
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

            FillDepartmentType();
        }
    }

    protected void FillDepartmentType()
    {
        ddlDepartmentType.Items.Clear();
        ListItem item = new ListItem("Select Department", "0");
        ddlDepartmentType.Items.Add(item);

        cmd = new SqlCommand("SP_DepartmentTypeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDepartmentType.DataSource = dt;
        ddlDepartmentType.DataTextField = "ddlText";
        ddlDepartmentType.DataValueField = "ddlValue";
        ddlDepartmentType.DataBind();
        dt.Dispose();
    }

    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_DepartmentMaster", conn);
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
        txtDepartmentName.Text = "";
        txtDepartmentRemarks.Text = "";
        txtPreference.Text = "";
        ddlDepartmentType.SelectedIndex = 0;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtDepartmentName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Department');ShowPopup3();", true);
            return;
        }
        if (txtDepartmentRemarks.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Department Remarks');ShowPopup3();", true);
            return;
        }
        if (ddlDepartmentType.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Department');ShowPopup3();", true);
            return;
        }
        if (txtPreference.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Preference');ShowPopup3();", true);
            return;
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_DepartmentMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@DepartmentName", txtDepartmentName.Text);
        cmd.Parameters.AddWithValue("@DepartmentRemarks", txtDepartmentRemarks.Text);
        cmd.Parameters.AddWithValue("@DepartmentTypeCode", ddlDepartmentType.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Preference", txtPreference.Text);
        cmd.Parameters.AddWithValue("@EmpCode", 1);
        //For Audit Trail
        //cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
        //cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
        //cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        //cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
        //cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Department Already exist');ShowPopup3();", true);
            txtDepartmentName.Text = "";
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Department Added successfully','success');$('.modal-backdrop').remove();", true);
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
            string DC = "",OldValue="";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            DC = GVDetails.Rows[RowIndex].Cells[1].Text;
            OldValue = GVDetails.Rows[RowIndex].Cells[2].Text;
            ViewState["DC"] = DC;
            ViewState["OldValue"] = OldValue;
            ClearDetails();
            txtDepartmentName.Text = GVDetails.Rows[RowIndex].Cells[2].Text.Replace("&amp;","&");
            txtDepartmentRemarks.Text = GVDetails.Rows[RowIndex].Cells[3].Text.Replace("&amp;","&");
            if (ddlDepartmentType.Items.FindByText(GVDetails.Rows[RowIndex].Cells[4].Text) != null)
            {
                ddlDepartmentType.ClearSelection();
                ddlDepartmentType.Items.FindByText(GVDetails.Rows[RowIndex].Cells[4].Text).Selected = true;
            }
            txtPreference.Text = GVDetails.Rows[RowIndex].Cells[5].Text.Replace("&amp;", "&");

            btnAdd.Visible = false;
            btnUpdate.Visible = true;

            ViewState["OldValue"] = GVDetails.Rows[RowIndex].Cells[2].Text.Replace("&amp;", "&");
            Popup3(true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtDepartmentName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Department Name');ShowPopup3();", true);
            return;
        }
        if (txtDepartmentRemarks.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Department Remarks');ShowPopup3();", true);
            return;
        }
        if (ddlDepartmentType.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Department');ShowPopup3();", true);
            return;
        }
        if (txtPreference.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Preference');ShowPopup3();", true);
            return;
        }
        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_DepartmentMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@DepartmentCode", ViewState["DC"].ToString());
        cmd.Parameters.AddWithValue("@DepartmentName", txtDepartmentName.Text);
        cmd.Parameters.AddWithValue("@DepartmentRemarks", txtDepartmentRemarks.Text);
        cmd.Parameters.AddWithValue("@DepartmentTypeCode", ddlDepartmentType.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Preference", txtPreference.Text);
        cmd.Parameters.AddWithValue("@EmpCode", 1);
        cmd.Parameters.AddWithValue("@OldValue", ViewState["OldValue"].ToString());
        //For Audit Trail
        //cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
        //cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
        //cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        //cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
        //cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Department Updated successfully','success');$('.modal-backdrop').remove();", true);
            
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Department Already exist');ShowPopup3();", true);
            return;
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_DepartmentMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@DepartmentCode", ViewState["DC"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record deleted successfully','success');$('.modal-backdrop').remove();", true);
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record can not be deleted');$('.modal-backdrop').remove();", true);
        }

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ViewState["OpenFlag"]!=null && ViewState["OpenFlag"].ToString() == "-1")
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('No Record is selected to remove');$('.modal-backdrop').remove();", true);
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
                    cmd = new SqlCommand("SP_DepartmentMaster", conn);
                    cmd.Parameters.AddWithValue("@calltype", 4);
                    cmd.Parameters.AddWithValue("@DepartmentCode", row.Cells[1].Text);
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

        txtSearch.Focus();

    }
    
}