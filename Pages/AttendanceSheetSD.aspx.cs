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
public partial class Pages_AttendanceSheetSD : System.Web.UI.Page
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
            //txtFromDate.Text = DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToShortDateString();
            txtFromDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            GVAttendanceSheet.DataSource = FillGrid();
            GVAttendanceSheet.DataBind();
            FillBranch();
            FillDepartment();
        }
    }
    public void FillBranch()
    {
        ddlBranch.Items.Clear();
        ListItem item = new ListItem("Select Branch", "0");
        ddlBranch.Items.Add(item);

        cmd = new SqlCommand("SP_Web_BioMetricAttendance", conn);
        cmd.Parameters.AddWithValue("@calltype", 8);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlBranch.DataSource = dt;
        ddlBranch.DataTextField = "BranchName";
        ddlBranch.DataValueField = "BranchID";
        ddlBranch.DataBind();
        dt.Dispose();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        GVAttendanceSheet.DataSource = FillGrid();
        GVAttendanceSheet.DataBind();
    }

    public DataTable FillGrid()
    {
        string Criteria = " and 1=1 ";
        if (ddlLeave.SelectedItem.Value == "1")
        {
            cmd = new SqlCommand("SP_AttendanceSheet_PresentEmployees", conn);
        }
        else if (ddlLeave.SelectedItem.Value == "2")
        {
            cmd = new SqlCommand("SP_AttendanceSheet_OnLeave", conn);
        }
        else if (ddlLeave.SelectedItem.Value == "3")
        {
            cmd = new SqlCommand("SP_AttendanceSheet_LateCommer", conn);
        }
        else if (ddlLeave.SelectedItem.Value == "4")
        {
            cmd = new SqlCommand("SP_AttendanceSheet_Absentees", conn);
        }
        else if (ddlLeave.SelectedItem.Value == "5")
        {
            cmd = new SqlCommand("SP_AttendanceSheet_AbsenteesLeave", conn);
        }
        else if (ddlLeave.SelectedItem.Value == "0")
        {
            cmd = new SqlCommand("SP_AttendanceSheet_All", conn);
        }

        if (ddlBranch.SelectedItem.Value != "0")
        {
            Criteria = " and EM.BranchID = " + ddlBranch.SelectedItem.Value;
        }
        if (ddlDepartment.SelectedItem.Value != "0")
        {
            Criteria += " and DM.DepartmentCode = " + ddlDepartment.SelectedItem.Value + "";
        }
        cmd.Parameters.AddWithValue("@Criteria", Criteria);
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture));
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=DailyBiometricAttendance.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        if (GVAttendanceSheet.Rows.Count > 0)
        {
            GVAttendanceSheet.RenderControl(htw);
        }
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    conn.Open();
    //    cmd = new SqlCommand("SP_Web_BioMetricAttendance", conn);
    //    cmd.Parameters.AddWithValue("@calltype", 11);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.ExecuteNonQuery();
    //    cmd.Dispose();
    //    conn.Close();

    //    Session["criteria"] = " 1=1";
    //    Session["date"] = "cast(dt as date) ='" + txtFromDate.Text + "'";
    //    if (ddlEmployeeType.SelectedItem.Value != "0")
    //    {
    //        Session["criteria"] = Session["criteria"].ToString() + " and em.designationcode in(select designationcode from designationmaster where designationtypecode =" + ddlEmployeeType.SelectedItem.Value + ")";
    //    }
    //    if (ddlDepartment.SelectedItem.Value != "0")
    //    {
    //        Session["criteria"] = Session["criteria"].ToString() + " and em.departmentcode =" + ddlDepartment.SelectedItem.Value + "";
    //    }

    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "pop", "<script>openPopup('PrintAttendanceSheet.aspx')</script>", false);
    //}

    protected void ddlEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();
    }

    protected void FillDepartment()
    {
        ddlDepartment.Items.Clear();
        ListItem item = new ListItem("Select Department", "0");
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

    protected void GVAttendanceSheet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count.ToString();
            count++;
        }
    }
}