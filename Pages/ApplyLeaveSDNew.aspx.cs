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

public partial class Forms_ApplyLeaveSDNew : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;
    string days = "";
    int count = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillLeaveTypes();
            FillRecommendingOfficer();
            FillApprovingOfficer();
            FillBalanceLeaves();
            FillApplyLeaves();

            //GVEngagement.DataSource = FillEngagement();
            //GVEngagement.DataBind();
        }
    }
    protected void FillLeaveTypes()
    {
        ddlLeaveCode.Items.Clear();
        ListItem item = new ListItem("Select Leave Type", "0");
        ddlLeaveCode.Items.Add(item);

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlLeaveCode.DataSource = dt;
        ddlLeaveCode.DataTextField = "leaveDescription";
        ddlLeaveCode.DataValueField = "leaveCode";
        ddlLeaveCode.DataBind();
        dt.Dispose();
    }
    protected void FillRecommendingOfficer()
    {
        ddlRecommendingOfficer.Items.Clear();
        ListItem item = new ListItem("Select Recommending Officer", "0");
        ddlRecommendingOfficer.Items.Add(item);

        cmd = new SqlCommand("SP_GetApprovalOfficers", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlRecommendingOfficer.DataSource = dt;
        ddlRecommendingOfficer.DataTextField = "EmployeeName";
        ddlRecommendingOfficer.DataValueField = "E_Code";
        ddlRecommendingOfficer.DataBind();
        dt.Dispose();

        if (ddlRecommendingOfficer.Items.Count > 1)
            ddlRecommendingOfficer.SelectedIndex = 1;
    }
    protected void FillApprovingOfficer()
    {
        ddlApprovingOfficer.Items.Clear();
        ListItem item = new ListItem("Select Approving Officer", "0");
        ddlApprovingOfficer.Items.Add(item);

        cmd = new SqlCommand("SP_GetApprovalOfficers", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
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

        if (ddlApprovingOfficer.Items.Count > 1)
            ddlApprovingOfficer.SelectedIndex = 1;
    }
    protected void FillBalanceLeaves()
    {
        cmd = new SqlCommand("SP_GetBalanceLeaves", conn);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
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
    protected void FillApplyLeaves()
    {
        GVApplyLeave.DataSource = FillGrid();
        GVApplyLeave.DataBind();
    }
    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 20);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();


        //-----Just for testing 
        dt.Columns.Add("Attachment");
        foreach (DataRow r in dt.Rows)
        {
            r["Attachment"] = "<a href=\"javascript: openPopup('../../Files/LeaveDocument/LeaveDocument_16_18ba772b-3595-4785-b762-99322f009989.jpg')\">View</a>";
        }
        //-----Just for testing
        return dt;
    }
    protected void ddlLeaveCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillHalfFull();
    }
    protected void FillHalfFull()
    {
        ddlHalfFull.Items.Clear();
        ListItem item = new ListItem("Select Type", "0");
        ddlHalfFull.Items.Add(item);

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 21);
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime startDate;
        DateTime endDate;
        string Limited = "";
        if (txtStartDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Please Enter Leave From Date";
            ViewState["OpenFlag"] = 0;
            return;
        }
        else if (txtEndDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Please Enter Leave To Date";
            ViewState["OpenFlag"] = 0;
            return;
        }
        else if (DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Please Select Valid Date Range";
            ViewState["OpenFlag"] = 0;
            return;
        }
        else if (DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) < Convert.ToDateTime(Session["LeavesessionFrom"].ToString()) || DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) > Convert.ToDateTime(Session["LeavesessionTo"].ToString()))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Please Select From Date For Current Session";
            ViewState["OpenFlag"] = 0;
            return;
        }
        else if (DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) < Convert.ToDateTime(Session["LeavesessionFrom"].ToString()) || DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) > Convert.ToDateTime(Session["LeavesessionTo"].ToString()))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Please Select To Date For Current Session";
            ViewState["OpenFlag"] = 0;
            return;
        }

        if (ddlLeaveCode.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Please Select Leave Type";
            ViewState["OpenFlag"] = 0;
            return;
        }
        else if (ddlHalfFull.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Please Select Type";
            ViewState["OpenFlag"] = 0;
            return;
        }
        else if (ddlRecommendingOfficer.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Please Select Recommending Officer";
            ViewState["OpenFlag"] = 0;
            return;
        }
        else if (ddlApprovingOfficer.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Please Select Approving Officer";
            ViewState["OpenFlag"] = 0;
            return;
        }
        else if (txtDescription.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Please Enter Reason/Description";
            ViewState["OpenFlag"] = 0;
            return;
        }
        else if (txtContactNo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Please Enter Contact No";
            ViewState["OpenFlag"] = 0;
            return;
        }
        else if (txtAddress.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Please Enter Address";
            ViewState["OpenFlag"] = 0;
            return;
        }
        startDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
        endDate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;

        days = ((DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) - DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)).TotalDays + 1).ToString();
        if (days == "1" && ddlHalfFull.SelectedItem.Text != "Full Day")
            days = ".5";

        if (ddlHalfFull.SelectedItem.Value != "Full Day" && double.Parse(days) > 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Please Select Valid Leave Type";
            ViewState["OpenFlag"] = 0;
            return;
        }

        double TotalDays= (DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) - DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)).TotalDays;
        TotalDays++;

        //Check if Payslip is generated
        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 22);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        if(dt.Rows.Count>0)
        {
            if(dt.Rows[0][0].ToString()!="0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
                dlglbl.Text = "Payslip already generated!!";
                ViewState["OpenFlag"] = 0;
                return;
            }
        }

        //Check For Medical Leave
        if (ddlLeaveCode.SelectedItem.Value == "2")
        {
            if (startDate > DateTime.Now.Date || endDate > DateTime.Now.Date)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
                dlglbl.Text = "Advance Medical Leave is not allowed";
                ViewState["OpenFlag"] = 0;
                return;
            }
            if (TotalDays > 2 && !FileUpload1.HasFile)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
                dlglbl.Text = "Please upload medical certificate";
                ViewState["OpenFlag"] = 0;
                return;
            }
        }

        ////Check for 2 CL in Academic Session
        //if (ddlLeaveCode.SelectedItem.Value == "1")
        //{
        //    cmd = new SqlCommand("SP_ApplyLeave", conn);
        //    cmd.Parameters.AddWithValue("@calltype", 4);
        //    cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    dt = new DataTable();
        //    da.Fill(dt);
        //    cmd.Dispose();
        //    da.Dispose();

        //    if (double.Parse(dt.Rows[0][0].ToString())+double.Parse(days) > 2)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
        //        dlglbl.Text = "Only 2 CL Allowed per Academic Session";
        //        ViewState["OpenFlag"] = 0;
        //        return;
        //    }
        //}

        //// Check for GL
        //if (ddlLeaveCode.SelectedItem.Value == "3")
        //{
        //    cmd = new SqlCommand("SP_ApplyLeave", conn);
        //    cmd.Parameters.AddWithValue("@calltype", 2);
        //    cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        //    cmd.Parameters.AddWithValue("@LFromDate", DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    dt = new DataTable();
        //    da.Fill(dt);
        //    cmd.Dispose();
        //    da.Dispose();

        //    if (dt.Rows[0][0].ToString() == "1")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
        //        dlglbl.Text = "You can not apply for General Leave before 6 month from your joining.";
        //        ViewState["OpenFlag"] = 0;
        //        return;
        //    }
        //}

        //Check for Short Leave

        if (ddlLeaveCode.SelectedItem.Value == "5")
        {
            //cmd = new SqlCommand("SP_ApplyLeave", conn);
            //cmd.Parameters.AddWithValue("@calltype", 19);
            //cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
            //cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
            //cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            //cmd.CommandType = CommandType.StoredProcedure;
            //da = new SqlDataAdapter();
            //da.SelectCommand = cmd;
            //dt = new DataTable();
            //da.Fill(dt);
            //cmd.Dispose();
            //da.Dispose();
            //if (int.Parse(dt.Rows[0][0].ToString()) > 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            //    dlglbl.Text = "You can not apply for 2 short leave on same day";
            //    ViewState["OpenFlag"] = 0;
            //    return;
            //}

            cmd = new SqlCommand("SP_ApplyLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 23);
            cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
            cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@Date", DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();
            if (double.Parse(dt.Rows[0][0].ToString()) >= 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
                dlglbl.Text = "Monthly limit reached for Short Leaves";
                ViewState["OpenFlag"] = 0;
                return;
            }
        }


        ////Check for Summer And Winter Break
        //if (ddlLeaveCode.SelectedItem.Value == "4" || ddlLeaveCode.SelectedItem.Value == "5")
        //{
        //    cmd = new SqlCommand("SP_ApplyLeave", conn);
        //    cmd.Parameters.AddWithValue("@calltype", 5);
        //    cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        //    cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
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
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
        //            dlglbl.Text = "Not Allowed";
        //            ViewState["OpenFlag"] = 0;
        //            return;
        //        }

        //        if (Convert.ToUInt64(dt.Rows[0][0].ToString()) > 4)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
        //            dlglbl.Text = "Not allowed due to more than 4 LWP in Academic Session";
        //            ViewState["OpenFlag"] = 0;
        //            return;
        //        }
        //    }
        //}

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 24);
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
        cmd.Parameters.AddWithValue("@calltype", 25);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@LeaveFrom", Convert.ToDateTime(Session["LeavesessionFrom"].ToString()).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@LeaveTo", Convert.ToDateTime(Session["LeavesessionTo"].ToString()).ToString("yyyy-MM-dd"));
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Employee Leave Structure Not Defined";
            ViewState["OpenFlag"] = 0;
            return;
        }

        if (dt.Rows.Count > 0)
        {
            if (double.Parse(days) > double.Parse(dt.Rows[0]["Total Leaves"].ToString()) - double.Parse(dt.Rows[0]["Days"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
                dlglbl.Text = "Balance Leaves Are " + (double.Parse(dt.Rows[0]["Total Leaves"].ToString()) - double.Parse(dt.Rows[0]["Days"].ToString())).ToString() + " Only ";
                ViewState["OpenFlag"] = 0;
                return;
            }

            cmd = new SqlCommand("SP_ApplyLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 26);
            cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
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
            cmd.Parameters.AddWithValue("@calltype", 27);
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
            if ((DateTime.Now.Date - DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)).TotalDays >= double.Parse(dt.Rows[0]["BackDays"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
                dlglbl.Text = "Not Allowed. Only Allowed " + (int.Parse(dt.Rows[0]["BackDays"].ToString()) - 1).ToString() + " Days Back";
                ViewState["OpenFlag"] = 0;
                return;
            }
        }

        if (double.Parse(dt.Rows[0]["FutureDays"].ToString()) > 0)
        {
            if (-(DateTime.Now.Date - DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)).TotalDays > double.Parse(dt.Rows[0]["FutureDays"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
                dlglbl.Text = "Not Allowed. Only Allowed " + dt.Rows[0]["FutureDays"].ToString() + " Days in Future";
                ViewState["OpenFlag"] = 0;
                return;
            }
        }

        if ((DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) - DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)).TotalDays > double.Parse(dt.Rows[0]["atatime"].ToString()) && double.Parse(dt.Rows[0]["atatime"].ToString()) > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "Can Not Take More Than " + dt.Rows[0]["atatime"].ToString() + " Leaves At A Time";
            ViewState["OpenFlag"] = 0;
            return;
        }

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 28);
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
            dlglbl.Text = "During This Period !!! Already On Leave";
            ViewState["OpenFlag"] = 0;
            return;
        }

        //Check Leave Combination
        if (!ddlHalfFull.SelectedItem.Text.Contains("Half"))
        {
            for (DateTime date2 = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date; date2.Date <= DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date; date2 = date2.AddDays(1))
            {
                conn.Open();
                cmd = new SqlCommand("SP_ApplyLeave", conn);
                cmd.Parameters.AddWithValue("@calltype", 29);
                cmd.Parameters.AddWithValue("@FromDate", date2);
                cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
                    dlglbl.Text = "Leave Cannot be Applied in Selected Combination";
                    ViewState["OpenFlag"] = 0;
                    return;
                }
            }
        }

        // Check SL And CL
        if (ddlHalfFull.SelectedItem.Text.Contains("Half"))
        {
            for (DateTime date2 = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date; date2.Date <= DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date; date2 = date2.AddDays(1))
            {
                conn.Open();
                cmd = new SqlCommand("SP_ApplyLeave", conn);
                cmd.Parameters.AddWithValue("@calltype", 30);
                cmd.Parameters.AddWithValue("@FromDate", date2);
                cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
                    dlglbl.Text = "Leave Cannot be Applied in Selected Combination";
                    ViewState["OpenFlag"] = 0;
                    return;
                }
            }
        }

        ////Check Sandwitch for Full Day
        if (ddlHalfFull.SelectedItem.Text == "Full Day")
        {
            cmd = new SqlCommand("SP_ApplyLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 31);
            cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();

            if (dt.Rows[0][0].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
                dlglbl.Text = "Leave Cannot be Applied Because of Sandwitch";
                ViewState["OpenFlag"] = 0;
                return;
            }

            cmd = new SqlCommand("SP_ApplyLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 32);
            //cmd.Parameters.AddWithValue("@FromDate", txtStartDate.Text);
            cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();

            if (dt.Rows[0][0].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
                dlglbl.Text = "Leave Cannot be Applied Because of Sandwitch";
                ViewState["OpenFlag"] = 0;
                return;
            }
        }

        //////Check Sandwitch for Half Day
        //////Check Sandwitch For Previous Dates
        //////Second Half is allowed

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
        //            cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        //            cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        //            cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            da = new SqlDataAdapter();
        //            da.SelectCommand = cmd;
        //            dt = new DataTable();
        //            da.Fill(dt);
        //            cmd.Dispose();
        //            da.Dispose();

        //            if (dt.Rows[0][0].ToString() == "1")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
        //                dlglbl.Text = "Leave Cannot be Applied Because of Sandwitch";
        //                ViewState["OpenFlag"] = 0;
        //                return;
        //            }
        //        }

        //        //Check Sandwitch For Next Dates
        //        //First Half is allowed
        //        if (ddlHalfFull.SelectedItem.Text != "I Half")
        //        {
        //            cmd = new SqlCommand("SP_ApplyLeave", conn);
        //            cmd.Parameters.AddWithValue("@calltype", 14);
        //            cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        //            cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        //            cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            da = new SqlDataAdapter();
        //            da.SelectCommand = cmd;
        //            dt = new DataTable();
        //            da.Fill(dt);
        //            cmd.Dispose();
        //            da.Dispose();

        //            if (dt.Rows[0][0].ToString() == "1")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
        //                dlglbl.Text = "Leave Cannot be Applied Because of Sandwitch";
        //                ViewState["OpenFlag"] = 0;
        //                return;
        //            }
        //        }
        //    }
        //}


        /////////Check Engagements Mark

        //cmd = new SqlCommand("SP_CheckEngagementBeforeLeave", conn);
        //cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        //cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        //cmd.Parameters.AddWithValue("@LeaveType", ddlHalfFull.SelectedItem.Value);
        //cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        //cmd.CommandType = CommandType.StoredProcedure;
        //da = new SqlDataAdapter();
        //da.SelectCommand = cmd;
        //dt = new DataTable();
        //da.Fill(dt);
        //cmd.Dispose();
        //da.Dispose();

        //if (dt.Rows[0][0].ToString() != "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
        //    dlglbl.Text = "You must mark engagements before applying for leave";
        //    ViewState["OpenFlag"] = 0;
        //    return;
        //}

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
        string FileName = "";
        string path = "";
        if (FileUpload1.HasFile)
        {
            
            FileInfo fi = new FileInfo(FileUpload1.FileName);
            string Ext = fi.Extension;
            string AllowedExt = ".jpg,.jpeg,.png,.pdf,.doc,.docx";
            if (AllowedExt.Contains(Ext))
            {
                try
                {
                    path = "../../Files/LeaveDocument/";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(path));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(path));
                    FileName = "LeaveDocument_" + Session["e_code"].ToString() + "_" + Guid.NewGuid() + Ext;
                    string SaveLocation = Server.MapPath(path) + FileName;
                    FileUpload1.SaveAs(SaveLocation);
                }
                catch (Exception ex)
                {
                    path = "../Files/LeaveDocument/";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(path));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(path));
                    FileName = "LeaveDocument_" + Session["e_code"].ToString() + "_" + Guid.NewGuid() + Ext;
                    string SaveLocation = Server.MapPath(path) + FileName;
                    FileUpload1.SaveAs(SaveLocation);
                }

                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
                dlglbl.Text = "Please Upload Valid File";
                ViewState["OpenFlag"] = 0;
                return;
            }
        }
        conn.Open();
        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 33);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveCode.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@LeaveType", ddlHalfFull.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
        cmd.Parameters.AddWithValue("@FirstLevelOfficer", ddlRecommendingOfficer.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@LastLevelOfficer", ddlApprovingOfficer.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@CurrentLeaveAt", ddlRecommendingOfficer.SelectedItem.Value);
        if(FileName!="")
        {
            cmd.Parameters.AddWithValue("@IsAttachment", 1);
            cmd.Parameters.AddWithValue("@AttachmentPath",FileName);
        }
        else
        {
                cmd.Parameters.AddWithValue("@IsAttachment", 0);
                cmd.Parameters.AddWithValue("@AttachmentPath", "");
        }
        cmd.Parameters.AddWithValue("@LeaveStatus", "Apply");
        cmd.Parameters.AddWithValue("@ApplyDate", DateTime.Now);
        cmd.Parameters.AddWithValue("@CreatedBy",Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DelayPopup();", true);
        dlglbl.Text = "" + ddlLeaveCode.SelectedItem.Text + " - " + days + " Applied Successfully ";
        ViewState["OpenFlag"] = 0;


        FillApplyLeaves();
        FillBalanceLeaves();
        //GVEngagement.DataSource = FillEngagement();
        //GVEngagement.DataBind();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Popup(true);
    }

    //Sorting And Paging of Leave Grid
    protected void GVApplyLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = count.ToString();
            count++;
        }
    }
    
    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeader = (CheckBox)GVApplyLeave.HeaderRow.FindControl("chkHeader");

        foreach (GridViewRow row in GVApplyLeave.Rows)
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

    //Popup
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
        FillApplyLeaves();
        Popup1(false);
    }
    protected void btnClose3_Click(object sender, EventArgs e)
    {
        FillApplyLeaves();
        Popup3(false);
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
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        FillApplyLeaves();
        Popup(false);
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        int flag = 0;
        foreach (GridViewRow row in GVApplyLeave.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    conn.Open();
                    cmd = new SqlCommand("SP_ApplyLeave", conn);
                    cmd.Parameters.AddWithValue("@calltype", 34);
                    cmd.Parameters.AddWithValue("@AttendanceCode", row.Cells[2].Text);
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["e_code"].ToString());
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
            dlglbl.Text = "No Leave selected to remove";
            ViewState["OpenFlag"] = 0;
            return;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Leave deleted successfully";
        ViewState["OpenFlag"] = 0;
        FillApplyLeaves();
    }
    protected void No_Click(object sender, EventArgs e)
    {
        FillApplyLeaves();
        Popup(false);
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        FillApplyLeaves();
        if (ViewState["OpenFlag"]!=null && ViewState["OpenFlag"].ToString() == "-1")
        {
            Popup1(true);
        }
        else
        {
            Popup2(false);
        }
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
        }
    }
    protected DataTable FillEngagement()
    {
        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 35);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        return dt;
    }
    protected void GVEngagement_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["ExamDuty"] == null)
            {
                FillExamDuty();
            }
            DropDownList ddlFreeEmployee = (DropDownList)e.Row.FindControl("ddlFreeEmployee");
            DropDownList ddlReason = (DropDownList)e.Row.FindControl("ddlReason");
            TextBox txtReason = (TextBox)e.Row.FindControl("txtReason");

            //Code For Reason drop down
            ddlReason.Items.Clear();
            ListItem item = new ListItem("Select Reason", "0");
            ddlReason.Items.Add(item);
            ddlReason.DataSource = ViewState["ExamDuty"] as DataTable;
            ddlReason.DataTextField = "OfficialDuty";
            ddlReason.DataValueField = "ID";
            ddlReason.DataBind();

            if (ddlReason.Items.FindByValue(e.Row.Cells[2].Text) != null)
            {
                ddlReason.ClearSelection();
                ddlReason.Items.FindByValue(e.Row.Cells[2].Text).Selected = true;
            }
            if (ddlReason.SelectedItem.Text == "Other")
            {
                txtReason.Enabled = true;
            }
            else
            {
                txtReason.Enabled = false;
            }

            //Code for Free Employee
            ddlFreeEmployee.Items.Clear();
            ListItem item2 = new ListItem("Select Engagement Faculty", "0");
            ddlFreeEmployee.Items.Add(item2);

            string Date = DateTime.ParseExact(e.Row.Cells[7].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            ddlFreeEmployee.DataSource = FillEmployees(Date, e.Row.Cells[1].Text);
            ddlFreeEmployee.DataTextField = "EmployeeName";
            ddlFreeEmployee.DataValueField = "e_code";
            ddlFreeEmployee.DataBind();
            if (ddlFreeEmployee.Items.FindByValue(e.Row.Cells[3].Text) != null)
            {
                ddlFreeEmployee.ClearSelection();
                ddlFreeEmployee.Items.FindByValue(e.Row.Cells[3].Text).Selected = true;
            }

            LinkButton btnSave = (LinkButton)e.Row.FindControl("lnlBtnSaveEngagement");
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("lnlBtnDeleteEngagement");


            if (e.Row.Cells[12].Text != "Pending")
            {
                ddlFreeEmployee.Visible=false;
                ddlReason.Visible = false;
                btnSave.Visible = false;
                btnDelete.Visible = true;
                string hex = "#82AF6F";
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(hex);
                e.Row.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                ddlFreeEmployee.Visible = true;
                ddlReason.Visible = true;
                btnSave.Visible = true;
                btnDelete.Visible = false;
                //string hex = "#E36D59"; //Red
                string hex = "#F0AC4A"; //Yellow
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(hex);
                e.Row.ForeColor = System.Drawing.Color.White;
            }
        }
    }
    protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow currentrow = (GridViewRow)((DropDownList)sender).Parent.Parent.Parent.Parent;
        DropDownList ddlReason = (DropDownList)currentrow.FindControl("ddlReason");
        TextBox txtReason = (TextBox)currentrow.FindControl("txtReason");

        if (ddlReason.SelectedItem.Text == "Other")
        {
            txtReason.Enabled = true;
        }
        else
        {
            txtReason.Enabled = false;
        }
    }
    protected DataTable FillEmployees(string Date, string PeriodCode)
    {
        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 36);
        cmd.Parameters.AddWithValue("@Date", Date);
        cmd.Parameters.AddWithValue("@PeriodCode", PeriodCode);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt1 = new DataTable();
        da.Fill(dt1);
        cmd.Dispose();
        da.Dispose();
        return dt1;
    }
    protected void FillExamDuty()
    {
        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 37);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt1 = new DataTable();
        da.Fill(dt1);
        cmd.Dispose();
        da.Dispose();

        ViewState["ExamDuty"] = dt1;
    }


    //protected void GVEngagement_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if(e.CommandName== "Del")
    //    {
    //        GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
    //        int RowIndex = oItem.RowIndex;
    //        string TTCode = GVEngagement.Rows[RowIndex].Cells[0].Text;

    //        conn.Open();
    //        cmd = new SqlCommand("SP_ApplyLeave", conn);
    //        cmd.Parameters.AddWithValue("@calltype", 38);
    //        cmd.Parameters.AddWithValue("@TTCode", TTCode);
    //        cmd.Parameters.AddWithValue("@CreatedBy", Session["e_code"].ToString());
    //        SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
    //        parm.Direction = ParameterDirection.ReturnValue;
    //        cmd.Parameters.Add(parm);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.ExecuteNonQuery();
    //        int result = Convert.ToInt32(parm.Value);
    //        cmd.Dispose();
    //        conn.Close();

    //        GVEngagement.DataSource = FillEngagement();
    //        GVEngagement.DataBind();

    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
    //        dlglbl.Text = "Engagement deleted successfully";
    //        ViewState["OpenFlag"] = 0;

    //    }
    //    else if (e.CommandName == "Save")
    //    {
    //        GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
    //        int RowIndex = oItem.RowIndex;

    //        DropDownList ddlFreeEmployee = (DropDownList)GVEngagement.Rows[RowIndex].FindControl("ddlFreeEmployee");
    //        DropDownList ddlReason = (DropDownList)GVEngagement.Rows[RowIndex].FindControl("ddlReason");
    //        TextBox txtReason = (TextBox)GVEngagement.Rows[RowIndex].FindControl("txtReason");

    //        string TTCode = GVEngagement.Rows[RowIndex].Cells[0].Text;
    //        string AttendanceCode = GVEngagement.Rows[RowIndex].Cells[4].Text;
    //        string Class = GVEngagement.Rows[RowIndex].Cells[8].Text;
    //        string Period = GVEngagement.Rows[RowIndex].Cells[10].Text;
    //        string TTDate = GVEngagement.Rows[RowIndex].Cells[7].Text;
    //        string E_Code = ddlFreeEmployee.SelectedItem.Value;

    //        if(E_Code=="0")
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
    //            dlglbl.Text = "Please Select Employee";
    //            ViewState["OpenFlag"] = -1;
    //            return;
    //        }
    //        if(ddlReason.SelectedItem.Value=="0")
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
    //            dlglbl.Text = "Please Select Reason";
    //            ViewState["OpenFlag"] = -1;
    //            return;
    //        }
    //        if(ddlReason.SelectedItem.Text == "Other" && txtReason.Text=="")
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
    //            dlglbl.Text = "Please Enter Reason";
    //            ViewState["OpenFlag"] = -1;
    //            return;
    //        }
    //        conn.Open();
    //        cmd = new SqlCommand("SP_ApplyLeave", conn);
    //        cmd.Parameters.AddWithValue("@calltype", 39);
    //        cmd.Parameters.AddWithValue("@AttendanceCode", AttendanceCode);
    //        cmd.Parameters.AddWithValue("@TTCode", TTCode);
    //        cmd.Parameters.AddWithValue("@SubjectCode", 0);
    //        cmd.Parameters.AddWithValue("@E_code", E_Code);
    //        cmd.Parameters.AddWithValue("@CreatedBy", Session["e_code"].ToString());
    //        cmd.Parameters.AddWithValue("@OfficeDutyCode", ddlReason.SelectedItem.Value);
    //        if (ddlReason.SelectedItem.Text == "Other")
    //        {
    //            cmd.Parameters.AddWithValue("@OfficeDutyRemarks", txtReason.Text);
    //        }
    //        else
    //        {
    //            cmd.Parameters.AddWithValue("@OfficeDutyRemarks", "");
    //        }
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
    //            dlglbl.Text = "Lecture Cannot Be Engaged. Attendance Marked";
    //            ViewState["OpenFlag"] = -1;
    //            return;
    //        }
    //        else if (result == 2)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
    //            dlglbl.Text = "Lecture Already Engaged. Approval Pending";
    //            ViewState["OpenFlag"] = -1;
    //            return;
    //        }
    //        else if (result == 3)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
    //            dlglbl.Text = "Lecture Already Engaged";
    //            ViewState["OpenFlag"] = -1;
    //            return;
    //        }
    //        else
    //        {
    //            if (Session["SendSMSEngagement"].ToString() == "Yes")
    //            {
    //                SendSMS(E_Code,Class,Period,TTDate);
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
    //                dlglbl.Text = "Lecture Engaged Successfully  and SMS Sent";
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
    //                dlglbl.Text = "Lecture Engaged Successfully";
    //            }

    //        }

    //        FillApplyLeaves();
    //        GVEngagement.DataSource = FillEngagement();
    //        GVEngagement.DataBind();
    //    }
    //}

    protected void SendSMS(string E_Code, string Class, string Period, string Date)
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
        Msg = "Respected Sir/Madam, You have new Engagement Request. \nEngagement By : " + Session["Name"].ToString() + "\nCourse : " + Class + "\nPeriod : " + Period + "\nDate : " + Date;

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
                //obj.SMSSend(dt1.Rows[0][0].ToString(), "Respected Sir/Madam, You have new Engagement Request for Approval. Please Login to Choose appropriate action. Thanks", Session["MSGUSERID"].ToString(), Session["MSGPASSWORD"].ToString(), Session["MSGSENDERID"].ToString(), Session["UserCode"].ToString(), Session["userid"].ToString(), Session["Name"].ToString(), Session["e_code"].ToString());
                obj.SMSSend(dt1.Rows[0][0].ToString(), Msg, Session["MSGUSERID"].ToString(), Session["MSGPASSWORD"].ToString(), Session["MSGSENDERID"].ToString(), Session["UserCode"].ToString(), Session["userid"].ToString(), Session["Name"].ToString(), Session["e_code"].ToString());
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
}
