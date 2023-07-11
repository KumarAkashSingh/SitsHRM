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

public partial class Pages_AddWorkingDays2020 : System.Web.UI.Page
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
            FillBranch();
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }

    }

    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_WorkingDaysAugNov2020", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@InstituteCode", ddlInstitute.SelectedItem.Value);
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

    protected void FillBranch()
    {
        ddlInstitute.Items.Clear();
        ListItem item = new ListItem("Select Organisation", "0");
        ddlInstitute.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlInstitute.DataSource = dt;
        ddlInstitute.DataTextField = "ddlText";
        ddlInstitute.DataValueField = "ddlValue";
        ddlInstitute.DataBind();
        dt.Dispose();
    }


    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="del")
        {
            //Code goes here if want delete option
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach(GridViewRow row in GVDetails.Rows)
        {
            TextBox txtWorkingDays = row.FindControl("txtWorkingDays") as TextBox;

            conn.Open();
            cmd = new SqlCommand("SP_WorkingDaysAugNov2020", conn);
            cmd.Parameters.AddWithValue("@calltype", 2);
            cmd.Parameters.AddWithValue("@E_Code", row.Cells[0].Text);
            cmd.Parameters.AddWithValue("@TotalDays", txtWorkingDays.Text);
            cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Working days Updated successfully','success');$('.modal-backdrop').remove();", true);

    }

    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    //protected void btnYes_Click(object sender, EventArgs e)
    //{

    //}

    //protected void btnNo_Click(object sender, EventArgs e)
    //{

    //}

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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup1s();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup1();", true);
        }
    }
}