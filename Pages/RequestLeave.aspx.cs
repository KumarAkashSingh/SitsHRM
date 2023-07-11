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
public partial class Forms_RequestLeave : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillLeaveTypes();
            FillApplyLeaves();
            FillAssignedBy();
        }
    }
    protected void FillLeaveTypes()
    {
        ddlLeaveType.Items.Clear();
        ListItem item = new ListItem("Select Leave Type", "0");
        ddlLeaveType.Items.Add(item);

        cmd = new SqlCommand("SP_RequestLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlLeaveType.DataSource = dt;
        ddlLeaveType.DataTextField = "LeaveDescription";
        ddlLeaveType.DataValueField = "LeaveCode";
        ddlLeaveType.DataBind();
        dt.Dispose();
    }
    protected void FillAssignedBy()
    {
        ddlAssignedBy.Items.Clear();
        ListItem item = new ListItem("Select Assigned By", "-1");
        ddlAssignedBy.Items.Add(item);

        cmd = new SqlCommand("SP_RequestLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlAssignedBy.DataSource = dt;
        ddlAssignedBy.DataTextField = "EmployeeName";
        ddlAssignedBy.DataValueField = "E_Code";
        ddlAssignedBy.DataBind();
        dt.Dispose();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtStartDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Enter Leave From Date";

            return;
        }
        else if (txtEndDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Enter Leave To Date";

            return;
        }
        else if (DateTime.ParseExact(txtStartDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture) > DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Valid Date Range";

            return;
        }
        else if (DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) != DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) && ddlLeaveType.SelectedItem.Text.Contains("Compensatory"))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Compensatory can be requested for Full day or Half Day for single Date";

            return;
        }
        //Check Punch Time for Compensatory Leave
        else if (ddlLeaveType.SelectedItem.Text.Contains("Compensatory"))
        {
            cmd = new SqlCommand("SP_RequestLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 17);
            cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();

            if(dt.Rows.Count>0)
            {
                int TotalSeconds = int.Parse(dt.Rows[0][0].ToString());
                if(TotalSeconds == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
                    dlglbl.Text = "No Biometric Punch found!! Can't apply";
                    return;
                }
                if (TotalSeconds != 0)
                {
                    if(ddlHalfFull.SelectedItem.Text=="Full Day" && TotalSeconds<21600)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
                        dlglbl.Text = "Less than 6 Hours working!! Only Half Day can be allowed";
                        return;
                    }
                }
            }
        }
        else if (ddlLeaveType.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Leave Type";

            return;
        }
        else if (ddlHalfFull.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Type";

            return;
        }
        else if (txtContactNo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Enter Contact No";

            return;
        }
        else if (txtAddress.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Enter Address";

            return;
        }

        double TotalDays = (DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) - DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)).TotalDays + 1;

        if (TotalDays > 1 && (ddlHalfFull.SelectedItem.Value == "2" || ddlHalfFull.SelectedItem.Value == "3"))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Half Day Only Allowed For Single Day";
            return;
        }

        cmd = new SqlCommand("SP_RequestLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtStartDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Already Made Request For This Period";
            return;
        }

        cmd = new SqlCommand("SP_ApplyLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 10);
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtStartDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@LeaveType", ddlLeaveType.SelectedItem.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "During This Period !!! Already On Leave";
            ViewState["OpenFlag"] = 0;
            return;
        }


        conn.Open();
        cmd = new SqlCommand("SP_RequestLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        cmd.Parameters.AddWithValue("@LeaveCode", ddlLeaveType.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Description", txtRemarks.Text);
        cmd.Parameters.AddWithValue("@AssignedByName", ddlAssignedBy.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@LeaveType", ddlHalfFull.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@AssignedBy", ddlAssignedBy.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Contact", txtContactNo.Text);
        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = ddlLeaveType.SelectedItem.Text + " Requested Successfully ";
        FillApplyLeaves();
    }
    protected void FillApplyLeaves()
    {
        GVApplyLeave.DataSource = FillGrid();
        GVApplyLeave.DataBind();
    }
    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_RequestLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
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
    
    protected void GVApplyLeave_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "getfile")
        {
            string AC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            AC = GVApplyLeave.Rows[RowIndex].Cells[1].Text;
            ViewState["AC"] = AC;

            Popup3(true);
        }
        else if (e.CommandName == "del")
        {
            string AC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            AC = GVApplyLeave.Rows[RowIndex].Cells[1].Text;
            ViewState["AC"] = AC;

            Popup1(true);
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Popup(false);
    }
    void Popup(bool isDisplay)
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
    protected void btnClose3_Click(object sender, EventArgs e)
    {
        Popup3(false);
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
    
    protected void btnYes1_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_RequestLeave", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@AttendanceCode", ViewState["AC"].ToString());
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
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
            dlglbl.Text = "Record deleted successfully";

            GVApplyLeave.DataSource = FillGrid();
            GVApplyLeave.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Record cannot be deleted";
        }

        FillApplyLeaves();
    }
    protected void btnNo2_Click(object sender, EventArgs e)
    {
        Popup2(false);
    }
    protected void btnNo1_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        FileInfo fi = null;
        byte[] documentcontent = new byte[1000];
        string Filename = "";
        string fileexten = "";
        string contenttype = "";
        if (FileUpload1.HasFile)
        {
            fi = new FileInfo(FileUpload1.FileName);
            documentcontent = FileUpload1.FileBytes;
            Filename = fi.Name;
            fileexten = fi.Extension;
            contenttype = FileUpload1.PostedFile.ContentType;

            conn.Open();
            cmd = new SqlCommand("SP_RequestLeave", conn);
            cmd.Parameters.AddWithValue("@calltype", 7);
            cmd.Parameters.AddWithValue("@Attachment", documentcontent);
            cmd.Parameters.AddWithValue("@contenttype", contenttype);
            cmd.Parameters.AddWithValue("@filename", Filename);
            cmd.Parameters.AddWithValue("@Attendancecode", ViewState["AC"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

            GVApplyLeave.DataSource = FillGrid();
            GVApplyLeave.DataBind();

            Refresh();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select File to Upload";

            return;
        }
    }
    void Popup2(bool isDisplay)
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
    protected void GVApplyLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[10].Text == "&nbsp;")
            {
                e.Row.Cells[12].Text = "No File";
            }

            if (e.Row.Cells[9].Text == "Approved")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                LinkButton btn = (LinkButton)e.Row.Cells[11].FindControl("lnlbtnDelete");
                btn.Visible = false;
            }
            else if (e.Row.Cells[9].Text == "Rejected")
            {
                e.Row.BackColor = System.Drawing.Color.LightPink;
            }
        }
    }

    protected void Refresh()
    {
        foreach (GridViewRow row in GVApplyLeave.Rows)
        {
            if (row.Cells[10].Text == "&nbsp;")
            {
                row.Cells[12].Text = "No File";
            }

            if (row.Cells[9].Text == "Approved")
            {
                row.BackColor = System.Drawing.Color.LightGreen;
                LinkButton btn = (LinkButton)row.Cells[13].FindControl("lnlbtnDelete");
                btn.Visible = false;
            }
            else if (row.Cells[9].Text == "Rejected")
            {
                row.BackColor = System.Drawing.Color.LightPink;
            }
        }
    }
}