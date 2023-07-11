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

public partial class Pages_UpdateLeaveBalance : System.Web.UI.Page
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
            FillYear();
            GVEmployee.DataSource = FillEmployee();
            GVEmployee.DataBind();
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
    }

    private void FillYear()
    {
        ddlYear.Items.Clear();
        ListItem item = new ListItem("Select Year", "0");
        ddlYear.Items.Add(item);
        for (int i = DateTime.Now.Year; i >= 2010; i--)
        {
            item = new ListItem(i.ToString(), i.ToString());
            ddlYear.Items.Add(item);
        }
        if (ddlYear.Items.FindByValue(DateTime.Now.Year.ToString()) != null)
        {
            ddlYear.ClearSelection();
            ddlYear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
        }
        if (ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()) != null)
        {
            ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
        }
    }

    private DataTable FillEmployee()
    {
        cmd = new SqlCommand("SP_EmployeeLeaveBalnce", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@InstituteCode", ddlInstitute.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        return dt;
    }

    protected DataTable FillGrid()
    {
        if (ViewState["E_Code"] == null)
        {
            lblName.Text = "";
            return null;
        }
        if (ddlYear.SelectedItem.Value == "0")
        {
            lblName.Text = "";
            return null;
        }
        if (ddlMonth.SelectedItem.Value == "0")
        {
            lblName.Text = "";
            return null;
        }
        cmd = new SqlCommand("SP_EmployeeLeaveBalnce", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["E_Code"].ToString());
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        if (dt.Rows.Count > 0)
        {
            lblName.Text = dt.Rows[0]["LabelText"].ToString();
        }
        else
        {
            lblName.Text = "";
        }
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

    protected void GVEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "OpenDetails")
        {
            string E_Code = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            E_Code = GVEmployee.Rows[RowIndex].Cells[0].Text;
            ViewState["E_Code"] = E_Code;
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }

    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVEmployee.DataSource = FillEmployee();
        GVEmployee.DataBind();
        ViewState["E_Code"] = null;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if(ddlYear.SelectedItem.Value=="0")
        {
            dlglbl.Text = "Please Select Year";
            Popup(true);
            return;
        }
        if(ddlMonth.SelectedItem.Value=="0")
        {
            dlglbl.Text = "Please Select Month";
            Popup(true);
            return;
        }
        if(GVDetails.Rows.Count==0)
        {
            dlglbl.Text = "NO record to update";
            Popup(true);
            return;
        }
        foreach (GridViewRow row in GVDetails.Rows)
        {
            TextBox txtTotalGranted = row.FindControl("txtTotalGranted") as TextBox;
            TextBox txtTotalAvailed = row.FindControl("txtTotalAvailed") as TextBox;

            conn.Open();
            cmd = new SqlCommand("SP_EmployeeLeaveBalnce", conn);
            cmd.Parameters.AddWithValue("@calltype", 3);
            cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@LeaveCode", row.Cells[1].Text);
            cmd.Parameters.AddWithValue("@E_Code", row.Cells[0].Text);
            cmd.Parameters.AddWithValue("@TotalGranted", txtTotalGranted.Text);
            cmd.Parameters.AddWithValue("@TotalAvailed", txtTotalAvailed.Text);
            cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
            cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
            cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
            cmd.Parameters.AddWithValue("@UserName", Session["UserName"].ToString());
            cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

        }

        Popup(true);
        dlglbl.Text = "Leave details updated successfully";
    }

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
}