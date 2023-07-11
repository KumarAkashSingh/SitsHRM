using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ClosedXML.Excel;
using Newtonsoft.Json;

public partial class EmployeePaySlips : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;
    int count = 1;
    int count1 = 1;
    int counter = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillYear();
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
    }

    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_EmployeePayroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@ForYear", ddlYear.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
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
        Popup(false);
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewSalary")
        {
            string MonthValue = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            MonthValue = GVDetails.Rows[RowIndex].Cells[0].Text;
            ViewState["MonthValue"] = MonthValue;
            ViewState["PSNo"] = GVDetails.Rows[RowIndex].Cells[1].Text;
            FillGridEmplyeeDetails();
            Popup5(true);
        }
    }

    protected void FillGridEmplyeeDetails()
    {
        cmd = new SqlCommand("SP_EmployeePayroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@PaySlipNo", ViewState["PSNo"].ToString());
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        hddtDetails.Value = JsonConvert.SerializeObject(dt);

        cmd = new SqlCommand("SP_EmployeePayroll", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@PaySlipNo", ViewState["PSNo"].ToString());
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        hddSalaryAmountDetails.Value = JsonConvert.SerializeObject(dt);

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
}