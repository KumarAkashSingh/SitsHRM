using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class Pages_SMSSendReport : System.Web.UI.Page
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
            //txtFromDate.Text = DateTime.Now.Date.ToShortDateString();
            //txtToDate.Text = DateTime.Now.Date.ToShortDateString();
            txtFromDate.Text = DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy") + " - " + DateTime.Now.ToString("dd/MM/yyyy");
            //FillDropDownList();
            GVReport.DataSource = FillGrid();
            GVReport.DataBind();

        }
    }

    public void FillDropDownList()
    {
        ddlCourse.Items.Clear();
        ListItem item = new ListItem("Select Course", "0");
        ddlCourse.Items.Add(item);

        cmd = new SqlCommand("SP_Web_Notices", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlCourse.DataSource = dt;
        ddlCourse.DataTextField = "classsectionname";
        ddlCourse.DataValueField = "classsectioncode";
        ddlCourse.DataBind();
        dt.Dispose();
    }


    protected void btnView_Click(object sender, EventArgs e)
    {
        GVReport.DataSource = FillGrid();
        GVReport.DataBind();
    }

    public DataTable FillGrid()
    {
        string Criteria = " and 1=1";

        cmd = new SqlCommand("SP_Web_Attendance", conn);
        cmd.Parameters.AddWithValue("@calltype", 197);
        if (ddlCourse.SelectedItem.Value != "0")
        {
            Criteria += " and (SM.StudentCode in (Select StudentCode from StudentMaster1 where CurrentClassSectionCode=" + ddlCourse.SelectedItem.Value + ") or ";
            Criteria += " SM.StudentCode in (Select SM.StudentCode from StudentMaster1 SM, CombinedClass CC where SM.CurrentClassSectionCode=CC.CurrentClassSectionCode ";
            Criteria += " and SM.StudentCode=CC.StudentCode and CC.CClassSectionCode=" + ddlCourse.SelectedItem.Value + "))";
        }

        Criteria += " and SS.sdate between '" +
            DateTime.ParseExact(txtFromDate.Text.Split('-')[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + "' and '" +
            DateTime.ParseExact(txtFromDate.Text.Split('-')[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + "' ";
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

    protected void GVReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count.ToString();
            count++;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVReport.DataSource = FillGrid();
        GVReport.DataBind();

    }
}