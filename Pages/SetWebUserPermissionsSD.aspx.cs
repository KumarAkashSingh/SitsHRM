using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Telerik.Web.UI;
public partial class Pages_SetWebUserPermissionsSD : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;
    DataTable dt2 = null;
    DataTable dt3 = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Form.DefaultButton = btnSearch.UniqueID;
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
        //GVDetails.DataSource = FillGrid();
        //GVDetails.DataBind();

    }
    
    public DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 9);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();


        if (dt.Rows.Count > 0)
        {
            btnPermission.Visible = true;
        }
        else
        {
            btnPermission.Visible = false;
        }
        lblRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " <span class='hidden-xs'>Record(s) Found</span>]";
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
    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            string WUC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            WUC = GVDetails.Rows[RowIndex].Cells[7].Text;
            ViewState["WUC"] = WUC;


            tv.Nodes.Clear();
            cmd = new SqlCommand("SP_Web_Masters", conn);
            cmd.Parameters.AddWithValue("@calltype", 11);
            //cmd.Parameters.AddWithValue("@type", ddlType.SelectedItem.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (TestPermision(dt.Rows[i][0].ToString(), ViewState["WUC"].ToString()))
                {
                    RadTreeNode tn = new RadTreeNode(dt.Rows[i]["webmodulename"].ToString());
                    cmd = new SqlCommand("SP_Web_Masters", conn);
                    cmd.Parameters.AddWithValue("@calltype", 12);
                    cmd.Parameters.AddWithValue("@wmcodeMain", dt.Rows[i][0].ToString());
                    //cmd.Parameters.AddWithValue("@type", ddlType.SelectedItem.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    dt1 = new DataTable();
                    da.Fill(dt1);
                    cmd.Dispose();
                    da.Dispose();
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        if (TestPermision(dt1.Rows[j][0].ToString(), ViewState["WUC"].ToString()))
                        {
                            RadTreeNode tnchild = new RadTreeNode(dt1.Rows[j]["webmodulename"].ToString());
                            cmd = new SqlCommand("SP_Web_Masters", conn);
                            cmd.Parameters.AddWithValue("@calltype", 12);
                            cmd.Parameters.AddWithValue("@wmcodeMain", dt1.Rows[j][0].ToString());
                            //cmd.Parameters.AddWithValue("@type", ddlType.SelectedItem.Text);
                            cmd.CommandType = CommandType.StoredProcedure;
                            da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            dt2 = new DataTable();
                            da.Fill(dt2);
                            cmd.Dispose();
                            da.Dispose();
                            for (int k = 0; k < dt2.Rows.Count; k++)
                            {
                                //code to test 
                                if (TestPermision(dt2.Rows[k][0].ToString(), ViewState["WUC"].ToString()))
                                {
                                    RadTreeNode tnsubchild = new RadTreeNode(dt2.Rows[k]["webmodulename"].ToString());
                                    tnchild.Nodes.Add(tnsubchild);
                                }
                            }
                            tn.Nodes.Add(tnchild);
                        }
                    }
                    tv.Nodes.Add(tn);
                }
            }
            if (tv.Nodes.Count == 0)
            {
                lblNoPermission.Visible = true;
            }
            else
            {
                lblNoPermission.Visible = false;
            }
            Popup5(true);
        }
        else if (e.CommandName == "modify")
        {
            string WUC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            WUC = GVDetails.Rows[RowIndex].Cells[7].Text;
            ViewState["WUC"] = WUC;
            FillTreeViewUpdate();
            Popup4(true);
        }
    }
    protected void btnCloseMain_Click(object sender, EventArgs e)
    {
        Popup3(false);
    }
    protected void btnCloseP_Click(object sender, EventArgs e)
    {
        Popup5(false);
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
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    GVDetails.DataSource = FillGrid();
    //    GVDetails.DataBind();
    //}
    protected void btnPermission_Click(object sender, EventArgs e)
    {
        int pr = 0;
        string WebUserCodes = "";
        foreach (GridViewRow row in GVDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.Cells[0].FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    pr = pr + 1;
                    WebUserCodes += row.Cells[4].Text + ",";
                }
            }
        }

        if (pr == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "No Record is Selected";
            ViewState["OpenFlag"] = 0;
            return;
        }
        if (WebUserCodes.Length > 0)
        {
            WebUserCodes = WebUserCodes.Substring(0, WebUserCodes.Length - 1);
        }
        ViewState["WebUserCodes"] = WebUserCodes;
        FillTreeView();
        Popup3(true);
    }
    public void FillTreeView()
    {
        treeView1.Nodes.Clear();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 11);
        //cmd.Parameters.AddWithValue("@type", ddlType.SelectedItem.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            cmd = new SqlCommand("SP_Web_Masters", conn);
            cmd.Parameters.AddWithValue("@calltype", 12);
            cmd.Parameters.AddWithValue("@wmcodeMain", dt.Rows[i][0].ToString());
            //cmd.Parameters.AddWithValue("@type", ddlType.SelectedItem.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt1 = new DataTable();
            da.Fill(dt1);
            cmd.Dispose();
            da.Dispose();

            if (dt1.Rows.Count > 0)
            {
                RadTreeNode tn = new RadTreeNode(dt.Rows[i]["webmodulename"].ToString());
                tn.Attributes.Add("wmcode", dt.Rows[i][0].ToString());
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    cmd = new SqlCommand("SP_Web_Masters", conn);
                    cmd.Parameters.AddWithValue("@calltype", 12);
                    cmd.Parameters.AddWithValue("@wmcodeMain", dt1.Rows[j][0].ToString());
                    //cmd.Parameters.AddWithValue("@type", ddlType.SelectedItem.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    dt2 = new DataTable();
                    da.Fill(dt2);
                    cmd.Dispose();
                    da.Dispose();
                    if (dt2.Rows.Count > 0)
                    {
                        RadTreeNode tnchild = new RadTreeNode(dt1.Rows[j]["webmodulename"].ToString());
                        tnchild.Attributes.Add("wmcode", dt1.Rows[j][0].ToString());
                        for (int k = 0; k < dt2.Rows.Count; k++)
                        {
                            RadTreeNode tnsubchild = new RadTreeNode(dt2.Rows[k]["webmodulename"].ToString());
                            tnsubchild.Attributes.Add("wmcode", dt2.Rows[k][0].ToString());
                            tnchild.Nodes.Add(tnsubchild);
                        }
                        tn.Nodes.Add(tnchild);
                    }
                }
                treeView1.Nodes.Add(tn);
            }
        }

    }
    public void FillTreeViewUpdate()
    {
        treeViewUpdate.Nodes.Clear();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 11);
        //cmd.Parameters.AddWithValue("@type", ddlType.SelectedItem.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            RadTreeNode tn = new RadTreeNode(dt.Rows[i]["webmodulename"].ToString());
            tn.Attributes.Add("wmcode", dt.Rows[i][0].ToString());
            cmd = new SqlCommand("SP_Web_Masters", conn);
            cmd.Parameters.AddWithValue("@calltype", 12);
            cmd.Parameters.AddWithValue("@wmcodeMain", dt.Rows[i][0].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt1 = new DataTable();
            da.Fill(dt1);
            cmd.Dispose();
            da.Dispose();
            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                RadTreeNode tnchild = new RadTreeNode(dt1.Rows[j]["webmodulename"].ToString());
                tnchild.Attributes.Add("wmcode", dt1.Rows[j][0].ToString());
                cmd = new SqlCommand("SP_Web_Masters", conn);
                cmd.Parameters.AddWithValue("@calltype", 12);
                cmd.Parameters.AddWithValue("@wmcodeMain", dt1.Rows[j][0].ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt2 = new DataTable();
                da.Fill(dt2);
                cmd.Dispose();
                da.Dispose();
                if (dt2.Rows.Count != 0)
                {
                    for (int k = 0; k < dt2.Rows.Count; k++)
                    {
                        RadTreeNode tnsubchild = new RadTreeNode(dt2.Rows[k]["webmodulename"].ToString());
                        tnsubchild.Attributes.Add("wmcode", dt2.Rows[k][0].ToString());
                        //code to test 
                        if (TestPermision(dt2.Rows[k][0].ToString(), ViewState["WUC"].ToString()))
                            tnsubchild.Checked = true;
                        tnchild.Nodes.Add(tnsubchild);
                    }
                }
                else
                {
                    if (TestPermision(dt1.Rows[j][0].ToString(), ViewState["WUC"].ToString()))
                        tnchild.Checked = true;
                }
                tn.Nodes.Add(tnchild);
            }
            tn.Checked = true;
            treeViewUpdate.Nodes.Add(tn);
        }

    }
    public Boolean TestPermision(string wmcode, string usercode)
    {
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 10);
        cmd.Parameters.AddWithValue("@wmcode", wmcode);
        cmd.Parameters.AddWithValue("@WebUserCode", usercode);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt3 = new DataTable();
        da.Fill(dt3);
        cmd.Dispose();
        da.Dispose();
        if (dt3.Rows.Count != 0)
        {
            return true;
        }
        else
            return false;
    }
    protected void btnSet_Click(object sender, EventArgs e)
    {
        string ModuleCodes = "";
        foreach (RadTreeNode item in treeView1.Nodes)
        {
            if (item.Checked)
            {
                //AddModule(row.Cells[8].Text, item.Attributes["wmcode"]);
                ModuleCodes += item.Attributes["wmcode"] + ",";
                foreach (RadTreeNode child in item.Nodes)
                {
                    if (child.Checked)
                    {
                        //AddModule(row.Cells[8].Text, child.Attributes["wmcode"]);
                        ModuleCodes += child.Attributes["wmcode"] + ",";
                        foreach (RadTreeNode subchild in child.Nodes)
                        {
                            if (subchild.Checked)
                            {
                                //AddModule(row.Cells[8].Text, subchild.Attributes["wmcode"]);
                                ModuleCodes += subchild.Attributes["wmcode"] + ",";
                            }
                        }
                    }
                }
            }
        }

        if (ModuleCodes.Length > 0)
        {
            ModuleCodes = ModuleCodes.Substring(0, ModuleCodes.Length - 1);
        }
        ViewState["ModuleCodes"] = ModuleCodes;

        //Code to Set Permission
        conn.Open();
        cmd = new SqlCommand("SP_Web_UserPermission", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@WebUserCode", ViewState["WebUserCodes"].ToString());
        cmd.Parameters.AddWithValue("@ModuleCode", ViewState["ModuleCodes"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Permissions granted Successfully','success');$('.modal-backdrop').remove();", true);
        return;
    }
    protected void btnReSet_Click(object sender, EventArgs e)
    {
        string ModuleCodes = "";
        foreach (RadTreeNode item in treeView1.Nodes)
        {
            if (item.Checked)
            {
                //AddModule(row.Cells[8].Text, item.Attributes["wmcode"]);
                ModuleCodes += item.Attributes["wmcode"] + ",";
                foreach (RadTreeNode child in item.Nodes)
                {
                    if (child.Checked)
                    {
                        //AddModule(row.Cells[8].Text, child.Attributes["wmcode"]);
                        ModuleCodes += child.Attributes["wmcode"] + ",";
                        foreach (RadTreeNode subchild in child.Nodes)
                        {
                            if (subchild.Checked)
                            {
                                //AddModule(row.Cells[8].Text, subchild.Attributes["wmcode"]);
                                ModuleCodes += subchild.Attributes["wmcode"] + ",";
                            }
                        }
                    }
                }
            }
        }

        if (ModuleCodes.Length > 0)
        {
            ModuleCodes = ModuleCodes.Substring(0, ModuleCodes.Length - 1);
        }
        ViewState["ModuleCodes"] = ModuleCodes;


        //Code to Remove Permission
        conn.Open();
        cmd = new SqlCommand("SP_Web_UserPermission", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@WebUserCode", ViewState["WebUserCodes"].ToString());
        cmd.Parameters.AddWithValue("@ModuleCode", ViewState["ModuleCodes"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Permissions revoked Successfully','success');$('.modal-backdrop').remove();", true);
        return;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string ModuleCodes = "";
        foreach (RadTreeNode item in treeViewUpdate.Nodes)
        {
            if (item.Checked)
            {
                //AddModule(row.Cells[8].Text, item.Attributes["wmcode"]);
                ModuleCodes += item.Attributes["wmcode"] + ",";
                foreach (RadTreeNode child in item.Nodes)
                {
                    if (child.Checked)
                    {
                        //AddModule(row.Cells[8].Text, child.Attributes["wmcode"]);
                        ModuleCodes += child.Attributes["wmcode"] + ",";
                        foreach (RadTreeNode subchild in child.Nodes)
                        {
                            if (subchild.Checked)
                            {
                                //AddModule(row.Cells[8].Text, subchild.Attributes["wmcode"]);
                                ModuleCodes += subchild.Attributes["wmcode"] + ",";
                            }
                        }
                    }
                }
            }
        }

        if (ModuleCodes.Length > 0)
        {
            ModuleCodes = ModuleCodes.Substring(0, ModuleCodes.Length - 1);
        }
        ViewState["ModuleCodes"] = ModuleCodes;

        //Code to Delete All Permission
        conn.Open();
        cmd = new SqlCommand("SP_Web_UserPermission", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@WebUserCode", ViewState["WUC"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();


        //Code To Set Permission
        conn.Open();
        cmd = new SqlCommand("SP_Web_UserPermission", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@WebUserCode", ViewState["WUC"].ToString());
        cmd.Parameters.AddWithValue("@ModuleCode", ViewState["ModuleCodes"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Permissions modified Successfully','success');$('.modal-backdrop').remove();", true);
        return;
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

    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
}