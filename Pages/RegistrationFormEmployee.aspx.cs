using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Pages_RegistrationFormEmployee : System.Web.UI.Page
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
            FillStatus();
            FillDesignation();
            FillDepartment();
            FillBranch();
            FillEmployeeType();

            if (Session["PageIndex"] != null)
            {
                GVDetails.PageIndex = int.Parse(Session["PageIndex"].ToString());
            }
            else
            {
                Session["PageIndex"] = 0;
            }
            if (Session["RegStatusEmp"] != null)
            {
                ddlStatus.ClearSelection();
                ddlStatus.Items.FindByValue(Session["RegStatusEmp"].ToString()).Selected = true;
            }
            if (Session["RegDesignationCode"] != null)
            {
                ddlDesignation.ClearSelection();
                ddlDesignation.Items.FindByValue(Session["RegDesignationCode"].ToString()).Selected = true;
            }
            if (Session["RegDepartmentCode"] != null)
            {
                ddlDepartment.ClearSelection();
                ddlDepartment.Items.FindByValue(Session["RegDepartmentCode"].ToString()).Selected = true;
            }
            if (Session["RegBranchCode"] != null)
            {
                ddlBranch.ClearSelection();
                ddlBranch.Items.FindByValue(Session["RegBranchCode"].ToString()).Selected = true;
            }

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
    }
    protected void FillStatus()
    {
        ddlStatus.Items.Clear();
        ListItem item = new ListItem("Select Status", "0");
        ddlStatus.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlStatus.DataSource = dt;
        ddlStatus.DataTextField = "Status";
        ddlStatus.DataValueField = "Status";
        ddlStatus.DataBind();
        dt.Dispose();
    }
    protected void FillDesignation()
    {
        ddlDesignation.Items.Clear();
        ListItem item = new ListItem("Select Designation", "0");
        ddlDesignation.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDesignation.DataSource = dt;
        ddlDesignation.DataTextField = "ddlText";
        ddlDesignation.DataValueField = "ddlValue";
        ddlDesignation.DataBind();
        dt.Dispose();
    }
    protected void FillDepartment()
    {
        ddlDepartment.Items.Clear();
        ListItem item = new ListItem("Select Department", "0");
        ddlDepartment.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDepartment.DataSource = dt;
        ddlDepartment.DataTextField = "ddlText";
        ddlDepartment.DataValueField = "ddlValue";
        ddlDepartment.DataBind();
        dt.Dispose();
    }
    protected void FillBranch()
    {
        ddlBranch.Items.Clear();
        ListItem item = new ListItem("Select Branch", "0");
        ddlBranch.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlBranch.DataSource = dt;
        ddlBranch.DataTextField = "ddlText";
        ddlBranch.DataValueField = "ddlValue";
        ddlBranch.DataBind();
        dt.Dispose();
    }
    protected void FillEmployeeType()
    {
        ddlEmployeeType.Items.Clear();
        ListItem item = new ListItem("Select Employee Type", "0");
        ddlEmployeeType.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlEmployeeType.DataSource = dt;
        ddlEmployeeType.DataTextField = "ddlText";
        ddlEmployeeType.DataValueField = "ddlValue";
        ddlEmployeeType.DataBind();
        dt.Dispose();
    }

    protected DataTable FillGrid()
    {
        string FilterCriteria = "";

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 11);

        if (ddlStatus.SelectedItem.Value != "0")
        {
            FilterCriteria = " status = '" + ddlStatus.SelectedItem.Value + "'";
        }
        else
        {
            FilterCriteria = "status='A'";
        }

        if (ddlDesignation.SelectedItem.Value != "0")
        {
            FilterCriteria = FilterCriteria + " and b.DesignationCode=" + ddlDesignation.SelectedItem.Value;
        }

        if (ddlDepartment.SelectedItem.Value != "0")
        {
            FilterCriteria = FilterCriteria + " and DepartmentCode=" + ddlDepartment.SelectedItem.Value;
        }

        if (ddlBranch.SelectedItem.Value != "0")
        {
            FilterCriteria = FilterCriteria + " and BranchID=" + ddlBranch.SelectedItem.Value;
        }

        if (ddlEmployeeType.SelectedItem.Value != "0")
        {
            FilterCriteria = FilterCriteria + " and EmployeetypeCode=" + ddlEmployeeType.SelectedItem.Value;
        }

        cmd.Parameters.AddWithValue("@Status", FilterCriteria);
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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ViewState["OpenFlag"]!=null && ViewState["OpenFlag"].ToString() == "-1")
        {
            Popup5(true);
        }
        else if (ViewState["OpenFlag"] != null && ViewState["OpenFlag"].ToString() == "-3")
        {
            Popup7(true);
        }
        else {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup5();", true);                   
        }
     
    }
    protected void btnYes1_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 22);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["EC"].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record deleted successfully','success');$('.modal-backdrop').remove();", true);

            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record cannot be deleted');$('.modal-backdrop').remove();", true);
        }
    }
    protected void btnYes2_Click(object sender, EventArgs e)
    {
        bool flag = false;
        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    conn.Open();
                    cmd = new SqlCommand("SP_EmployeeMaster", conn);
                    cmd.Parameters.AddWithValue("@calltype", 22);
                    cmd.Parameters.AddWithValue("@E_Code", row.Cells[3].Text);
                    //cmd.Parameters.AddWithValue("@loginname", Session["userName"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    int result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();

                    if (result != 1)
                    {
                        flag = true;
                    }
                }
            }
        }

        if (flag == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Record deleted successfully";
        }
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnNo1_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }
    protected void btnNo2_Click(object sender, EventArgs e)
    {
        Popup2(false);
    }

    
    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[4].Text;
            ViewState["EC"] = EC;
            Session["View"] = 0;
            Popup1(true);
        }
        else if (e.CommandName == "modify")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[4].Text;
            Session["ECode"] = EC;
            Session["View"] = 2;
            Session["PageURL"] = "RegistrationFormEmployee.aspx";

            Session["RegStatusEmp"] = ddlStatus.SelectedItem.Value;
            Session["RegDesignationCode"] = ddlDesignation.SelectedItem.Value;
            Session["RegDepartmentCode"] = ddlDepartment.SelectedItem.Value;
            Session["RegBranchCode"] = ddlBranch.SelectedItem.Value;

            Session["documentcontent"] = null;
            Session["Filename"] = null;
            Session["fileexten"] = null;
            Session["contenttype"] = null;
            Response.Redirect("EmployeeMaster_Update.aspx");
        }
        else if (e.CommandName == "view")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[4].Text;
            Session["ECode"] = EC;
            Session["View"] = 1;
            Session["PageURL"] = "RegistrationFormEmployee.aspx";
            Session["documentcontent"] = null;
            Session["Filename"] = null;
            Session["fileexten"] = null;
            Session["contenttype"] = null;
            Response.Redirect("EmployeeMaster_Update.aspx");
        }
        else if (e.CommandName == "Remarks")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[4].Text;
            Session["ECode"] = EC;
            Popup5(true);
            txtRemarks.Text = "";
            GVRemarks.DataSource = FillGridRemarks();
            GVRemarks.DataBind();
        }
        else if (e.CommandName == "viewphoto")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[4].Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", ("<script>openPopup('ViewImage.aspx?Code=" + EC + "&Type=E')</script>"), false);
        }
        else if (e.CommandName == "LeaveDetails")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[4].Text;
            Popup6(true);

            cmd = new SqlCommand("SP_Web_LeaveDetails", conn);
            cmd.Parameters.AddWithValue("@calltype", 1);
            cmd.Parameters.AddWithValue("@empcode", EC);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();

            GVDetailsLeaveDetails.DataSource = dt;
            GVDetailsLeaveDetails.DataBind();

            cmd = new SqlCommand("SP_Web_Leave", conn);
            cmd.Parameters.AddWithValue("@calltype", 11);
            cmd.Parameters.AddWithValue("@empcode", EC);
            cmd.Parameters.AddWithValue("@LeaveFrom", Convert.ToDateTime(Session["LeavesessionFrom"].ToString()).ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@LeaveTo", Convert.ToDateTime(Session["LeavesessionTo"].ToString()).ToString("yyyy-MM-dd"));
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();

            GVLeaveBalance.DataSource = dt;
            GVLeaveBalance.DataBind();
        }
        if (e.CommandName == "Print")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[4].Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", ("<script>openPopup('PrintEmployeeForm.aspx?EC=" + EC + "')</script>"), false);
        }
        if (e.CommandName == "PrintJoining")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[4].Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", ("<script>openPopup('PrintJoining.aspx?EC=" + EC + "')</script>"), false);
        }
        if (e.CommandName == "PrintTransport")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[4].Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", ("<script>openPopup('PrintTransport.aspx?EC=" + EC + "')</script>"), false);
        }
        if (e.CommandName == "PrintFileCover")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[4].Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", ("<script>openPopup('PrintFileCover.aspx?EC=" + EC + "')</script>"), false);
        }
    }
    public object AddZero(int Num, int dig)
    {
        string st;
        int i77;
        st = Num.ToString();
        if (st.Length < dig && st.Length > 0)
        {
            for (i77 = 1; i77 <= dig - Num.ToString().Length; i77++)
            {
                st = "0" + st;
            }
        }
        return st;
    }
    void Popup4(bool isDisplay)
    {
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup4();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup4();", true);
        }
    }

    protected void btnYes4_Click(object sender, EventArgs e)
    {
        string SMSConfirm = Session["body"].ToString() + '\n' + " Your user name is " + ViewState["MCodeNumber"].ToString() + '\n' + " password is  " + ViewState["rand"].ToString() + '\n' + " Thanks";
        SendSMS obj = new SendSMS();
        obj.SMSSend(ViewState["PhoneStudent"].ToString(), SMSConfirm, Session["MSGUSERID"].ToString(), Session["MSGPASSWORD"].ToString(), Session["MSGSENDERID"].ToString(), Session["UserCode"].ToString(), Session["userid"].ToString(), Session["userName"].ToString(), Session["empCode"].ToString());

        SendMail obj1 = new SendMail();
        obj1.SendMails("Admission Confirmmation Mail", Session["bodymail"].ToString() + '\n' + " Your user name is " + ViewState["MCodeNumber"].ToString() + '\n' + " password is  " + ViewState["rand"].ToString() + '\n' + " Thanks", ViewState["EmailStudent"].ToString());

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Admission Confirmed With " + ViewState["MCodeNumber"].ToString();

    }

    protected void SMSSettings()
    {
        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 72);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt1 = new DataTable();
        da.Fill(dt1);
        cmd.Dispose();
        da.Dispose();

        Session["bodysms"] = dt1.Rows[0]["bodysmsstudent"].ToString();
        Session["bodymail"] = dt1.Rows[0]["bodymailstudent"].ToString();
        Session["MSGUSERID"] = dt1.Rows[0]["msgid"].ToString();
        Session["MSGPASSWORD"] = dt1.Rows[0]["msgpassword"].ToString();
        Session["MSGSENDERID"] = dt1.Rows[0]["msgsenderId"].ToString();
    }
    protected void btnNo4_Click(object sender, EventArgs e)
    {
        Popup4(false);
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

    }

    protected void GVDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if(e.Row.Cells[8].Text == "Left")
            {
                LinkButton btn = (LinkButton)e.Row.FindControl("lnlbtnDelete");
                btn.Visible = false;
            }
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Session["PageURL"] = "RegistrationFormEmployee.aspx";
        Session["ECode"] = null;
        Session["View"] = 0;
        Response.Redirect("EmployeeMaster_Update.aspx");
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int pr = 0;
        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    pr = pr + 1;
                }
            }
        }

        if (pr == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "No Record is selected to remove";
            return;
        }

        Popup2(true);
    }

    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeader = (CheckBox)GVDetails.HeaderRow.FindControl("chkHeader");

        foreach (GridViewRow row in GVDetails.Rows)
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

    protected void btnAddRemarks_Click(object sender, EventArgs e)
    {
        if (txtRemarks.Text=="")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Add Remarks";
            ViewState["OpenFlag"] = -1;
            return;
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 149);
        cmd.Parameters.AddWithValue("@E_Code", Session["ECode"].ToString());
        cmd.Parameters.AddWithValue("@remarks", txtRemarks.Text);
        cmd.Parameters.AddWithValue("@employeename", Session["userName"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        int result = cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Remarks Added successfully";

        GVRemarks.DataSource = FillGridRemarks();
        GVRemarks.DataBind();
    }
    void Popup5(bool isDisplay)
    {
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup5();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup5();", true);
        }
    }

    protected DataTable FillGridRemarks()
    {
        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 148);
        cmd.Parameters.AddWithValue("@E_Code", Session["ECode"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        return dt;
    }

    protected void GVRemarks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count.ToString();
            count++;
        }
    }
    protected void btnNo6_Click(object sender, EventArgs e)
    {
        Popup6(false);
    }
    void Popup6(bool isDisplay)
    {
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup6();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup6();", true);
        }
    }
    protected void GVDetailsLeaveDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count.ToString();
            count++;
        }
    }

    protected void btnUpdateDesignation_Click(object sender, EventArgs e)
    {
        if (ddlUpdateDesignation.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Designation";
            ViewState["OpenFlag"] = -3;
            return;
        }


        String ecodes = "";

        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    ecodes = ecodes + row.Cells[3].Text + ",";

                }
            }
        }

        ecodes = ecodes.Remove(ecodes.Length - 1, 1);
        conn.Open();
        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 195);
        cmd.Parameters.AddWithValue("@ecodes", ecodes);
        cmd.Parameters.AddWithValue("@DesignationCode", ddlUpdateDesignation.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        int result = cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();


        ViewState["OpenFlag"] = 0;
   

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Mapped successfully";

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void FillUpdateDesignation()
    {
        ddlUpdateDesignation.Items.Clear();
        ListItem item = new ListItem("Select Designation", "0");
        ddlUpdateDesignation.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlUpdateDesignation.DataSource = dt;
        ddlUpdateDesignation.DataTextField = "ddlText";
        ddlUpdateDesignation.DataValueField = "ddlValue";
        ddlUpdateDesignation.DataBind();
        dt.Dispose();
    }

    protected void btnUpdDesignation_Click(object sender, EventArgs e)
    {
        int pr = 0;
        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    pr = pr + 1;
                }
            }
        }

        if (pr == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "No Record is selected";
            return;
        }
        FillUpdateDesignation();
        Popup7(true);
    }

    void Popup7(bool isDisplay)
    {
        if (isDisplay)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup7();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup7();", true);
        }
    }
}