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

public partial class Forms_ApproveWithdrawlLeaveSDNew : System.Web.UI.Page
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
            FillApplyLeaves();
        }
    }
    protected void FillApplyLeaves()
    {
        GVApprovedLeaveDetails.DataSource = FillGrid();
        GVApprovedLeaveDetails.DataBind();
    }
    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_ApproveLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 15);
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
        return dt;
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlAction.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Action";
            return;
        }
        else if (GVApprovedLeaveDetails.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Nothing is to Approve";
            return;
        }

        if (txtRemarks.Text == "")
        {
            txtRemarks.Text = "-";
        }

        Popup(true);
        //FillApplyLeaves();

    }
    
    protected void GVApprovedLeaveDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "fetch")
        {
            string AttendanceCode = "", ECODE = "", EmployeeName = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            AttendanceCode = GVApprovedLeaveDetails.Rows[RowIndex].Cells[1].Text;
            ECODE = GVApprovedLeaveDetails.Rows[RowIndex].Cells[2].Text;
            EmployeeName = GVApprovedLeaveDetails.Rows[RowIndex].Cells[2].Text;
            ViewState["EmployeeName"] = EmployeeName;
            ViewState["AttendanceCode"] = AttendanceCode;
            ViewState["ECODE"] = ECODE;
            Popup1(true);

            DateTime MDate = DateTime.ParseExact(GVApprovedLeaveDetails.Rows[RowIndex].Cells[18].Text, "dd MMM yyyy", CultureInfo.InvariantCulture);
            DateTime TDate = DateTime.ParseExact(GVApprovedLeaveDetails.Rows[RowIndex].Cells[19].Text, "dd MMM yyyy", CultureInfo.InvariantCulture);
            ViewState["FD"] = MDate;
            ViewState["TD"] = TDate;

            ddlDate.Items.Clear();
            ListItem item = new ListItem("Select Date", "0");
            ddlDate.Items.Add(item);

            while (MDate <= TDate)
            {
                ddlDate.Items.Add(MDate.Date.ToString("yyyy-MM-dd"));
                MDate = MDate.AddDays(1);
            }

            ddlCourse.SelectedIndex = -1;
            ddlSubject.SelectedIndex = -1;
            ddlPeriod.SelectedIndex = -1;
            ddlBatch.SelectedIndex = -1;
            ddlDate.SelectedIndex = -1;
            ddlEFaculty.SelectedIndex = -1;
            ddlReason.SelectedIndex = -1;
            ClearCourses();
            ClearSubjects();
            ClearPeriods();
            ClearGroups();
            ClearEmployees();
            FillApplyLeaves();
        }
        if (e.CommandName == "view")
        {
            string MID = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            MID = GVApprovedLeaveDetails.Rows[RowIndex].Cells[2].Text;
            FillBalanceLeaves(MID);
            Popup3(true);
            FillApplyLeaves();
        }
    }
    void Popup1(bool isDisplay)
    {
        StringBuilder builder = new StringBuilder();
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup1();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup1();", true);
        }
    }
    protected void btnClose1_Click(object sender, EventArgs e)
    {
        Popup1(false);
        FillApplyLeaves();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ddlPeriod.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Period";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (ddlEFaculty.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Faculty";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (ddlDate.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Date";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (ddlReason.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Reason";
            ViewState["OpenFlag"] = -1;
            return;
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 50);
        cmd.Parameters.AddWithValue("@AttendanceCode", ViewState["AttendanceCode"].ToString());
        cmd.Parameters.AddWithValue("@TTCode", ViewState["TTCode"].ToString());
        cmd.Parameters.AddWithValue("@Subjectcode", 0);
        cmd.Parameters.AddWithValue("@E_Code", ddlEFaculty.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@CreatedBy", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@OfficeDutyCode", ddlReason.SelectedItem.Value);
        if (ddlReason.SelectedItem.Text == "Other")
        {
            cmd.Parameters.AddWithValue("@OfficeDutyRemarks", txtOfficeDutyRemarksME.Text);
        }
        else
        {
            cmd.Parameters.AddWithValue("@OfficeDutyRemarks", "");
        }
        SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
        parm.Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(parm);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        int result = Convert.ToInt32(parm.Value);
        cmd.Dispose();
        conn.Close();

        if (result == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Lecture Cannot Be Engaged. Attendance Marked";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (result == 2)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Lecture Already Engaged. Approval Pending";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (result == 3)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Lecture Already Engaged";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else
        {
            if (Session["SendSMSEngagement"].ToString() == "Yes")
            {
                string Class = ddlCourse.SelectedItem.Text;
                string Period = ddlPeriod.SelectedItem.Text;
                string TTDate = ddlDate.SelectedItem.Text;
                string E_Code = ddlEFaculty.SelectedItem.Value;
                string EmployeeName = ViewState["EmployeeName"].ToString();

                SendSMS(E_Code, Class, Period, TTDate, EmployeeName);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
                dlglbl.Text = "Lecture Engaged Successfully  and SMS Sent";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
                dlglbl.Text = "Lecture Engaged Successfully";
            }

        }

        FillApplyLeaves();
    }
    protected void SendSMS(string E_Code, string Class, string Period, string Date, string EmployeeName)
    {
        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 40);
        cmd.Parameters.AddWithValue("@E_Code", E_Code);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt1 = new DataTable();
        da.Fill(dt1);
        cmd.Dispose();
        da.Dispose();

        string Msg = "";
        Msg = "Respected Sir/Madam, You have new Engagement Request. \nEngagement By : " + EmployeeName + "\nCourse : " + Class + "\nPeriod : " + Period + "\nDate : " + Date;

        if (Session["EngagementResponse"].ToString() == "Yes")
        {
            Msg += "\nPlease Login to Choose appropriate action.";
        }

        if (dt1.Rows.Count > 0)
        {
            if (dt1.Rows[0][0].ToString().Length > 9)
            {
                SMSSettings();
                SendSMS obj = new SendSMS();
                //obj.SMSSend(dt1.Rows[0][0].ToString(), "Respected Sir/Madam, You have new Engagement Request for Approval. Please Login to Choose appropriate action. Thanks", Session["MSGUSERID"].ToString(), Session["MSGPASSWORD"].ToString(), Session["MSGSENDERID"].ToString(), Session["UserCode"].ToString(), Session["UserCode"].ToString(), Session["Name"].ToString(), Session["e_code"].ToString());
                obj.SMSSend(dt1.Rows[0][0].ToString(), Msg, Session["MSGUSERID"].ToString(), Session["MSGPASSWORD"].ToString(), Session["MSGSENDERID"].ToString(), Session["UserCode"].ToString(), Session["UserCode"].ToString(), Session["Name"].ToString(), Session["e_code"].ToString());
            }
        }
    }
    protected void SMSSettings()
    {
        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        Session["bodysms"] = dt.Rows[0]["bodysmsemployee"].ToString();
        Session["bodymail"] = dt.Rows[0]["bodymailemployee"].ToString();
        Session["MSGUSERID"] = dt.Rows[0]["msgid"].ToString();
        Session["MSGPASSWORD"] = dt.Rows[0]["msgpassword"].ToString();
        Session["MSGSENDERID"] = dt.Rows[0]["msgsenderId"].ToString();
    }
    protected DataTable FillGVEng()
    {
        cmd = new SqlCommand("SP_ApproveLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 9);
        cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(ViewState["FD"].ToString()));
        cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(ViewState["TD"].ToString()));
        cmd.Parameters.AddWithValue("@E_Code", ViewState["ECODE"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        return dt;
    }
    
    protected void btnYes_Click(object sender, EventArgs e)
    {
        int flag = 0;
        foreach (GridViewRow row in GVApprovedLeaveDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    string AttendanceCode = row.Cells[1].Text;
                    string E_Code = row.Cells[2].Text;
                    string NextAuthority = row.Cells[4].Text;

                    conn.Open();
                    cmd = new SqlCommand("SP_ApproveLeave", conn);
                    cmd.Parameters.AddWithValue("@calltype", 7);
                    cmd.Parameters.AddWithValue("@WithdrawlRequest", ddlAction.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
                    cmd.Parameters.AddWithValue("@WithdrawlApprovedRemarks", txtRemarks.Text);
                    cmd.Parameters.AddWithValue("@AttendanceCode", AttendanceCode);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                    flag++;
                }
            }
        }

        if (flag == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "No Record is selected";
            return;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Record updated successfully";

        FillApplyLeaves();
    }
    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeader = (CheckBox)GVApprovedLeaveDetails.HeaderRow.FindControl("chkHeader");

        foreach (GridViewRow row in GVApprovedLeaveDetails.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("chkCtrl");
            if (chkHeader.Checked && row.Enabled)
            {
                cbx.Checked = true;
            }
            else
            {
                cbx.Checked = false;
            }
        }
    }
    protected void No_Click(object sender, EventArgs e)
    {
        Popup(false);
    }

    void Popup(bool isDisplay)
    {
        StringBuilder builder = new StringBuilder();
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup2();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup2();", true);
            FillApplyLeaves();
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Popup(false);
    }

    protected void GVApprovedLeaveDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string AC = e.Row.Cells[1].Text;
            cmd = new SqlCommand("SP_CheckEngagementsNew", conn);
            cmd.Parameters.AddWithValue("@E_Code", e.Row.Cells[2].Text);
            cmd.Parameters.AddWithValue("@AttendanceCode", e.Row.Cells[1].Text);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();
            if (dt.Rows.Count > 0)
            {
                LinkButton btn = (LinkButton)e.Row.FindControl("LinkButton2");
                if (dt.Rows[0][0].ToString() == "0")
                {
                    btn.Text = "No Classes For Engagement";
                }
                else if (dt.Rows[0][0].ToString() == "1")
                {
                    btn.Text = "Engagements Pending";
                }
                else if (dt.Rows[0][0].ToString() == "2")
                {
                    btn.Text = "Engagements Marked";
                }
            }

            //if (e.Row.Cells[5].Text != Session["e_code"].ToString() && e.Row.Cells[4].Text != "&nbsp;")
            //{
            //    string hex = "#82AF6F";
            //    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(hex);
            //    e.Row.ForeColor = System.Drawing.Color.White;
            //}
        }
    }
    protected void ddlDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCourses();
        GVSubEngagemnt.DataSource = FillGVEng();
        GVSubEngagemnt.DataBind();
    }

    protected void ClearCourses()
    {
        ddlCourse.Items.Clear();
        ListItem item = new ListItem("Select Course", "0");
        ddlCourse.Items.Add(item);
    }
    protected void FillCourses()
    {
        ddlCourse.Items.Clear();
        ListItem item = new ListItem("Select Course", "0");
        ddlCourse.Items.Add(item);

        cmd = new SqlCommand("SP_ApproveLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 10);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["ECODE"].ToString());
        cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(ddlDate.SelectedItem.Text));
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
        Popup1(true);
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSubjects();
        Popup1(true);
    }
    protected void FillSubjects()
    {
        ddlSubject.Items.Clear();
        ListItem item = new ListItem("Select Subject", "0");
        ddlSubject.Items.Add(item);

        cmd = new SqlCommand("SP_ApproveLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 11);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["ECODE"].ToString());
        cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(ddlDate.SelectedItem.Text));
        cmd.Parameters.AddWithValue("@ClassSectionCode", ddlCourse.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlSubject.DataSource = dt;
        ddlSubject.DataTextField = "subjectdescription";
        ddlSubject.DataValueField = "Subjectcode";
        ddlSubject.DataBind();
        dt.Dispose();
        Popup1(true);
    }
    protected void ClearSubjects()
    {
        ddlSubject.Items.Clear();
        ListItem item = new ListItem("Select Subject", "0");
        ddlSubject.Items.Add(item);
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPeriods();
        FillGroups();
        FillExamDuty();
        Popup1(true);
    }
    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGroups();
        FillEmployees();
        Popup1(true);
    }
    protected void FillPeriods()
    {
        ddlPeriod.Items.Clear();
        ListItem item = new ListItem("Select Period", "0");
        ddlPeriod.Items.Add(item);

        cmd = new SqlCommand("SP_ApproveLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 12);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["ECODE"].ToString());
        cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(ddlDate.SelectedItem.Text));
        cmd.Parameters.AddWithValue("@ClassSectionCode", ddlCourse.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@SubjectCode", ddlSubject.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlPeriod.DataSource = dt;
        ddlPeriod.DataTextField = "periodname";
        ddlPeriod.DataValueField = "periodcode";
        ddlPeriod.DataBind();
        dt.Dispose();
        Popup1(true);
    }
    protected void ClearPeriods()
    {
        ddlPeriod.Items.Clear();
        ListItem item = new ListItem("Select Period", "0");
        ddlPeriod.Items.Add(item);
    }
    protected void FillGroups()
    {
        ddlBatch.Items.Clear();
        ListItem item = new ListItem("Select Batch", "0");
        ddlBatch.Items.Add(item);

        cmd = new SqlCommand("SP_ApproveLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 13);
        cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(ddlDate.SelectedItem.Text));
        cmd.Parameters.AddWithValue("@PeriodCode", ddlPeriod.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@ClassSectionCode", ddlCourse.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlBatch.DataSource = dt;
        ddlBatch.DataTextField = "groupname";
        ddlBatch.DataValueField = "Groupcode";
        ddlBatch.DataBind();
        dt.Dispose();
        Popup1(true);
    }
    protected void ClearGroups()
    {
        ddlBatch.Items.Clear();
        ListItem item = new ListItem("Select Batch", "0");
        ddlBatch.Items.Add(item);
    }
    protected void FillEmployees()
    {
        ddlEFaculty.Items.Clear();
        ListItem item = new ListItem("Select Engagement Faculty", "0");
        ddlEFaculty.Items.Add(item);

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 36);
        cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(ddlDate.SelectedItem.Text));
        cmd.Parameters.AddWithValue("@PeriodCode", ddlPeriod.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlEFaculty.DataSource = dt;
        ddlEFaculty.DataTextField = "EmployeeName";
        ddlEFaculty.DataValueField = "e_code";
        ddlEFaculty.DataBind();
        dt.Dispose();
    }
    protected void ClearEmployees()
    {
        ddlEFaculty.Items.Clear();
        ListItem item = new ListItem("Select Engagement Faculty", "0");
        ddlEFaculty.Items.Add(item);
    }
    protected void FillExamDuty()
    {
        ddlReason.Items.Clear();
        ListItem item = new ListItem("Select Reason", "0");
        ddlReason.Items.Add(item);

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 37);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlReason.DataSource = dt;
        ddlReason.DataTextField = "OfficialDuty";
        ddlReason.DataValueField = "ID";
        ddlReason.DataBind();
        dt.Dispose();
    }
    protected void SetTTCode()
    {
        cmd = new SqlCommand("SP_ApproveLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 14);
        cmd.Parameters.AddWithValue("@ClassSectionCode", ddlCourse.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(ddlDate.SelectedItem.Text));
        cmd.Parameters.AddWithValue("@PeriodCode", ddlPeriod.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@GroupCode", ddlBatch.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        if (dt.Rows.Count > 0)
        {
            ViewState["TTCode"] = dt.Rows[0][0].ToString();
        }
        else
        {
            ViewState["TTCode"] = 0;
        }
    }
    protected void ddlEFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetTTCode();
        Popup1(true);
    }

    protected void GVSubEngagemnt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string CC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            CC = GVSubEngagemnt.Rows[RowIndex].Cells[0].Text;
            ViewState["CC"] = CC;
            Popup10(true);
        }
    }
    protected void btnYes10_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_ApproveLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 8);
        cmd.Parameters.AddWithValue("@ENRecNo", ViewState["CC"].ToString());
        SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
        parm.Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(parm);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        int result = Convert.ToInt32(parm.Value);
        cmd.Dispose();
        conn.Close();

        if (result == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Record Removed Successfully";
            ViewState["OpenFlag"] = 0;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Record cannot be removed. Attendance Already Marked";
            ViewState["OpenFlag"] = 0;
        }

        GVSubEngagemnt.DataSource = FillGVEng();
        GVSubEngagemnt.DataBind();

        FillApplyLeaves();
    }
    protected void btnNo10_Click(object sender, EventArgs e)
    {
        Popup10(false);
    }
    void Popup10(bool isDisplay)
    {
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup10();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup10();", true);
        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Popup2(false);
    }
    void Popup2(bool isDisplay)
    {
        StringBuilder builder = new StringBuilder();
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup();", true);
            FillApplyLeaves();
        }
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
            FillApplyLeaves();
        }
    }
    protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReason.SelectedItem.Text == "Other")
        {
            txtOfficeDutyRemarksME.Enabled = true;
        }
        else
        {
            txtOfficeDutyRemarksME.Enabled = false;
        }

        Popup1(true);
    }
}