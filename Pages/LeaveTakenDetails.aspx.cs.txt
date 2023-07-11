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

public partial class Pages_LeaveTakenDetails : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    int count = 1;
    int count1 = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy")+" - "+DateTime.Now.ToString("dd/MM/yyyy");
            //FillInstitute();
            GVLeaveDetails.DataSource = FillGrid();
            GVLeaveDetails.DataBind();

            GVLeaveDetailsNew.DataSource = FillGridNew();
            GVLeaveDetailsNew.DataBind();
        }
    }

    protected void FillInstitute()
    {
        ddlInstitute.Items.Clear();
        ListItem item = new ListItem("Select Department", "0");
        ddlInstitute.Items.Add(item);

        cmd = new SqlCommand("SP_GetInstitute", conn);
        cmd.Parameters.AddWithValue("@e_code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlInstitute.DataSource = dt;
        ddlInstitute.DataTextField = "InstituteAlias";
        ddlInstitute.DataValueField = "InstituteCode";
        ddlInstitute.DataBind();
        dt.Dispose();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        GVLeaveDetails.DataSource = FillGrid();
        GVLeaveDetails.DataBind();

        GVLeaveDetailsNew.DataSource = FillGridNew();
        GVLeaveDetailsNew.DataBind();
    }

    public DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_GetLeaveTakenDetails", conn);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtFromDate.Text.Split('-')[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(txtFromDate.Text.Split('-')[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
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

    public DataTable FillGridNew()
    {
        cmd = new SqlCommand("SP_GetAllLeaveDetails", conn);
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtFromDate.Text.Split('-')[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(txtFromDate.Text.Split('-')[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
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
    
    protected void GVLeaveDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count.ToString();
            count++;
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=LeaveTakenDetails.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        if (GVLeaveDetails.Rows.Count > 0)
        {
            GVLeaveDetails.RenderControl(htw);
        }
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnExportNew_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=LeaveTakenDetailsAll.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        if (GVLeaveDetailsNew.Rows.Count > 0)
        {
            GVLeaveDetailsNew.RenderControl(htw);
        }
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void GVLeaveDetailsNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "print")
        {
            string AttendanceCode = "",Type="";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            AttendanceCode = GVLeaveDetailsNew.Rows[RowIndex].Cells[4].Text;
            Type = GVLeaveDetailsNew.Rows[RowIndex].Cells[3].Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", ("<script>openPopup('PrintLeaveApplicationFormNew.aspx?AttendanceCode=" + AttendanceCode + "&Type=" + Type + "')</script>"), false);
        }
    }

    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVLeaveDetails.DataSource = FillGrid();
        GVLeaveDetails.DataBind();

        GVLeaveDetailsNew.DataSource = FillGridNew();
        GVLeaveDetailsNew.DataBind();
    }

    protected void GVLeaveDetailsNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count1.ToString();
            count1++;

            if(e.Row.Cells[3].Text=="RL")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
                e.Row.ForeColor = System.Drawing.Color.White;
                e.Row.ToolTip = "Requested Leave";
            }
        }
    }
}