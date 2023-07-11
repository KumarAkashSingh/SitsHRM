using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Reflection;
using System.Globalization;

public partial class Pages_AlumniRegistration : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;
    byte[] documentcontent;
    string Filename;
    string fileexten;
    string contenttype;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DisableControls();
            FillDetails();
            FillOrganisationDetails();
        }

    }
    void DisableControls()
    {
        txtName.Enabled = false;
        txtFatherName.Enabled=false;
        txtContactNo.Enabled=false;
        txtEmail.Enabled = false; ;
        txtDOB.Enabled = false;
        txtPassingSession.Enabled = false;
        txtInstitute.Enabled = false;
        txtCourse.Enabled = false;
        txtStream.Enabled = false;
        txtEnrollmentNo.Enabled = false;
    }

    void FillDetails()
    {
        cmd = new SqlCommand("SP_Alumni_v2", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@StudentCode", Session["empCode"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ViewState["StudentCode"] = dt.Rows[0]["StudentCode"].ToString();
        ViewState["Form_Code"] = dt.Rows[0]["Form_Code"].ToString();
        txtName.Text = dt.Rows[0]["NameOfStudent"].ToString();
        txtFatherName.Text = dt.Rows[0]["FatherName"].ToString();
        txtContactNo.Text = dt.Rows[0]["Phonestudent"].ToString();
        txtEmail.Text = dt.Rows[0]["email"].ToString();
        txtDOB.Text = dt.Rows[0]["DOB"].ToString();
        txtPassingSession.Text = dt.Rows[0]["PassingSession"].ToString();
        txtInstitute.Text = dt.Rows[0]["InstituteName"].ToString();
        txtCourse.Text = dt.Rows[0]["CurrentClassSection"].ToString();
        txtStream.Text = dt.Rows[0]["stream"].ToString();
        txtEnrollmentNo.Text = dt.Rows[0]["enrollmentno"].ToString();

        txtFacebookProfile.Text = dt.Rows[0]["FacebookProfile"].ToString();
        txtInstagramProfile.Text = dt.Rows[0]["InstagramProfile"].ToString();
        txtLinkedinProfile.Text = dt.Rows[0]["LinkedinProfile"].ToString();
        txtTwitterProfile.Text = dt.Rows[0]["TwitterProfile"].ToString();
        txtMessage.Text = dt.Rows[0]["Message"].ToString();
    }

    void FillOrganisationDetails()
    {
        cmd = new SqlCommand("SP_Alumni_v2", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.Parameters.AddWithValue("@StudentCode", ViewState["StudentCode"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        lblRecords.Text = "&nbsp;[" + dt.Rows.Count.ToString() + " <span class='hidden-xs'>Record(s) Found</span>]";

        GVQualifications.DataSource = dt;
        GVQualifications.DataBind();
    }


    protected void ChkIsWorkingHere_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkIsWorkingHere.Checked)
        {
            LeftDate.Visible = false;
        }
        else {
            LeftDate.Visible = true;
        }

    }


    protected void btnSaveSocialMediaProfiles_Click(object sender, EventArgs e)
    {
        if (txtFacebookProfile.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Facebook Profile');$('.modal-backdrop').remove", true);
        }
        if (txtInstagramProfile.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Instagram Profile');$('.modal-backdrop').remove", true);
        }
        if (txtLinkedinProfile.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Linkedin Profile');$('.modal-backdrop').remove", true);
        }
        if (txtTwitterProfile.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Twitter Profile');$('.modal-backdrop').remove", true);
        }

        conn.Open();
        cmd = new SqlCommand("SP_Alumni_v2", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@FacebookProfile", txtFacebookProfile.Text);
        cmd.Parameters.AddWithValue("@InstagramProfile", txtInstagramProfile.Text);
        cmd.Parameters.AddWithValue("@LinkedinProfile", txtLinkedinProfile.Text);
        cmd.Parameters.AddWithValue("@TwitterProfile", txtTwitterProfile.Text);
        cmd.Parameters.AddWithValue("@Form_Code", ViewState["Form_Code"].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["empCode"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Social Media Profile Inserted','success');$('.modal-backdrop').remove();", true);
            txtFacebookProfile.Text = "";
            txtInstagramProfile.Text = "";
            txtLinkedinProfile.Text = "";
            txtTwitterProfile.Text = "";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Social Media can not be Inserted');$('.modal-backdrop').remove();", true);
        }
    }

    protected void btnSaveOrganisationDetails_Click(object sender, EventArgs e)
    {
        if (txtOrganisationName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Organisation name');$('.modal-backdrop').remove", true);
        }
        if (txtOrganisationWebsite.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Organisation Website');$('.modal-backdrop').remove", true);
        }
        if (txtJobLocation.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Job Location');$('.modal-backdrop').remove", true);
        }
        if (txtDesignation.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Job Profile');$('.modal-backdrop').remove", true);
        }
        if (txtJoiningDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Joining Date');$('.modal-backdrop').remove", true);
        }
        if (txtSalaryPackage.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Salary package');$('.modal-backdrop').remove", true);
        }

        conn.Open();
        cmd = new SqlCommand("SP_Alumni_v2", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@StudentCode", ViewState["StudentCode"].ToString());
        cmd.Parameters.AddWithValue("@OrganisationName", txtOrganisationName.Text);
        cmd.Parameters.AddWithValue("@OrganisationWebsite", txtOrganisationWebsite.Text);
        cmd.Parameters.AddWithValue("@JobLocation", txtJobLocation.Text);
        cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
        cmd.Parameters.AddWithValue("@JoiningDate", DateTime.ParseExact(txtJoiningDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@Package", txtSalaryPackage.Text);
        cmd.Parameters.AddWithValue("@OfficialEmail", txtOfficialEmail.Text);
        if(ChkIsWorkingHere.Checked)
        {
            cmd.Parameters.AddWithValue("@IsCurrentlyWorkingHere", 1);
        }
        else
        {
            cmd.Parameters.AddWithValue("@IsCurrentlyWorkingHere", 0);
            cmd.Parameters.AddWithValue("@LeftDate", DateTime.ParseExact(txtLeftDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        }
        cmd.Parameters.AddWithValue("@EmpCode", Session["empCode"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Organisation Details Inserted','success');$('.modal-backdrop').remove();", true);
            txtOrganisationName.Text = "";
            txtOrganisationWebsite.Text = "";
            txtJobLocation.Text = "";
            txtDesignation.Text = "";
            txtJoiningDate.Text = "";
            txtSalaryPackage.Text = "";
            txtOfficialEmail.Text = "";
            txtLeftDate.Text = "";
            ChkIsWorkingHere.Checked = false;
            FillOrganisationDetails();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Organisation Details can not be Inserted');$('.modal-backdrop').remove();", true);
        }
    }

    protected void btnMessage_Click(object sender, EventArgs e)
    {
        if (txtMessage.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Message');$('.modal-backdrop').remove", true);
        }

        conn.Open();
        cmd = new SqlCommand("SP_Alumni_v2", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@Message", txtMessage.Text);
        cmd.Parameters.AddWithValue("@Form_Code", ViewState["Form_Code"].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["empCode"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Message Inserted','success');$('.modal-backdrop').remove();", true);
            txtMessage.Text = "";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Message can not be Inserted');$('.modal-backdrop').remove();", true);
        }
    }

    protected void GVQualifications_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="del")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            ID = GVQualifications.Rows[RowIndex].Cells[0].Text;
            ViewState["ID"] = ID;
            Delete();
        }
    }

    void Delete()
    {
        conn.Open();
        cmd = new SqlCommand("SP_Alumni_v2", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@ID", ViewState["ID"].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["empCode"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Organisation Details Deleted Successfully','success');$('.modal-backdrop').remove();", true);
            FillOrganisationDetails();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Organisation Details can not be deleted');$('.modal-backdrop').remove();", true);
        }

    }
}