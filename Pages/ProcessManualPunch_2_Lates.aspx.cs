using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

public partial class Pages_ProcessManualPunch_2_Lates : System.Web.UI.Page
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
            FillYear();

            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
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
        cmd = new SqlCommand("SP_ManualPunch", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
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
    protected void GVDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVDetails.PageIndex = e.NewPageIndex;

        Session["PageIndex"] = e.NewPageIndex;
        if (Session["SortedView"] != null)
        {
            GVDetails.DataSource = Session["SortedView"];
            GVDetails.DataBind();
        }
        else
        {
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
    }
    public SortDirection direction
    {
        get
        {
            if (ViewState["directionState"] == null)
            {
                ViewState["directionState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["directionState"];
        }
        set
        {
            ViewState["directionState"] = value;
        }
    }
    protected void GVDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (direction == SortDirection.Ascending)
        {
            direction = SortDirection.Descending;
            sortingDirection = "Desc";

        }
        else
        {
            direction = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        DataView sortedView = new DataView(FillGrid());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        GVDetails.DataSource = sortedView;
        GVDetails.DataBind();
    }
    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       if (e.CommandName == "del")
        {
            string Month = "",Year="";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Month = GVDetails.Rows[RowIndex].Cells[3].Text;
            Year = GVDetails.Rows[RowIndex].Cells[2].Text;

            conn.Open();
            cmd = new SqlCommand("SP_ManualPunch", conn);
            cmd.Parameters.AddWithValue("@calltype", 6);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);
            cmd.Parameters.AddWithValue("@empCode", Session["e_code"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            conn.Close();

            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();

            dlglbl.Text = "Auto Manual Punches removed";
            Popup(true);

        }
    }
    protected void GVDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count.ToString();
            count++;
        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        Thread thd = new Thread(delegate ()
        {
            ProcessData();
        });
        thd.Start();
        
        dlglbl.Text = "Auto Manual Punches will get processed in sometime";
        Popup(true);
    }
    void ProcessData()
    {
        cmd = new SqlCommand("SP_ManualPunch", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@empCode", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        cmd = new SqlCommand("SP_ManualPunch", conn);
        cmd.Parameters.AddWithValue("@calltype", 8);
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@empCode", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
}