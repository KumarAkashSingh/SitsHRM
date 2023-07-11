using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
public partial class Pages_HR_EmployeeLeaveBalanceReport : System.Web.UI.Page
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
            GVAttendanceSheet.DataSource = FillGrid();
            GVAttendanceSheet.DataBind();
            FillDepartment();
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        GVAttendanceSheet.DataSource = FillGrid();
        GVAttendanceSheet.DataBind();
    }

    public DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_EmployeeLeaveBalanceReport", conn);
        cmd.Parameters.AddWithValue("@CallType", 1);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        //cmd.Parameters.AddWithValue("@LeaveFrom", Convert.ToDateTime(Session["LeavesessionFrom"].ToString()).ToString("yyyy-MM-dd"));
        //cmd.Parameters.AddWithValue("@LeaveTo", Convert.ToDateTime(Session["LeavesessionTo"].ToString()).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@LeaveFrom", ddlSession.SelectedItem.Value + "-08-01");
        cmd.Parameters.AddWithValue("@LeaveTo", (Int32.Parse(ddlSession.SelectedItem.Value) +1)+ "-07-31");
        cmd.Parameters.AddWithValue("@DepartmentCode", ddlDepartment.SelectedItem.Value);
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
    protected void btnNo_Click(object sender, EventArgs e)
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

    protected void ddlEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();
    }

    protected void FillDepartment()
    {
        ddlDepartment.Items.Clear();
        ListItem item = new ListItem("View All", "0");
        ddlDepartment.Items.Add(item);

        cmd = new SqlCommand("SP_Web_BioMetricAttendance", conn);
        cmd.Parameters.AddWithValue("@calltype", 9);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDepartment.DataSource = dt;
        ddlDepartment.DataTextField = "DepartmentName";
        ddlDepartment.DataValueField = "DepartmentCode";
        ddlDepartment.DataBind();
        dt.Dispose();
    }
}