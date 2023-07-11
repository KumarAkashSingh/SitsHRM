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

public partial class View_Payroll_SetSalary : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    int count = 1;
    int count1 = 1;
    int counter = 1;
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
            Form.DefaultButton = btnSearch.UniqueID;
            txtAmount.Attributes.Add("onkeypress", "return isNumberKey(event)");
        }
    }
    protected void FillStatus()
    {
        ddlStatus.Items.Clear();
        ListItem item = new ListItem("Select Status", "0");
        ddlStatus.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 87);
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
        cmd.Parameters.AddWithValue("@calltype", 88);
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

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 89);
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

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 90);
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

        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 9);

        if (txtSearch.Text == "")
        {
            cmd.Parameters.AddWithValue("@search", "");
        }
        else
        {
            cmd.Parameters.AddWithValue("@search", txtSearch.Text);
        }

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
            FilterCriteria = FilterCriteria + " and DesignationCode=" + ddlDesignation.SelectedItem.Value;
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ViewState["OpenFlag"] != null && ViewState["OpenFlag"].ToString() == "-1")
        {
            Popup5(true);
        }
        else
        {
            Popup(false);
        }
    }
    protected void btnYes1_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 10);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["EC"].ToString());
        cmd.Parameters.AddWithValue("@DeletedBy", Session["empCode"].ToString());
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
            dlglbl.Text = "Salary Structure Deleted Successfully";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Salary Structure Cannot Be Deleted";
        }
        ViewState["OpenFlag"] = 0;
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnYes2_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 11);
        cmd.Parameters.AddWithValue("@RecordNo", ViewState["RN"].ToString());
        cmd.Parameters.AddWithValue("@DeletedBy", Session["empCode"].ToString());
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
            dlglbl.Text = "Allowance Type Deleted Successfully";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Allowance Type Cannot Be Deleted";
        }
        ViewState["OpenFlag"] = -1;
        GVSalary.DataSource = FillGridSalary();
        GVSalary.DataBind();
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
        FillAllowanceTypes();
    }
    protected void btnYes4_Click(object sender, EventArgs e)
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
                    cmd = new SqlCommand("SP_Payroll", conn);
                    cmd.Parameters.AddWithValue("@calltype", 10);
                    cmd.Parameters.AddWithValue("@E_Code", row.Cells[3].Text);
                    cmd.Parameters.AddWithValue("@DeletedBy", Session["empCode"].ToString());
                    SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    int result = Convert.ToInt32(parm.Value);
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
            dlglbl.Text = "Some records can not be removed";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Allowance Type Deleted Successfully";
        }
        ViewState["OpenFlag"] = 0;
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnNo1_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }
    protected void btnNo2_Click(object sender, EventArgs e)
    {
        Popup5(true);
    }
    protected void btnNo4_Click(object sender, EventArgs e)
    {
        Popup4(false);
    }
    
    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         if (e.CommandName == "UpdateSalary")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[2].Text;
            ViewState["EC"] = EC;
            GVSalaryUpdate.DataSource = FillGridSalaryUpdate();
            GVSalaryUpdate.DataBind();
            Popup6(true);
        }
       
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

        txtSearch.Focus();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
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
            ViewState["OpenFlag"] = 0;
            return;
        }

        Popup4(true);
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
    protected void btnAddAmount_Click(object sender, EventArgs e)
    {
        if (ddlAllowance.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Add Allowance Type";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (txtAmount.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Add Amount";
            ViewState["OpenFlag"] = -1;
            return;
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 12);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["EC"].ToString());
        cmd.Parameters.AddWithValue("@AllowanceCode", ddlAllowance.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
        cmd.Parameters.AddWithValue("@EmpCode", Session["empCode"].ToString());
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
            dlglbl.Text = "Allowance Type Already exist";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Allowance Type Added successfully";

            FillAllowanceTypes();
            txtAmount.Text = "";
        }
        GVSalary.DataSource = FillGridSalary();
        GVSalary.DataBind();
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
        ViewState["OpenFlag"] = -1;
    }
    protected void FillAllowanceTypes()
    {
        ddlAllowance.Items.Clear();
        ListItem item = new ListItem("Select Allowance Type", "0");
        ddlAllowance.Items.Add(item);

        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 13);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["EC"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlAllowance.DataSource = dt;
        ddlAllowance.DataTextField = "AllowanceDescription";
        ddlAllowance.DataValueField = "AllowanceCode";
        ddlAllowance.DataBind();
        dt.Dispose();
    }
    protected DataTable FillGridSalary()
    {
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 14);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["EC"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        lblSalaryRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " Record(s) Found]";
        return dt;
    }
    protected DataTable FillGridSalaryUpdate()
    {
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 24);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["EC"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        lblSalaryRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " Record(s) Found]";
        return dt;
    }
    protected void GVSalary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count.ToString();
            count++;
        }
    }
    protected void GVSalary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DelAllowance")
        {
            string RN = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            RN = GVSalary.Rows[RowIndex].Cells[2].Text;
            ViewState["RN"] = RN;
            Popup2(true);
        }
    }
    protected void ddlAllowance_SelectedIndexChanged(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 15);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["EC"].ToString());
        cmd.Parameters.AddWithValue("@AllowanceCode", ddlAllowance.SelectedItem.Value);
        SqlParameter parm = new SqlParameter("@AmountPercent", SqlDbType.Float);
        parm.Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(parm);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        float AmountPercent = Convert.ToInt32(parm.Value);
        cmd.Dispose();
        conn.Close();

        txtAmount.Text = AmountPercent.ToString();

        Popup5(true);
    }

    protected void GVDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = counter.ToString();
            counter++;
        }
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GVSalaryUpdate.Rows)
        {
            TextBox txtAmount = default(TextBox);
            txtAmount = (TextBox)row.FindControl("txtAmount");
            conn.Open();
            cmd = new SqlCommand("SP_Payroll", conn);
            cmd.Parameters.AddWithValue("@calltype", 25);
            cmd.Parameters.AddWithValue("@RecordNo", row.Cells[1].Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmount.Text );
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Salary Updated successfully";

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void btnClose1_Click(object sender, EventArgs e)
    {
        Popup6(false);
    }

    protected void GVSalaryUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count1.ToString();
            count1++;
        }
    }
}