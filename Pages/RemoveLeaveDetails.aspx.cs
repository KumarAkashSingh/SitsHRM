﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
public partial class RemoveLeaveDetails : System.Web.UI.Page
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
            txtDateRange.Text = DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy") + " - " + DateTime.Now.ToString("dd/MM/yyyy");
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
        string FromDate = "", ToDate = "";
        if (txtDateRange.Text.Contains("-"))
        {
            FromDate = DateTime.ParseExact(txtDateRange.Text.Split('-')[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            ToDate = DateTime.ParseExact(txtDateRange.Text.Split('-')[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
        }
        if (FromDate != "" && ToDate != "")
        {
            Criteria += " and FromDate between '" + FromDate + "' and '" + ToDate + "'";
        }

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
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

    protected void GVLeaveDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count.ToString();
            count++;
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
    protected void GVLeaveDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string AC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            AC = GVLeaveDetails.Rows[RowIndex].Cells[2].Text;

            conn.Open();
            cmd = new SqlCommand("SP_ApplyLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 5);
            cmd.Parameters.AddWithValue("@AttendanceCode", AC);
            cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Leave deleted successfully";

            GVLeaveDetails.DataSource = FillGrid();
            GVLeaveDetails.DataBind();
        }
        if (e.CommandName == "can")
        {
            string AC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            AC = GVLeaveDetails.Rows[RowIndex].Cells[2].Text;

            conn.Open();
            cmd = new SqlCommand("SP_ApplyLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 6);
            cmd.Parameters.AddWithValue("@AttendanceCode", AC);
            cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Leave cancelled successfully";

            GVLeaveDetails.DataSource = FillGrid();
            GVLeaveDetails.DataBind();
        }
    }
}