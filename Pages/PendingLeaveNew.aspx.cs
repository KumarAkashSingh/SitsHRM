using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Forms_PendingLeaveNew : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillGrid();
        }
    }

    public void FillGrid()
    {
        cmd = new SqlCommand("SP_GetPendingLeaveData", conn);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["UserCode"].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        lblRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " Record(s) Found]";

        GVPendingLeaves.DataSource = dt;
        GVPendingLeaves.DataBind();

    }
}