using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
public partial class Pages_UploadEmployeeDocs : System.Web.UI.Page
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
            txtDate.Text = DateTime.Now.Date.ToShortDateString();
            FillEmployee();
            FillDocType();
        }
    }
    protected void FillEmployee()
    {
        ddlEmployee.Items.Clear();
        ListItem item = new ListItem("Select Employee", "0");
        ddlEmployee.Items.Add(item);

        cmd = new SqlCommand("SP_Web_Documents", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@Type", Session["Type"].ToString());
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlEmployee.DataSource = dt;
        ddlEmployee.DataTextField = "EmployeeName";
        ddlEmployee.DataValueField = "E_Code";
        ddlEmployee.DataBind();
        dt.Dispose();
    }
    protected void FillDocType()
    {
        ddlDocType.Items.Clear();
        ListItem item = new ListItem("Select Document Type", "0");
        ddlDocType.Items.Add(item);

        cmd = new SqlCommand("SP_Web_Documents", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDocType.DataSource = dt;
        ddlDocType.DataTextField = "DocumentTypeName";
        ddlDocType.DataValueField = "DocumentTypeCode";
        ddlDocType.DataBind();
        dt.Dispose();
    }
    protected void FillDocName()
    {
        ddlDocName.Items.Clear();
        ListItem item = new ListItem("Select Document Name", "0");
        ddlDocName.Items.Add(item);

        cmd = new SqlCommand("SP_Web_Documents", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@DocumentTypeCode", ddlDocType.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDocName.DataSource = dt;
        ddlDocName.DataTextField = "documentname";
        ddlDocName.DataValueField = "documentcode";
        ddlDocName.DataBind();
        dt.Dispose();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {      
        if (Check() == true && CheckFile() == true)
        {
            FileInfo fi = new FileInfo(FileUpload1.FileName);
            byte[] documentcontent = FileUpload1.FileBytes;
            string Filename = fi.Name;
            string fileexten = fi.Extension;
            string contenttype = FileUpload1.PostedFile.ContentType;

            conn.Open();
            cmd = new SqlCommand("SP_Web_Documents", conn);
            cmd.Parameters.AddWithValue("@calltype", 5);
            cmd.Parameters.AddWithValue("@E_Code", ddlEmployee.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@DOU", Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@DocumentTypeCode", ddlDocType.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@DocumentCode", ddlDocName.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@LoginBy", Session["userid"].ToString());
            cmd.Parameters.AddWithValue("@Post", documentcontent);
            cmd.Parameters.AddWithValue("@ContentType", contenttype);
            cmd.Parameters.AddWithValue("@Filename", Filename);
            SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
            parm.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(parm);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            int result = Convert.ToInt32(parm.Value);
            cmd.Dispose();
            conn.Close();

            GVEmpDocs.DataSource = FillGrid();
            GVEmpDocs.DataBind();

            if (result == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('Document Already Exist');", true);
                //dlglbl.Text = "Document Already Exist";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('Document uploded successfully');", true);
                //dlglbl.Text = "Document uploded successfully";
            }
        }

    }

    public bool Check()
    {
        if (ddlEmployee.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('Please Select Employee');", true);
            //dlglbl.Text = "Please Select Employee";
            return false;
        }
        else if (ddlDocType.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('Please Select Document Type');", true);
            //dlglbl.Text = "Please Select Document Type";
            return false;
        }
        else if (ddlDocName.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('Please Select Document Name');", true);
            //dlglbl.Text = "Please Select Document Name";
            return false;
        }
        return true;
    }

    public bool CheckFile()
    {
        if (FileUpload1.HasFile)
        {
            if (FileUpload1.PostedFile.ContentLength > (1048576 * 5))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('Can Not Uploded the file more than 5 MB');", true);
                //dlglbl.Text = "Can Not Uploded the file more than 5 MB";
                return false;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('Nothing Is To Uploded');", true);
            //dlglbl.Text = "Nothing Is To Uploded";
            return false;
        }
        return true;
    }

    public DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_Web_Documents", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@E_Code", ddlEmployee.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        return dt;
    }
    
    protected void GVEmpDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = gvr.RowIndex;
            string RecordNo=GVEmpDocs.Rows[RowIndex].Cells[1].Text;
            ViewState["RecordNo"] = RecordNo;
            Popup1(true);
        }
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
    protected void btnNo1_Click(object sender, EventArgs e)
    {
        Popup1(false);
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
    protected void btnYes1_Click(object sender, EventArgs e)
    {
        conn.Open();
        cmd = new SqlCommand("SP_Web_Documents", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@RecordNo", ViewState["RecordNo"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup('Record deleted successfully');", true);
        //dlglbl.Text = "Record deleted successfully";

        GVEmpDocs.DataSource = FillGrid();
        GVEmpDocs.DataBind();
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVEmpDocs.DataSource = FillGrid();
        GVEmpDocs.DataBind();
    }
    protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocName();
    }
    protected void GVEmpDocs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = count.ToString();
            count++;
        }
    }
}