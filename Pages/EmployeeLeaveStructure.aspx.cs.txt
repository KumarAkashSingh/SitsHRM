using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Globalization;
public partial class Pages_EmployeeLeaveStructure : System.Web.UI.Page
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
            FillStatus();
            FillDesignation();
            FillDepartment();
            FillBranch();
            FillEmployeeType();

            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
    }
    protected void FillStatus()
    {
        ddlStatus.Items.Clear();
        ListItem item = new ListItem("Select Status", "0");
        ddlStatus.Items.Add(item);

        cmd = new SqlCommand("SP_EmployeeLeaveStructure", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
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

        cmd = new SqlCommand("SP_EmployeeLeaveStructure", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDesignation.DataSource = dt;
        ddlDesignation.DataTextField = "DesignationDescription";
        ddlDesignation.DataValueField = "DesignationCode";
        ddlDesignation.DataBind();
        dt.Dispose();
    }
    protected void FillDepartment()
    {
        ddlDepartment.Items.Clear();
        ListItem item = new ListItem("Select Department", "0");
        ddlDepartment.Items.Add(item);

        cmd = new SqlCommand("SP_EmployeeLeaveStructure", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
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
    protected void FillBranch()
    {
        ddlBranch.Items.Clear();
        ListItem item = new ListItem("Select Branch", "0");
        ddlBranch.Items.Add(item);

        cmd = new SqlCommand("SP_EmployeeLeaveStructure", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
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
    protected void FillEmployeeType()
    {
        ddlEmployeeType.Items.Clear();
        ListItem item = new ListItem("Select Employee Type", "0");
        ddlEmployeeType.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 191);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlEmployeeType.DataSource = dt;
        ddlEmployeeType.DataTextField = "Type";
        ddlEmployeeType.DataValueField = "EmpTypeCode";
        ddlEmployeeType.DataBind();
        dt.Dispose();
    }
    protected DataTable FillGrid()
    {
        string FilterCriteria = "";

        cmd = new SqlCommand("SP_EmployeeLeaveStructure", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
       
        if (ddlStatus.SelectedItem.Value != "0")
        {
            FilterCriteria = " status = '" + ddlStatus.SelectedItem.Text + "'";
        }
        else
        {
            FilterCriteria = "status='A'";
        }

        if (ddlDesignation.SelectedItem.Value != "0")
        {
            FilterCriteria = FilterCriteria + " and a.DesignationCode=" + ddlDesignation.SelectedItem.Value;
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
        Popup(false);
    }
    protected void btnYes1_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_EmployeeLeaveStructure", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@empcode", ViewState["EC"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        int result = cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Record deleted successfully";

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnYes2_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_EmployeeLeaveStructure", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
        cmd.Parameters.AddWithValue("@recno", ViewState["RecNo"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        int result = cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Record deleted successfully";
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
            EC = GVDetails.Rows[RowIndex].Cells[3].Text;
            ViewState["EC"] = EC;
            Session["View"] = 0;
            Popup1(true);
        }
        else if (e.CommandName == "modify")
        {
            string EC = "",DC="";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[3].Text;
            DC = GVDetails.Rows[RowIndex].Cells[11].Text;
            //UpdateEmployeeLeaveStructure(EC, DC);
            Session["ECode"] = EC;
            Session["View"] = 0;
            ddlSession.Visible = true;
            FillSession();
            FillGridEmployeeLeave();
            btnSave.Visible = false;
            FillBalanceLeaves();
            Popup3(true);
            GVEmployeeLeaveStructure.Enabled = true;
        }
        else if (e.CommandName == "view")
        {
            string EC = "",DC="";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[3].Text;
            DC = GVDetails.Rows[RowIndex].Cells[11].Text;
            //UpdateEmployeeLeaveStructure(EC, DC);
            Session["ECode"] = EC;
            Session["View"] = 1;
            ddlSession.Visible = true;
            FillSession();
            FillGridEmployeeLeave();
            btnSave.Visible = false;
            FillBalanceLeaves();
            Popup3(true);
            GVEmployeeLeaveStructure.Enabled = false;
        }
        else if (e.CommandName == "viewphoto")
        {
            string FC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            FC = GVDetails.Rows[RowIndex].Cells[3].Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", ("<script>openPopup('ViewImage.aspx?Code=" + FC + "&Type=E')</script>"), false);
        }
    }
  
    protected void GVEmployeeLeaveStructure_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string RecNo = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            RecNo = GVEmployeeLeaveStructure.Rows[RowIndex].Cells[0].Text;
            ViewState["RecNo"] = RecNo;
            Popup2(true);
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

    }
    protected void btnApplyStructure_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        Popup3(true);
        FillGridLeave();
        ddlSession.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string LeaveCode = "";
        string TotalLeaves = "";
        //string LPD = "";
        //string Frac = "";
        string backdays = "";
        string Futuredays = "";
        string Limited = "";
        string atatime = "";
        string FromDate = "";
        string ToDate = "";

        int pr = 0;
        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    pr = pr + 1;
                    break;
                }
            }
        }

        if (pr == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "No Record is selected to Update";
            return;
        }

        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    string E_Code = row.Cells[3].Text;

                    foreach (GridViewRow row1 in GVLeaveStructure.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow1 = row1.Cells[0].FindControl("chkCtrl") as CheckBox;
                            if (chkRow1.Checked)
                            {
                                LeaveCode = row1.Cells[1].Text;

                                TextBox Txb = default(TextBox);
                                Txb = (TextBox)row1.FindControl("txtTotalLeaves");
                                TotalLeaves = Txb.Text;

                                //Txb = (TextBox)row1.FindControl("txtDueDate");
                                //Frac = Txb.Text;

                                //Txb = (TextBox)row1.FindControl("txtWithoutPay");
                                //LPD = Txb.Text;

                                Txb = (TextBox)row1.FindControl("txtDaysBack");
                                backdays = Txb.Text;

                                Txb = (TextBox)row1.FindControl("txtFromDate");
                                FromDate = Txb.Text;

                                Txb = (TextBox)row1.FindControl("txtToDate");
                                ToDate = Txb.Text;

                                Txb = (TextBox)row1.FindControl("txtFutureDays");
                                Futuredays = Txb.Text;

                                Txb = (TextBox)row1.FindControl("txtAtATime");
                                atatime = Txb.Text;

                                DropDownList ddl = default(DropDownList);
                                ddl = (DropDownList)row1.FindControl("ddlLimited");
                                Limited = ddl.SelectedItem.Text;

                                conn.Open();
                                cmd = new SqlCommand("SP_EmployeeLeaveStructure", conn);
                                cmd.Parameters.AddWithValue("@calltype", 8);
                                cmd.Parameters.AddWithValue("@ECode", E_Code);
                                cmd.Parameters.AddWithValue("@LeaveCode", LeaveCode);
                                cmd.Parameters.AddWithValue("@TotalLeaves", TotalLeaves);
                                cmd.Parameters.AddWithValue("@Frac", 0);
                                cmd.Parameters.AddWithValue("@Lpd", 0);
                                cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(FromDate,"dd/MM/yyyy",CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@Backdays", backdays);
                                cmd.Parameters.AddWithValue("@Futuredays", Futuredays);
                                cmd.Parameters.AddWithValue("@atatime", atatime);
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["e_code"].ToString());
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                conn.Close();
                            }
                        }
                    }

                }
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Employee Leave Structure Created";
    }
    protected void FillGridLeave()
    {
        cmd = new SqlCommand("SP_EmployeeLeaveStructure", conn);
        cmd.Parameters.AddWithValue("@calltype", 9);

        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        lblLeaveRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " Record(s) Found]";

        GVLeaveStructure.DataSource = dt;
        GVLeaveStructure.DataBind();

        GVLeaveStructure.Visible = true;
        GVEmployeeLeaveStructure.Visible = false;
        GVLeave.Visible = false;
    }
    protected void FillGridEmployeeLeave()
    {
        cmd = new SqlCommand("SP_EmployeeLeaveStructure", conn);
        cmd.Parameters.AddWithValue("@calltype", 10);
        cmd.Parameters.AddWithValue("@ECode", Session["ECode"].ToString());
        cmd.Parameters.AddWithValue("@FromDate", ddlSession.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        lblLeaveRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " Record(s) Found]";

        GVEmployeeLeaveStructure.DataSource = dt;
        GVEmployeeLeaveStructure.DataBind();

        GVLeaveStructure.Visible = false;
        GVEmployeeLeaveStructure.Visible = true;
        GVLeave.Visible = true;
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
    protected void chkHeader1_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeader = (CheckBox)GVLeaveStructure.HeaderRow.FindControl("chkHeader1");

        foreach (GridViewRow row in GVLeaveStructure.Rows)
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

        Popup3(true);
    }
    protected void txtTotalLeaves_TextChanged(object sender, EventArgs e)
    {
        GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtTotalLeaves = default(TextBox);
        //TextBox txtWithoutPay = default(TextBox);
        //TextBox txtDueDate = default(TextBox);
        TextBox txtDaysBack = default(TextBox);
        TextBox txtFutureDays = default(TextBox);
        TextBox txtAtATime = default(TextBox);
        txtTotalLeaves = (TextBox)currentrow.FindControl("txtTotalLeaves");
        //txtWithoutPay = (TextBox)currentrow.FindControl("txtWithoutPay");
        //txtDueDate = (TextBox)currentrow.FindControl("txtDueDate");
        txtDaysBack = (TextBox)currentrow.FindControl("txtDaysBack");
        txtFutureDays = (TextBox)currentrow.FindControl("txtFutureDays");
        txtAtATime = (TextBox)currentrow.FindControl("txtAtATime");
        if (txtTotalLeaves.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Leaves can not be Empty";
            return;
        }
        //if (txtWithoutPay.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //    dlglbl.Text = "Without Pay can not be Empty";
        //    return;
        //}
        //if (txtDueDate.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //    dlglbl.Text = "Due After can not be Empty";
        //    return;
        //}
        if (txtDaysBack.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Days(Back) can not be Empty";
            return;
        }
        if (txtFutureDays.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Days(Future) can not be Empty";
            return;
        }
        if (txtAtATime.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "At A Time can not be Empty";
            return;
        }

        conn.Open();
        cmd = new SqlCommand("SP_EmployeeLeaveStructure", conn);
        cmd.Parameters.AddWithValue("@calltype", 11);
        cmd.Parameters.AddWithValue("@TotalLeaves", txtTotalLeaves.Text);
        cmd.Parameters.AddWithValue("@Frac", 0);
        cmd.Parameters.AddWithValue("@Lpd", 0);
        cmd.Parameters.AddWithValue("@Backdays", txtDaysBack.Text);
        cmd.Parameters.AddWithValue("@Futuredays", txtFutureDays.Text);
        cmd.Parameters.AddWithValue("@atatime", txtAtATime.Text);
        cmd.Parameters.AddWithValue("@recno", currentrow.Cells[0].Text);
        cmd.Parameters.AddWithValue("@CreatedBy", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();


        Popup3(true);
    }
    protected void FillSession()
    {
        ddlSession.Items.Clear();
        ListItem item = new ListItem("Select Session", Session["LeaveSessionFrom"].ToString());
        ddlSession.Items.Add(item);

        cmd = new SqlCommand("SP_EmployeeLeaveStructure", conn);
        cmd.Parameters.AddWithValue("@calltype", 12);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlSession.DataSource = dt;
        ddlSession.DataTextField = "Session";
        ddlSession.DataValueField = "FromDate";
        ddlSession.DataBind();
        dt.Dispose();
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridEmployeeLeave();
        Popup3(true);
    }
    protected void FillBalanceLeaves()
    {
        cmd = new SqlCommand("SP_GetBalanceLeaves", conn);
        cmd.Parameters.AddWithValue("@E_Code", Session["ECode"].ToString());
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
}