using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class Pages_MarkLeaveNew : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;
    string days = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillEmployee();
            FillLeaveTypes();
            FillApprovingOfficer();
            FillHalfFull();
            string FromDate = DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy");
            string ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            txtFromDate.Text = FromDate + " - " + ToDate;

            GVLeaveDetails.DataSource = FillGrid();
            GVLeaveDetails.DataBind();
        }
    }
    protected void FillEmployee()
    {
        ddlEmployee.Items.Clear();
        ddlEmployeeLeave.Items.Clear();
        ListItem item = new ListItem("Select Employee", "0");
        ddlEmployee.Items.Add(item);
        ddlEmployeeLeave.Items.Add(item);

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 44);
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

        ddlEmployeeLeave.DataSource = dt;
        ddlEmployeeLeave.DataTextField = "EmployeeName";
        ddlEmployeeLeave.DataValueField = "E_Code";
        ddlEmployeeLeave.DataBind();
        dt.Dispose();
    }
    protected void FillApprovingOfficer()
    {
        ddlApprovingOfficer.Items.Clear();
        ListItem item = new ListItem("Select Approving Officer", "0");
        ddlApprovingOfficer.Items.Add(item);

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 41);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlApprovingOfficer.DataSource = dt;
        ddlApprovingOfficer.DataTextField = "EmployeeName";
        ddlApprovingOfficer.DataValueField = "E_Code";
        ddlApprovingOfficer.DataBind();
        dt.Dispose();
    }
    protected DataTable FillGrid()
    {
        string FilterCriteria = "";
        if (ddlStatus.SelectedItem.Value != "0")
        {
            FilterCriteria = FilterCriteria + " and ELM.LeaveStatus= '" + ddlStatus.SelectedItem.Text + "'";
        }

        if (ddlEmployee.SelectedItem.Value != "0")
        {
            FilterCriteria = FilterCriteria + " and ELM.E_Code=" + ddlEmployee.SelectedItem.Value;
        }

        if (ddlLeave.SelectedItem.Value != "0")
        {
            FilterCriteria = FilterCriteria + " and ELM.LeaveCode=" + ddlLeave.SelectedItem.Value;
        }

        string FromDate = DateTime.ParseExact(txtFromDate.Text.Split('-')[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
        string ToDate = DateTime.ParseExact(txtFromDate.Text.Split('-')[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 27);
        cmd.Parameters.AddWithValue("@FromDate", FromDate);
        cmd.Parameters.AddWithValue("@ToDate", ToDate);
        cmd.Parameters.AddWithValue("@Criteria", FilterCriteria);
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
    void Popup2(bool isDisplay)
    {
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup2();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup2();", true);
        }
    }
    void clear()
    {
        //FillEmployee();
        //FillLeaveTypes();
        //FillApprovingOfficer();
        //FillHalfFull();
        ddlEmployeeLeave.SelectedIndex = 0;
        txtDOL.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        txtStartDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy - dd/MM/yyyy");
        ddlLeaveCode.SelectedIndex = 0;
        ddlHalfFull.SelectedIndex = 0;
        ddlStatusLeave.SelectedIndex = 0;
        ddlApprovingOfficer.SelectedIndex = 0;
        txtReason.Text = "";
        txtRemarks.Text = "";
        txtContactNo.Text = "";
        txtAddress.Text = "";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ViewState["OpenFlag"] != null && ViewState["OpenFlag"].ToString() == "-1")
        {
            Popup3(true);
        }
        else
        {
            Popup(false);
        }
    }
    protected void btnYes2_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 43);
        cmd.Parameters.AddWithValue("@AttendanceCode", ViewState["AC"].ToString());
        cmd.Parameters.AddWithValue("@CreatedBy", Session["empCode"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();
        Popup(true);
        dlglbl.Text = "Record deleted Successfully";
        ViewState["OpenFlag"] = 0;
        GVLeaveDetails.DataSource = FillGrid();
        GVLeaveDetails.DataBind();
    }
    protected void btnNo2_Click(object sender, EventArgs e)
    {
        Popup2(false);
    }
    protected void FillLeaveTypes()
    {
        ddlLeave.Items.Clear();
        ddlLeaveCode.Items.Clear();
        ListItem item = new ListItem("Select Leave Type", "0");
        ddlLeave.Items.Add(item);
        ddlLeaveCode.Items.Add(item);

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 42);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlLeave.DataSource = dt;
        ddlLeave.DataTextField = "leaveDescription";
        ddlLeave.DataValueField = "leaveCode";
        ddlLeave.DataBind();

        ddlLeaveCode.DataSource = dt;
        ddlLeaveCode.DataTextField = "leaveDescription";
        ddlLeaveCode.DataValueField = "leaveCode";
        ddlLeaveCode.DataBind();
        dt.Dispose();
    }
    protected void btnCloseMain_Click(object sender, EventArgs e)
    {
        Popup3(false);
    }
    void Popup3(bool isDisplay)
    {
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup3();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup3();", true);
        }
    }
    protected bool ValidateInput()
    {
        bool flag = false;
        DateTime startDate;
        DateTime endDate;
        string Limited = "";
        if (ddlSession.SelectedItem.Value == "0")
        {
            Popup(true);
            dlglbl.Text = "Please Select Session";
            ViewState["OpenFlag"] = -1;
            return flag;
        }
        else if (ddlEmployeeLeave.SelectedItem.Value == "0")
        {
            Popup(true);
            dlglbl.Text = "Please Select Employee";
            ViewState["OpenFlag"] = -1;
            return flag;
        }
        else if (txtDOL.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Choose Date of Leave Application";
            ViewState["OpenFlag"] = -1;
            return flag;
        }
        else if (txtStartDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Choose From Date Range";
            ViewState["OpenFlag"] = -1;
            return flag;
        }
        txtStartDate.Text = txtStartDate.Text.Split(',')[0];
        string FromDate = DateTime.ParseExact(txtStartDate.Text.Split('-')[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
        string ToDate = DateTime.ParseExact(txtStartDate.Text.Split('-')[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

        startDate = Convert.ToDateTime(FromDate);
        endDate = Convert.ToDateTime(ToDate);

        days = ((endDate - startDate).TotalDays + 1).ToString();
        if (days == "1" && ddlHalfFull.SelectedItem.Text != "Full Day")
            days = ".5";

        if (Convert.ToDateTime(FromDate) > Convert.ToDateTime(ToDate))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Choose Valid Date Range";
            ViewState["OpenFlag"] = -1;
            return flag;
        }

        //else if (Convert.ToDateTime(ToDate) < Convert.ToDateTime(Session["LeavesessionFrom"].ToString()) || Convert.ToDateTime(ToDate) > Convert.ToDateTime(Session["LeavesessionTo"].ToString()))
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //    dlglbl.Text = "Please Select From Date For Current Session";
        //    ViewState["OpenFlag"] = 0;
        //    return flag;
        //}
        //else if (Convert.ToDateTime(ToDate) < Convert.ToDateTime(Session["LeavesessionFrom"].ToString()) || Convert.ToDateTime(ToDate) > Convert.ToDateTime(Session["LeavesessionTo"].ToString()))
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //    dlglbl.Text = "Please Select To Date For Current Session";
        //    ViewState["OpenFlag"] = 0;
        //    return flag;
        //}

        else if (ddlLeaveCode.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Leave Type";
            ViewState["OpenFlag"] = -1;
            return flag;
        }
        else if (ddlHalfFull.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Type";
            ViewState["OpenFlag"] = -1;
            return flag;
        }
        else if (ddlStatusLeave.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Leave Status";
            ViewState["OpenFlag"] = -1;
            return flag;
        }
        else if (ddlApprovingOfficer.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Approving Officer";
            ViewState["OpenFlag"] = -1;
            return flag;
        }

        double TotalDays = (endDate - startDate).TotalDays;
        TotalDays++;
        //Check For Medical Leave
        if (ddlLeaveCode.SelectedItem.Value == "2")
        {
            if (startDate > DateTime.Now.Date || endDate > DateTime.Now.Date)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
                dlglbl.Text = "Advance Medical Leave is not allowed";
                ViewState["OpenFlag"] = -1;
                return flag;
            }
        }

        ////Check for 2 CL in Academic Session
        //if (ddlLeaveCode.SelectedItem.Value == "1")
        //{
        //    cmd = new SqlCommand("SP_ApplyLeave", conn);
        //    cmd.Parameters.AddWithValue("@calltype", 4);
        //    cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    dt = new DataTable();
        //    da.Fill(dt);
        //    cmd.Dispose();
        //    da.Dispose();

        //    if (double.Parse(dt.Rows[0][0].ToString()) + double.Parse(days) > 2)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //        dlglbl.Text = "Only 2 CL Allowed per Academic Session";
        //        ViewState["OpenFlag"] = -1;
        //        return flag;
        //    }
        //}

        //// Check for GL
        //if (ddlLeaveCode.SelectedItem.Value == "3")
        //{
        //    cmd = new SqlCommand("SP_ApplyLeave", conn);
        //    cmd.Parameters.AddWithValue("@calltype", 2);
        //    cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
        //    cmd.Parameters.AddWithValue("@LFromDate", startDate);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    dt = new DataTable();
        //    da.Fill(dt);
        //    cmd.Dispose();
        //    da.Dispose();

        //    if (dt.Rows[0][0].ToString() == "1")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //        dlglbl.Text = "You can not apply for General Leave before 6 month from your joining.";
        //        ViewState["OpenFlag"] = -1;
        //        return flag;
        //    }
        //}

        ////Check for Short Leave

        //if (ddlLeaveCode.SelectedItem.Value == "9")
        //{
        //    cmd = new SqlCommand("SP_ApplyLeave", conn);
        //    cmd.Parameters.AddWithValue("@calltype", 19);
        //    cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
        //    cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        //    cmd.Parameters.AddWithValue("@FromDate", startDate);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    dt = new DataTable();
        //    da.Fill(dt);
        //    cmd.Dispose();
        //    da.Dispose();
        //    if (int.Parse(dt.Rows[0][0].ToString()) > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //        dlglbl.Text = "You can not apply for 2 short leave on same day";
        //        ViewState["OpenFlag"] = -1;
        //        return flag;
        //    }

        //    cmd = new SqlCommand("SP_ApplyLeave", conn);
        //    cmd.Parameters.AddWithValue("@calltype", 3);
        //    cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
        //    cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        //    cmd.Parameters.AddWithValue("@FromDate", startDate);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    dt = new DataTable();
        //    da.Fill(dt);
        //    cmd.Dispose();
        //    da.Dispose();
        //    if (double.Parse(dt.Rows[0][0].ToString()) >= 2)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //        dlglbl.Text = "Monthly limit reached for Short Leaves";
        //        ViewState["OpenFlag"] = -1;
        //        return flag;
        //    }
        //}


        ////Check for Summer And Winter Break
        //if (ddlLeaveCode.SelectedItem.Value == "4" || ddlLeaveCode.SelectedItem.Value == "5")
        //{
        //    cmd = new SqlCommand("SP_ApplyLeave", conn);
        //    cmd.Parameters.AddWithValue("@calltype", 5);
        //    cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        //    cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    dt = new DataTable();
        //    da.Fill(dt);
        //    cmd.Dispose();
        //    da.Dispose();

        //    if (dt.Rows.Count > 0)
        //    {
        //        if (dt.Rows[0][0].ToString() == "-1")
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //            dlglbl.Text = "Not Allowed";
        //            ViewState["OpenFlag"] = -1;
        //            return flag;
        //        }

        //        if (Convert.ToUInt64(dt.Rows[0][0].ToString()) > 4)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //            dlglbl.Text = "Not allowed due to more than 4 LWP in Academic Session";
        //            ViewState["OpenFlag"] = -1;
        //            return flag;
        //        }
        //    }
        //}
        ViewState["OpenFlag"] = 0;

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        if (dt.Rows.Count > 0)
        {
            Limited = dt.Rows[0][0].ToString();
        }



        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
        cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
        //cmd.Parameters.AddWithValue("@LeaveFrom", Convert.ToDateTime(Session["LeavesessionFrom"].ToString()).ToString("yyyy-MM-dd"));
        //cmd.Parameters.AddWithValue("@LeaveTo", Convert.ToDateTime(Session["LeavesessionTo"].ToString()).ToString("yyyy-MM-dd"));
        //cmd.Parameters.AddWithValue("@LeaveFrom", "2021-08-01");
        //cmd.Parameters.AddWithValue("@LeaveTo", "2022-07-31");
        cmd.Parameters.AddWithValue("@LeaveFrom", ddlSession.SelectedItem.Value + "-08-01");
        cmd.Parameters.AddWithValue("@LeaveTo", (Int32.Parse(ddlSession.SelectedItem.Value) + 1) + "-07-31");
        cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        if (Limited == "Y" && dt.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Employee Leave Structure Not Defined";
            ViewState["OpenFlag"] = -1;
            return flag;
        }


        if (dt.Rows.Count > 0)
        {
            if (double.Parse(days) > double.Parse(dt.Rows[0]["Total Leaves"].ToString()) - double.Parse(dt.Rows[0]["Days"].ToString()))
            {
                Popup(true);
                dlglbl.Text = "Balance Leaves Are " + (double.Parse(dt.Rows[0]["Total Leaves"].ToString()) - double.Parse(dt.Rows[0]["Days"].ToString())).ToString() + " Only ";
                ViewState["OpenFlag"] = -1;
                return flag;
            }

            cmd = new SqlCommand("SP_ApplyLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 8);
            cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();
        }
        else
        {
            cmd = new SqlCommand("SP_ApplyLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 9);
            cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();
        }

        if (double.Parse(dt.Rows[0]["BackDays"].ToString()) > 0)
        {
            if ((DateTime.Now.Date - Convert.ToDateTime(FromDate)).TotalDays > double.Parse(dt.Rows[0]["BackDays"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
                dlglbl.Text = "Not Allowed. Only Allowed " + (int.Parse(dt.Rows[0]["BackDays"].ToString()) - 1).ToString() + " Days Back";
                ViewState["OpenFlag"] = -1;
                return flag;
            }
        }

        if (double.Parse(dt.Rows[0]["FutureDays"].ToString()) > 0)
        {
            if (-(DateTime.Now.Date - Convert.ToDateTime(FromDate)).TotalDays > double.Parse(dt.Rows[0]["FutureDays"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
                dlglbl.Text = "Not Allowed. Only Allowed " + dt.Rows[0]["FutureDays"].ToString() + " Days in Future";
                ViewState["OpenFlag"] = -1;
                return flag;
            }
        }

        if ((Convert.ToDateTime(ToDate) - Convert.ToDateTime(FromDate)).TotalDays > double.Parse(dt.Rows[0]["atatime"].ToString()) && double.Parse(dt.Rows[0]["atatime"].ToString()) > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Can Not Take More Than " + dt.Rows[0]["atatime"].ToString() + " Leaves At A Time";
            ViewState["OpenFlag"] = -1;
            return flag;
        }

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 10);
        cmd.Parameters.AddWithValue("@FromDate", FromDate);
        cmd.Parameters.AddWithValue("@ToDate", ToDate);
        cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@LeaveType", ddlHalfFull.SelectedItem.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "During This Period !!! Already On Leave";
            ViewState["OpenFlag"] = -1;
            return flag;
        }
        //if (!ddlHalfFull.SelectedItem.Text.Contains("Half"))
        //{
        //    for (DateTime date2 = startDate; date2.Date <= endDate; date2 = date2.AddDays(1))
        //    {
        //        conn.Open();
        //        cmd = new SqlCommand("SP_ApplyLeave", conn);
        //        cmd.Parameters.AddWithValue("@calltype", 11);
        //        cmd.Parameters.AddWithValue("@FromDate", date2);
        //        cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        //        cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
        //        SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
        //        parm.Direction = ParameterDirection.ReturnValue;
        //        cmd.Parameters.Add(parm);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        int result = Convert.ToInt32(parm.Value);
        //        cmd.Dispose();
        //        conn.Close();

        //        if (result == 0)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //            dlglbl.Text = "Leave Cannot be Applied in Selected Combination";
        //            ViewState["OpenFlag"] = -1;
        //            return flag;
        //        }
        //    }
        //}

        //// Check SL And CL
        //if (ddlHalfFull.SelectedItem.Text.Contains("Half"))
        //{
        //    for (DateTime date2 = startDate; date2.Date <= endDate; date2 = date2.AddDays(1))
        //    {
        //        conn.Open();
        //        cmd = new SqlCommand("SP_ApplyLeave", conn);
        //        cmd.Parameters.AddWithValue("@calltype", 12);
        //        cmd.Parameters.AddWithValue("@FromDate", date2);
        //        cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        //        cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
        //        SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
        //        parm.Direction = ParameterDirection.ReturnValue;
        //        cmd.Parameters.Add(parm);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        int result = Convert.ToInt32(parm.Value);
        //        cmd.Dispose();
        //        conn.Close();

        //        if (result == 0)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //            dlglbl.Text = "Leave Cannot be Applied in Selected Combination";
        //            ViewState["OpenFlag"] = -1;
        //            return flag;
        //        }
        //    }
        //}

        ////Check Sandwitch for Full Day
        //if (ddlHalfFull.SelectedItem.Text == "Full Day")
        //{
        //    cmd = new SqlCommand("SP_ApplyLeave", conn);
        //    cmd.Parameters.AddWithValue("@calltype", 13);
        //    cmd.Parameters.AddWithValue("@FromDate", startDate);
        //    cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        //    cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    dt = new DataTable();
        //    da.Fill(dt);
        //    cmd.Dispose();
        //    da.Dispose();

        //    if (dt.Rows[0][0].ToString() == "1")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //        dlglbl.Text = "Leave Cannot be Applied Because of Sandwitch";
        //        ViewState["OpenFlag"] = -1;
        //        return flag;
        //    }

        //    cmd = new SqlCommand("SP_ApplyLeave", conn);
        //    cmd.Parameters.AddWithValue("@calltype", 14);
        //    //cmd.Parameters.AddWithValue("@FromDate", txtStartDate.Text);
        //    cmd.Parameters.AddWithValue("@ToDate", endDate);
        //    cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        //    cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    dt = new DataTable();
        //    da.Fill(dt);
        //    cmd.Dispose();
        //    da.Dispose();

        //    if (dt.Rows[0][0].ToString() == "1")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //        dlglbl.Text = "Leave Cannot be Applied Because of Sandwitch";
        //        ViewState["OpenFlag"] = -1;
        //        return flag;
        //    }
        //}

        ////Check Sandwitch for Half Day
        ////Check Sandwitch For Previous Dates
        ////Second Half is allowed

        //cmd = new SqlCommand("SP_ApplyLeave", conn);
        //cmd.Parameters.AddWithValue("@calltype", 15);
        //cmd.CommandType = CommandType.StoredProcedure;
        //da = new SqlDataAdapter();
        //da.SelectCommand = cmd;
        //dt = new DataTable();
        //da.Fill(dt);
        //cmd.Dispose();
        //da.Dispose();

        //if (dt.Rows.Count > 0)
        //{
        //    if (dt.Rows[0][0].ToString() != "Yes")
        //    {
        //        if (ddlHalfFull.SelectedItem.Text != "II Half")
        //        {
        //            cmd = new SqlCommand("SP_ApplyLeave", conn);
        //            cmd.Parameters.AddWithValue("@calltype", 13);
        //            cmd.Parameters.AddWithValue("@FromDate", endDate);
        //            cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        //            cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            da = new SqlDataAdapter();
        //            da.SelectCommand = cmd;
        //            dt = new DataTable();
        //            da.Fill(dt);
        //            cmd.Dispose();
        //            da.Dispose();

        //            if (dt.Rows[0][0].ToString() == "1")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //                dlglbl.Text = "Leave Cannot be Applied Because of Sandwitch";
        //                ViewState["OpenFlag"] = -1;
        //                return flag;
        //            }
        //        }

        //        //Check Sandwitch For Next Dates
        //        //First Half is allowed
        //        if (ddlHalfFull.SelectedItem.Text != "I Half")
        //        {
        //            cmd = new SqlCommand("SP_ApplyLeave", conn);
        //            cmd.Parameters.AddWithValue("@calltype", 14);
        //            cmd.Parameters.AddWithValue("@ToDate", endDate);
        //            cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        //            cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            da = new SqlDataAdapter();
        //            da.SelectCommand = cmd;
        //            dt = new DataTable();
        //            da.Fill(dt);
        //            cmd.Dispose();
        //            da.Dispose();

        //            if (dt.Rows[0][0].ToString() == "1")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //                dlglbl.Text = "Leave Cannot be Applied Because of Sandwitch";
        //                ViewState["OpenFlag"] = -1;
        //                return flag;
        //            }
        //        }
        //    }
        //}
        return true;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string FromDate = "";
        string ToDate = "";

        if (ddlLeaveCode.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Leave Type";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (txtDOL.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Choose Date of Leave Application";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (txtStartDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Choose From Date Range";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (ddlLeaveCode.SelectedItem.Value == "3" || ddlLeaveCode.SelectedItem.Value == "4")
        {
            FromDate = DateTime.ParseExact(txtStartDate.Text.Split('-')[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            ToDate = DateTime.ParseExact(txtStartDate.Text.Split('-')[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            SaveRequestLeave(FromDate, ToDate);
        }
        else if (ValidateInput())
        {
            string msg = "";
            if (ddlHalfFull.SelectedItem.Text == "Full Day")
            {
                msg = "Full Day";
            }
            else
            {
                msg = ddlHalfFull.SelectedItem.Text;
            }
            if (txtAddress.Text == "")
            {
                txtAddress.Text = "-";
            }

            if (txtContactNo.Text == "")
            {
                txtContactNo.Text = "-";
            }

            if (txtRemarks.Text == "")
            {
                txtRemarks.Text = "-";
            }

            if (txtReason.Text == "")
            {
                txtReason.Text = "-";
            }
            FromDate = DateTime.ParseExact(txtStartDate.Text.Split('-')[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            ToDate = DateTime.ParseExact(txtStartDate.Text.Split('-')[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            conn.Open();
            cmd = new SqlCommand("SP_ApplyLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 16);
            cmd.Parameters.AddWithValue("@E_Code", ddlEmployeeLeave.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@FromDate", FromDate);
            cmd.Parameters.AddWithValue("@ToDate", ToDate);
            cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@LeaveType", ddlHalfFull.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@Description", txtReason.Text);
            cmd.Parameters.AddWithValue("@FirstLevelOfficer", ddlApprovingOfficer.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@LastLevelOfficer", ddlApprovingOfficer.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@CurrentLeaveAt", ddlApprovingOfficer.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@IsAttachment", 0);
            cmd.Parameters.AddWithValue("@AttachmentPath", "");
            cmd.Parameters.AddWithValue("@LeaveStatus", ddlStatusLeave.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@ApplyDate", DateTime.ParseExact(txtDOL.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@CreatedBy", Session["empCode"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Leave Posted Successfully ";
            ViewState["OpenFlag"] = 0;

            GVLeaveDetails.DataSource = FillGrid();
            GVLeaveDetails.DataBind();
        }
    }

    private void SaveRequestLeave(string fromDate, string toDate)
    {
        if (ddlEmployeeLeave.SelectedItem.Value == "0")
        {
            Popup(true);
            dlglbl.Text = "Please Select Employee";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (txtDOL.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Choose Date of Leave Application";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (txtStartDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Choose From Date Range";
            ViewState["OpenFlag"] = -1;
            return;
        }
        txtStartDate.Text = txtStartDate.Text.Split(',')[0];
        string FromDate = DateTime.ParseExact(txtStartDate.Text.Split('-')[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
        string ToDate = DateTime.ParseExact(txtStartDate.Text.Split('-')[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

        if (Convert.ToDateTime(FromDate) > Convert.ToDateTime(ToDate))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Choose Valid Date Range";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (ddlLeaveCode.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Leave Type";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (ddlHalfFull.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Type";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (ddlStatusLeave.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Leave Status";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (ddlApprovingOfficer.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Approving Officer";
            ViewState["OpenFlag"] = -1;
            return;
        }

        double TotalDays = (DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture) - DateTime.ParseExact(FromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)).TotalDays + 1;

        if (TotalDays > 1 && (ddlHalfFull.SelectedItem.Value == "2" || ddlHalfFull.SelectedItem.Value == "3"))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Half Day Only Allowed For Single Day";
            return;
        }

        cmd = new SqlCommand("SP_RequestLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@E_Code", Session["empcode"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Already Made Request For This Period";
            return;
        }

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 10);
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(FromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@E_Code", Session["empcode"].ToString());
        cmd.Parameters.AddWithValue("@LeaveType", ddlLeaveCode.SelectedItem.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "During This Period !!! Already On Leave";
            ViewState["OpenFlag"] = 0;
            return;
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_RequestLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 16);
        cmd.Parameters.AddWithValue("@E_Code", Session["empcode"].ToString());
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@StatusLeave", ddlStatusLeave.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Description", txtRemarks.Text);
        cmd.Parameters.AddWithValue("@AssignedByName", ddlApprovingOfficer.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@LeaveType", ddlHalfFull.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@AssignedBy", ddlApprovingOfficer.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Contact", txtContactNo.Text);
        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = ddlLeaveCode.SelectedItem.Text + " Requested Successfully ";

        GVLeaveDetails.DataSource = FillGrid();
        GVLeaveDetails.DataBind();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        GVLeaveDetails.DataSource = FillGrid();
        GVLeaveDetails.DataBind();
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        clear();
        ddlEmployeeLeave.Enabled = true;
        txtDOL.Enabled = true;
        btnAdd.Visible = true;
        Popup3(true);
    }
    protected void GVLeaveDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string AC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            AC = GVLeaveDetails.Rows[RowIndex].Cells[0].Text;
            ViewState["AC"] = AC;
            Popup2(true);
        }
    }
    protected void ddlLeaveCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillHalfFull();
        Popup3(true);
    }

    protected void FillHalfFull()
    {
        ddlHalfFull.Items.Clear();
        ListItem item = new ListItem("Select Type", "0");
        ddlHalfFull.Items.Add(item);

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 18);
        cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlHalfFull.DataSource = dt;
        ddlHalfFull.DataTextField = "HalfFull";
        ddlHalfFull.DataValueField = "HalfFull";
        ddlHalfFull.DataBind();
        dt.Dispose();
    }
}