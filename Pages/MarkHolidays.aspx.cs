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

public partial class Pages_MarkHolidays : System.Web.UI.Page
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
            ViewState["OpenFlag"] = 1;
            FillHolidays();
        }
    }
    protected void FillHolidays()
    {
        ddlHolidayName.Items.Clear();
        ListItem item = new ListItem("Select Holiday", "0");
        ddlHolidayName.Items.Add(item);

        cmd = new SqlCommand("SP_HolidayMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlHolidayName.DataSource = dt;
        ddlHolidayName.DataTextField = "ddlText";
        ddlHolidayName.DataValueField = "ddlValue";
        ddlHolidayName.DataBind();
        dt.Dispose();
    }
    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_MarkHolidays", conn);
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
        ddlCalendatType.SelectedIndex = -1;
        ddlHolidayName.SelectedIndex = 0;
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtRemarks.Text = "";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlCalendatType.SelectedItem.Value == "-1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Calendar Type";
            ViewState["OpenFlag"] = -1;
            return;
        }
        if (ddlHolidayName.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Holiday Name";
            ViewState["OpenFlag"] = -1;
            return;
        }
        if (txtFromDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Add From Date";
            ViewState["OpenFlag"] = -1;
            return;
        }
        if (txtToDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Add To Date";
            ViewState["OpenFlag"] = -1;
            return;
        }

        if (txtRemarks.Text == "")
            txtRemarks.Text = "-";

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_MarkHolidays", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@HolidayCode", ddlHolidayName.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@CalendarTypeCode", ddlCalendatType.SelectedItem.Value);
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
            dlglbl.Text = "Holiday Already exist";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Holiday Added successfully";
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string DID = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            DID = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["DID"] = DID;
            Popup1(true);
        }
        else if (e.CommandName == "modify")
        {
            string DID = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            DID = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["DID"] = DID;
            ClearDetails();
            txtFromDate.Text = DateTime.ParseExact(GVDetails.Rows[RowIndex].Cells[3].Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToShortDateString();
            txtToDate.Text = DateTime.ParseExact(GVDetails.Rows[RowIndex].Cells[4].Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToShortDateString();
            txtRemarks.Text = GVDetails.Rows[RowIndex].Cells[5].Text;
            ddlCalendatType.ClearSelection();
            ddlCalendatType.Items.FindByValue(GVDetails.Rows[RowIndex].Cells[6].Text).Selected = true;

            FillHolidays();
            ddlHolidayName.ClearSelection();
            ddlHolidayName.Items.FindByText(GVDetails.Rows[RowIndex].Cells[2].Text).Selected = true;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;

            Popup3(true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ddlCalendatType.SelectedItem.Value == "-1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Calendar Type";
            ViewState["OpenFlag"] = -1;
            return;
        }
        if (ddlHolidayName.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Holiday Name";
            ViewState["OpenFlag"] = -1;
            return;
        }
        if (txtFromDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Add From Date";
            ViewState["OpenFlag"] = -1;
            return;
        }
        if (txtToDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Add To Date";
            ViewState["OpenFlag"] = -1;
            return;
        }

        if (txtRemarks.Text == "")
            txtRemarks.Text = "-";

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_MarkHolidays", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@HolidayCode", ddlHolidayName.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
        cmd.Parameters.AddWithValue("@CalendarTypeCode", ddlCalendatType.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@HCode", ViewState["DID"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Holiday Updated successfully";

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_MarkHolidays", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@HCode", ViewState["DID"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        int result = cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        if (result == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Record deleted successfully";

            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Record cannot be deleted";
        }

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ViewState["OpenFlag"].ToString() == "-1")
        {
            Popup3(true);
        }
        else
        {
            Popup(false);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "No Record is selected to remove";
            return;
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
                    cmd = new SqlCommand("SP_MarkHolidays", conn);
                    cmd.Parameters.AddWithValue("@calltype", 4);
                    cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
                    cmd.Parameters.AddWithValue("@HCode", row.Cells[1].Text);
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
            dlglbl.Text = "Some Records can not be removed";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Record deleted successfully";
        }
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
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

}