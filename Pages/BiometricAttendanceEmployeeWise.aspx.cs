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

public partial class Forms_BiometricAttendanceEmployeeWise : System.Web.UI.Page
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
            txtFromDate.Text = DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            FillDropDownList();
            GVAttendance.DataSource = FillGrid();
            GVAttendance.DataBind();
        }
    }

    public void FillDropDownList()
    {
        cmd = new SqlCommand("SP_GetECode_HR", conn);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlUpload.DataSource = dt;
        ddlUpload.DataTextField = "Name";
        ddlUpload.DataValueField = "E_Code";
        ddlUpload.DataBind();
        dt.Dispose();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        GVAttendance.DataSource = FillGrid();
        GVAttendance.DataBind();
    }

    public DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_Web_BioMetricAttendance", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@E_Code", ddlUpload.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        lblRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " Record(s) Found]";
        return dt;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ddlUpload.SelectedItem.Value != "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", ("<script>openPopup('PrintBiometricAttendance.aspx?FD=" + txtFromDate.Text + "&TD=" + txtToDate.Text + "&EC=" + ddlUpload.SelectedItem.Value + "&EName=" + ddlUpload.SelectedItem.Text + "')</script>"), false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Employee";
        }
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


    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=BiometricAttendance.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        if (GVAttendance.Rows.Count > 0)
        {
            GVAttendance.RenderControl(htw);
        }
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}