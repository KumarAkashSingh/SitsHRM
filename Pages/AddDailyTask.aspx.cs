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
using System.IO;
using System.Configuration;

public partial class AddDailyTask : System.Web.UI.Page
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
        }
    }
    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_AddDailyTask", conn);
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

    protected DataTable FillGridPhoto()
    {
        cmd = new SqlCommand("SP_AddDailyTask", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@TaskID", ViewState["TaskID"].ToString());
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
        txtTaskName.Text = "";
        txtDate.Text = "";
        txtRemark.Text = "";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtTaskName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Daily Task Name');ShowPopup3();", true);
            return;
        }
        if (txtDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Daily Task Date');ShowPopup3();", true);
            return;
        }
        if (txtRemark.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Daily Task Remark');ShowPopup3();", true);
            return;
        }
        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_AddDailyTask", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@TaskName", txtTaskName.Text);
        cmd.Parameters.AddWithValue("@DOU", DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@AddTaskRemark", txtRemark.Text);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Already exist','success');$('.modal-backdrop').remove();", true);
            ViewState["OpenFlag"] = -1;
        }
        else
        {
            if (fileupload.HasFile)
            {
                foreach (var file in fileupload.PostedFiles)
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    string FileName = Session["userName"].ToString() + "_" + Guid.NewGuid().ToString() + fi.Extension;
                    try
                    {
                        string subPath = "../Files/DailyTaskFiles/";
                        bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                        string FilePath = Path.Combine(Server.MapPath("../Files/DailyTaskFiles/"), FileName);
                        file.SaveAs(FilePath);
                    }
                    catch (Exception ex)
                    {

                    }
                    conn.Open();
                    cmd = new SqlCommand("SP_AddDailyTask", conn);
                    cmd.Parameters.AddWithValue("@calltype", 5);
                    cmd.Parameters.AddWithValue("@TaskID", result);
                    cmd.Parameters.AddWithValue("@FileName", FileName);
                    cmd.Parameters.AddWithValue("@empcode", Session["e_code"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Todays Task successfully','success');$('.modal-backdrop').remove();", true);
            ViewState["OpenFlag"] = 0;
        }

        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();
    }

    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string TaskID = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            TaskID = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["TaskID"] = TaskID;
            Popup1(true);
        }
        else if (e.CommandName == "modify")
        {
            string TaskID = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            TaskID = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["TaskID"] = TaskID;
            ClearDetails();
            txtTaskName.Text = GVDetails.Rows[RowIndex].Cells[2].Text.Replace("&amp;", "&");
            txtDate.Text = GVDetails.Rows[RowIndex].Cells[3].Text.Replace("&amp;", "&");
            txtRemark.Text = GVDetails.Rows[RowIndex].Cells[4].Text.Replace("&amp;", "&");
            btnAdd.Visible = false;
            btnUpdate.Visible = true;

            Popup3(true);
        }

        else if (e.CommandName == "view")
        {
            string TaskID = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            TaskID = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["TaskID"] = TaskID;

            GVPhoto.DataSource = FillGridPhoto();
            GVPhoto.DataBind();
            Popup4(true);
        }            
    }

    protected void btnClose_Click1(object sender, EventArgs e)
    {
        if (ViewState["OpenFlag"] != null && ViewState["OpenFlag"].ToString() == "-1")
        {
            Popup3(true);
        }
        if (ViewState["OpenFlag"].ToString() == "-2")
        {
            Popup4(true);
        }
        else
        {
            Popup(false);
        }
    }

    protected void btnClosePhoto_Click1(object sender, EventArgs e)
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtTaskName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Task Name');ShowPopup();", true);
            return;
        }
        if (txtDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Date');ShowPopup();", true);
            return;
        }
        if (txtRemark.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please  Enter Remarks');ShowPopup();", true);
            return;
        }
        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_AddDailyTask", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@TaskID", ViewState["TaskID"].ToString());
        cmd.Parameters.AddWithValue("@TaskName", txtTaskName.Text);
        cmd.Parameters.AddWithValue("@DOU", DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@AddTaskRemark", txtRemark.Text);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Already exist','success');$('.modal-backdrop').remove();", true);
            ViewState["OpenFlag"] = -1;
        }
        else
        {
            if (fileupload.HasFile)
            {
                foreach (var file in fileupload.PostedFiles)
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    string FileName = Session["userName"].ToString() + "_" + Guid.NewGuid().ToString() + fi.Extension;
                    try
                    {
                        string subPath = "../SitsHRM_New/Files/DailyTaskFiles/";
                        bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                        string FilePath = Path.Combine(Server.MapPath("../SitsHRM_New/Files/DailyTaskFiles/"), FileName);
                        file.SaveAs(FilePath);
                    }
                    catch (Exception ex)
                    {
                       
                    }

                    conn.Open();
                    cmd = new SqlCommand("SP_AddDailyTask", conn);
                    cmd.Parameters.AddWithValue("@calltype", 5);
                    cmd.Parameters.AddWithValue("@TaskID", ViewState["TaskID"].ToString());
                    cmd.Parameters.AddWithValue("@FileName", file.FileName);
                    cmd.Parameters.AddWithValue("@empcode", Session["e_code"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Task Updated successfully','success');$('.modal-backdrop').remove();", true);
            ViewState["OpenFlag"] = 0;
        }
        GVDetails.DataSource = FillGrid();
        GVDetails.DataBind();

    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_AddDailyTask", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@TaskID", ViewState["TaskID"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Record can not be deleted');$('.modal-backdrop').remove();", true);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('No Record is selected to remove');$('.modal-backdrop').remove();", true);
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
                    cmd = new SqlCommand("SP_AddDailyTask", conn);
                    cmd.Parameters.AddWithValue("@calltype", 4);
                    cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
                    cmd.Parameters.AddWithValue("@TaskID", row.Cells[1].Text);
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


    protected void GVDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnlViewFiles = (LinkButton)e.Row.Cells[6].FindControl("lnlViewFiles");
            if (e.Row.Cells[5].Text == "0")
            {
                lnlViewFiles.Visible = false;
            }
            else
            {
                lnlViewFiles.Visible = true;
            }
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_AddDailyTask", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@TaskID", ViewState["TaskID"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();
        GVPhoto.DataSource = FillGridPhoto();
        GVPhoto.DataBind();
    }
}