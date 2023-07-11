using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
//using Telerik.Web.UI;

public partial class Pages_WebPermissionSet : System.Web.UI.Page
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
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();

            Form.DefaultButton = btnSearch.UniqueID;
        }
    }

    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@search", txtSearch.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        lblRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " Record(s)]";
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
        txtPermissionSetName.Text = "";
        ddlType.SelectedIndex = -1;
        ddlDefaultSet.SelectedIndex = -1;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtPermissionSetName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Add Set Name";
            ViewState["OpenFlag"] = -1;
            return;
        }
        if (ddlType.SelectedItem.Value == "-1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Type";
            ViewState["OpenFlag"] = -1;
            return;
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 262);
        cmd.Parameters.AddWithValue("@wpsname", txtPermissionSetName.Text);
        cmd.Parameters.AddWithValue("@type", ViewState["PType"].ToString());
        cmd.Parameters.AddWithValue("@DefaultSet", ddlDefaultSet.SelectedItem.Text);
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
            dlglbl.Text = "Set Name Already exist";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (result == 2)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Only one Permission Set Can Be Set As Default";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Set Name Added successfully";
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
            txtPermissionSetName.Text = GVDetails.Rows[RowIndex].Cells[2].Text;
            ddlType.ClearSelection();
            ddlType.Items.FindByText(GVDetails.Rows[RowIndex].Cells[3].Text).Selected = true;
            ddlDefaultSet.ClearSelection();
            ddlDefaultSet.Items.FindByText(GVDetails.Rows[RowIndex].Cells[4].Text).Selected = true;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;

            ViewState["OldValue"] = GVDetails.Rows[RowIndex].Cells[2].Text;
            Popup3(true);
        }
        else if (e.CommandName == "modifyP")
        {
            string WPC = "",PType="";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            WPC = GVDetails.Rows[RowIndex].Cells[1].Text;
            PType = GVDetails.Rows[RowIndex].Cells[3].Text;
            ViewState["WPC"] = WPC;
            ViewState["PType"] = PType;
            FillTreeViewUpdate();
            Popup4(true);
        }
    }
    
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtPermissionSetName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Add Set Name";
            ViewState["OpenFlag"] = -1;
            return;
        }
        if (ddlType.SelectedItem.Value == "-1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Please Select Type";
            ViewState["OpenFlag"] = -1;
            return;
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 263);
        cmd.Parameters.AddWithValue("@wpsname", txtPermissionSetName.Text);
        cmd.Parameters.AddWithValue("@wps_code", ViewState["DID"].ToString());
        cmd.Parameters.AddWithValue("@type", ViewState["PType"].ToString());
        cmd.Parameters.AddWithValue("@DefaultSet", ddlDefaultSet.SelectedItem.Text);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Set Name Already exist";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else if (result == 2)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Only one Permission Set Can Be Set As Default";
            ViewState["OpenFlag"] = -1;
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Set Name Updated successfully";
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Web_Masters", conn);
        cmd.Parameters.AddWithValue("@calltype", 264);
        cmd.Parameters.AddWithValue("@wps_code", ViewState["DID"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        int result = cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        if (result == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Record deleted successfully";
            ViewState["OpenFlag"] = 0;
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "Record cannot be deleted";
            ViewState["OpenFlag"] = 0;
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
            dlglbl.Text = "No Record is selected to remove";
            ViewState["OpenFlag"] = 0;
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
                    cmd.Parameters.AddWithValue("@calltype", 264);
                    cmd.Parameters.AddWithValue("@wps_code", row.Cells[1].Text);
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
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

        txtSearch.Focus();

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
    public void FillTreeViewUpdate()
    {
        //treeViewUpdate.Nodes.Clear();
        HiddenFieldParents.Value = "";
        cmd = new SqlCommand("SP_Web_UserPermission", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //RadTreeNode tn = new RadTreeNode(dt.Rows[i]["webmodulename"].ToString());
            //tn.Attributes.Add("wmcode", dt.Rows[i][0].ToString());
            HiddenFieldParents.Value += "{\"text\" :\"" + dt.Rows[i]["webmodulename"].ToString()+ "\",\"id\" : \"" + dt.Rows[i][0].ToString() + "\",";

            cmd = new SqlCommand("SP_Web_UserPermission", conn);
            cmd.Parameters.AddWithValue("@calltype", 8);
            cmd.Parameters.AddWithValue("@wmcodeMain", dt.Rows[i][0].ToString());
            cmd.Parameters.AddWithValue("@type", ViewState["PType"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt1 = new DataTable();
            da.Fill(dt1);
            cmd.Dispose();
            da.Dispose();
            HiddenFieldParents.Value += "\"children\" : [";
            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                //RadTreeNode tnchild = new RadTreeNode(dt1.Rows[j]["webmodulename"].ToString());
                //tnchild.Attributes.Add("wmcode", dt1.Rows[j][0].ToString());
                HiddenFieldParents.Value += "{\"text\" :\"" + dt1.Rows[j]["webmodulename"].ToString() + "\",\"id\" : \"" + dt1.Rows[j][0].ToString() + "\"";

                cmd = new SqlCommand("SP_Web_UserPermission", conn);
                cmd.Parameters.AddWithValue("@calltype", 8);
                cmd.Parameters.AddWithValue("@wmcodeMain", dt1.Rows[j][0].ToString());
                cmd.Parameters.AddWithValue("@type", ViewState["PType"].ToString());
                cmd.Parameters.AddWithValue("@Wps_Code", ViewState["WPC"].ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt2 = new DataTable();
                da.Fill(dt2);
                cmd.Dispose();
                da.Dispose();
                HiddenFieldParents.Value += ","+"\"children\" : [";
                if (dt2.Rows.Count != 0)
                {

                    for (int k = 0; k < dt2.Rows.Count; k++)
                    {
                        //RadTreeNode tnsubchild = new RadTreeNode(dt2.Rows[k]["webmodulename"].ToString());
                        //tnsubchild.Attributes.Add("wmcode", dt2.Rows[k][0].ToString());

                        HiddenFieldParents.Value += "{\"text\" :\"" + dt2.Rows[k]["webmodulename"].ToString() + "\",\"id\" : \"" + dt2.Rows[k][0].ToString() + "\"";

                        //code to test 
                        if (dt2.Rows[k][2].ToString() == "1")
                        { 
                        HiddenFieldParents.Value +=","+ "\"state\" :{\"selected\" : true,\"opened\": false}";
                        }
                        HiddenFieldParents.Value += "},";
                        //tnsubchild.Checked = true;
                        //tnchild.Nodes.Add(tnsubchild);
                    }
                }
                HiddenFieldParents.Value += "]},";
                //tn.Nodes.Add(tnchild);
            }
            //treeViewUpdate.Nodes.Add(tn);
            HiddenFieldParents.Value += "]},";
        }
        HiddenFieldParents.Value = HiddenFieldParents.Value.Substring(0, HiddenFieldParents.Value.Length-1);
    }

    protected void btnSet_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Web_UserPermission", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.Parameters.AddWithValue("@wps_code", ViewState["WPC"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        int result = cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        string ModuleCodes = HiddenFieldSelectValues.Value;
        //foreach (RadTreeNode item in treeViewUpdate.Nodes)
        //{
        //    if (item.Checked)
        //    {
        //        //AddModule(row.Cells[8].Text, item.Attributes["wmcode"]);
        //        ModuleCodes += item.Attributes["wmcode"] + ",";
        //        foreach (RadTreeNode child in item.Nodes)
        //        {
        //            if (child.Checked)
        //            {
        //                //AddModule(row.Cells[8].Text, child.Attributes["wmcode"]);
        //                ModuleCodes += child.Attributes["wmcode"] + ",";
        //                foreach (RadTreeNode subchild in child.Nodes)
        //                {
        //                    if (subchild.Checked)
        //                    {
        //                        //AddModule(row.Cells[8].Text, subchild.Attributes["wmcode"]);
        //                        ModuleCodes += subchild.Attributes["wmcode"] + ",";
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //if (ModuleCodes.Length > 0)
        //{
        //    ModuleCodes = ModuleCodes.Substring(0, ModuleCodes.Length - 1);
        //}

        ViewState["ModuleCodes"] = ModuleCodes;

        //Code to Set Permission
        conn.Open();
        cmd = new SqlCommand("SP_Web_UserPermission", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@wps_code", ViewState["WPC"].ToString());
        cmd.Parameters.AddWithValue("@ModuleCode", ViewState["ModuleCodes"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ViewState["OpenFlag"] = 0;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Permissions set updated Successfully";
    }
}