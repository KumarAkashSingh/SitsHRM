using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class Pages_Payroll_AllowanceMaster : System.Web.UI.Page
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
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();

            txtPreference.Attributes.Add("onkeypress", "return isNumberKey(event)");
            txtAbsDeduction.Attributes.Add("onkeypress", "return isNumberKey(event)");
        }
    }

    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
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
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearDetails();
        btnAdd.Visible = true;
        btnUpdate.Visible = false;
        Popup3(true);
    }
    protected void ClearDetails()
    {
        txtAllowance.Text = "";
        IsAddToGrossSalary.Checked = false;
        IsAllowDeduction.Checked = false;
        txtAbsDeduction.Text = "0";
        txtPreference.Text = "0";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtAllowance.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Allowance Type');ShowPopup3();", true);
        }
        else if (txtPreference.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Preference');ShowPopup3();", true);
        }
        else if (txtAbsDeduction.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Absentism Deduction');ShowPopup3();", true);
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@AllowanceDescription", txtAllowance.Text);
        cmd.Parameters.AddWithValue("@IsAddToGrossSalary", IsAddToGrossSalary.Checked);
        cmd.Parameters.AddWithValue("@IsAllowDeduction", IsAllowDeduction.Checked);
        cmd.Parameters.AddWithValue("@AbsentismDeduction", txtAbsDeduction.Text);
        cmd.Parameters.AddWithValue("@Preference", txtPreference.Text);
        cmd.Parameters.AddWithValue("@CreatedBy", Session["e_code"].ToString());
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
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Allowance Type Added successfully','success');$('.modal-backdrop').remove();", true);
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string AC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            AC = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["AC"] = AC;
            Popup1(true);
        }
        else if (e.CommandName == "modify")
        {
            string AC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            AC = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["AC"] = AC;
            ClearDetails();
            txtAllowance.Text = GVDetails.Rows[RowIndex].Cells[2].Text;

            CheckBox chKAddToGrossSalary = (CheckBox)GVDetails.Rows[RowIndex].FindControl("chKAddToGrossSalary");
            IsAddToGrossSalary.Checked = chKAddToGrossSalary.Checked;

            CheckBox chKAllowDeduction = (CheckBox)GVDetails.Rows[RowIndex].FindControl("chKAllowDeduction");
            IsAllowDeduction.Checked = chKAllowDeduction.Checked;

            txtAbsDeduction.Text = GVDetails.Rows[RowIndex].Cells[5].Text;

            txtPreference.Text = GVDetails.Rows[RowIndex].Cells[6].Text;

            btnAdd.Visible = false;
            btnUpdate.Visible = true;

            ViewState["OldValue"] = GVDetails.Rows[RowIndex].Cells[2].Text;
            Popup3(true);
        }
        else if (e.CommandName == "AddPercent")
        {
            string AC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            AC = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["AC"] = AC;
            FillAllowanceTypes();
            GVPercent.DataSource = FillGridPercent();
            GVPercent.DataBind();
            txtPercent.Text = "";
            Popup4(true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtAllowance.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Allowance Type');ShowPopup3();", true);
        }
        else if (txtPreference.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Preference');ShowPopup3();", true);
        }
        else if (txtAbsDeduction.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Absentism Deduction');ShowPopup3();", true);
        }
        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@AllowanceDescription", txtAllowance.Text);
        cmd.Parameters.AddWithValue("@AllowanceCode", ViewState["AC"].ToString());
        cmd.Parameters.AddWithValue("@IsAddToGrossSalary", IsAddToGrossSalary.Checked);
        cmd.Parameters.AddWithValue("@IsAllowDeduction", IsAllowDeduction.Checked);
        cmd.Parameters.AddWithValue("@AbsentismDeduction", txtAbsDeduction.Text);
        cmd.Parameters.AddWithValue("@Preference", txtPreference.Text);
        cmd.Parameters.AddWithValue("@UpdatedBy", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Allowance Type Updated successfully','success');$('.modal-backdrop').remove();", true);
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@AllowanceCode", ViewState["AC"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record deleted successfully',success);$('.modal-backdrop').remove();", true);

            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record cannot be deleted');$('.modal-backdrop').remove();", true);
        }
        ViewState["OpenFlag"] = 0;
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ViewState["OpenFlag"] != null && ViewState["OpenFlag"].ToString() == "-1")
        {
            Popup3(true);
        }
        else if (ViewState["OpenFlag"] != null && ViewState["OpenFlag"].ToString() == "-2")
        {
            Popup4(true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "HidePopup();", true);
        }
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

        Popup2(true);
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
                    cmd = new SqlCommand("SP_Payroll", conn);
                    cmd.Parameters.AddWithValue("@calltype", 4);
                    cmd.Parameters.AddWithValue("@AllowanceCode", row.Cells[1].Text);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Some Record cannot be removed');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record deleted successfully',success);$('.modal-backdrop').remove();", true);
        }
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
        ViewState["OpenFlag"] = 0;
    }
    protected void btnNo2_Click(object sender, EventArgs e)
    {
        Popup2(false);
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

    }
    protected void GVPercent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeletePercent")
        {
            string PAC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            PAC = GVPercent.Rows[RowIndex].Cells[0].Text;
            ViewState["PAC"] = PAC;
            Popup5(true);
        }
    }
    protected void btnNo5_Click(object sender, EventArgs e)
    {
        Popup4(true);
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
    protected void btnYes5_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 8);
        cmd.Parameters.AddWithValue("@AllowanceCode", ViewState["AC"].ToString());
        cmd.Parameters.AddWithValue("@PercentOfAllowanceCode", ViewState["PAC"].ToString());
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
            GVPercent.DataSource = FillGridPercent();
            GVPercent.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record cannot be deleted');$('.modal-backdrop').remove();", true);
        }

        ViewState["OpenFlag"] = -2;
    }
    protected void btnClosePercent_Click(object sender, EventArgs e)
    {
        Popup4(false);
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
    protected void FillAllowanceTypes()
    {
        ddlAllowance.Items.Clear();
        ListItem item = new ListItem("Select Allowance Type", "0");
        ddlAllowance.Items.Add(item);

        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.Parameters.AddWithValue("@AllowanceCode", ViewState["AC"].ToString());
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
    protected void btnSavePercent_Click(object sender, EventArgs e)
    {
        if (txtPercent.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Percent');ShowPopup4();", true);
        }
        else if (ddlAllowance.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Allowance');ShowPopup4();", true);
        }

        conn.Open();
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
        cmd.Parameters.AddWithValue("@AllowanceCode", ViewState["AC"].ToString());
        cmd.Parameters.AddWithValue("@PercentOfAllowanceCode", ddlAllowance.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Percentage", txtPercent.Text);
        cmd.Parameters.AddWithValue("@CreatedBy", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Percent Already exist');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Percent Added successfully','success');$('.modal-backdrop').remove();", true);
        }

        GVPercent.DataSource = FillGridPercent();
        GVPercent.DataBind();
        ViewState["OpenFlag"] = -2;
    }
    protected DataTable FillGridPercent()
    {
        cmd = new SqlCommand("SP_Payroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@AllowanceCode", ViewState["AC"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        lblPercentRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " Record(s) Found]";
        return dt;
    }
}