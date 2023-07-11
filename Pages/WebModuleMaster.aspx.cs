using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class Pages_WebModuleMaster : System.Web.UI.Page
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
        }
    }

    protected DataTable FillGrid()
    {
        string Criteria = " where IsActive=1";
        if (ddlTypeFilter.SelectedItem.Value != "0")
        {
            Criteria += " and wmtype='" + ddlTypeFilter.SelectedItem.Text + "'";
        }
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
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
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearDetails();
        btnAdd.Visible = true;
        btnUpdate.Visible = false;
        Popup3(true);
    }
    protected void ClearDetails()
    {
        txtModuleName.Text = "";
        ddlType.SelectedIndex = 0;
        ClearParent();
        pnlParent.Visible = false;
        pnlSessionName.Visible = false;
        txtPreference.Text = "";
        txtURL.Text = "";

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtModuleName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Module Name');ShowPopup3();", true);
            return;
        }
        if (ddlType.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Type');ShowPopup3();", true);
            return;
        }
        if (ddlType.SelectedItem.Text == "T" && txtSessionName.Text=="")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Session Name');ShowPopup3();", true);
            return;
        }
        if (ddlType.SelectedItem.Text != "T" && ddlParent.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please select parent');ShowPopup3();", true);
            return;
        }

        ViewState["OpenFlag"] = 0;
        if (txtPreference.Text == "")
        {
            txtPreference.Text = "0";
        }

        if (txtURL.Text == "")
        {
            txtURL.Text = "javascript;";
        }

        conn.Open();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@webmodulename", txtModuleName.Text);
        cmd.Parameters.AddWithValue("@type", ddlType.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@wmcodeMain", ddlParent.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@preference", txtPreference.Text);
        cmd.Parameters.AddWithValue("@url", txtURL.Text);
        cmd.Parameters.AddWithValue("@SessionName", txtSessionName.Text);
        cmd.Parameters.AddWithValue("@empcode", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Module Name Already Exist');ShowPopup3();", true);
            return;
            
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Module Name Added successfully','success');$('.modal-backdrop').remove();", true);
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string WMC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            WMC = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["WMC"] = WMC;
            Popup1(true);
        }
        else if (e.CommandName == "modify")
        {
            string WMC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            WMC = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["WMC"] = WMC;
            ClearDetails();

            txtModuleName.Text = GVDetails.Rows[RowIndex].Cells[2].Text;
            ddlType.ClearSelection();
            if (ddlType.Items.FindByText(GVDetails.Rows[RowIndex].Cells[3].Text) != null)
            {
                ddlType.Items.FindByText(GVDetails.Rows[RowIndex].Cells[3].Text).Selected = true;
            }

            if (GVDetails.Rows[RowIndex].Cells[3].Text == "T")

            {
                pnlSessionName.Visible = true;
                txtSessionName.Text = GVDetails.Rows[RowIndex].Cells[9].Text;
            }
            else
            {
                pnlParent.Visible = true;
                FillParent();
                ddlParent.ClearSelection();
                if (ddlParent.Items.FindByValue(GVDetails.Rows[RowIndex].Cells[4].Text) != null)
                {
                    ddlParent.Items.FindByValue(GVDetails.Rows[RowIndex].Cells[4].Text).Selected = true;
                }
            }


            txtPreference.Text = GVDetails.Rows[RowIndex].Cells[6].Text;

            txtURL.Text = GVDetails.Rows[RowIndex].Cells[8].Text;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;

            ViewState["OldValue"] = GVDetails.Rows[RowIndex].Cells[2].Text;

            Popup3(true);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtModuleName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Add Module Name');ShowPopup3();", true);
            return;
        }
        if (ddlType.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Type');ShowPopup3();", true);
            return;
        }
        if (ddlType.SelectedItem.Text == "T" && txtSessionName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Session Name');ShowPopup3();", true);
            return;
        }
        if (ddlType.SelectedItem.Text != "T" && ddlParent.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please select parent');ShowPopup3();", true);
            return;
        }
        
        ViewState["OpenFlag"] = 0;
        if (txtPreference.Text == "")
        {
            txtPreference.Text = "0";
        }

        if (txtURL.Text == "")
        {
            txtURL.Text = "javascript;";
        }

        conn.Open();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@webmodulename", txtModuleName.Text);
        cmd.Parameters.AddWithValue("@type", ddlType.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@wmcodeMain", ddlParent.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@preference", txtPreference.Text);
        cmd.Parameters.AddWithValue("@url", txtURL.Text);
        cmd.Parameters.AddWithValue("@SessionName", txtSessionName.Text);
        cmd.Parameters.AddWithValue("@wmCode", ViewState["WMC"].ToString());
        cmd.Parameters.AddWithValue("@OldValue", ViewState["OldValue"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Module Name Already Exist');ShowPopup3();", true);
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Module Details Updated successfully','success');$('.modal-backdrop').remove();", true);
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.Parameters.AddWithValue("@wmCode", ViewState["WMC"].ToString());
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
            return;
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('No Record is selected to remove','warn');$('.modal-backdrop').remove();", true);
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
                    cmd = new SqlCommand("SP_Web_Masters", conn);
                    cmd.Parameters.AddWithValue("@calltype", 5);
                    cmd.Parameters.AddWithValue("@wmCode", row.Cells[1].Text);
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
                        flag = true;
                    }
                }
            }
        }

        if (flag == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Some Records can not be removed');$('.modal-backdrop').remove();", true);
            return;
           
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record deleted successfully','success');$('.modal-backdrop').remove();", true);
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


    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlParent.Visible = false;
        pnlSessionName.Visible = false;
        if (ddlType.SelectedItem.Text == "M" || ddlType.SelectedItem.Text == "S")
        {
            FillParent();
            pnlParent.Visible = true;
        }
        else if (ddlType.SelectedItem.Text == "T")
        {
            pnlSessionName.Visible = true;
            ClearParent();
        }
        Popup3(true);
    }

    protected void ClearParent()
    {
        ddlParent.Items.Clear();
        ListItem item = new ListItem("Select Parent", "0");
        ddlParent.Items.Add(item);
    }
    protected void FillParent()
    {
        ddlParent.Items.Clear();
        ListItem item = new ListItem("Select Parent", "0");
        ddlParent.Items.Add(item);

        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@type", ddlType.SelectedItem.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlParent.DataSource = dt;
        ddlParent.DataTextField = "webmodulename";
        ddlParent.DataValueField = "wmcode";
        ddlParent.DataBind();
        dt.Dispose();
    }

    protected void ddlTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
}