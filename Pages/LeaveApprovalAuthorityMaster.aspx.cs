using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class Pages_LeaveApprovalAuthorityMaster : System.Web.UI.Page
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
        cmd.Parameters.AddWithValue("@calltype", 17);
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
        txtLevel.Text = "";
        txtPreference.Text = "";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtLevel.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Level / Authority');ShowPopup3();", true);
        }
        if (txtPreference.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Preference');ShowPopup3();", true);
        }
        
        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 18);
        cmd.Parameters.AddWithValue("@LevelName", txtLevel.Text);
        cmd.Parameters.AddWithValue("@Preference", txtPreference.Text);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Level / Authority Already exist',);$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Level / Authority Added successfully','success');$('.modal-backdrop').remove();", true);
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string SID = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            SID = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["SID"] = SID;
            Popup1(true);
        }
        else if (e.CommandName == "modify")
        {
            string SID = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            SID = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["SID"] = SID;
            ClearDetails();
            txtLevel.Text = GVDetails.Rows[RowIndex].Cells[2].Text;
            txtPreference.Text = GVDetails.Rows[RowIndex].Cells[3].Text;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;

            ViewState["OldValue"] = GVDetails.Rows[RowIndex].Cells[2].Text;
            Popup3(true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtLevel.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Level / Authority');ShowPopup3();", true);
        }
        if (txtPreference.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Preference');ShowPopup3();", true);
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 19);
        cmd.Parameters.AddWithValue("@LevelName", txtLevel.Text);
        cmd.Parameters.AddWithValue("@Preference", txtPreference.Text);
        cmd.Parameters.AddWithValue("@LevelID", ViewState["SID"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Level / Authority Already exist');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Level / Authority Updated successfully','success');$('.modal-backdrop').remove();", true);
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 20);
        cmd.Parameters.AddWithValue("@LevelID", ViewState["SID"].ToString());
        cmd.Parameters.AddWithValue("@empcode", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        int result = cmd.ExecuteNonQuery();
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record cannot be deleted');$('.modal-backdrop').remove();", true);
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
        }
        ViewState["OpenFlag"] = 0;

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
                    cmd.Parameters.AddWithValue("@calltype", 20);
                    cmd.Parameters.AddWithValue("@LevelID", row.Cells[1].Text);
                    cmd.Parameters.AddWithValue("@empcode", Session["e_code"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    int result = cmd.ExecuteNonQuery();
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
        ViewState["OpenFlag"] = 0;
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
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

    }
    
}