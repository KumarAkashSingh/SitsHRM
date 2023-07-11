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

public partial class Pages_SetMonthlySalarySD : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;
    DataTable dt2 = null;
    int count = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillYear();
            FillStatus();
            FillDesignation();
            FillDepartment();
            FillBranch();
            FillEmployeeType();

            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
    }
    private void FillYear()
    {
        ddlYear.Items.Clear();
        int i = DateTime.Now.Year;
        for (int j = i; j > i - 5; j--)
        {
            ListItem item = new ListItem(j.ToString(), j.ToString());
            ddlYear.Items.Add(item);
        }
        if (ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()) != null)
        {
            ddlMonth.ClearSelection();
            ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
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
        DateTime date = new DateTime(int.Parse(ddlYear.SelectedItem.Value), int.Parse(ddlMonth.SelectedItem.Value), 1);
        var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
        string FilterCriteria = " and 1=1";

        if (ddlStatus.SelectedItem.Value != "0")
        {
            FilterCriteria += " and status = '" + ddlStatus.SelectedItem.Text + "'";
        }
        else
        {
            FilterCriteria += " and status = 'A'";
        }

        if (ddlDesignation.SelectedItem.Value != "0")
        {
            FilterCriteria += " and EM.DesignationCode=" + ddlDesignation.SelectedItem.Value;
        }

        if (ddlDepartment.SelectedItem.Value != "0")
        {
            FilterCriteria += " and EM.DepartmentCode=" + ddlDepartment.SelectedItem.Value;
        }

        if (ddlBranch.SelectedItem.Value != "0")
        {
            FilterCriteria += " and EM.BranchID=" + ddlBranch.SelectedItem.Value;
        }
        if (ddlEmployeeType.SelectedItem.Value != "0")
        {
            FilterCriteria += " and EmployeetypeCode=" + ddlEmployeeType.SelectedItem.Value;
        }
        //if (chkFilTer.Checked)
        //{
        //    FilterCriteria += "and EM.IsReducedSalary=1";
        //}
        cmd = new SqlCommand("SP_Payroll_SetMonthlySalaryNew2", conn);
        cmd.Parameters.AddWithValue("@FromDate", firstDayOfMonth);
        cmd.Parameters.AddWithValue("@ToDate", lastDayOfMonth);
        cmd.Parameters.AddWithValue("@FilterCriteria", FilterCriteria);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 600;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        string[] arr = new string[10];

        for (int i = 1; i < 10; i++)
        {
            dt.Columns.RemoveAt(12);
        }

        foreach (DataRow row in dt.Rows)
        {
            double AbsentDays = 0;
            double TotalDays = 0;
            double PresentDays = 0;
            double GrossSalary = 0;
            double NetSalary = 0;
            double NetSalaryForOtherAllowance = 0;
            bool ESIAllowanceFlag = false;
            double ESIAllowance = 0;
            double NetPositive = 0;
            double NetNegative = 0;

            TotalDays = double.Parse(row[7].ToString());
            PresentDays = double.Parse(row[8].ToString());
            AbsentDays = double.Parse(row[9].ToString());

            GrossSalary = double.Parse(row[10].ToString());

            //Procedure to find NetSalary
            cmd = new SqlCommand("SP_Payroll", conn);
            cmd.Parameters.AddWithValue("@calltype", 26);
            cmd.Parameters.AddWithValue("@E_Code", row[3].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt2 = new DataTable();
            da.Fill(dt2);
            cmd.Dispose();
            da.Dispose();
            NetSalary = Convert.ToDouble(dt2.Rows[0][0].ToString());

            //All Allowances of Employee
            cmd = new SqlCommand("SP_Payroll", conn);
            cmd.Parameters.AddWithValue("@calltype", 18);
            cmd.Parameters.AddWithValue("@E_Code", row[3].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt1 = new DataTable();
            da.Fill(dt1);
            cmd.Dispose();
            da.Dispose();

            //double netsalary = 0, netsalary1 = 0;
            //double temp = 0;
            //double AllowanceWiseSalary = 0;

            //netsalary = (Present / Total) * Convert.ToDouble(row[11].ToString());
            //netsalary1 = netsalary;

            double temp = 0;
            double AllowanceWiseSalary = 0;
            double OtherAllowance = 0;
            double Arrear = 0;
            double Basic = 0, HRA = 0, Allowance = 0;

            NetSalaryForOtherAllowance = ((TotalDays - AbsentDays) / TotalDays) * NetSalary;

            OtherAllowance = NetSalaryForOtherAllowance;

            foreach (DataRow row1 in dt1.Rows)
            {
                if (row1["AllowanceCode"].ToString() == "4")
                {

                }
                if (row1["AllowanceCode"].ToString() != "5")
                {
                    //Calculation For PF according to Basic Salary
                    if (row1["IsAddToGrossSalary"].ToString() == "True" && row1["AllowanceCode"].ToString() == "1" && row1["IsPF"].ToString() == "1")
                    {
                        temp = (PresentDays / TotalDays) * Convert.ToDouble(row1["Amount"].ToString()) * .12;
                        if (temp > 1800)
                        {
                            temp = 1800;
                        }
                        NetNegative += Math.Round(temp);
                        //PF Allowance
                        row[17] = row[31] = Math.Round(temp);
                    }
                    //Calculation for ESI
                    if (row1["IsAddToGrossSalary"].ToString() == "False" && row1["AllowanceCode"].ToString() == "7")
                    {
                        ESIAllowanceFlag = true;

                        //ESI Allowance
                        //row[18] = row[31] = Math.Ceiling(temp);
                    }

                    if (row1["IsAddToGrossSalary"].ToString() == "False" && row1["AllowanceCode"].ToString() != "6" && row1["AllowanceCode"].ToString() != "7")
                    {
                        temp = Convert.ToDouble(row1["Amount"].ToString());

                        //Just to display the Allowance Codes Salary

                        AllowanceWiseSalary = Convert.ToDouble(row1["Amount"].ToString());
                        AllowanceWiseSalary = Math.Round(AllowanceWiseSalary);

                        NetNegative += Math.Round(temp);

                        //Transport
                        if (row1["AllowanceCode"].ToString() == "8")
                        {
                            row[19] = row[33] = AllowanceWiseSalary;
                        }
                        //Electricity
                        if (row1["AllowanceCode"].ToString() == "9")
                        {
                            row[20] = row[34] = AllowanceWiseSalary;
                        }
                        //Accomodation
                        if (row1["AllowanceCode"].ToString() == "10")
                        {
                            row[21] = row[35] = AllowanceWiseSalary;
                        }
                        //TDS
                        if (row1["AllowanceCode"].ToString() == "11")
                        {
                            row[22] = row[36] = AllowanceWiseSalary;
                        }
                        //Advance Loan
                        if (row1["AllowanceCode"].ToString() == "12")
                        {
                            row[23] = row[37] = AllowanceWiseSalary;
                        }
                        //Security Amount
                        if (row1["AllowanceCode"].ToString() == "15")
                        {
                            row[25] = row[39] = AllowanceWiseSalary;
                        }
                    }

                    //Just to display the Allowance Codes Salary
                    if (row1["IsAddToGrossSalary"].ToString() == "True" && row1["AllowanceCode"].ToString() != "6" && row1["AllowanceCode"].ToString() != "7")
                    {
                        if (row1["AllowanceCode"].ToString() == "13")
                        {
                            AllowanceWiseSalary = Convert.ToDouble(row1["Amount"].ToString());
                            AllowanceWiseSalary = Math.Round(AllowanceWiseSalary);
                        }
                        else
                        {
                            AllowanceWiseSalary = (PresentDays / TotalDays) * Convert.ToDouble(row1["Amount"].ToString());
                            AllowanceWiseSalary = Math.Round(AllowanceWiseSalary);
                        }

                        NetPositive += AllowanceWiseSalary;

                        OtherAllowance = OtherAllowance - Math.Round((PresentDays / TotalDays) * Convert.ToDouble(row1["Amount"].ToString()));



                        //Basic + DA
                        if (row1["AllowanceCode"].ToString() == "1")
                        {
                            row[12] = row[26] = AllowanceWiseSalary;
                            Basic = AllowanceWiseSalary;
                        }
                        //HRA
                        if (row1["AllowanceCode"].ToString() == "2")
                        {
                            row[13] = row[27] = AllowanceWiseSalary;
                            HRA = AllowanceWiseSalary;
                        }
                        //Allowance
                        if (row1["AllowanceCode"].ToString() == "3")
                        {
                            row[14] = row[28] = AllowanceWiseSalary;
                            Allowance = AllowanceWiseSalary;
                        }
                        //if (row1["AllowanceCode"].ToString() == "4")
                        //{
                        //    row[15] = row[28] = AllowanceWiseSalary;
                        //}
                        //Arrear
                        if (row1["AllowanceCode"].ToString() == "13")
                        {
                            row[24] = row[38] = AllowanceWiseSalary;
                            Arrear = AllowanceWiseSalary;
                        }
                    }
                }
            }
            //row[11] = Math.Round(netsalary);

            //ESI Allowance
            if (ESIAllowanceFlag)
            {
                //ESIAllowance = (PresentDays / TotalDays) * GrossSalary * .75 / 100;
                ESIAllowance = (Basic + HRA + Allowance) * .75 / 100;

                if (OtherAllowance != 0)
                {
                    ESIAllowance = ESIAllowance + (OtherAllowance * .75) / 100;
                }
                if (Arrear > 0)
                {
                    ESIAllowance = ESIAllowance + (Arrear * .75) / 100;
                }
                NetNegative += Math.Ceiling(ESIAllowance);
                row[18] = row[32] = Math.Ceiling(ESIAllowance).ToString();
            }

            NetPositive += OtherAllowance;
            row[15] = row[29] = Math.Round(OtherAllowance).ToString();

            //Calculation of Net Salary based on Allowance heads
            row[11] = Math.Round(NetPositive - NetNegative).ToString();
        }

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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        //Popup(false);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "runReplacement();HidePopup();", true);

    }
    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeader = (CheckBox)GVDetails.HeaderRow.FindControl("chkHeader");

        foreach (GridViewRow row in GVDetails.Rows)
        {
            int x = row.Cells.Count;

            CheckBox cbx = (CheckBox)row.FindControl("chkCtrl");
            if (chkHeader.Checked)
            {
                cbx.Checked = true;
            }
            else
            {
                cbx.Checked = false;
            }

            if (row.Cells[x - 1].Text != "0")
            {
                cbx.Enabled = false;
            }
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "runReplacement();", true);
    }

    protected void GVDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[4].CssClass = "hidden";
            e.Row.Cells[5].CssClass = "hidden";
            for (int i = 13; i <= 26; i++)
                e.Row.Cells[i].CssClass = "hidden";

            int x = e.Row.Cells.Count;
            e.Row.Cells[x - 2].CssClass = "hidden";
            e.Row.Cells[x - 3].CssClass = "hidden";

            for (int i = 41; i < e.Row.Cells.Count - 3; i++)
                e.Row.Cells[i].Text = Convert.ToDateTime(e.Row.Cells[i].Text).ToString("dd MMM");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                string encoded = e.Row.Cells[i].Text;
                e.Row.Cells[i].Text = Context.Server.HtmlDecode(encoded);

                e.Row.Cells[i].ToolTip = e.Row.Cells[3].Text + " [" + e.Row.Cells[6].Text + "]" + " (" + e.Row.Cells[7].Text + ")";
            }

            e.Row.Cells[1].Text = count.ToString();
            count++;

            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }

            e.Row.Cells[4].CssClass = "hidden";
            e.Row.Cells[5].CssClass = "hidden";

            int x = e.Row.Cells.Count;
            e.Row.Cells[x - 2].CssClass = "hidden";
            e.Row.Cells[x - 3].CssClass = "hidden";

            for (int i = 13; i <= 26; i++)
                e.Row.Cells[i].CssClass = "hidden";

            if (e.Row.Cells[x - 1].Text != "0")
            {
                CheckBox chkCtrl = (CheckBox)e.Row.Cells[0].FindControl("chkCtrl");
                chkCtrl.Enabled = false;
                e.Row.ToolTip = "Leave Pending For Approval. Salary can't be generated";
            }
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
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    string InPunch = DateTime.ParseExact(hidDate.Value + " " + txtInPunchDateTime.Text, "yyyy-MM-dd h:mm tt", CultureInfo.InvariantCulture).ToString();
    //    string OutPunch = DateTime.ParseExact(hidDate.Value + " " + txtOutPunchDateTime.Text, "yyyy-MM-dd h:mm tt", CultureInfo.InvariantCulture).ToString();

    //    if (hidNacCode.Value == "0")
    //    {
    //        dlglbl.Text = "Please Update Biometric code first";
    //        ViewState["OpenFlag"] = -1;
    //        Popup(true);
    //        return;
    //    }
    //    //conn.Open();
    //    //cmd = new SqlCommand("SP_ManualPunch", conn);
    //    //cmd.Parameters.AddWithValue("@calltype", 2);
    //    //cmd.Parameters.AddWithValue("@BiometricCode", hidNacCode.Value);
    //    //cmd.Parameters.AddWithValue("@PunchDateTime", InPunch);
    //    //cmd.Parameters.AddWithValue("@empCode", Session["empCode"]);
    //    //cmd.CommandType = CommandType.StoredProcedure;
    //    //cmd.ExecuteNonQuery();
    //    //conn.Close();

    //    //conn.Open();
    //    //cmd = new SqlCommand("SP_ManualPunch", conn);
    //    //cmd.Parameters.AddWithValue("@calltype", 2);
    //    //cmd.Parameters.AddWithValue("@BiometricCode", hidNacCode.Value);
    //    //cmd.Parameters.AddWithValue("@PunchDateTime", OutPunch);
    //    //cmd.Parameters.AddWithValue("@empCode", Session["empCode"]);
    //    //cmd.CommandType = CommandType.StoredProcedure;
    //    //cmd.ExecuteNonQuery();
    //    //conn.Close();

    //    dlglbl.Text = "Punch details sent for Approval";
    //    ViewState["OpenFlag"] = null;
    //    //Popup(true);

    //    GVDetails.DataSource = FillGrid();
    //    GVDetails.DataBind();
    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "runReplacement();ShowPopup();", true);

    //}
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
}