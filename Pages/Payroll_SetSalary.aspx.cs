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

public partial class Pages_Payroll_SetSalary : System.Web.UI.Page
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
            txtAmount.Attributes.Add("onkeypress", "return isNumberKey(event)");
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

        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 9);

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
        cmd.Parameters.AddWithValue("@DeletedBy", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Salary Structure Deleted Successfully','success');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Salary Structure Cannot Be Deleted');$('.modal-backdrop').remove();", true);
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
        cmd.Parameters.AddWithValue("@DeletedBy", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Allowance Type Deleted Successfully','success');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Allowance Type Cannot Be Deleted');$('.modal-backdrop').remove();", true);
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
                    cmd.Parameters.AddWithValue("@DeletedBy", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Some records can not be removed');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Allowance Type Deleted Successfully','success');$('.modal-backdrop').remove();", true);
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
        if (e.CommandName == "AddSalary")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[3].Text;
            ViewState["EC"] = EC;
            GVSalary.DataSource = FillGridSalary();
            GVSalary.DataBind();
            FillAllowanceTypes();
            txtAmount.Text = "";
            Popup5(true);
        }
        else if (e.CommandName == "UpdateSalary")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[3].Text;
            ViewState["EC"] = EC;
            GVSalaryUpdate.DataSource = FillGridSalaryUpdate();
            GVSalaryUpdate.DataBind();
            Popup6(true);
        }
        else if (e.CommandName == "del")
        {
            string EC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            EC = GVDetails.Rows[RowIndex].Cells[3].Text;
            ViewState["EC"] = EC;
            Session["View"] = 0;
            Popup1(true);
        }
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('No Record is selected to remove');$('.modal-backdrop').remove();", true);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Allowance Type');ShowPopup5();", true);
        }
        else if (txtAmount.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Amount');ShowPopup5();", true);
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 12);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["EC"].ToString());
        cmd.Parameters.AddWithValue("@AllowanceCode", ddlAllowance.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Allowance Type Already exist');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Allowance Type Added successfully','success');$('.modal-backdrop').remove();", true);

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
            e.Row.Cells[1].Text = counter.ToString();
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
            cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Salary Updated successfully','success');$('.modal-backdrop').remove();", true);

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