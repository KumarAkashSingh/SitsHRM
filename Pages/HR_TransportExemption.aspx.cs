using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class Pages_HR_TransportExemption : System.Web.UI.Page
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

            FillYear();

            txtDuration.Attributes.Add("onkeypress", "return isNumberKey(event)");
        }
    }

    private void FillYear()
    {
        ddlYear.Items.Clear();
        int i = DateTime.Now.Year;
        for (int j = i; j > i - 5; j--)
        {
            ListItem item = new ListItem(j.ToString(), j.ToString());
            ddlYear.Items.Add(item);
        }
        if (ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()) != null)
        {
            ddlMonth.ClearSelection();
            ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
        }
    }

    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_TransportExemption", conn);
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
        Popup3(true);
    }
    protected void ClearDetails()
    {
        ddlMonth.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        txtDuration.Text = "";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Month');ShowPopup();", true);
        }
        else if (ddlYear.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Year";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (txtDuration.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Enter Duration In Miinute";
            ViewState["OpenFlag"] = -1;
            return;
        }


        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_TransportExemption", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@DurationInMinutes ", txtDuration.Text);
        //For Audit Trail
        //cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
        //cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
        //cmd.Parameters.AddWithValue("@EmpCode", Session["empCode"].ToString());
        //cmd.Parameters.AddWithValue("@UserName", Session["Name"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Already exist";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Added successfully";
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
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_TransportExemption", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@MaxID", ViewState["DC"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        int result = cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        if (result > 1)
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
                    cmd = new SqlCommand("SP_TransportExemption", conn);
                    cmd.Parameters.AddWithValue("@calltype", 3);
                    cmd.Parameters.AddWithValue("@MaxID", row.Cells[1].Text);
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