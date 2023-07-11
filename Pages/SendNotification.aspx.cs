using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

public partial class Pages_SendNotification : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    int count = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GVViewCircularNotice.DataSource = FillGrid();
            GVViewCircularNotice.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int result = 0;
        string TypeCodes = "";

        foreach (ListItem item in ddlEmpDept.Items)
        {
            if (item.Selected)
            {
                TypeCodes += item.Value + ',';
            }
        }
        if (TypeCodes.Length > 0)
        {
            TypeCodes = TypeCodes.Substring(0, TypeCodes.Length - 1);
        }

        conn.Open();
        cmd = new SqlCommand("SP_SendNotifications", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@Title", txtSubject.Text);
        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
        cmd.Parameters.AddWithValue("@Type", ddlEmp.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@empcode", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@TypeCodes", TypeCodes);
        SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
        parm.Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(parm);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        result = Convert.ToInt32(parm.Value);
        cmd.Dispose();
        conn.Close();

        GVViewCircularNotice.DataSource = FillGrid();
        GVViewCircularNotice.DataBind();
        ddlEmp.ClearSelection();
        ddlEmpDept.ClearSelection();
        txtSubject.Text = "";
        txtDescription.Text = "";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('Notification sent successfully');", true);
        //dlglbl.Text = "Circular uploded successfully";
    }
    public DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_SendNotifications", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@empcode", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        lblRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + "<span class='hidden-xs'> Record(s) Found</span>]";
        return dt;
    }
    protected void chkCtrl_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow currentrow = (GridViewRow)((CheckBox)sender).Parent.Parent.Parent.Parent;
        CheckBox cbx = default(CheckBox);
        cbx = (CheckBox)currentrow.FindControl("chkCtrl");

        conn.Open();
        cmd = new SqlCommand("SP_SendNotifications", conn);
        if(cbx.Checked)
        {
            cmd.Parameters.AddWithValue("@calltype", 5);
        }
        else
        {
            cmd.Parameters.AddWithValue("@calltype", 4);
        }
        cmd.Parameters.AddWithValue("@NotificationID", currentrow.Cells[0].Text);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Popup(false);
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
    
    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmp.SelectedItem.Value == "0")
        {
            ddlEmpDept.Items.Clear();
        }
        else if (ddlEmp.SelectedItem.Value == "1")
        {
            FillDepartment();
        }
        else if (ddlEmp.SelectedItem.Value == "2")
        {
            FillDesignation();
        }
        else if (ddlEmp.SelectedItem.Value == "3")
        {
            FillEmployee();
        }
    }
    protected void FillDepartment()
    {
        ddlEmpDept.Items.Clear();
        ListItem item = new ListItem("Select Department", "0");
        ddlEmpDept.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlEmpDept.DataSource = dt;
        ddlEmpDept.DataTextField = "ddlText";
        ddlEmpDept.DataValueField = "ddlValue";
        ddlEmpDept.DataBind();
        dt.Dispose();
    }

    protected void FillDesignation()
    {
        ddlEmpDept.Items.Clear();
        ListItem item = new ListItem("Select Designation", "0");
        ddlEmpDept.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlEmpDept.DataSource = dt;
        ddlEmpDept.DataTextField = "ddlText";
        ddlEmpDept.DataValueField = "ddlValue";
        ddlEmpDept.DataBind();
        dt.Dispose();
    }
    protected void FillEmployee()
    {
        ddlEmpDept.Items.Clear();
        ListItem item = new ListItem("Select Employee", "0");
        ddlEmpDept.Items.Add(item);

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 8);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlEmpDept.DataSource = dt;
        ddlEmpDept.DataTextField = "EmployeeName";
        ddlEmpDept.DataValueField = "E_Code";
        ddlEmpDept.DataBind();
        dt.Dispose();
    }

    protected void GVViewCircularNotice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = count.ToString();
            count++;
        }
    }
}