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
public partial class Forms_LeaveDetailsNew : System.Web.UI.Page
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
            txtDateRange.Text = DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy")+" - "+DateTime.Now.ToString("dd/MM/yyyy");
            FillDropDownList();
            GVLeaveDetails.DataSource = FillGrid();
            GVLeaveDetails.DataBind();
        }
    }

    public void FillDropDownList()
    {
        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlLeaveType.DataSource = dt;
        ddlLeaveType.DataTextField = "leavedescription";
        ddlLeaveType.DataValueField = "leavecode";
        ddlLeaveType.DataBind();
        dt.Dispose();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        GVLeaveDetails.DataSource = FillGrid();
        GVLeaveDetails.DataBind();
    }

    public DataTable FillGrid()
    {
        string Criteria = "";
        if (ddlLeaveType.SelectedItem.Value != "0")
        {
            Criteria += " and ELM.LeaveCode=" + ddlLeaveType.SelectedItem.Value;
        }
        string FromDate = "",ToDate="";
        if(txtDateRange.Text.Contains("-"))
        {
            FromDate = DateTime.ParseExact(txtDateRange.Text.Split('-')[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            ToDate = DateTime.ParseExact(txtDateRange.Text.Split('-')[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
        }
        if(FromDate!="" && ToDate!="")
        {
            Criteria += " and FromDate between '"+FromDate+"' and '"+ToDate+"'";
        }

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 45);
        cmd.Parameters.AddWithValue("@Criteria", Criteria);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=LeaveDetails.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GVLeaveDetails.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    
    protected void GVLeaveDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count.ToString();
            count++;

            cmd = new SqlCommand("SP_ApplyLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 46);
            cmd.Parameters.AddWithValue("@LeaveCode", e.Row.Cells[1].Text);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();

            DateTime FromDate = DateTime.ParseExact(e.Row.Cells[5].Text, "dd MMM yyyy", CultureInfo.InvariantCulture);

            double TotalDays = 0;
            TotalDays = (DateTime.Now.Date - FromDate).TotalDays;

            DateTime ToDate = DateTime.ParseExact(e.Row.Cells[6].Text, "dd MMM yyyy", CultureInfo.InvariantCulture);
            if ((int.Parse(TotalDays.ToString()) <= int.Parse(dt.Rows[0][0].ToString())) && e.Row.Cells[8].Text == "Approved" && e.Row.Cells[11].Text == "N")
            {
                Button btn = (Button)e.Row.FindControl("btnWithDraw");
                e.Row.Cells[11].Enabled = true;
            }
            else
            {
                Button btn = (Button)e.Row.FindControl("btnWithDraw");
                btn.Visible = false;
            }

            //Check if Payslip is generated
            cmd = new SqlCommand("SP_ApplyLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 47);
            cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
            cmd.Parameters.AddWithValue("@FromDate", FromDate);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() != "0")
                {
                    Button btn = (Button)e.Row.FindControl("btnWithDraw");
                    btn.Enabled = false;
                    e.Row.ToolTip = "Can't withdraw, Payslip already generated!!";
                }
            }

            //if (int.Parse(dt.Rows[0][0].ToString()) != 0)
            //{
            //    double TotalDays = 0;
            //    TotalDays = (DateTime.Now.Date - FromDate).TotalDays;

            //    DateTime ToDate = DateTime.ParseExact(e.Row.Cells[6].Text, "dd MM yyyy", CultureInfo.InvariantCulture);
            //    if ((int.Parse(TotalDays.ToString()) <= int.Parse(dt.Rows[0][0].ToString())) && e.Row.Cells[8].Text == "Approved" && e.Row.Cells[11].Text == "N")
            //    {
            //        Button btn = (Button)e.Row.FindControl("btnWithDraw");
            //        e.Row.Cells[11].Enabled = true;
            //    }
            //    else
            //    {
            //        Button btn = (Button)e.Row.FindControl("btnWithDraw");
            //        btn.Visible = false;
            //    }
            //}
            //else
            //{
            //    Button btn = (Button)e.Row.FindControl("btnWithDraw");
            //    btn.Visible = false;
            //}
        }
    }
    protected void GVLeaveDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "withdraw")
        {
            string AC = "";
            GridViewRow oItem = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            AC = GVLeaveDetails.Rows[RowIndex].Cells[2].Text;
            ViewState["AC"] = AC;
            Popup1(true);
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ViewState["OpenFlag"].ToString() == "-1")
        {
            Popup1(true);
        }
        else
        {
            Popup(false);
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
    protected void btnYes1_Click(object sender, EventArgs e)
    {
        if (txtWithdrawReason.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Enter Reason";
            ViewState["OpenFlag"] = -1;
            return;
        }

        ViewState["OpenFlag"] = 0;

        conn.Open();
        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 48);
        cmd.Parameters.AddWithValue("@AttendanceCode", ViewState["AC"].ToString());
        cmd.Parameters.AddWithValue("@CreatedBy", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@WithdrawlRequestRemarks", txtWithdrawReason.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "WithDrawl Request Accepted";

        GVLeaveDetails.DataSource = FillGrid();
        GVLeaveDetails.DataBind();
    }
    protected void btnNo1_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }

}