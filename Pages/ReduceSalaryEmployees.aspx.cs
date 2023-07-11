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

public partial class ReduceSalaryEmployees : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;
    int count = 1;
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

        cmd = new SqlCommand("SP_ExemptEmployees", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
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
            Criteria += "and EM.Status='" + ddlStatus.SelectedItem.Value + "'";
        }
        if (chkFilTer.Checked)
        {
            Criteria += "and EM.IsReducedSalary=1";
        }
        cmd = new SqlCommand("SP_ExemptEmployees", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
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
        Popup(false);
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
    protected void chkCtrl_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow currentrow = (GridViewRow)((CheckBox)sender).Parent.Parent.Parent.Parent;
        CheckBox cbx = default(CheckBox);
        cbx = (CheckBox)currentrow.FindControl("chkCtrl");
        
        conn.Open();
        cmd = new SqlCommand("SP_ExemptEmployees", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.Parameters.AddWithValue("@IsReducedSalary", cbx.Checked);
        cmd.Parameters.AddWithValue("@E_Code", currentrow.Cells[8].Text);
        //For Audit Trail
        //cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
        //cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
        //cmd.Parameters.AddWithValue("@EmpCode", Session["empCode"].ToString());
        //cmd.Parameters.AddWithValue("@UserName", Session["Name"].ToString());
        //cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();
    }

    protected void GVDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count.ToString();
            count++;
        }
    }
}