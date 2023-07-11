using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
public partial class Pages_ResponsibilityTransaction : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    int count = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
    }

    protected void ClearDetails()
    {
        ddlstatus.SelectedIndex = 0;        
        txtremark.Text = "";        
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
    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_ResponsibilityTransaction", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        return dt;
    }
    protected DataTable FillGrid1()
    {
        cmd = new SqlCommand("SP_ResponsibilityTransaction", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@ID", ViewState["ID"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        return dt;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlstatus.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select  Status');ShowPopup3();", true);
            return;
        }


        if (txtremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Task Remarks');ShowPopup3();", true);
            return;
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_ResponsibilityTransaction", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@TaskStatus", ddlstatus.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremark.Text);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@ID", ViewState["ID"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Task Submitted successfully','success');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Task can not be Submitted');ShowPopup3();", true);
            return;
        }

        GridView1.DataSource = FillGrid1();
        GridView1.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "ShowPopup3();", true);
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
    protected void btnCloseMain_Click(object sender, EventArgs e)
    {
        Popup3(false);
    }

    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string ID = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            ID = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["ID"] = ID;
            GridView1.DataSource = FillGrid1();
            GridView1.DataBind();
            Popup3(true);
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

    protected void btnNo2_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }


    protected void btnYes2_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_ResponsibilityTransaction", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
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

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string DC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            DC = GridView1.Rows[RowIndex].Cells[1].Text;
            ViewState["DC"] = DC;
            Popup1(true);
        }
    }
}