using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Globalization;
public partial class Forms_ApproveRequestedLeaveSD : System.Web.UI.Page
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
            FillFilterStatus();
            FillEmployee();

            int i = 0;
            int j = 1;
            while (j <= 10)
            {
                int Year = DateTime.Now.Year - i;
                ListItem item = new ListItem(Year.ToString(), Year.ToString());
                ddlYear.Items.Add(item);
                i++;
                j++;
            }
            ddlMonth.ClearSelection();
            ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
            ddlYear.ClearSelection();
            ddlYear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;

            txtDateofApproval.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            FillApplyLeaves();
        }
    }
    protected void FillFilterStatus()
    {
        ddlFilterStatus.Items.Clear();
        ListItem item = new ListItem("Select Status", "0");
        ddlFilterStatus.Items.Add(item);

        cmd = new SqlCommand("SP_ApproveLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlFilterStatus.DataSource = dt;
        ddlFilterStatus.DataTextField = "LeaveStatus";
        ddlFilterStatus.DataValueField = "LeaveStatus";
        ddlFilterStatus.DataBind();
        dt.Dispose();
    }
    protected void FillEmployee()
    {
        ddlEmployee.Items.Clear();
        ListItem item = new ListItem("Select Employee", "0");
        ddlEmployee.Items.Add(item);

        cmd = new SqlCommand("SP_ApproveLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["UserCode"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlEmployee.DataSource = dt;
        ddlEmployee.DataTextField = "EmployeeName";
        ddlEmployee.DataValueField = "E_Code";
        ddlEmployee.DataBind();
        dt.Dispose();
    }
    protected void FillApplyLeaves()
    {
        GVApprovedLeaveDetails.DataSource = FillGrid();
        GVApprovedLeaveDetails.DataBind();
        //RefreshGrid();
    }
    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_RequestLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 9);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@Status", ddlFilterStatus.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@EmployeeCode", ddlEmployee.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedItem.Value);
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int flag = 0;
        if (txtDateofApproval.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Enter Date of Approval";
            ViewState["OpenFlag"] = 0;
            return;
        }
        else if (ddlAction.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Action";
            ViewState["OpenFlag"] = 0;
            return;
        }
        else if (GVApprovedLeaveDetails.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Nothing is to Approve";
            ViewState["OpenFlag"] = 0;
            return;
        }

        foreach (GridViewRow row in GVApprovedLeaveDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    cmd = new SqlCommand("SP_GetApprovalOfficers", conn);
                    cmd.Parameters.AddWithValue("@calltype", 2);
                    cmd.Parameters.AddWithValue("@E_Code", row.Cells[3].Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    dt1 = new DataTable();
                    da.Fill(dt1);
                    cmd.Dispose();
                    da.Dispose();

                    if (dt1.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
                        dlglbl.Text = "No Approval Authority Defined";
                        ViewState["OpenFlag"] = 0;
                        return;
                    }

                    string LeaveCode = row.Cells[4].Text;
                    DateTime To_dt = DateTime.ParseExact(row.Cells[7].Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    DateTime From_dt = DateTime.ParseExact(row.Cells[6].Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                    //double TotalDays = (Convert.ToDateTime(row.Cells[8].Text) - Convert.ToDateTime(row.Cells[7].Text)).TotalDays + 1;
                    double TotalDays = (To_dt - From_dt).TotalDays + 1;

                    cmd = new SqlCommand("SP_RequestLeave", conn);
                    cmd.Parameters.AddWithValue("@calltype", 11);
                    cmd.Parameters.AddWithValue("@LeaveCode", LeaveCode);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    dt = new DataTable();
                    da.Fill(dt);
                    cmd.Dispose();
                    da.Dispose();


                    if (dt1.Rows[0][0].ToString() != row.Cells[5].Text)
                    {
                        if (ddlAction.SelectedItem.Value == "1")
                        {
                            conn.Open();
                            cmd = new SqlCommand("SP_RequestLeave", conn);
                            cmd.Parameters.AddWithValue("@calltype", 12);
                            cmd.Parameters.AddWithValue("@AttendanceCode", row.Cells[2].Text);
                            cmd.Parameters.AddWithValue("@Markto", dt1.Rows[0][0].ToString());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                        else
                        {
                            conn.Open();
                            cmd = new SqlCommand("SP_RequestLeave", conn);
                            cmd.Parameters.AddWithValue("@calltype", 13);
                            cmd.Parameters.AddWithValue("@AttendanceCode", row.Cells[2].Text);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }
                    else
                    {
                        if (ddlAction.SelectedItem.Value == "1")
                        {
                            if (row.Cells[8].Text != "Full Day")
                            {
                                TotalDays = 0.5;
                            }

                            CheckBox cbx = (CheckBox)row.Cells[18].FindControl("chkFullDay");
                            
                            if (dt.Rows[0]["RequestLeave"].ToString() == "YD")
                            {
                                conn.Open();
                                cmd = new SqlCommand("SP_RequestLeave", conn);
                                cmd.Parameters.AddWithValue("@calltype", 14);
                                cmd.Parameters.AddWithValue("@AttendanceCode", row.Cells[2].Text);
                                cmd.Parameters.AddWithValue("@E_Code", row.Cells[3].Text);
                                cmd.Parameters.AddWithValue("@LeaveCode", LeaveCode);
                                cmd.Parameters.AddWithValue("@TotalLeaves", TotalDays);
                                cmd.Parameters.AddWithValue("@Frac", dt.Rows[0]["frac"].ToString());
                                cmd.Parameters.AddWithValue("@Lpd", dt.Rows[0]["lpd"].ToString());
                                cmd.Parameters.AddWithValue("@FromDate", Session["LeaveSessionFrom"]);
                                cmd.Parameters.AddWithValue("@ToDate", Session["LeaveSessionTo"]);
                                cmd.Parameters.AddWithValue("@Backdays", dt.Rows[0]["backdays"].ToString());
                                cmd.Parameters.AddWithValue("@Futuredays", dt.Rows[0]["futuredays"].ToString());
                                cmd.Parameters.AddWithValue("@atatime", dt.Rows[0]["atatime"].ToString());
                                if(cbx.Checked)
                                {
                                    cmd.Parameters.AddWithValue("@LeaveType", "Full Day");
                                }
                                
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                conn.Close();
                            }
                            else
                            {
                                string msg = "";
                                if (row.Cells[8].Text == "Full Day")
                                {
                                    msg = "Full Day";
                                }
                                else
                                {
                                    msg = row.Cells[8].Text;
                                }

                                conn.Open();
                                cmd = new SqlCommand("SP_RequestLeave", conn);
                                cmd.Parameters.AddWithValue("@calltype", 15);
                                cmd.Parameters.AddWithValue("@Attendancecode", row.Cells[2].Text);
                                cmd.Parameters.AddWithValue("@E_Code", row.Cells[3].Text);
                                cmd.Parameters.AddWithValue("@FromDate", From_dt);
                                cmd.Parameters.AddWithValue("@ToDate", To_dt);
                                cmd.Parameters.AddWithValue("@LeaveCode", LeaveCode);
                                cmd.Parameters.AddWithValue("@Description", row.Cells[13].Text);
                                cmd.Parameters.AddWithValue("@LeaveType", msg);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                conn.Close();
                            }
                        }
                        else
                        {
                            conn.Open();
                            cmd = new SqlCommand("SP_RequestLeave", conn);
                            cmd.Parameters.AddWithValue("@calltype", 13);
                            cmd.Parameters.AddWithValue("@Attendancecode", row.Cells[2].Text);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }

                    flag++;
                }
            }
        }

        if (flag == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "No Record is selected";
            ViewState["OpenFlag"] = 0;
            return;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Record updated successfully";
        ViewState["OpenFlag"] = 0;

        FillApplyLeaves();
    }
    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeader = (CheckBox)GVApprovedLeaveDetails.HeaderRow.FindControl("chkHeader");

        foreach (GridViewRow row in GVApprovedLeaveDetails.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("chkCtrl");
            if (chkHeader.Checked)
            {
                cbx.Checked = true;
            }
            else
            {
                cbx.Checked = false;
            }
        }
    }
    void Popup(bool isDisplay)
    {
        StringBuilder builder = new StringBuilder();
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
    protected void btnClose1_Click(object sender, EventArgs e)
    {
        Popup3(false);
    }
    
    protected void GVApprovedLeaveDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view1")
        {
            string MID = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            MID = GVApprovedLeaveDetails.Rows[RowIndex].Cells[3].Text;


            FillBalanceLeaves(MID);
            Popup3(true);
            //RefreshGrid();
            GVApprovedLeaveDetails.DataSource = FillGrid();
            GVApprovedLeaveDetails.DataBind();
        }
    }
    protected void FillBalanceLeaves(string MID)
    {
        cmd = new SqlCommand("SP_GetBalanceLeaves", conn);
        cmd.Parameters.AddWithValue("@E_Code", MID);
        cmd.Parameters.AddWithValue("@LeaveFrom", Convert.ToDateTime(Session["LeavesessionFrom"].ToString()).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@LeaveTo", Convert.ToDateTime(Session["LeavesessionTo"].ToString()).ToString("yyyy-MM-dd"));
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        GVLeave.DataSource = dt;
        GVLeave.DataBind();
    }
    void Popup3(bool isDisplay)
    {
        StringBuilder builder = new StringBuilder();
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup3();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup3();", true);
           //RefreshGrid();
        }
    }
    protected void ddlFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVApprovedLeaveDetails.DataSource = FillGrid();
        GVApprovedLeaveDetails.DataBind();
        FillApplyLeaves();
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVApprovedLeaveDetails.DataSource = FillGrid();
        GVApprovedLeaveDetails.DataBind();
        FillApplyLeaves();
    }

    protected void GVApprovedLeaveDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            cmd = new SqlCommand("SP_RequestLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 10);
            cmd.Parameters.AddWithValue("@E_Code", e.Row.Cells[3].Text);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt1 = new DataTable();
            da.Fill(dt1);
            cmd.Dispose();
            da.Dispose();

            if (dt1.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
                dlglbl.Text = "No Approval Authority Defined";
                ViewState["OpenFlag"] = 0;
                return;
            }

            if (dt1.Rows[0][0].ToString() != e.Row.Cells[5].Text)
            {
                e.Row.Cells[17].Enabled = false;
            }

            if (e.Row.Cells[16].Text == "&nbsp;")
            {
                e.Row.Cells[17].Text = "No File";
            }

            CheckBox cbx = (CheckBox)e.Row.Cells[18].FindControl("chkFullDay");
            if (e.Row.Cells[12].Text.Contains("CPL") || e.Row.Cells[12].Text.Contains("Compensatory"))
            {
                if (e.Row.Cells[8].Text.Contains("Half"))
                {
                    cbx.Visible = true;
                }
                else
                {
                    cbx.Visible = false;
                }
            }
            else
            {
                cbx.Visible = false;
            }
        }
    }
}