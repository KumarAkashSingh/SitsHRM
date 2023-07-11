using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Globalization;

public partial class Pages_ManualPunch : System.Web.UI.Page
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
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            txtDate.Text = DateTime.Now.ToString(firstDayOfMonth.ToString("dd/MM/yyyy") + " - " + lastDayOfMonth.ToString("dd/MM/yyyy"));
            FillDesignation();
            FillDepartment();
            FillBranch();
            FillEmployeeType();
            FillEmployee();
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
    }

    protected void FillBranch()
    {
        ddlBranch.Items.Clear();
        ListItem item = new ListItem("Select Organisation", "0");
        ddlBranch.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
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
        string Criteria = "";
        string FromDate = DateTime.Now.ToString("yyyy-MM-dd");
        string ToDate = DateTime.Now.ToString("yyyy-MM-dd");
        try
        {
            FromDate = DateTime.ParseExact(txtDate.Text.Split('-')[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            ToDate = DateTime.ParseExact(txtDate.Text.Split('-')[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
        }
        catch
        {

        }
        Criteria += "and cast(TransactionTime as date) between '" + FromDate + "' and '" + ToDate + "'";
        if (ddlDesignation.SelectedItem.Value != "0")
        {
            Criteria += " and EM.DesignationCode=" + ddlDesignation.SelectedItem.Value;
        }

        if (ddlDepartment.SelectedItem.Value != "0")
        {
            Criteria += " and EM.DepartmentCode=" + ddlDepartment.SelectedItem.Value;
        }
        if (ddlBranch.SelectedItem.Value != "0")
        {
            Criteria += " and EM.BranchID=" + ddlBranch.SelectedItem.Value;
        }
        if (ddlEmployeeType.SelectedItem.Value != "0")
        {
            Criteria += " and EM.EmployeetypeCode=" + ddlEmployeeType.SelectedItem.Value;
        }

        cmd = new SqlCommand("SP_ManualPunch", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@Criteria", Criteria);
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        if(ViewState["OpenFlag"] != null && ViewState["OpenFlag"].ToString()=="-1")
        {
            Popup2(true);
        }
        else
        {
            Popup(false);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(txtPunchDate.Text=="")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Punch Date');ShowPopup2();", true);
        }
        string InPunch = DateTime.ParseExact(txtPunchDate.Text + " " + txtInPunchDateTime.Text, "dd/MM/yyyy h:mm tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd h:mm tt");
        string OutPunch = DateTime.ParseExact(txtPunchDate.Text + " " + txtOutPunchDateTime.Text, "dd/MM/yyyy h:mm tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd h:mm tt");
        if (ddlEmployee.SelectedItem.Value == "-1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Employee');ShowPopup2();", true);
        }

        string BiometricCodes = "";
        foreach (ListItem item in ddlEmployee.Items)
        {
            if (item.Selected)
            {
                if(item.Value!="0")
                {
                    BiometricCodes += item.Value + ",";
                }
            }
        }
        if(BiometricCodes.Length>0)
        {
            BiometricCodes = BiometricCodes.Substring(0, BiometricCodes.Length - 1);
        }

        conn.Open();
        cmd = new SqlCommand("SP_ManualPunch", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@BiometricCodes", BiometricCodes);
        cmd.Parameters.AddWithValue("@PunchDateTime", InPunch);
        cmd.Parameters.AddWithValue("@empCode", Session["e_code"]);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        conn.Close();

        conn.Open();
        cmd = new SqlCommand("SP_ManualPunch", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@BiometricCodes", BiometricCodes);
        cmd.Parameters.AddWithValue("@PunchDateTime", OutPunch);
        cmd.Parameters.AddWithValue("@empCode", Session["e_code"]);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Punch Added Successfully','success');$('.modal-backdrop').remove();", true);


        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtPunchDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtInPunchDateTime.Text = "09:00 AM";
        txtOutPunchDateTime.Text = "05:00 PM";
        
        Popup2(true);
    }
    protected void FillEmployee()
    {
        ddlEmployee.Items.Clear();
        ListItem item = new ListItem("Select Employee", "0");
        ddlEmployee.Items.Add(item);
        item.Selected = true;

        cmd = new SqlCommand("SP_ManualPunch", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlEmployee.DataSource = dt;
        ddlEmployee.DataTextField = "EmployeeName";
        ddlEmployee.DataValueField = "NACCode";
        ddlEmployee.DataBind();
        dt.Dispose();
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
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Popup1(false);
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
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = DateTime.ParseExact(ViewState["PunchDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            conn.Open();
            cmd = new SqlCommand("SP_ManualPunch", conn);
            cmd.Parameters.AddWithValue("@calltype", 4);
            cmd.Parameters.AddWithValue("@BiometricCode", ViewState["BiometricCode"].ToString());
            cmd.Parameters.AddWithValue("@PunchDate", DateTime.ParseExact(ViewState["PunchDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@EmpCode", ViewState["E_Code"].ToString());
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
                dlglbl.Text = "Punch deleted successfully";

                GVDetails.DataSource = FillGrid();
                GVDetails.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
                dlglbl.Text = "Punch cannot be deleted. Salary processed..";
            }
        }
        catch(Exception ex)
        {

        }
        
        ViewState["OpenFlag"] = 0;
    }

    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string BiometricCode = "",PunchDate="",E_Code="";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            BiometricCode = GVDetails.Rows[RowIndex].Cells[2].Text;
            PunchDate = GVDetails.Rows[RowIndex].Cells[3].Text;
            E_Code = GVDetails.Rows[RowIndex].Cells[7].Text;

            ViewState["BiometricCode"] = BiometricCode;
            ViewState["PunchDate"] = PunchDate;
            ViewState["E_Code"] = E_Code;
            Popup1(true);
        }
    }

    protected void GVDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            if(e.Row.Cells[6].Text=="Not Approved")
            {
                LinkButton btn = (LinkButton)e.Row.Cells[7].FindControl("lnlbtnDelete");
                btn.Visible = true;
            }
            else
            {
                LinkButton btn = (LinkButton)e.Row.Cells[7].FindControl("lnlbtnDelete");
                btn.Visible = false;
            }
        }
    }

}