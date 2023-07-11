using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class Pages_UpdateStatusEmployee : System.Web.UI.Page
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

            FillBranch();
            ViewState["OpenFlag"] = 0;
        }
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
        string Criteria = "";
        if (ddlBranch.SelectedItem.Value != "0")
        {
            Criteria += "and EM.BranchID=" + ddlBranch.SelectedItem.Value;
        }

        if (ddlStatus.SelectedItem.Value != "0")
        {
            Criteria += "and EM.Status='" + ddlStatus.SelectedItem.Value+"'";
        }
        if(chkFilTerByDate.Checked)
        {
            string FromDate = DateTime.Now.ToString("yyyy-MM-dd");
            string ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            Criteria += "and cast(LOE.dol as date) between '"+FromDate+"' and '"+ToDate+"'";
        }
        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 14);
        cmd.Parameters.AddWithValue("@Criteria", Criteria);
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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ViewState["OpenFlag"].ToString() == "-1")
        {
            Popup3(true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup();", true);
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
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
    protected void btnUpdateStatus_Click(object sender, EventArgs e)
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
        txtDateStatus.Text = "";
        ddlStatusChanged.ClearSelection();
        Popup3(true);
    }
    protected void btnChangeStatus_Click(object sender, EventArgs e)
    {
        if (txtDateStatus.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Date');ShowPopup3();", true);
        }
        if (ddlStatusChanged.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Status');ShowPopup3();", true);
        }
        ViewState["OpenFlag"] = 0;
        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    conn.Open();
                    cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
                    cmd.Parameters.AddWithValue("@calltype", 15);
                    cmd.Parameters.AddWithValue("@Status", ddlStatusChanged.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@empcode", row.Cells[3].Text);
                    cmd.Parameters.AddWithValue("@DateofStatus", DateTime.ParseExact(txtDateStatus.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Reason", txtReason.Text);
                    cmd.Parameters.AddWithValue("@loginname", Session["userName"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Status Updated successfully','success');$('.modal-backdrop').remove();", true);

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
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

    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName== "viewphoto")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[3].Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", ("<script>openPopup('ViewImage.aspx?Code=" + EC + "&Type=E')</script>"), false);
        }
    }
}