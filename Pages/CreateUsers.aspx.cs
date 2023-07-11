using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
public partial class Pages_CreateUsers : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Form.DefaultButton = btnSearch.UniqueID;
        
        FillStatus();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCourse();
        if (ddlType.SelectedItem.Value == "2")
        {
            ddlBulkCreateDelete.Enabled = true;
            btnBulk.Enabled = true;
        }
        else
        {
            ddlBulkCreateDelete.Enabled = false;
            btnBulk.Enabled = false;
        }
        ViewState["OpenFlag"] = 0;
    }
    protected void FillCourse()
    {
        ddlCourse.Items.Clear();
        ListItem item = new ListItem("Select Department", "0");
        ddlCourse.Items.Add(item);

        cmd = new SqlCommand("SP_WebUsers", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@type",2);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlCourse.DataSource = dt;

        if (ddlType.SelectedItem.Text == "Employee")
        {
            ddlCourse.DataTextField = "departmentname";
            ddlCourse.DataValueField = "departmentcode";
        }
        ddlCourse.DataBind();
        dt.Dispose();
    }
    public void FillStatus()
    {
        ddlStatus.Items.Clear();
        ListItem item = new ListItem("Select Status", "0");
        ddlStatus.Items.Add(item);

        cmd = new SqlCommand("SP_WebUsers", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@type", ddlType.SelectedItem.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlStatus.DataSource = dt;
        ddlStatus.DataTextField = "status";
        ddlStatus.DataValueField = "Value";
        ddlStatus.DataBind();
        dt.Dispose();
    }


    protected void btnView_Click(object sender, EventArgs e)
    {
        ////System.Threading.Thread.Sleep(20000);
        //if (ddlStatus.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //    dlglbl.Text = "Please Select Status";
        //    ViewState["OpenFlag"] = 0;
        //    return;
        //}
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    public DataTable FillGrid()
    {
        string Criteria = "";
        cmd = new SqlCommand("SP_WebUsers", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@type", ddlType.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.Value);
        if (ddlCourse.SelectedItem.Value != "0")
        {
            if (ddlType.SelectedItem.Text == "Employee")            
            {
                Criteria += " and EM.departmentcode = " + ddlCourse.SelectedItem.Value;
            }

        }
        //if (txtSearch.Text != "")
        //{
        //    if (ddlType.SelectedItem.Text == "Employee")
        //    {
        //        Criteria += " and (employeename like '%" + txtSearch.Text + "%' or EmployeeCode like '%" + txtSearch.Text + "%')";
        //    }

        //}
        //if (ddlAStatus.SelectedItem.Value != "0")
        //{
        //    if (ddlAStatus.SelectedItem.Text == "Delete User")
        //    {
        //        Criteria += " and WU.LoginName is not null and WU.isActive=1";
        //    }
        //    else
        //    {
        //        Criteria += " and (WU.LoginName is null or WU.isactive=0)";
        //    }

        //}

        cmd.Parameters.AddWithValue("@Criteria", Criteria);
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

        GVDetails.DataSource = sortedView;
        GVDetails.DataBind();
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        if (ViewState["OpenFlag"].ToString() == "-1")
        {
            Popup3(true);
        }
        else if (ViewState["OpenFlag"].ToString() == "-2")
        {
            Popup4(true);
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
    protected void GVDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Button btn = (Button)e.Row.Cells[7].FindControl("btnAction");
            LinkButton btnModify = (LinkButton)e.Row.Cells[6].FindControl("lnlbtnModify");
            LinkButton btnMapping = (LinkButton)e.Row.Cells[6].FindControl("lnlbtnMapping");

            if (e.Row.Cells[8].Text == "1")
            {
                btn.Text = "Delete User";
                btn.CssClass = "btn btn-danger btn-xs";
                btnModify.Visible = true;
                btnMapping.Visible = true;
            }
            else
            {
                btn.Text = "Create User";
                btn.CssClass = "btn btn-success btn-xs";
                btnModify.Visible = false;
                btnMapping.Visible = false;
            }
            btn.Width = Unit.Pixel(140);
        }
    }

    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Action")
        {
            GridViewRow oItem = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Button btn = oItem.FindControl("btnAction") as Button;

            ViewState["LoginName"] = GVDetails.Rows[RowIndex].Cells[2].Text;
            ViewState["E_Code"] = GVDetails.Rows[RowIndex].Cells[5].Text;
            ViewState["WUC"] = GVDetails.Rows[RowIndex].Cells[9].Text;

            if (btn.Text == "Create User")
            {
                if (ddlType.SelectedItem.Value == "1")
                {
                    Random rnd = new Random();
                    int pwd = rnd.Next(1001, 9999);
                    conn.Open();
                    cmd = new SqlCommand("SP_WebUsers", conn);
                    cmd.Parameters.AddWithValue("@calltype", 4);
                    cmd.Parameters.AddWithValue("@Loginname", ViewState["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@Password", pwd);
                    cmd.Parameters.AddWithValue("@Type", ddlType.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@E_Code", ViewState["E_Code"].ToString());
                    cmd.Parameters.AddWithValue("@EmpCode", Session["EmpCode"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
                    dlglbl.Text = "User Created Successfully";

                    GVDetails.DataSource = FillGrid();
                    GVDetails.DataBind();
                    ViewState["OpenFlag"] = 0;
                }
                else
                {
                    //FillRecommendingOfficer();
                    //FillApprovalOfficer();
                    ddlWebUserType.Items.Clear();
                    ListItem item = new ListItem("Select Type", "0");
                    ddlWebUserType.Items.Add(item);
                    item = new ListItem("Admin", "2");
                    ddlWebUserType.Items.Add(item);
                    item = new ListItem("Employee", "3");
                    ddlWebUserType.Items.Add(item);
                    Popup3(true);
                }

            }
            else
            {
                Popup1(true);
            }

        }
        else if (e.CommandName == "modify")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;

            ViewState["LoginName"] = GVDetails.Rows[RowIndex].Cells[2].Text;
            ViewState["E_Code"] = GVDetails.Rows[RowIndex].Cells[5].Text;
            ViewState["WUC"] = GVDetails.Rows[RowIndex].Cells[9].Text;

            //FillRO();
            //FillAO();

            if (ddlType.SelectedItem.Value == "1")
            {
                ddlT.Items.Clear();
                ListItem item = new ListItem("Select Type", "0");
                ddlT.Items.Add(item);
                item = new ListItem("Student", "1");
                ddlT.Items.Add(item);
            }
            else
            {
                ddlT.Items.Clear();
                ListItem item = new ListItem("Select Type", "0");
                ddlT.Items.Add(item);
                item = new ListItem("Admin", "2");
                ddlT.Items.Add(item);
                item = new ListItem("Employee", "3");
                ddlT.Items.Add(item);
            }

            cmd = new SqlCommand("SP_WebUsers", conn);
            cmd.Parameters.AddWithValue("@calltype", 7);
            cmd.Parameters.AddWithValue("@Loginname", ViewState["LoginName"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();

            ddlT.ClearSelection();
            if (ddlT.Items.FindByText(dt.Rows[0]["Logintype"].ToString()) != null)
            {
                ddlT.ClearSelection();
                ddlT.Items.FindByText(dt.Rows[0]["Logintype"].ToString()).Selected = true;
            }

            txtPassword.Text = dt.Rows[0]["password"].ToString();
            txtParentPassword.Text = dt.Rows[0]["ParentPassword"].ToString();
            Popup4(true);
        }
        else if (e.CommandName == "mapping")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            ViewState["WUC"] = GVDetails.Rows[RowIndex].Cells[9].Text;

            FillRoleTransaction();
            Popup5(true);
        }
    }

    private void FillRoleTransaction()
    {
        cmd = new SqlCommand("SP_WebUsers", conn);
        cmd.Parameters.AddWithValue("@calltype", 8);
        cmd.Parameters.AddWithValue("@type", ddlType.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@WebUserCode", ViewState["WUC"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        GVRoleMapping.DataSource = dt;
        GVRoleMapping.DataBind();
        dt.Dispose();
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
    protected void btnYes1_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_WebUsers", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.Parameters.AddWithValue("@LoginName", ViewState["LoginName"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "User Deleted Successfully";

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

        ViewState["OpenFlag"] = 0;
    }
    protected void btnNo1_Click(object sender, EventArgs e)
    {
        Popup1(false);
    }

    protected void btnAllocate_Click(object sender, EventArgs e)
    {
        //if (ddlRecommending.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //    dlglbl.Text = "Please Select Recommending Officer";
        //    ViewState["OpenFlag"] = -1;
        //    return;
        //}
        //if (ddlApproval.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //    dlglbl.Text = "Please Select Approval Officer";
        //    ViewState["OpenFlag"] = -1;
        //    return;
        //}
        if (ddlWebUserType.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Type";
            ViewState["OpenFlag"] = -1;
            return;
        }
        ViewState["OpenFlag"] = 0;

        Random rnd = new Random();
        int pwd = rnd.Next(1001, 9999);
        conn.Open();
        cmd = new SqlCommand("SP_WebUsers", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@Loginname", ViewState["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@Password", pwd);
        cmd.Parameters.AddWithValue("@Type", ddlWebUserType.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["E_Code"].ToString());
        //cmd.Parameters.AddWithValue("@EmpCode", Session["EmpCode"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "User Created Successfully";

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
        ViewState["OpenFlag"] = 0;
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
    protected void btnCloseMainUpdate_Click(object sender, EventArgs e)
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
    protected void ddlAStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

        //txtSearch.Focus();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //if (ddlRO.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //    dlglbl.Text = "Please Select Recommending Officer";
        //    ViewState["OpenFlag"] = -2;
        //    return;
        //}
        //if (ddlAO.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        //    dlglbl.Text = "Please Select Approval Officer";
        //    ViewState["OpenFlag"] = -2;
        //    return;
        //}
        if (ddlT.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Type";
            ViewState["OpenFlag"] = -2;
            return;
        }
        if (txtPassword.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Enter Password";
            ViewState["OpenFlag"] = -2;
            return;
        }
        if (txtParentPassword.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Enter Parent Password";
            ViewState["OpenFlag"] = -2;
            return;
        }
        ViewState["OpenFlag"] = 0;

        conn.Open();
        cmd = new SqlCommand("SP_WebUsers", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@Loginname", ViewState["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
        cmd.Parameters.AddWithValue("@Type", ddlT.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@ParentPassword", txtParentPassword.Text);
        cmd.Parameters.AddWithValue("@e_code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Updated Successfully";

        GVDetails.DataSource = FillGrid();
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
    protected void btnBulk_Click(object sender, EventArgs e)
    {
        if (ddlBulkCreateDelete.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Bulk Action";
            return;
        }
        int flag = 0;
        foreach (GridViewRow row in GVDetails.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("chkCtrl");
            if (cbx.Checked)
            {
                flag++;
                break;
            }
        }
        if (flag == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "No Record is selected";
            return;
        }
        if (ddlBulkCreateDelete.SelectedItem.Value == "1")
        {
            foreach (GridViewRow row in GVDetails.Rows)
            {
                CheckBox cbx = (CheckBox)row.FindControl("chkCtrl");
                if (cbx.Checked)
                {
                    if (ddlType.SelectedItem.Value == "1")
                    {
                        Random rnd = new Random();
                        int pwd = rnd.Next(1001, 9999);
                        conn.Open();
                        cmd = new SqlCommand("SP_WebUsers", conn);
                        cmd.Parameters.AddWithValue("@calltype", 4);
                        cmd.Parameters.AddWithValue("@Loginname", row.Cells[2].Text);
                        cmd.Parameters.AddWithValue("@Password", pwd);
                        cmd.Parameters.AddWithValue("@Type", ddlType.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@E_Code", row.Cells[5].Text);
                        cmd.Parameters.AddWithValue("@EmpCode", Session["EmpCode"].ToString());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        conn.Close();


                    }
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "User Created Successfully";

            ViewState["OpenFlag"] = 0;
        }
        else if (ddlBulkCreateDelete.SelectedItem.Value == "2")
        {
            foreach (GridViewRow row in GVDetails.Rows)
            {
                CheckBox cbx = (CheckBox)row.FindControl("chkCtrl");
                if (cbx.Checked)
                {
                    conn.Open();
                    cmd = new SqlCommand("SP_WebUsers", conn);
                    cmd.Parameters.AddWithValue("@calltype", 5);
                    cmd.Parameters.AddWithValue("@LoginName", row.Cells[2].Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();

                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "User Deleted Successfully";

            ViewState["OpenFlag"] = 0;
        }
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void chkStatus_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow currentrow = (GridViewRow)((CheckBox)sender).Parent.Parent.Parent.Parent;
        CheckBox chk = default(CheckBox);
        chk = (CheckBox)currentrow.FindControl("chkStatus");

        conn.Open();
        cmd = new SqlCommand("SP_WebUsers", conn);
        cmd.Parameters.AddWithValue("@Calltype", 9);
        cmd.Parameters.AddWithValue("@RoleID", GVRoleMapping.Rows[currentrow.RowIndex].Cells[0].Text);
        if (chk.Checked)
        {
            cmd.Parameters.AddWithValue("@Status", 1);
        }
        else
        {
            cmd.Parameters.AddWithValue("@Status", 0);
        }
        cmd.Parameters.AddWithValue("@WebUserCode", ViewState["WUC"].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["empCode"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        Popup5(true);
    }
}