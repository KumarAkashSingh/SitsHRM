using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MasterEmployeeTiming : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    System.Data.DataTable dt = null;
    System.Data.DataTable dt1 = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();

            //FillInstitute();
            //FillInstituteFilter();

            txtOutTime.Attributes.Add("onkeypress", "return isNumberKey(event)");
            txtLunchFrom.Attributes.Add("onkeypress", "return isNumberKey(event)");
            txtLunchTo.Attributes.Add("onkeypress", "return isNumberKey(event)");
        }
    }
    protected System.Data.DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_MasterEmploeeTiming", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new System.Data.DataTable();
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
        txtDescription.Text = "";
        txtInTime.Text = "";
        txtOutTime.Text = "";
        txtLunchFrom.Text = "";
        txtLunchTo.Text = "";
        //ddlInstitute.SelectedIndex = 0;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int res;
        if (txtDescription.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Description');ShowPopup3();", true);
            return;
        }
        if (txtInTime.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add In Time');ShowPopup3();", true);
            return;
        }
        if (txtOutTime.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Out Time');ShowPopup3();", true);
            return;
        }
        if (txtLunchFrom.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Lunch Time');ShowPopup3();", true);
            return;
        }
        if (txtLunchTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Lunch Time');ShowPopup3();", true);
            return;
        }


        conn.Open();
        cmd = new SqlCommand("SP_MasterEmploeeTiming", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
        cmd.Parameters.AddWithValue("@InTime", txtInTime.Text);
        cmd.Parameters.AddWithValue("@OutTime", txtOutTime.Text);
        cmd.Parameters.AddWithValue("@Ltimefrom", txtLunchFrom.Text);
        cmd.Parameters.AddWithValue("@Ltimeto", txtLunchTo.Text);

        //For Audit Trail
        cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
        cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
        cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Entry Already exist','warn');ShowPopup3();", true);
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Entry Added successfully','success'); $('.modal-backdrop').remove();", true);
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string SID = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            SID = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["SID"] = SID;
            Popup1(true);
        }
        else if (e.CommandName == "modify")
        {
            string SID = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            SID = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["SID"] = SID;

            ClearDetails();
            //if (ddlInstitute.Items.FindByValue(GVDetails.Rows[RowIndex].Cells[2].Text) != null)
            //{
            //    ddlInstitute.ClearSelection();
            //    ddlInstitute.Items.FindByValue(GVDetails.Rows[RowIndex].Cells[2].Text).Selected = true;
            //}

            txtDescription.Text = GVDetails.Rows[RowIndex].Cells[2].Text;
            txtInTime.Text = GVDetails.Rows[RowIndex].Cells[3].Text;

            

            txtLunchFrom.Text = GVDetails.Rows[RowIndex].Cells[5].Text.Replace("&nbsp;", "");
            txtLunchTo.Text = GVDetails.Rows[RowIndex].Cells[6].Text.Replace("&nbsp;", "");
            txtOutTime.Text = GVDetails.Rows[RowIndex].Cells[4].Text;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;

            ViewState["OldValue"] = GVDetails.Rows[RowIndex].Cells[2].Text;
            Popup3(true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int res;
        if (txtDescription.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Description');ShowPopup3();", true);
            return;
        }
        if (txtInTime.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add In Time');ShowPopup3();", true);
            return;
        }
        if (txtOutTime.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Out Time');ShowPopup3();", true);
            return;
        }
        if (txtLunchFrom.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Lunch Time');ShowPopup3();", true);
            return;
        }
        if (txtLunchTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Lunch Time');ShowPopup3();", true);
            return;
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_MasterEmploeeTiming", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
        cmd.Parameters.AddWithValue("@ID", ViewState["SID"]);
        cmd.Parameters.AddWithValue("@InTime", txtInTime.Text);
        cmd.Parameters.AddWithValue("@OutTime", txtOutTime.Text);
        cmd.Parameters.AddWithValue("@Ltimefrom", txtLunchFrom.Text);
        cmd.Parameters.AddWithValue("@Ltimeto", txtLunchTo.Text);
        cmd.Parameters.AddWithValue("@OldValue", ViewState["OldValue"].ToString());

        //For Audit Trail
        cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
        cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
        cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Entry Already exist', 'warn');ShowPopup3();", true);
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Entry Updated successfully', 'success');$('.modal-backdrop').remove();", true);
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_MasterEmploeeTiming", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@ID", ViewState["SID"].ToString());

        //For Audit Trail
        cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
        cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
        cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
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

            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record cannot be deleted');$('.modal-backdrop').remove();", true);
        }
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


    protected void GVDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVDetails.PageIndex = e.NewPageIndex;
        if (Session["SortedView"] != null)
        {
            GVDetails.DataSource = Session["SortedView"];
            GVDetails.DataBind();
        }
        else
        {
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
    }
    public SortDirection direction
    {
        get
        {
            if (ViewState["directionState"] == null)
            {
                ViewState["directionState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["directionState"];
        }
        set
        {
            ViewState["directionState"] = value;
        }
    }
    protected void GVDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (direction == SortDirection.Ascending)
        {
            direction = SortDirection.Descending;
            sortingDirection = "Desc";

        }
        else
        {
            direction = SortDirection.Ascending;
            sortingDirection = "Asc";

        }
        DataView sortedView = new DataView(FillGrid());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        //
        GVDetails.DataSource = sortedView;
        GVDetails.DataBind();
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('No Record is selected to remove','warn');", true);
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
                    cmd = new SqlCommand("SP_MasterEmploeeTiming", conn);
                    cmd.Parameters.AddWithValue("@calltype", 4);
                    cmd.Parameters.AddWithValue("@ID", row.Cells[1].Text);

                    //For Audit Trail
                    cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
                    cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
                    cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
                    cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
                    cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Some Records can not be removed');$('.modal-backdrop').remove();", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record deleted successfully','success');$('.modal-backdrop').remove();", true);
        }
        ViewState["OpenFlag"] = 0;
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
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "RemoveBackDrop();", true);
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void ddlInstituteFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
}