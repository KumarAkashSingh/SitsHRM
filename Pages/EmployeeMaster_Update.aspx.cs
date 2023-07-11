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

public partial class Pages_EmployeeMaster_Update : System.Web.UI.Page
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
            FillSalutation();
            FillBloodGroup();
            FillCategory();
            FillReligion();
            FillDepartmentType();
            FillStateAdd();
            FillStateAdd1();
            FillBranch();
            FillDesignationType();
            FillEmployeeType();
            FillStateAcad();
            FillYear();
            FillSubject();
            if (Session["e_code"] == null)
            {
                btnSaveEmpDetails.Enabled = false;
                btnSaveAddress.Enabled = false;
                btnSaveEmployeeJoiningDetails.Enabled = false;
                btnSaveAccountDetails.Enabled = false;
                CmdAdd.Enabled = false;
                CmdAddW.Enabled = false;
                btnSaveBooksPublished.Enabled = false;
                btnSavePaperPublished.Enabled = false;
            }
            else
            {
                btnSaveEmpDetails.Enabled = true;
                btnSaveAddress.Enabled = true;
                btnSaveEmployeeJoiningDetails.Enabled = true;
                btnSaveAccountDetails.Enabled = true;
                CmdAdd.Enabled = true;
                CmdAddW.Enabled = true;
                btnSaveBooksPublished.Enabled = true;
                btnSavePaperPublished.Enabled = true;
                

                if (Session["View"].ToString() == "1")
                {
                    btnBack.Enabled = true;
                    btnSaveEmpDetails.Enabled = false;
                    btnSaveAddress.Enabled = false;
                    btnSaveEmployeeJoiningDetails.Enabled = false;
                    btnSaveAccountDetails.Enabled = false;
                    CmdAdd.Enabled = false;
                    CmdAddW.Enabled = false;
                    btnSaveBooksPublished.Enabled = false;
                    btnSavePaperPublished.Enabled = false;
                    FillDetails();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "DisableHtmlTags();", true);
                }
                if(Session["View"].ToString() == "2")
                {
                    HdnE_Code.Value = Session["ECode"].ToString();

                    btnSaveEmpDetails.Visible = false;
                    btnUpdateEmpDetails.Visible = true;
                    btnSaveAddress.Visible = false;
                    btnUpdateAddress.Visible = true;
                    btnSaveEmployeeJoiningDetails.Visible = false;
                    btnUpdateEmployeeJoiningDetails.Visible = true;
                    btnSaveAccountDetails.Visible = false;
                    btnUpdateAccountDetails.Visible = true;
                    CmdAdd.Visible = false;
                    CmdUpdateAcadmic.Visible = true;
                    CmdAddW.Visible = false;
                    cmdUpdate.Visible = true;
                    btnSaveBooksPublished.Visible = false;
                    btnUpdateBooksPublished.Visible = true;
                    btnSavePaperPublished.Visible = false;
                    btnUpdatePaperPublished.Visible = true;
                    FillDetails();
                }
                else
                {
                    btnSaveEmpDetails.Enabled = true;
                    btnSaveAddress.Enabled = true;
                    btnSaveEmployeeJoiningDetails.Enabled = true;
                    btnSaveAccountDetails.Enabled = true;
                    CmdAdd.Enabled = true;
                    CmdAddW.Enabled = true;
                    btnSaveBooksPublished.Enabled = true;
                    btnSavePaperPublished.Enabled = true;
                }


            }
        }

    }

    protected void FillYear()
    {
        int currentYear = DateTime.Today.Year;
        for (int i=0;i<=60;i++)
        {
            CmbYear.Items.Add((currentYear-i).ToString());
        }
    }
    
     protected void FillSubject()
    {
        CmbSubject.Items.Clear();
        ListItem item = new ListItem("Select Subject", "0");
        CmbSubject.Items.Add(item);

        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 9);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        CmbSubject.DataSource = dt;
        CmbSubject.DataTextField = "ddlText";
        CmbSubject.DataValueField = "ddlvalue";
        CmbSubject.DataBind();
        dt.Dispose();
    }

    protected void FillEmployeeType()
    {
        ddlEmployeeType.Items.Clear();
        ListItem item = new ListItem("Select Employee Type", "0");
        ddlEmployeeType.Items.Add(item);

        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 8);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlEmployeeType.DataSource = dt;
        ddlEmployeeType.DataTextField = "ddlText";
        ddlEmployeeType.DataValueField = "ddlvalue";
        ddlEmployeeType.DataBind();
        dt.Dispose();
    }

    void FillResume()
    {
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 15);
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
            litResume.Text = "<a href='../Files/EmployeeResume/" + dt.Rows[0]["ResumeFileName"].ToString() + "' target='_blank'><i class='fa fa-file'></i>&nbsp;" + dt.Rows[0]["OriginalFileName"].ToString() + "</a>";
        }
        else
        {
            litResume.Text = "";
        }
    }

    void FillPublishedBookData()
    {
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 16);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        GVPublishedBooks.DataSource = dt;
        GVPublishedBooks.DataBind();

    }

    void FillPublishedPaperData()
    {
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 17);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        GVConferenceORJournal.DataSource = dt;
        GVConferenceORJournal.DataBind();

    }

    protected void FillDetails()
    {
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 18);
        cmd.Parameters.AddWithValue("@empCode", HdnE_Code.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        cmd.Dispose();
        da.Dispose();


        if (dt1.Rows[0]["ImageFile"].ToString() != "")
        {
            Image1.ImageUrl = dt1.Rows[0]["ImageFile"].ToString();
        }

        if (ddlSalutation.Items.FindByValue(dt1.Rows[0]["salutation"].ToString()) != null)
        {
            ddlSalutation.ClearSelection();
            ddlSalutation.Items.FindByValue(dt1.Rows[0]["salutation"].ToString()).Selected = true;
        }

        TxtFirstName.Text = dt1.Rows[0]["Firstname"].ToString();
        TxtMiddleName.Text = dt1.Rows[0]["MiddleName"].ToString();
        TxtLastName.Text = dt1.Rows[0]["Lastname"].ToString();

        if (dt1.Rows[0]["DateOfBirth"].ToString() != "")
        {
            DTPDOB.Text = DateTime.ParseExact(dt1.Rows[0]["DateOfBirth"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
        }

        if (CmbGender.Items.FindByText(dt1.Rows[0]["Gender"].ToString()) != null)
        {
            CmbGender.ClearSelection();
            CmbGender.Items.FindByText(dt1.Rows[0]["Gender"].ToString()).Selected = true;
        }


        if (CmbBloodGroup.Items.FindByValue(dt1.Rows[0]["BloodGroup"].ToString()) != null)
        {
            CmbBloodGroup.ClearSelection();
            CmbBloodGroup.Items.FindByValue(dt1.Rows[0]["BloodGroup"].ToString()).Selected = true;
        }


        if (CmbCategory.Items.FindByValue(dt1.Rows[0]["Category"].ToString()) != null)
        {
            CmbCategory.ClearSelection();
            CmbCategory.Items.FindByValue(dt1.Rows[0]["Category"].ToString()).Selected = true;
        }

        if (CmbReligion.Items.FindByValue(dt1.Rows[0]["Religion"].ToString()) != null)
        {
            CmbReligion.ClearSelection();
            CmbReligion.Items.FindByValue(dt1.Rows[0]["Religion"].ToString()).Selected = true;
        }


        if (CmbMaritalStatus.Items.FindByText(dt1.Rows[0]["Maritalstatus"].ToString()) != null)
        {
            CmbMaritalStatus.ClearSelection();
            CmbMaritalStatus.Items.FindByText(dt1.Rows[0]["Maritalstatus"].ToString()).Selected = true;
        }

        TxtMobileEmployee.Text = dt1.Rows[0]["Contact"].ToString();
        TxtEmail.Text = dt1.Rows[0]["Email"].ToString();
        TxtFatherName.Text = dt1.Rows[0]["FatherName"].ToString();
        TxtMotherName.Text = dt1.Rows[0]["MotherName"].ToString();
        Txtspousename.Text = dt1.Rows[0]["SpouseName"].ToString();

        if (dt1.Rows[0]["DateOfAnnv"].ToString() != "-")
        {
            if (dt1.Rows[0]["DateOfAnnv"].ToString() != "")
            {
                TxtWa.Text = DateTime.ParseExact(dt1.Rows[0]["DateOfAnnv"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            }
        }

        TxtAddPermanent.Text = dt1.Rows[0]["AddressP"].ToString();

        if (CmbStateP.Items.FindByValue(dt1.Rows[0]["StateP"].ToString()) != null)
        {
            CmbStateP.ClearSelection();
            CmbStateP.Items.FindByValue(dt1.Rows[0]["StateP"].ToString()).Selected = true;
        }
        FillDistrictAdd();
        if (CmbDistrictP.Items.FindByValue(dt1.Rows[0]["DistrictP"].ToString()) != null)
        {
            CmbDistrictP.ClearSelection();
            CmbDistrictP.Items.FindByValue(dt1.Rows[0]["DistrictP"].ToString()).Selected = true;
        }

        TxtThanaP.Text = dt1.Rows[0]["ThanaP"].ToString();
        TxtTownP.Text = dt1.Rows[0]["TownOrCityP"].ToString();
        TxtAddC.Text = dt1.Rows[0]["AddressC"].ToString();

        if (CmbStateC.Items.FindByValue(dt1.Rows[0]["StateC"].ToString()) != null)
        {
            CmbStateC.ClearSelection();
            CmbStateC.Items.FindByValue(dt1.Rows[0]["StateC"].ToString()).Selected = true;
        }
        FillDistrictAdd1();
        if (CmbDistrictC.Items.FindByValue(dt1.Rows[0]["DistrictC"].ToString()) != null)
        {
            CmbDistrictC.ClearSelection();
            CmbDistrictC.Items.FindByValue(dt1.Rows[0]["DistrictC"].ToString()).Selected = true;
        }

        TxtThanaC.Text = dt1.Rows[0]["ThanaC"].ToString();
        TxtTownC.Text = dt1.Rows[0]["TownOrCityC"].ToString();

        if (ddlBranch.Items.FindByValue(dt1.Rows[0]["BranchID"].ToString()) != null)
        {
            ddlBranch.ClearSelection();
            ddlBranch.Items.FindByValue(dt1.Rows[0]["BranchID"].ToString()).Selected = true;
        }

        if (ddlDesignation.Items.FindByValue(dt1.Rows[0]["DesignationTypeCode"].ToString()) != null)
        {
            ddlDesignation.ClearSelection();
            ddlDesignation.Items.FindByValue(dt1.Rows[0]["DesignationTypeCode"].ToString()).Selected = true;
        }
        if (ddlDepartmentType.Items.FindByValue(dt1.Rows[0]["DepartmentTypeCode"].ToString()) != null)
        {
            ddlDepartmentType.ClearSelection();
            ddlDepartmentType.Items.FindByValue(dt1.Rows[0]["DepartmentTypeCode"].ToString()).Selected = true;
        }
        FillDepartment();
        if (ddlDepartment.Items.FindByValue(dt1.Rows[0]["DepartmentCode"].ToString()) != null)
        {
            ddlDepartment.ClearSelection();
            ddlDepartment.Items.FindByValue(dt1.Rows[0]["DepartmentCode"].ToString()).Selected = true;
        }
        FillDesignation();
        if (ddlSubDesignation.Items.FindByValue(dt1.Rows[0]["DesignationCode"].ToString()) != null)
        {
            ddlSubDesignation.ClearSelection();
            ddlSubDesignation.Items.FindByValue(dt1.Rows[0]["DesignationCode"].ToString()).Selected = true;
        }

        

        if (ddlEmployeeType.Items.FindByValue(dt1.Rows[0]["EmployeetypeCode"].ToString()) != null)
        {
            ddlEmployeeType.ClearSelection();
            ddlEmployeeType.Items.FindByValue(dt1.Rows[0]["EmployeetypeCode"].ToString()).Selected = true;
        }

        if (ddlAdministrator.Items.FindByValue(dt1.Rows[0]["Administrator"].ToString()) != null)
        {
            ddlAdministrator.ClearSelection();
            ddlAdministrator.Items.FindByValue(dt1.Rows[0]["Administrator"].ToString()).Selected = true;
        }

        txtEmployeeID.Text = dt1.Rows[0]["EmployeeID"].ToString();
        TxtMachineID.Text = dt1.Rows[0]["NACCode"].ToString();

        if (dt1.Rows[0]["DateOfJoining"].ToString() != "")
        {
            txtJoiningDate.Text = DateTime.ParseExact(dt1.Rows[0]["DateOfJoining"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
        }


        //if (dt1.Rows[0]["LeftDate"].ToString() != "")
        //{
        //    txtLeftDate.Text = DateTime.ParseExact(dt1.Rows[0]["LeftDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
        //}
       
        TxtPF.Text = dt1.Rows[0]["PFAccountNo"].ToString();
        TxtPanNumber.Text = dt1.Rows[0]["Panno"].ToString();
        TxtBank.Text = dt1.Rows[0]["BankName"].ToString();
        TxtAccountNumber.Text = dt1.Rows[0]["BankACNo"].ToString();
        TxtAadhar.Text = dt1.Rows[0]["AdharNo"].ToString();
        txtIFSCCOde.Text = dt1.Rows[0]["IFSC_Code"].ToString();

        cmd = new SqlCommand("SP_DisplayQualifications", conn);
        cmd.Parameters.AddWithValue("@ECode", HdnE_Code.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        GVQualifications.DataSource = dt;
        GVQualifications.DataBind();


        cmd = new SqlCommand("SP_DisplayExperience", conn);
        cmd.Parameters.AddWithValue("@E_Code", HdnE_Code.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        Gvexperience.DataSource = dt;
        Gvexperience.DataBind();

        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 16);
        cmd.Parameters.AddWithValue("@E_Code", HdnE_Code.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        GVPublishedBooks.DataSource = dt;
        GVPublishedBooks.DataBind();

        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 17);
        cmd.Parameters.AddWithValue("@E_Code", HdnE_Code.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        GVConferenceORJournal.DataSource = dt;
        GVConferenceORJournal.DataBind();


    }

    protected void FillStateAcad()
    {
        CmbState.Items.Clear();
        ListItem item = new ListItem("Select State", "0");
        CmbState.Items.Add(item);

        cmd = new SqlCommand("SP_StateMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        CmbState.DataSource = dt;
        CmbState.DataTextField = "ddlText";
        CmbState.DataValueField = "ddlvalue";
        CmbState.DataBind();
        dt.Dispose();
    }

    protected void FillStateAdd()
    {
        CmbStateP.Items.Clear();
        ListItem item = new ListItem("Select State", "0");
        CmbStateP.Items.Add(item);

        cmd = new SqlCommand("SP_StateMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        CmbStateP.DataSource = dt;
        CmbStateP.DataTextField = "ddlText";
        CmbStateP.DataValueField = "ddlvalue";
        CmbStateP.DataBind();
        dt.Dispose();
    }

    protected void FillBranch()
    {
        ddlBranch.Items.Clear();
        ListItem item = new ListItem("Select Branch", "0");
        ddlBranch.Items.Add(item);

        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 10);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlBranch.DataSource = dt;
        ddlBranch.DataTextField = "ddlText";
        ddlBranch.DataValueField = "ddlvalue";
        ddlBranch.DataBind();
        dt.Dispose();
    }

    protected void FillDesignationType()
    {
        ddlDesignation.Items.Clear();
        ListItem item = new ListItem("Select Designation Type", "0");
        ddlDesignation.Items.Add(item);

        cmd = new SqlCommand("SP_DesignationTypeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDesignation.DataSource = dt;
        ddlDesignation.DataTextField = "ddlText";
        ddlDesignation.DataValueField = "ddlvalue";
        ddlDesignation.DataBind();
        dt.Dispose();
    }

    protected void FillDesignation()
    {
        ddlSubDesignation.Items.Clear();
        ListItem item = new ListItem("Select Designation", "0");
        ddlSubDesignation.Items.Add(item);

        cmd = new SqlCommand("SP_DesignationTypeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@DesignationTypeCode", ddlDesignation.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlSubDesignation.DataSource = dt;
        ddlSubDesignation.DataTextField = "ddlText";
        ddlSubDesignation.DataValueField = "ddlvalue";
        ddlSubDesignation.DataBind();
        dt.Dispose();
    }

    protected void FillStateAdd1()
    {
        CmbStateC.Items.Clear();
        ListItem item = new ListItem("Select State", "0");
        CmbStateC.Items.Add(item);

        cmd = new SqlCommand("SP_StateMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        CmbStateC.DataSource = dt;
        CmbStateC.DataTextField = "ddlText";
        CmbStateC.DataValueField = "ddlvalue";
        CmbStateC.DataBind();
        dt.Dispose();
    }

    protected void FillDistrictAdd()
    {
        CmbDistrictP.Items.Clear();
        ListItem item = new ListItem("Select District", "0");
        CmbDistrictP.Items.Add(item);

        cmd = new SqlCommand("SP_DistrictMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.Parameters.AddWithValue("@Stateid", CmbStateP.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        CmbDistrictP.DataSource = dt;
        CmbDistrictP.DataTextField = "ddlText";
        CmbDistrictP.DataValueField = "ddlvalue";
        CmbDistrictP.DataBind();
        dt.Dispose();
    }

    protected void FillDistrictAdd1()
    {
        CmbDistrictC.Items.Clear();
        ListItem item = new ListItem("Select District", "0");
        CmbDistrictC.Items.Add(item);

        cmd = new SqlCommand("SP_DistrictMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.Parameters.AddWithValue("@Stateid", CmbStateC.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        CmbDistrictC.DataSource = dt;
        CmbDistrictC.DataTextField = "ddlText";
        CmbDistrictC.DataValueField = "ddlvalue";
        CmbDistrictC.DataBind();
        dt.Dispose();
    }

    protected void FillDepartmentType()
    {
        ddlDepartmentType.Items.Clear();
        ListItem item = new ListItem("Select Department Type", "0");
        ddlDepartmentType.Items.Add(item);

        cmd = new SqlCommand("SP_DepartmentTypeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDepartmentType.DataSource = dt;
        ddlDepartmentType.DataTextField = "ddlText";
        ddlDepartmentType.DataValueField = "ddlvalue";
        ddlDepartmentType.DataBind();
        dt.Dispose();
    }

    protected void FillDepartment()
    {
        ddlDepartment.Items.Clear();
        ListItem item = new ListItem("Select Department", "0");
        ddlDepartment.Items.Add(item);

        cmd = new SqlCommand("SP_DepartmentMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.Parameters.AddWithValue("@DepartmentTypeCode", ddlDepartmentType.SelectedItem.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlDepartment.DataSource = dt;
        ddlDepartment.DataTextField = "ddlText";
        ddlDepartment.DataValueField = "ddlvalue";
        ddlDepartment.DataBind();
        dt.Dispose();
    }

    protected void FillReligion()
    {
        CmbReligion.Items.Clear();
        ListItem item = new ListItem("Select Religion", "0");
        CmbReligion.Items.Add(item);

        cmd = new SqlCommand("SP_ReligionMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        CmbReligion.DataSource = dt;
        CmbReligion.DataTextField = "ddlText";
        CmbReligion.DataValueField = "ddlvalue";
        CmbReligion.DataBind();
        dt.Dispose();
    }

    protected void FillCategory()
    {
        CmbCategory.Items.Clear();
        ListItem item = new ListItem("Select Category", "0");
        CmbCategory.Items.Add(item);

        cmd = new SqlCommand("SP_CategoryMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        CmbCategory.DataSource = dt;
        CmbCategory.DataTextField = "ddlText";
        CmbCategory.DataValueField = "ddlvalue";
        CmbCategory.DataBind();
        dt.Dispose();
    }

    protected void FillBloodGroup()
    {
        CmbBloodGroup.Items.Clear();
        ListItem item = new ListItem("Select Blood Group", "0");
        CmbBloodGroup.Items.Add(item);

        cmd = new SqlCommand("SP_BloodGroupMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        CmbBloodGroup.DataSource = dt;
        CmbBloodGroup.DataTextField = "ddlText";
        CmbBloodGroup.DataValueField = "ddlvalue";
        CmbBloodGroup.DataBind();
        dt.Dispose();
    }

    protected void FillSalutation()
    {
        ddlSalutation.Items.Clear();
        ListItem item = new ListItem("Select Salutation", "0");
        ddlSalutation.Items.Add(item);

        cmd = new SqlCommand("SP_SalutationMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        ddlSalutation.DataSource = dt;
        ddlSalutation.DataTextField = "ddlText";
        ddlSalutation.DataValueField = "ddlvalue";
        ddlSalutation.DataBind();
        dt.Dispose();
    }

    protected void GVConferenceORJournal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlConferenceORJournal = (DropDownList)e.Row.FindControl("ddlConferenceORJournal");
            DropDownList ddlType = (DropDownList)e.Row.FindControl("ddlType");
            if (ddlConferenceORJournal != null && ddlConferenceORJournal.Items.FindByValue(e.Row.Cells[1].Text) != null)
            {
                ddlConferenceORJournal.ClearSelection();
                ddlConferenceORJournal.Items.FindByValue(e.Row.Cells[1].Text).Selected = true;
            }
            if (ddlType != null && ddlType.Items.FindByValue(e.Row.Cells[2].Text) != null)
            {
                ddlType.ClearSelection();
                ddlType.Items.FindByValue(e.Row.Cells[2].Text).Selected = true;
            }
        }
    }
    
    protected void GVConferenceORJournal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            ViewState["PaperData"] = CopyPapersData();
            if (GVConferenceORJournal.Rows[RowIndex].Cells[0].Text != "0")
            {
                DeletePaperDetailsFromDB(GVPublishedBooks.Rows[RowIndex].Cells[0].Text);
            }
            GVConferenceORJournal.DeleteRow(RowIndex);
            GVConferenceORJournal.DataSource = ViewState["PaperData"] as DataTable;
            GVConferenceORJournal.DataBind();
        }
    }

    protected void btnNo4_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
        dlglbl.Text = "Joining Confirmed With " + ViewState["EmployeeCode"].ToString();
    }


    protected void CmdSave_Click(object sender, EventArgs e)
    {
        string ECode = "";
        string EmployeeCode = "";
        string EmployeeCodeWithoutPrefix = "";

        if (TxtFirstName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter First Name');$('.modal-backdrop').remove();", true);
            return;
        }
        else if (TxtFatherName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Father Name');$('.modal-backdrop').remove();", true);
            return;
        }
        else if (TxtMotherName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Mother Name');$('.modal-backdrop').remove();", true);
            return;
        }
        else if (ddlDepartment.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter First Name');$('.modal-backdrop').remove();", true);
            dlglbl.Text = "Please Select Department";
            return;
        }
        else if (ddlDesignation.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Designation');$('.modal-backdrop').remove();", true);
            return;
        }
        else if (ddlEmployeeType.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Employee Type');$('.modal-backdrop').remove();", true);
            return;
        }

        if (TxtMiddleName.Text == "") TxtMiddleName.Text = "-";
        if (TxtLastName.Text == "") TxtLastName.Text = "-";
        if (TxtEmail.Text == "") TxtEmail.Text = "-";
        if (TxtMobileEmployee.Text == "") TxtMobileEmployee.Text = "-";
        if (TxtAddPermanent.Text == "") TxtAddPermanent.Text = "-";
        if (TxtThanaP.Text == "") TxtThanaP.Text = "-";
        if (TxtTownP.Text == "") TxtTownP.Text = "-";
        if (TxtAddC.Text == "") TxtAddC.Text = "-";
        if (TxtTownC.Text == "") TxtTownC.Text = "-";
        if (TxtThanaC.Text == "") TxtThanaC.Text = "-";
        if (Txtspousename.Text == "") Txtspousename.Text = "-";
        if (TxtMobileEmployee.Text == "") TxtMobileEmployee.Text = "-";
        if (TxtPF.Text == "") TxtPF.Text = "-";
        if (TxtPanNumber.Text == "") TxtPanNumber.Text = "-";
        if (TxtAccountNumber.Text == "") TxtAccountNumber.Text = "-";
        if (TxtBank.Text == "") TxtBank.Text = "-";
        if (txtIFSCCOde.Text == "") txtIFSCCOde.Text = "-";
        if (TxtAadhar.Text == "") TxtAadhar.Text = "-";
        if (TxtMachineID.Text == "") TxtMachineID.Text = "0";
        //if (txtAppointmentLetterNo.Text == "") txtAppointmentLetterNo.Text = "-";


        //string mchildName = TxtFirstName.Text.Trim() + " " + TxtMiddleName.Text.Replace("-", "").Trim() + " " + TxtLastName.Text.Replace("-", "").Trim();

        string mchildName = "";
        if (ddlSalutation.SelectedItem.Value != "0")
        {
            mchildName = ddlSalutation.SelectedItem.Text + " " + TxtFirstName.Text.Trim() + " " + TxtMiddleName.Text.Replace("-", "").Trim() + " " + TxtLastName.Text.Replace("-", "").Trim();
        }
        else
        {
            mchildName = TxtFirstName.Text.Trim() + " " + TxtMiddleName.Text.Replace("-", "").Trim() + " " + TxtLastName.Text.Replace("-", "").Trim();
        }

        string msg = "";

        if (ddlAdministrator.SelectedItem.Text == "Y")
        {
            msg = "1";
        }
        else
        {
            msg = "0";
        }

        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        SqlParameter parm = new SqlParameter("@MaximumECode", SqlDbType.Int);
        parm.Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(parm);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        int E_Code = Convert.ToInt32(parm.Value);
        cmd.Dispose();
        conn.Close();

        ECode = E_Code.ToString();

        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        string str = dt.Rows[0][0].ToString();
        if (str.Length == 1)
        {
            str = "000" + str;
        }
        else if (str.Length == 2)
        {
            str = "00" + str;
        }
        else if (str.Length == 3)
        {
            str = "0" + str;
        }
        EmployeeCode = dt.Rows[0][1].ToString() + str;
        ViewState["EmployeeCode"] = dt.Rows[0][1].ToString() + str;
        EmployeeCodeWithoutPrefix = dt.Rows[0][0].ToString();

        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@MaximumECode", ECode);
        cmd.Parameters.AddWithValue("@MaximumEmpCode", EmployeeCode);
        cmd.Parameters.AddWithValue("@employeename", mchildName);
        cmd.Parameters.AddWithValue("@DesignationCode", ddlDesignation.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@DepartmentCode", ddlDepartment.SelectedItem.Value);
        if (txtJoiningDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@DOJ", DateTime.ParseExact(txtJoiningDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        }
        if (DTPDOB.Text != "")
        {
            cmd.Parameters.AddWithValue("@dob", DateTime.ParseExact(DTPDOB.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        }
        if (TxtWa.Text != "")
        {
            cmd.Parameters.AddWithValue("@DOA", DateTime.ParseExact(TxtWa.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        }
        cmd.Parameters.AddWithValue("@pfaccountno", TxtPF.Text);
        if (CmbBloodGroup.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@bloodgroup", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@bloodgroup", CmbBloodGroup.SelectedItem.Text);
        }
        cmd.Parameters.AddWithValue("@dlno", TxtAadhar.Text);
        cmd.Parameters.AddWithValue("@IFSC_Code", txtIFSCCOde.Text);
        cmd.Parameters.AddWithValue("@bankname", TxtBank.Text);
        cmd.Parameters.AddWithValue("@bankacno", TxtAccountNumber.Text);
        cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@EmployeeType", ddlEmployeeType.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@fathername", TxtFatherName.Text);
        cmd.Parameters.AddWithValue("@mothername", TxtMotherName.Text);
        cmd.Parameters.AddWithValue("@msg", msg);
        //cmd.Parameters.AddWithValue("@TaxCategory", ddlTaxCategory.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@naccode", TxtMachineID.Text);
        //cmd.Parameters.AddWithValue("@SalaryCategory", ddlSalaryCategory.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@contact", TxtMobileEmployee.Text);
        cmd.Parameters.AddWithValue("@email", TxtEmail.Text);
        if (CmbCategory.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@category", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@category", CmbCategory.SelectedItem.Text);
        }
        if (CmbGender.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@mf", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@mf", CmbGender.SelectedItem.Text);
        }

        if (CmbMaritalStatus.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@maritalstatus", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@maritalstatus", CmbMaritalStatus.SelectedItem.Text);
        }
        if (CmbReligion.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@religion", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@religion", CmbReligion.SelectedItem.Text);
        }

        cmd.Parameters.AddWithValue("@addressp", TxtAddPermanent.Text);
        cmd.Parameters.AddWithValue("@townorcityp", TxtTownP.Text);

        if (CmbStateP.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@statep", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@statep", CmbStateP.SelectedItem.Text);
        }

        if (CmbDistrictP.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@districtp", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@districtp", CmbDistrictP.SelectedItem.Text);
        }

        cmd.Parameters.AddWithValue("@thanap", TxtThanaP.Text);
        cmd.Parameters.AddWithValue("@addressc", TxtAddC.Text);
        cmd.Parameters.AddWithValue("@townorcityc", TxtTownC.Text);

        if (CmbStateC.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@statec", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@statec", CmbStateC.SelectedItem.Text);
        }

        if (CmbDistrictC.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@districtc", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@districtc", CmbDistrictC.SelectedItem.Text);
        }
        cmd.Parameters.AddWithValue("@thanac", TxtThanaC.Text);
        cmd.Parameters.AddWithValue("@panno", TxtPanNumber.Text);
        cmd.Parameters.AddWithValue("@SubDepartment", ddlDepartment.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@SubDesignation", ddlDesignation.SelectedItem.Value);
        //if (txtRegistrationDate.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@DOR", DateTime.ParseExact(txtRegistrationDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        //}
        if (txtLeftDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@DOL", DateTime.ParseExact(txtLeftDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        }
        //if (txtAppointmentDate.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@DOApp", DateTime.ParseExact(txtAppointmentDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        //}
        //cmd.Parameters.AddWithValue("@LetterNo", txtAppointmentLetterNo.Text);
        cmd.Parameters.AddWithValue("@firstname", TxtFirstName.Text);
        cmd.Parameters.AddWithValue("@middlename", TxtMiddleName.Text);
        cmd.Parameters.AddWithValue("@lastname", TxtLastName.Text);
        if (ddlSalutation.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@Salutation", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@Salutation", ddlSalutation.SelectedItem.Value);
        }
        cmd.Parameters.AddWithValue("@spousename", Txtspousename.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        Session["ECode"] = ECode;
        UpdatePhoto();
        UpdateBookData();
        UpdatePaperData();
        Session["ECode"] = null;
        Session["documentcontent"] = null;
        Session["Filename"] = null;
        Session["fileexten"] = null;
        Session["contenttype"] = null;

        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        string rand = dt.Rows[0][0].ToString();
        ViewState["rand"] = rand;

        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.Parameters.AddWithValue("@MaximumECode", ECode);
        cmd.Parameters.AddWithValue("@MaximumEmpCode", EmployeeCode);
        cmd.Parameters.AddWithValue("@EmpCode", EmployeeCodeWithoutPrefix);
        cmd.Parameters.AddWithValue("@rand", rand);
        cmd.Parameters.AddWithValue("@contact", TxtMobileEmployee.Text);
        cmd.Parameters.AddWithValue("@employeename", Session["userName"].ToString());
        SqlParameter parm2 = new SqlParameter("@WUC", SqlDbType.NVarChar);
        parm2.Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(parm2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        string WUC = parm2.Value.ToString();
        cmd.Dispose();
        conn.Close();

        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@type", "Employee");
        cmd.Parameters.AddWithValue("@WUC", WUC);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        Popup4(true);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Details Saved successfully','success');$('.modal-backdrop').remove();", true);
        Session["Updated"] = 1;
    }

    bool UpdatePaperData()
    {
        bool flag = false;
        foreach (GridViewRow row in GVConferenceORJournal.Rows)
        {
            TextBox txtPaperTitle = (TextBox)row.FindControl("txtPaperTitle");
            DropDownList ddlConferenceORJournal = (DropDownList)row.FindControl("ddlConferenceORJournal");
            TextBox txtConferenceJournalName = (TextBox)row.FindControl("txtConferenceJournalName");
            DropDownList ddlType = (DropDownList)row.FindControl("ddlType");
            TextBox txtPublishedDate = (TextBox)row.FindControl("txtPublishedDate");
            TextBox txtJournalNo = (TextBox)row.FindControl("txtJournalNo");
            TextBox txtCPlace = (TextBox)row.FindControl("txtCPlace");
            if (txtPaperTitle.Text != "" && txtPublishedDate.Text != "")
            {
                conn.Open();
                cmd = new SqlCommand("SP_EmployeeMaster", conn);
                cmd.Parameters.AddWithValue("@calltype", 14);
                cmd.Parameters.AddWithValue("@PaperTitle", txtPaperTitle.Text);
                cmd.Parameters.AddWithValue("@ConferenceORJournal", ddlConferenceORJournal.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@ConferenceJournalName", txtConferenceJournalName.Text);
                cmd.Parameters.AddWithValue("@Type", ddlType.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@PublishedDate", DateTime.ParseExact(txtPublishedDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@JournalNo", txtJournalNo.Text);
                cmd.Parameters.AddWithValue("@Place", txtCPlace.Text);
                cmd.Parameters.AddWithValue("@ID", row.Cells[0].Text);
                cmd.Parameters.AddWithValue("@E_Code", ViewState["ID"].ToString());
                cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
                //For Audit Trail

                //cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
                //cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
                //cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
                //cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Paper Added Successfully','success');$('.modal-backdrop').remove();", true);
            }
            else
            {
                flag = true;
            }
        }
        return flag;
    }


    bool UpdatePaperData1()
    {
        bool flag = false;
        foreach (GridViewRow row in GVConferenceORJournal.Rows)
        {
            TextBox txtPaperTitle = (TextBox)row.FindControl("txtPaperTitle");
            DropDownList ddlConferenceORJournal = (DropDownList)row.FindControl("ddlConferenceORJournal");
            TextBox txtConferenceJournalName = (TextBox)row.FindControl("txtConferenceJournalName");
            DropDownList ddlType = (DropDownList)row.FindControl("ddlType");
            TextBox txtPublishedDate = (TextBox)row.FindControl("txtPublishedDate");
            TextBox txtJournalNo = (TextBox)row.FindControl("txtJournalNo");
            TextBox txtCPlace = (TextBox)row.FindControl("txtCPlace");
            if (txtPaperTitle.Text != "" && txtPublishedDate.Text != "")
            {
                conn.Open();
                cmd = new SqlCommand("SP_EmployeeMaster", conn);
                cmd.Parameters.AddWithValue("@calltype", 14);
                cmd.Parameters.AddWithValue("@PaperTitle", txtPaperTitle.Text);
                cmd.Parameters.AddWithValue("@ConferenceORJournal", ddlConferenceORJournal.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@ConferenceJournalName", txtConferenceJournalName.Text);
                cmd.Parameters.AddWithValue("@Type", ddlType.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@PublishedDate", DateTime.ParseExact(txtPublishedDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@JournalNo", txtJournalNo.Text);
                cmd.Parameters.AddWithValue("@Place", txtCPlace.Text);
                cmd.Parameters.AddWithValue("@ID", row.Cells[0].Text);
                cmd.Parameters.AddWithValue("@E_Code", HdnE_Code.Value);
                cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Paper Added Successfully','success');$('.modal-backdrop').remove();", true);
            }
            else
            {
                flag = true;
            }
        }
        return flag;
    }

    bool UpdateBookData()
    {
        bool flag = false;
        foreach (GridViewRow row in GVPublishedBooks.Rows)
        {
            TextBox txtBookTitle = (TextBox)row.FindControl("txtBookTitle");
            TextBox txtAuthorsName = (TextBox)row.FindControl("txtAuthorsName");
            TextBox txtISBNNo = (TextBox)row.FindControl("txtISBNNo");
            TextBox txtPublication = (TextBox)row.FindControl("txtPublication");
            TextBox txtPlace = (TextBox)row.FindControl("txtPlace");
            TextBox txtYear = (TextBox)row.FindControl("txtYear");
            if (txtBookTitle.Text != "" && txtAuthorsName.Text != "" && txtYear.Text != "")
            {
                conn.Open();
                cmd = new SqlCommand("SP_EmployeeMaster", conn);
                cmd.Parameters.AddWithValue("@calltype", 13);
                cmd.Parameters.AddWithValue("@BookTitle", txtBookTitle.Text);
                cmd.Parameters.AddWithValue("@AuthorsName", txtAuthorsName.Text);
                cmd.Parameters.AddWithValue("@ISBNNumber", txtISBNNo.Text);
                cmd.Parameters.AddWithValue("@Publication", txtPublication.Text);
                cmd.Parameters.AddWithValue("@Place", txtPlace.Text);
                cmd.Parameters.AddWithValue("@Year", txtYear.Text);
                cmd.Parameters.AddWithValue("@ID", row.Cells[0].Text);
                cmd.Parameters.AddWithValue("@E_Code", ViewState["ID"].ToString());
                cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
                //For Audit Trail

                //cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
                //cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
                //cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
                //cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Book Added Successfully','success');$('.modal-backdrop').remove();", true);
            }
            else
            {
                flag = true;
            }
        }
        return flag;
    }

    bool UpdateBookData1()
    {
        bool flag = false;
        foreach (GridViewRow row in GVPublishedBooks.Rows)
        {
            TextBox txtBookTitle = (TextBox)row.FindControl("txtBookTitle");
            TextBox txtAuthorsName = (TextBox)row.FindControl("txtAuthorsName");
            TextBox txtISBNNo = (TextBox)row.FindControl("txtISBNNo");
            TextBox txtPublication = (TextBox)row.FindControl("txtPublication");
            TextBox txtPlace = (TextBox)row.FindControl("txtPlace");
            TextBox txtYear = (TextBox)row.FindControl("txtYear");
            if (txtBookTitle.Text != "" && txtAuthorsName.Text != "" && txtYear.Text != "")
            {
                conn.Open();
                cmd = new SqlCommand("SP_EmployeeMaster", conn);
                cmd.Parameters.AddWithValue("@calltype", 13);
                cmd.Parameters.AddWithValue("@BookTitle", txtBookTitle.Text);
                cmd.Parameters.AddWithValue("@AuthorsName", txtAuthorsName.Text);
                cmd.Parameters.AddWithValue("@ISBNNumber", txtISBNNo.Text);
                cmd.Parameters.AddWithValue("@Publication", txtPublication.Text);
                cmd.Parameters.AddWithValue("@Place", txtPlace.Text);
                cmd.Parameters.AddWithValue("@Year", txtYear.Text);
                cmd.Parameters.AddWithValue("@ID", row.Cells[0].Text);
                cmd.Parameters.AddWithValue("@E_Code", HdnE_Code.Value);
                cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
                //For Audit Trail

                //cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
                //cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
                //cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
                //cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Book Added Successfully','success');$('.modal-backdrop').remove();", true);
            }
            else
            {
                flag = true;
            }
        }
        return flag;
    }

    protected void UpdatePhoto()
    {
        if (ViewState["ProfileImagePath"] != null && ViewState["ProfileImagePath"].ToString() != "")
        {
            conn.Open();
            cmd = new SqlCommand("SP_EmployeeMaster", conn);
            cmd.Parameters.AddWithValue("@calltype", 9);
            cmd.Parameters.AddWithValue("@FilePath", ViewState["ProfileImagePath"].ToString());
            cmd.Parameters.AddWithValue("@E_Code", HdnE_Code.Value);
            cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
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

    private void DeletePaperDetailsFromDB(string ID)
    {
        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 20);
        cmd.Parameters.AddWithValue("@ID", ID);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        //For Audit Trail

        //cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
        //cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
        //cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
        //cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();
    }

    protected void GVConferenceORJournal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt2 = ViewState["PaperData"] as DataTable;
        dt2.Rows[index].Delete();
        ViewState["PaperData"] = dt2;
    }

    protected DataTable CopyPapersData()
    {
        DataTable Details = new DataTable();
        Details.Clear();
        Details.Columns.Add("ID");
        Details.Columns.Add("PaperTitle");
        Details.Columns.Add("ConferenceORJournal");
        Details.Columns.Add("ConferenceJournalName");
        Details.Columns.Add("Type");
        Details.Columns.Add("PublishedDate");
        Details.Columns.Add("JournalNo");
        Details.Columns.Add("Place");

        foreach (GridViewRow row in GVConferenceORJournal.Rows)
        {
            TextBox txtPaperTitle = (TextBox)row.FindControl("txtPaperTitle");
            DropDownList ddlConferenceORJournal = (DropDownList)row.FindControl("ddlConferenceORJournal");
            TextBox txtConferenceJournalName = (TextBox)row.FindControl("txtConferenceJournalName");
            DropDownList ddlType = (DropDownList)row.FindControl("ddlType");
            TextBox txtPublishedDate = (TextBox)row.FindControl("txtPublishedDate");
            TextBox txtJournalNo = (TextBox)row.FindControl("txtJournalNo");
            TextBox txtCPlace = (TextBox)row.FindControl("txtCPlace");
            DataRow r1 = Details.NewRow();
            r1[0] = row.Cells[0].Text;
            r1[1] = txtPaperTitle.Text;
            r1[2] = ddlConferenceORJournal.SelectedItem.Text;
            r1[3] = txtConferenceJournalName.Text;
            r1[4] = ddlType.SelectedItem.Text;
            r1[5] = txtPublishedDate.Text;
            r1[6] = txtJournalNo.Text;
            r1[7] = txtCPlace.Text;
            Details.Rows.Add(r1);
        }
        return Details;
    }
    protected void btnAddPaperRow_Click(object sender, EventArgs e)
    {
        DataTable Details = CopyPapersData();
        DataRow r2 = Details.NewRow();
        r2[0] = "0";
        Details.Rows.Add(r2);

        GVConferenceORJournal.DataSource = Details;
        GVConferenceORJournal.DataBind();

    }
    private void DeleteBookDetailsFromDB(string ID)
    {
        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 19);
        cmd.Parameters.AddWithValue("@ID", ID);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        //For Audit Trail

        //cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
        //cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
        //cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
        //cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();
    }
    protected void GVPublishedBooks_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt2 = ViewState["BooksData"] as DataTable;
        dt2.Rows[index].Delete();
        ViewState["BooksData"] = dt2;
    }
    protected void GVPublishedBooks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            ViewState["BooksData"] = CopyBooksData();
            if (GVPublishedBooks.Rows[RowIndex].Cells[0].Text != "0")
            {
                DeleteBookDetailsFromDB(GVPublishedBooks.Rows[RowIndex].Cells[0].Text);
            }
            GVPublishedBooks.DeleteRow(RowIndex);
            GVPublishedBooks.DataSource = ViewState["BooksData"] as DataTable;
            GVPublishedBooks.DataBind();
        }
    }

    protected DataTable CopyBooksData()
    {
        DataTable Details = new DataTable();
        Details.Clear();
        Details.Columns.Add("ID");
        Details.Columns.Add("BookTitle");
        Details.Columns.Add("AuthorsName");
        Details.Columns.Add("Publication");
        Details.Columns.Add("Place");
        Details.Columns.Add("Year");
        Details.Columns.Add("ISBNNumber");

        foreach (GridViewRow row in GVPublishedBooks.Rows)
        {
            TextBox txtBookTitle = (TextBox)row.FindControl("txtBookTitle");
            TextBox txtAuthorsName = (TextBox)row.FindControl("txtAuthorsName");
            TextBox txtISBNNo = (TextBox)row.FindControl("txtISBNNo");
            TextBox txtPublication = (TextBox)row.FindControl("txtPublication");
            TextBox txtPlace = (TextBox)row.FindControl("txtPlace");
            TextBox txtYear = (TextBox)row.FindControl("txtYear");
            DataRow r1 = Details.NewRow();
            r1[0] = row.Cells[0].Text;
            r1[1] = txtBookTitle.Text;
            r1[2] = txtAuthorsName.Text;
            r1[3] = txtPublication.Text;
            r1[4] = txtPlace.Text;
            r1[5] = txtYear.Text;
            r1[6] = txtISBNNo.Text;
            Details.Rows.Add(r1);
        }
        return Details;
    }
    protected void btnAddBooksRow_Click(object sender, EventArgs e)
    {
        DataTable Details = CopyBooksData();
        DataRow r2 = Details.NewRow();
        r2[0] = "0";
        Details.Rows.Add(r2);

        GVPublishedBooks.DataSource = Details;
        GVPublishedBooks.DataBind();
    }

    protected void CmdAddW_Click(object sender, EventArgs e)
    {
        if (TxtOrganisation.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter The Organisation');$('.modal-backdrop').remove();", true);
            return;
        }
        if (DateFrm.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select From Date');$('.modal-backdrop').remove();", true);
            return;
        }
        if (DateTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select To Date');$('.modal-backdrop').remove();", true);
            return;
        }
        if (DateTime.ParseExact(DateFrm.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(DateTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Valid Date Range');$('.modal-backdrop').remove();", true);
            return;
        }

        if (Txtaddressworking.Text == "")
        {
            Txtaddressworking.Text = "-";
        }
        if (TxtDesignation.Text == "")
        {
            TxtDesignation.Text = "-";
        }

        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@organisation", TxtOrganisation.Text.ToUpper());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Already Entered');$('.modal-backdrop').remove();", true);
            return;
        }

        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["ID"].ToString());
        cmd.Parameters.AddWithValue("@organisation", TxtOrganisation.Text);
        cmd.Parameters.AddWithValue("@address", Txtaddressworking.Text);
        cmd.Parameters.AddWithValue("@desg", TxtDesignation.Text);
        cmd.Parameters.AddWithValue("@datefrom", DateTime.ParseExact(DateFrm.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@dateto", DateTime.ParseExact(DateTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();
        DisplayExperience();

        TxtOrganisation.Text = "";
        Txtaddressworking.Text = "";
        TxtDesignation.Text = "";
        DateFrm.Text = "";
        DateTo.Text = "";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Entered successfully','success');$('.modal-backdrop').remove();", true);
    }

    protected void GVQualifications_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string MLC = "", MLC1 = "", MLC2 = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            MLC = GVQualifications.Rows[RowIndex].Cells[0].Text;
            MLC1 = GVQualifications.Rows[RowIndex].Cells[1].Text;
            MLC2 = GVQualifications.Rows[RowIndex].Cells[3].Text;

            conn.Open();
            cmd = new SqlCommand("SP_EmployeeMaster", conn);
            cmd.Parameters.AddWithValue("@calltype", 21);
            cmd.Parameters.AddWithValue("@E_Code", MLC); 
            cmd.Parameters.AddWithValue("@acadq_code", MLC1);
            cmd.Parameters.AddWithValue("@acadb_code", MLC2);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            DisplayQualifications();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Deleted successfully','success');$('.modal-backdrop').remove();", true);
        }
    }

    protected void GVQualifications_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
    }
    protected void Gvexperience_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string MLC = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            MLC = Gvexperience.Rows[RowIndex].Cells[0].Text;

            conn.Open();
            cmd = new SqlCommand("SP_EmployeeMaster", conn);
            cmd.Parameters.AddWithValue("@calltype", 11);
            cmd.Parameters.AddWithValue("@recno", MLC);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            DisplayExperience();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Deleted successfully','success');$('.modal-backdrop').remove();", true);
        }
    }

    protected void Gvexperience_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }

    protected void CmdClose_Click(object sender, EventArgs e)
    {
        txtQualification.Text = "";
        CmbYear.SelectedIndex = 0;
        CmbSubject.SelectedIndex = 0;
        TxtSchool.Text = "";
        txtBoard.Text = "";
        TxtRollNo.Text = "";
        TxtEnrollNo.Text = "";
        TxtCertificateNo.Text = "";
        CmbState.SelectedIndex = 0;
        TxtMM.Text = "";
        Txtobt.Text = "";
        Txtper.Text = "";
        CmbDiv.SelectedIndex = 0;
        CmdAdd.Visible = true;
        CmdClose.Visible = false;
    }
    

    protected void CmdAdd_Click(object sender, EventArgs e)
    {
        
        if (txtQualification.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Qualification');$('.modal-backdrop').remove();", true);
            return;
        }
        if (CmbYear.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Year');$('.modal-backdrop').remove();", true);
            return;
        }

        if (TxtSchool.Text == "")
        {
            TxtSchool.Text = "-";
        }
        if (TxtRollNo.Text == "")
        {
            TxtRollNo.Text = "-";
        }
        if (TxtEnrollNo.Text == "")
        {
            TxtEnrollNo.Text = "-";
        }
        if (TxtMM.Text == "")
        {
            TxtMM.Text = "0";
        }
        if (Txtobt.Text == "")
        {
            Txtobt.Text = "0";
        }
        if (Txtper.Text == "")
        {
            Txtper.Text = "0";
        }

        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["ID"].ToString());
        cmd.Parameters.AddWithValue("@acadqname", txtQualification.Text);
        cmd.Parameters.AddWithValue("@schoolname", TxtSchool.Text);
        cmd.Parameters.AddWithValue("@acadbname", txtBoard.Text);
        cmd.Parameters.AddWithValue("@percent", Txtper.Text);
        if (CmbDiv.SelectedItem.Text == "Select Division")
        {
            cmd.Parameters.AddWithValue("@division", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@division", CmbDiv.SelectedItem.Text);
        }

        cmd.Parameters.AddWithValue("@year", CmbYear.SelectedItem.Text);
        if (CmbSubject.SelectedItem.Text == "Select Subjects")
        {
            cmd.Parameters.AddWithValue("@subjects", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@subjects", CmbSubject.SelectedItem.Text);
        }
        cmd.Parameters.AddWithValue("@stateid", CmbState.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@rollno", TxtRollNo.Text);
        cmd.Parameters.AddWithValue("@certificateno", TxtCertificateNo.Text);
        cmd.Parameters.AddWithValue("@enrollno", TxtEnrollNo.Text);
        cmd.Parameters.AddWithValue("@MM", TxtMM.Text);
        cmd.Parameters.AddWithValue("@MO", Txtobt.Text);
        cmd.Parameters.AddWithValue("@empCode", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Academic Details Already Entered');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Academic Details Entered successfully','success');$('.modal-backdrop').remove();", true);
            DisplayQualifications();
        }

    }

    protected void CmbStateC_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDistrictAdd1();
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            TxtAddC.Text = TxtAddPermanent.Text;
            CmbStateC.SelectedItem.Text = CmbStateP.SelectedItem.Text;
            CmbDistrictC.SelectedItem.Text = CmbDistrictP.SelectedItem.Text;
            TxtTownC.Text = TxtTownP.Text;
            TxtThanaC.Text = TxtThanaP.Text;
        }
        else
        {
            TxtAddC.Text = "-";
            CmbStateC.SelectedIndex = 0;
            CmbDistrictC.SelectedIndex = 0;
            TxtTownC.Text = "-";
            TxtThanaC.Text = "-";
        }
    }
    //HdnE_Code.Value

    protected void DisplayQualifications()
    {
        cmd = new SqlCommand("SP_DisplayQualifications", conn);
        cmd.Parameters.AddWithValue("@ECode", ViewState["ID"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        GVQualifications.DataSource = dt;
        GVQualifications.DataBind();
    }

    protected void DisplayExperience()
    {
        cmd = new SqlCommand("SP_DisplayExperience", conn);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["ID"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        Gvexperience.DataSource = dt;
        Gvexperience.DataBind();
    }
    protected void CmbStateP_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDistrictAdd();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session["PageURL"].ToString());
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearDetails();
        Popup3(true);
    }
    protected void ClearDetails()
    {

    }

    protected void CmdUpdate_Click(object sender, EventArgs e)
    {
        if (TxtFirstName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter First Name');$('.modal-backdrop').remove();", true);
            return;
        }
        else if (TxtFatherName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Father Name');$('.modal-backdrop').remove();", true);
            return;
        }
        else if (TxtMotherName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Mother Name');$('.modal-backdrop').remove();", true);
            return;
        }
        else if (ddlDepartment.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Department');$('.modal-backdrop').remove();", true);
            return;
        }
        else if (ddlDesignation.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Designation');$('.modal-backdrop').remove();", true);
            return;
        }
        else if (ddlEmployeeType.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Employee Type');$('.modal-backdrop').remove();", true);
            return;
        }

        if (TxtMiddleName.Text == "") TxtMiddleName.Text = "-";
        if (TxtLastName.Text == "") TxtLastName.Text = "-";
        if (TxtEmail.Text == "") TxtEmail.Text = "-";
        if (TxtMobileEmployee.Text == "") TxtMobileEmployee.Text = "-";
        if (TxtAddPermanent.Text == "") TxtAddPermanent.Text = "-";
        if (TxtThanaP.Text == "") TxtThanaP.Text = "-";
        if (TxtTownP.Text == "") TxtTownP.Text = "-";
        if (TxtAddC.Text == "") TxtAddC.Text = "-";
        if (TxtTownC.Text == "") TxtTownC.Text = "-";
        if (TxtThanaC.Text == "") TxtThanaC.Text = "-";
        if (Txtspousename.Text == "") Txtspousename.Text = "-";
        if (TxtMobileEmployee.Text == "") TxtMobileEmployee.Text = "-";
        if (TxtPF.Text == "") TxtPF.Text = "-";
        if (TxtPanNumber.Text == "") TxtPanNumber.Text = "-";
        if (TxtAccountNumber.Text == "") TxtAccountNumber.Text = "-";
        if (TxtBank.Text == "") TxtBank.Text = "-";
        if (txtIFSCCOde.Text == "") txtIFSCCOde.Text = "-";
        if (TxtAadhar.Text == "") TxtAadhar.Text = "-";
        if (TxtMachineID.Text == "") TxtMachineID.Text = "0";
        //if (txtAppointmentLetterNo.Text == "") txtAppointmentLetterNo.Text = "-";

        string mchildName = "";
        if (ddlSalutation.SelectedItem.Value != "0")
        {
            mchildName = ddlSalutation.SelectedItem.Text + " " + TxtFirstName.Text.Trim() + " " + TxtMiddleName.Text.Replace("-", "").Trim() + " " + TxtLastName.Text.Replace("-", "").Trim();
        }
        else
        {
            mchildName = TxtFirstName.Text.Trim() + " " + TxtMiddleName.Text.Replace("-", "").Trim() + " " + TxtLastName.Text.Replace("-", "").Trim();
        }
        string msg = "";

        if (ddlAdministrator.SelectedItem.Text == "Y")
        {
            msg = "1";
        }
        else
        {
            msg = "0";
        }

        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 18);
        cmd.Parameters.AddWithValue("@MaximumECode", HdnE_Code.Value);
        cmd.Parameters.AddWithValue("@employeename", mchildName);
        cmd.Parameters.AddWithValue("@DesignationCode", ddlDesignation.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@DepartmentCode", ddlDepartment.SelectedItem.Value);
        if (txtJoiningDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@DOJ", DateTime.ParseExact(txtJoiningDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        }
        if (DTPDOB.Text != "")
        {
            cmd.Parameters.AddWithValue("@dob", DateTime.ParseExact(DTPDOB.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        }
        if (TxtWa.Text != "")
        {
            cmd.Parameters.AddWithValue("@DOA", DateTime.ParseExact(TxtWa.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        }
        cmd.Parameters.AddWithValue("@pfaccountno", TxtPF.Text);
        if (CmbBloodGroup.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@bloodgroup", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@bloodgroup", CmbBloodGroup.SelectedItem.Text);
        }

        cmd.Parameters.AddWithValue("@dlno", TxtAadhar.Text);
        cmd.Parameters.AddWithValue("@IFSC_Code", txtIFSCCOde.Text);
        cmd.Parameters.AddWithValue("@bankname", TxtBank.Text);
        cmd.Parameters.AddWithValue("@bankacno", TxtAccountNumber.Text);
        cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@EmployeeType", ddlEmployeeType.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@fathername", TxtFatherName.Text);
        cmd.Parameters.AddWithValue("@mothername", TxtMotherName.Text);
        cmd.Parameters.AddWithValue("@msg", msg);
        //cmd.Parameters.AddWithValue("@TaxCategory", ddlTaxCategory.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@naccode", TxtMachineID.Text);
        //cmd.Parameters.AddWithValue("@SalaryCategory", ddlSalaryCategory.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@contact", TxtMobileEmployee.Text);
        cmd.Parameters.AddWithValue("@email", TxtEmail.Text);

        if (CmbCategory.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@category", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@category", CmbCategory.SelectedItem.Text);
        }
        if (CmbGender.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@mf", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@mf", CmbGender.SelectedItem.Text);
        }

        if (CmbMaritalStatus.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@maritalstatus", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@maritalstatus", CmbMaritalStatus.SelectedItem.Text);
        }
        if (CmbReligion.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@religion", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@religion", CmbReligion.SelectedItem.Text);
        }
        cmd.Parameters.AddWithValue("@addressp", TxtAddPermanent.Text);
        cmd.Parameters.AddWithValue("@townorcityp", TxtTownP.Text);

        if (CmbStateP.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@statep", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@statep", CmbStateP.SelectedItem.Text);
        }

        if (CmbDistrictP.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@districtp", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@districtp", CmbDistrictP.SelectedItem.Text);
        }

        cmd.Parameters.AddWithValue("@thanap", TxtThanaP.Text);
        cmd.Parameters.AddWithValue("@addressc", TxtAddC.Text);
        cmd.Parameters.AddWithValue("@townorcityc", TxtTownC.Text);

        if (CmbStateC.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@statec", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@statec", CmbStateC.SelectedItem.Text);
        }

        if (CmbDistrictC.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@districtc", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@districtc", CmbDistrictC.SelectedItem.Text);
        }

        cmd.Parameters.AddWithValue("@thanac", TxtThanaC.Text);
        cmd.Parameters.AddWithValue("@panno", TxtPanNumber.Text);
        cmd.Parameters.AddWithValue("@SubDepartment", ddlDepartment.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@SubDesignation", ddlDesignation.SelectedItem.Value);
        
        if (txtLeftDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@DOL", DateTime.ParseExact(txtLeftDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        }
        cmd.Parameters.AddWithValue("@firstname", TxtFirstName.Text);
        cmd.Parameters.AddWithValue("@middlename", TxtMiddleName.Text);
        cmd.Parameters.AddWithValue("@lastname", TxtLastName.Text);
        if (ddlSalutation.SelectedItem.Value == "0")
        {
            cmd.Parameters.AddWithValue("@Salutation", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@Salutation", ddlSalutation.SelectedItem.Text);
        }
        cmd.Parameters.AddWithValue("@spousename", Txtspousename.Text);
        cmd.Parameters.AddWithValue("@Username", Session["userName"]);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();

        UpdatePhoto();
        UpdateBookData();
        UpdatePaperData();
        Session["ECode"] = null;
        Session["documentcontent"] = null;
        Session["Filename"] = null;
        Session["fileexten"] = null;
        Session["contenttype"] = null;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Updated successfully','success');$('.modal-backdrop').remove();", true);
        Session["Updated"] = 1;
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

    protected void btnResumeUpload_Click(object sender, EventArgs e)
    {
        if (!fuResume.HasFile)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No File Selected');", true);
            //dlglbl.Text = "No File Selected";

            return;
        }
        else
        {
            FileInfo fi = new FileInfo(fuResume.FileName);
            Filename = fi.Name;
            fileexten = fi.Extension;
            contenttype = fuResume.PostedFile.ContentType;

            if (fileexten.ToUpper() == ".PDF" || fileexten.ToUpper() == ".DOC" || fileexten.ToUpper() == ".DOCX")
            {
                string fn = "";
                string Path = "";
                try
                {
                    string subPath = "../../Files/EmployeeResume/"; // your code goes here
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                    fn = "EmployeeResume_" + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + fi.Extension;
                    string SaveLocation = Server.MapPath("../../Files/EmployeeResume/") + fn;
                    fuResume.PostedFile.SaveAs(SaveLocation);
                    Path = "../../Files/EmployeeResume/" + fn;
                }
                catch
                {
                    string subPath = "../Files/EmployeeResume/"; // your code goes here
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                    fn = "EmployeeResume_" + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + fi.Extension;
                    string SaveLocation = Server.MapPath("../Files/EmployeeResume/") + fn;
                    fuResume.PostedFile.SaveAs(SaveLocation);
                    Path = "../Files/EmployeeResume/" + fn;
                }

                ViewState["ResumeFile1"] = fn;
                ViewState["OriginalFile1"] = Filename;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select valid image file');", true);
                return;
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.FileName == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No File Selected');", true);
            //dlglbl.Text = "No File Selected";
            return;
        }
        else
        {
            FileInfo fi = new FileInfo(FileUpload1.FileName);
            string fn = "";
            try
            {
                string subPath = "../../Files/EmployeeProfile/"; // your code goes here
                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                fn = "EmployeeProfile_" + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + fi.Extension;
                string SaveLocation = Server.MapPath("../../Files/EmployeeProfile/") + fn;
                FileUpload1.PostedFile.SaveAs(SaveLocation);
            }
            catch
            {
                string subPath = "../Files/EmployeeProfile/"; // your code goes here
                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                fn = "EmployeeProfile_" + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + fi.Extension;
                string SaveLocation = Server.MapPath("../Files/EmployeeProfile/") + fn;
                FileUpload1.PostedFile.SaveAs(SaveLocation);
            }

            ViewState["ProfileImagePath"] = fn;
            documentcontent = FileUpload1.FileBytes;
            Filename = fi.Name;
            fileexten = fi.Extension;
            contenttype = FileUpload1.PostedFile.ContentType;
            Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(documentcontent, 0, documentcontent.Length);

            Session["documentcontent"] = documentcontent;
            Session["Filename"] = Filename;
            Session["fileexten"] = fileexten;
            Session["contenttype"] = contenttype;
        }
    }

    protected void ddlDepartmentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();
    }

    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDesignation();
    }

    protected void btnSaveEmpDetails_Click(object sender, EventArgs e)
    {
        if (ddlSalutation.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Salutation');$('.modal-backdrop').remove();", true);
            return;
        }
        if (TxtFirstName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter First Name');$('.modal-backdrop').remove", true);
            return;
        }
        //if (TxtMiddleName.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Middle Name');$('.modal-backdrop').remove", true);
        //    return;
        //}
        if (TxtLastName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Last Name');$('.modal-backdrop').remove", true);
            return;
        }
        if (DTPDOB.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Date of birth');$('.modal-backdrop').remove", true);
            return;
        }
        if (CmbGender.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Gender');$('.modal-backdrop').remove", true);
            return;
        }
        //if (CmbBloodGroup.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Blood Group');$('.modal-backdrop').remove", true);
        //    return;
        //}
        if (CmbCategory.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Category');$('.modal-backdrop').remove", true);
            return;
        }
        if (CmbReligion.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Religion');$('.modal-backdrop').remove", true);
            return;
        }
        //if (CmbMaritalStatus.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Marital Status');$('.modal-backdrop').remove", true);
        //    return;
        //}
        if (TxtMobileEmployee.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Mobile Number');$('.modal-backdrop').remove", true);
            return;
        }
        //if (TxtEmail.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Email');$('.modal-backdrop').remove", true);
        //    return;
        //}
        if (TxtFatherName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Father Name');$('.modal-backdrop').remove", true);
            return;
        }
        //if (TxtMotherName.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Mother Name');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (Txtspousename.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Spouse Name');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (TxtWa.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please select Anniversary Date');$('.modal-backdrop').remove", true);
        //    return;
        //}

        if (FileUpload1.HasFile)
        {
            FileInfo fi = new FileInfo(FileUpload1.FileName);
            string fn = "";
            try
            {
                string subPath = "../../Files/EmployeeProfile/"; // your code goes here
                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                fn = "EmployeeProfile_" + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + fi.Extension;
                string SaveLocation = Server.MapPath("../../Files/EmployeeProfile/") + fn;
                FileUpload1.PostedFile.SaveAs(SaveLocation);
            }
            catch
            {
                string subPath = "../Files/EmployeeProfile/"; // your code goes here
                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                fn = "EmployeeProfile_" + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + fi.Extension;
                string SaveLocation = Server.MapPath("../Files/EmployeeProfile/") + fn;
                FileUpload1.PostedFile.SaveAs(SaveLocation);
            }

            ViewState["ProfileImagePath"] = fn;
            documentcontent = FileUpload1.FileBytes;
            Filename = fi.Name;
            fileexten = fi.Extension;
            contenttype = FileUpload1.PostedFile.ContentType;
            Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(documentcontent, 0, documentcontent.Length);

            Session["documentcontent"] = documentcontent;
            Session["Filename"] = Filename;
            Session["fileexten"] = fileexten;
            Session["contenttype"] = contenttype;
        }

        if (fuResume.HasFile)
        {
            FileInfo fi = new FileInfo(fuResume.FileName);
            Filename = fi.Name;
            fileexten = fi.Extension;
            contenttype = fuResume.PostedFile.ContentType;

            if (fileexten.ToUpper() == ".PDF" || fileexten.ToUpper() == ".DOC" || fileexten.ToUpper() == ".DOCX")
            {
                string fn = "";
                string Path = "";
                try
                {
                    string subPath = "../../Files/EmployeeResume/"; // your code goes here
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                    fn = "EmployeeResume_" + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + fi.Extension;
                    string SaveLocation = Server.MapPath("../../Files/EmployeeResume/") + fn;
                    fuResume.PostedFile.SaveAs(SaveLocation);
                    Path = "../../Files/EmployeeResume/" + fn;
                }
                catch
                {
                    string subPath = "../Files/EmployeeResume/"; // your code goes here
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                    fn = "EmployeeResume_" + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + fi.Extension;
                    string SaveLocation = Server.MapPath("../Files/EmployeeResume/") + fn;
                    fuResume.PostedFile.SaveAs(SaveLocation);
                    Path = "../Files/EmployeeResume/" + fn;
                }

                ViewState["ResumeFile1"] = fn;
                ViewState["OriginalFile1"] = Filename;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select valid image file');", true);
                return;
            }
        }
        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@Salutation", ddlSalutation.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@firstname", TxtFirstName.Text);
        cmd.Parameters.AddWithValue("@middlename", TxtMiddleName.Text);
        cmd.Parameters.AddWithValue("@lastname", TxtLastName.Text);
        cmd.Parameters.AddWithValue("@dob", DateTime.ParseExact(DTPDOB.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@mf", CmbGender.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@bloodgroup", CmbBloodGroup.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@category", CmbCategory.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@religion", CmbReligion.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@maritalstatus", CmbMaritalStatus.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@contact", TxtMobileEmployee.Text);
        cmd.Parameters.AddWithValue("@email", TxtEmail.Text);
        cmd.Parameters.AddWithValue("@fathername", TxtFatherName.Text);
        cmd.Parameters.AddWithValue("@mothername", TxtMotherName.Text);
        cmd.Parameters.AddWithValue("@spousename", Txtspousename.Text);
        cmd.Parameters.AddWithValue("@ResumeFileName", ViewState["ResumeFile1"]==null?"": ViewState["ResumeFile1"].ToString());
        cmd.Parameters.AddWithValue("@OriginalFileName", ViewState["OriginalFile1"] == null ? "" : ViewState["OriginalFile1"].ToString());
        cmd.Parameters.AddWithValue("@Photo", Session["Filename"] == null ? "" : Session["Filename"].ToString());
        cmd.Parameters.AddWithValue("@FilePath", ViewState["ProfileImagePath"] == null ? "" : ViewState["ProfileImagePath"].ToString());
        if (TxtWa.Text != "")
        {
            cmd.Parameters.AddWithValue("@DOA", DateTime.ParseExact(TxtWa.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        }
        else
        {
            cmd.Parameters.AddWithValue("@DOA", null);
        }

        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
        parm.Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(parm);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        int result = Convert.ToInt32(parm.Value);
        cmd.Dispose();
        conn.Close();

        ViewState["ID"] = result;

        if (result == -1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Employee Already exist');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Employee Added successfully','success');$('.modal-backdrop').remove();", true);
        }
    }

    protected void btnSaveAddress_Click(object sender, EventArgs e)
    {
        if (TxtAddPermanent.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Permanent Address');$('.modal-backdrop').remove", true);
            return;
        }
        if (CmbStateP.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Permanent State');$('.modal-backdrop').remove", true);
            return;
        }
        if (CmbDistrictP.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Permanent District');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtTownP.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Permanent Town');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtThanaP.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Permanent Thana');$('.modal-backdrop').remove", true);
            return;
        }

        if (TxtAddC.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Corresponding Address');$('.modal-backdrop').remove", true);
            return;
        }
        if (CmbStateC.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Corresponding State');$('.modal-backdrop').remove", true);
            return;
        }
        if (CmbDistrictC.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Corresponding District');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtTownC.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Corresponding Town');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtThanaC.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Corresponding Thana');$('.modal-backdrop').remove", true);
            return;
        }


        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@addressp", TxtAddPermanent.Text);
        cmd.Parameters.AddWithValue("@statep", CmbStateP.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@districtp", CmbDistrictP.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@townorcityp", TxtTownP.Text);
        cmd.Parameters.AddWithValue("@thanap", TxtThanaP.Text);
        cmd.Parameters.AddWithValue("@addressc", TxtAddC.Text);
        cmd.Parameters.AddWithValue("@statec", CmbStateC.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@districtc", CmbDistrictC.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@townorcityc", TxtTownC.Text);
        cmd.Parameters.AddWithValue("@thanac", TxtThanaC.Text);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["ID"].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());

        //For Audit Trail

        //cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
        //cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
        //cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        //cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
        //cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Address Added successfully','success');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Address Already exist');$('.modal-backdrop').remove();", true);
            return;
        }
    }

    protected void btnSaveEmployeeJoiningDetails_Click(object sender, EventArgs e)
    {
        //if (ddlBranch.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Branch');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (ddlDesignation.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Designation Type');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (ddlDepartmentType.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Departement Type');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (ddlDepartment.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Department');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (ddlSubDesignation.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Designation');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (ddlEmployeeType.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Employee Type');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (ddlAdministrator.SelectedItem.Value == "-1")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Administrator');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (txtEmployeeID.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter EmployeeID');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (TxtMachineID.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Machine ID');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (txtJoiningDate.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Joining Date');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (txtLeftDate.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Left Date');$('.modal-backdrop').remove", true);
        //    return;
        //}
        

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@DesignationTypeCode", ddlDesignation.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@DesignationCode", ddlSubDesignation.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@DepartmentTypeCode", ddlDepartmentType.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@DepartmentCode", ddlDepartment.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@EmployeeType", ddlEmployeeType.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@datefrom", DateTime.ParseExact(txtJoiningDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@dateto", DateTime.ParseExact(txtLeftDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@Administrator", ddlAdministrator.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@EmployeeID", txtEmployeeID.Text);
        cmd.Parameters.AddWithValue("@MachineID", TxtMachineID.Text);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["ID"].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Employee Joining Details Added successfully','success');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Employee Joining Details Already exist');$('.modal-backdrop').remove();", true);
            return;
        }
    }

    protected void btnSaveAccountDetails_Click(object sender, EventArgs e)
    {
        if (TxtPF.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter PF Account Number');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtPanNumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter PAN Number');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtBank.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Bank Name');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtAccountNumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Account Number');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtAadhar.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Adhar Number');$('.modal-backdrop').remove", true);
            return;
        }
        if (txtIFSCCOde.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter IFSC Code');$('.modal-backdrop').remove", true);
            return;
        }


        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@pfaccountno", TxtPF.Text);
        cmd.Parameters.AddWithValue("@panno", TxtPanNumber.Text);
        cmd.Parameters.AddWithValue("@bankname", TxtBank.Text);
        cmd.Parameters.AddWithValue("@bankacno", TxtAccountNumber.Text);
        cmd.Parameters.AddWithValue("@AdharNo", TxtAadhar.Text);
        cmd.Parameters.AddWithValue("@IFSC_Code", txtIFSCCOde.Text);
        cmd.Parameters.AddWithValue("@E_Code", ViewState["ID"].ToString());
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());

        //For Audit Trail

        //cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
        //cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
        //cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        //cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
        //cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Account Details Added successfully','success');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Account Details Already exist');$('.modal-backdrop').remove();", true);
            return;
        }
    }

    protected void btnSaveBooksPublished_Click(object sender, EventArgs e)
    {
        UpdateBookData();
    }

    protected void btnSavePaperPublished_Click(object sender, EventArgs e)
    {
        UpdatePaperData();
    }

    protected void btnUpdateEmpDetails_Click(object sender, EventArgs e)
    {
        if (ddlSalutation.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Salutation');$('.modal-backdrop').remove();", true);
            return;
        }
        if (TxtFirstName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter First Name');$('.modal-backdrop').remove", true);
            return;
        }
        //if (TxtMiddleName.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Middle Name');$('.modal-backdrop').remove", true);
        //    return;
        //}
        if (TxtLastName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Last Name');$('.modal-backdrop').remove", true);
            return;
        }
        if (DTPDOB.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Date of birth');$('.modal-backdrop').remove", true);
            return;
        }
        if (CmbGender.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Gender');$('.modal-backdrop').remove", true);
            return;
        }
        //if (CmbBloodGroup.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Blood Group');$('.modal-backdrop').remove", true);
        //    return;
        //}
        if (CmbCategory.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Category');$('.modal-backdrop').remove", true);
            return;
        }
        if (CmbReligion.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Religion');$('.modal-backdrop').remove", true);
            return;
        }
        //if (CmbMaritalStatus.SelectedItem.Value == "0")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Marital Status');$('.modal-backdrop').remove", true);
        //    return;
        //}
        if (TxtMobileEmployee.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Mobile Number');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtEmail.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Email');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtFatherName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Father Name');$('.modal-backdrop').remove", true);
            return;
        }
        //if (TxtMotherName.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Mother Name');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (Txtspousename.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Spouse Name');$('.modal-backdrop').remove", true);
        //    return;
        //}
        //if (TxtWa.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please select Anniversary Date');$('.modal-backdrop').remove", true);
        //    return;
        //}

        if (FileUpload1.HasFile)
        {
            FileInfo fi = new FileInfo(FileUpload1.FileName);
            string fn = "";
            try
            {
                string subPath = "../../Files/EmployeeProfile/"; // your code goes here
                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                fn = "EmployeeProfile_" + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + fi.Extension;
                string SaveLocation = Server.MapPath("../../Files/EmployeeProfile/") + fn;
                FileUpload1.PostedFile.SaveAs(SaveLocation);
            }
            catch
            {
                string subPath = "../Files/EmployeeProfile/"; // your code goes here
                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                fn = "EmployeeProfile_" + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + fi.Extension;
                string SaveLocation = Server.MapPath("../Files/EmployeeProfile/") + fn;
                FileUpload1.PostedFile.SaveAs(SaveLocation);
            }

            ViewState["ProfileImagePath"] = fn;
            documentcontent = FileUpload1.FileBytes;
            Filename = fi.Name;
            fileexten = fi.Extension;
            contenttype = FileUpload1.PostedFile.ContentType;
            Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(documentcontent, 0, documentcontent.Length);

            Session["documentcontent"] = documentcontent;
            Session["Filename"] = Filename;
            Session["fileexten"] = fileexten;
            Session["contenttype"] = contenttype;
        }

        if (fuResume.HasFile)
        {
            FileInfo fi = new FileInfo(fuResume.FileName);
            Filename = fi.Name;
            fileexten = fi.Extension;
            contenttype = fuResume.PostedFile.ContentType;

            if (fileexten.ToUpper() == ".PDF" || fileexten.ToUpper() == ".DOC" || fileexten.ToUpper() == ".DOCX")
            {
                string fn = "";
                string Path = "";
                try
                {
                    string subPath = "../../Files/EmployeeResume/"; // your code goes here
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                    fn = "EmployeeResume_" + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + fi.Extension;
                    string SaveLocation = Server.MapPath("../../Files/EmployeeResume/") + fn;
                    fuResume.PostedFile.SaveAs(SaveLocation);
                    Path = "../../Files/EmployeeResume/" + fn;
                }
                catch
                {
                    string subPath = "../Files/EmployeeResume/"; // your code goes here
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                    fn = "EmployeeResume_" + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + fi.Extension;
                    string SaveLocation = Server.MapPath("../Files/EmployeeResume/") + fn;
                    fuResume.PostedFile.SaveAs(SaveLocation);
                    Path = "../Files/EmployeeResume/" + fn;
                }

                ViewState["ResumeFile1"] = fn;
                ViewState["OriginalFile1"] = Filename;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select valid image file');", true);
                return;
            }
        }

        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 23);
        cmd.Parameters.AddWithValue("@Salutation", ddlSalutation.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@firstname", TxtFirstName.Text);
        cmd.Parameters.AddWithValue("@middlename", TxtMiddleName.Text);
        cmd.Parameters.AddWithValue("@lastname", TxtLastName.Text);
        cmd.Parameters.AddWithValue("@dob", DateTime.ParseExact(DTPDOB.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@mf", CmbGender.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@bloodgroup", CmbBloodGroup.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@category", CmbCategory.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@religion", CmbReligion.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@maritalstatus", CmbMaritalStatus.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@contact", TxtMobileEmployee.Text);
        cmd.Parameters.AddWithValue("@email", TxtEmail.Text);
        cmd.Parameters.AddWithValue("@fathername", TxtFatherName.Text);
        cmd.Parameters.AddWithValue("@mothername", TxtMotherName.Text);
        cmd.Parameters.AddWithValue("@spousename", Txtspousename.Text);
        if(FileUpload1.HasFile)
        {
            cmd.Parameters.AddWithValue("@Photo", Session["Filename"].ToString());
            cmd.Parameters.AddWithValue("@FilePath", ViewState["ProfileImagePath"].ToString());
        }
        else
        {
            cmd.Parameters.AddWithValue("@Photo", null);
            cmd.Parameters.AddWithValue("@FilePath", null);
        }
        if (fuResume.HasFile)
        {
            cmd.Parameters.AddWithValue("@ResumeFileName", ViewState["ResumeFile1"].ToString());
            cmd.Parameters.AddWithValue("@OriginalFileName", ViewState["OriginalFile1"].ToString());
        }
        else
        {
            cmd.Parameters.AddWithValue("@ResumeFileName", null);
            cmd.Parameters.AddWithValue("@OriginalFileName", null);
        }
        if(TxtWa.Text!="")
        {
            cmd.Parameters.AddWithValue("@DOA", DateTime.ParseExact(TxtWa.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        }
        else
        {
            cmd.Parameters.AddWithValue("@DOA", null);
        }
        cmd.Parameters.AddWithValue("@E_Code", HdnE_Code.Value);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Employee can not be updated');$('.modal-backdrop').remove();", true);
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Employee Updated successfully','success');$('.modal-backdrop').remove();", true);
        }
    }

    protected void btnUpdateAddress_Click(object sender, EventArgs e)
    {
        if (TxtAddPermanent.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Permanent Address');$('.modal-backdrop').remove", true);
            return;
        }
        if (CmbStateP.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Permanent State');$('.modal-backdrop').remove", true);
            return;
        }
        if (CmbDistrictP.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Permanent District');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtTownP.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Permanent Town');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtThanaP.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Permanent Thana');$('.modal-backdrop').remove", true);
            return;
        }

        if (TxtAddC.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Corresponding Address');$('.modal-backdrop').remove", true);
            return;
        }
        if (CmbStateC.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Corresponding State');$('.modal-backdrop').remove", true);
            return;
        }
        if (CmbDistrictC.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Corresponding District');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtTownC.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Corresponding Town');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtThanaC.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Corresponding Thana');$('.modal-backdrop').remove", true);
            return;
        }


        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 2);
        cmd.Parameters.AddWithValue("@addressp", TxtAddPermanent.Text);
        cmd.Parameters.AddWithValue("@statep", CmbStateP.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@districtp", CmbDistrictP.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@townorcityp", TxtTownP.Text);
        cmd.Parameters.AddWithValue("@thanap", TxtThanaP.Text);
        cmd.Parameters.AddWithValue("@addressc", TxtAddC.Text);
        cmd.Parameters.AddWithValue("@statec", CmbStateC.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@districtc", CmbDistrictC.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@townorcityc", TxtTownC.Text);
        cmd.Parameters.AddWithValue("@thanac", TxtThanaC.Text);
        cmd.Parameters.AddWithValue("@E_Code", HdnE_Code.Value);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Address Updated successfully','success');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Address can not be updated');$('.modal-backdrop').remove();", true);
            return;
        }
    }

    protected void btnUpdateEmployeeJoiningDetails_Click(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Branch');$('.modal-backdrop').remove", true);
            return;
        }
        if (ddlDesignation.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Designation Type');$('.modal-backdrop').remove", true);
            return;
        }
        if (ddlDepartmentType.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Departement Type');$('.modal-backdrop').remove", true);
            return;
        }
        if (ddlDepartment.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Department');$('.modal-backdrop').remove", true);
            return;
        }
        if (ddlSubDesignation.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Designation');$('.modal-backdrop').remove", true);
            return;
        }
        if (ddlEmployeeType.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Employee Type');$('.modal-backdrop').remove", true);
            return;
        }
        if (ddlAdministrator.SelectedItem.Value == "-1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Administrator');$('.modal-backdrop').remove", true);
            return;
        }
        if (txtEmployeeID.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter EmployeeID');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtMachineID.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Machine ID');$('.modal-backdrop').remove", true);
            return;
        }
        if (txtJoiningDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Joining Date');$('.modal-backdrop').remove", true);
            return;
        }
        //if (txtLeftDate.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Left Date');$('.modal-backdrop').remove", true);
        //    return;
        //}


        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 3);
        cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@DesignationTypeCode", ddlDesignation.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@DesignationCode", ddlSubDesignation.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@DepartmentTypeCode", ddlDepartmentType.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@DepartmentCode", ddlDepartment.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@EmployeeType", ddlEmployeeType.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@datefrom", DateTime.ParseExact(txtJoiningDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        //cmd.Parameters.AddWithValue("@dateto", DateTime.ParseExact(txtLeftDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@Administrator", ddlAdministrator.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@EmployeeID", txtEmployeeID.Text);
        cmd.Parameters.AddWithValue("@MachineID", TxtMachineID.Text);
        cmd.Parameters.AddWithValue("@E_Code", HdnE_Code.Value);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Employee Joining Details Updated successfully','success');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Employee Joining Details can not be updated');$('.modal-backdrop').remove();", true);
            return;
        }
    }

    protected void btnUpdateAccountDetails_Click(object sender, EventArgs e)
    {
        if (TxtPF.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter PF Account Number');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtPanNumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter PAN Number');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtBank.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Bank Name');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtAccountNumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Account Number');$('.modal-backdrop').remove", true);
            return;
        }
        if (TxtAadhar.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Adhar Number');$('.modal-backdrop').remove", true);
            return;
        }
        if (txtIFSCCOde.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter IFSC Code');$('.modal-backdrop').remove", true);
            return;
        }


        ViewState["OpenFlag"] = 0;
        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 4);
        cmd.Parameters.AddWithValue("@pfaccountno", TxtPF.Text);
        cmd.Parameters.AddWithValue("@panno", TxtPanNumber.Text);
        cmd.Parameters.AddWithValue("@bankname", TxtBank.Text);
        cmd.Parameters.AddWithValue("@bankacno", TxtAccountNumber.Text);
        cmd.Parameters.AddWithValue("@AdharNo", TxtAadhar.Text);
        cmd.Parameters.AddWithValue("@IFSC_Code", txtIFSCCOde.Text);
        cmd.Parameters.AddWithValue("@E_Code", HdnE_Code.Value);
        cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());

        //For Audit Trail

        //cmd.Parameters.AddWithValue("@IPAddress", UserHistory.GetIPAddress());
        //cmd.Parameters.AddWithValue("@URL", Request.ServerVariables[42].ToString());
        //cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        //cmd.Parameters.AddWithValue("@UserName", Session["userName"].ToString());
        //cmd.Parameters.AddWithValue("@UserType", Session["Type"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Account Details Updated successfully','success');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Account Details can not be Updated');$('.modal-backdrop').remove();", true);
            return;
        }
    }



    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        if (TxtOrganisation.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter The Organisation');$('.modal-backdrop').remove();", true);
            return;
        }
        if (DateFrm.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select From Date');$('.modal-backdrop').remove();", true);
            return;
        }
        if (DateTo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select To Date');$('.modal-backdrop').remove();", true);
            return;
        }
        if (DateTime.ParseExact(DateFrm.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(DateTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Valid Date Range');$('.modal-backdrop').remove();", true);
            return;
        }

        if (Txtaddressworking.Text == "")
        {
            Txtaddressworking.Text = "-";
        }
        if (TxtDesignation.Text == "")
        {
            TxtDesignation.Text = "-";
        }

        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.Parameters.AddWithValue("@organisation", TxtOrganisation.Text.ToUpper());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Already Entered');$('.modal-backdrop').remove();", true);
            return;
        }

        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 7);
        cmd.Parameters.AddWithValue("@E_Code", HdnE_Code.Value);
        cmd.Parameters.AddWithValue("@organisation", TxtOrganisation.Text);
        cmd.Parameters.AddWithValue("@address", Txtaddressworking.Text);
        cmd.Parameters.AddWithValue("@desg", TxtDesignation.Text);
        cmd.Parameters.AddWithValue("@datefrom", DateTime.ParseExact(DateFrm.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@dateto", DateTime.ParseExact(DateTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();
        DisplayExperience();

        TxtOrganisation.Text = "";
        Txtaddressworking.Text = "";
        TxtDesignation.Text = "";
        DateFrm.Text = "";
        DateTo.Text = "";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Entered successfully','success');$('.modal-backdrop').remove();", true);
    }

    protected void CmdUpdateAcadmic_Click(object sender, EventArgs e)
    {

        if (txtQualification.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Qualification');$('.modal-backdrop').remove();", true);
            return;
        }
        if (CmbYear.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Select Year');$('.modal-backdrop').remove();", true);
            return;
        }

        if (TxtSchool.Text == "")
        {
            TxtSchool.Text = "-";
        }
        if (TxtRollNo.Text == "")
        {
            TxtRollNo.Text = "-";
        }
        if (TxtEnrollNo.Text == "")
        {
            TxtEnrollNo.Text = "-";
        }
        if (TxtMM.Text == "")
        {
            TxtMM.Text = "0";
        }
        if (Txtobt.Text == "")
        {
            Txtobt.Text = "0";
        }
        if (Txtper.Text == "")
        {
            Txtper.Text = "0";
        }

        conn.Open();
        cmd = new SqlCommand("SP_EmployeeMaster", conn);
        cmd.Parameters.AddWithValue("@calltype", 5);
        cmd.Parameters.AddWithValue("@E_Code", HdnE_Code.Value);
        cmd.Parameters.AddWithValue("@acadqname", txtQualification.Text);
        cmd.Parameters.AddWithValue("@schoolname", TxtSchool.Text);
        cmd.Parameters.AddWithValue("@acadbname", txtBoard.Text);
        cmd.Parameters.AddWithValue("@percent", Txtper.Text);
        if (CmbDiv.SelectedItem.Text == "Select Division")
        {
            cmd.Parameters.AddWithValue("@division", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@division", CmbDiv.SelectedItem.Text);
        }

        cmd.Parameters.AddWithValue("@year", CmbYear.SelectedItem.Text);
        if (CmbSubject.SelectedItem.Text == "Select Subjects")
        {
            cmd.Parameters.AddWithValue("@subjects", "-");
        }
        else
        {
            cmd.Parameters.AddWithValue("@subjects", CmbSubject.SelectedItem.Text);
        }
        cmd.Parameters.AddWithValue("@stateid", CmbState.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@rollno", TxtRollNo.Text);
        cmd.Parameters.AddWithValue("@certificateno", TxtCertificateNo.Text);
        cmd.Parameters.AddWithValue("@enrollno", TxtEnrollNo.Text);
        cmd.Parameters.AddWithValue("@MM", TxtMM.Text);
        cmd.Parameters.AddWithValue("@MO", Txtobt.Text);
        cmd.Parameters.AddWithValue("@empCode", Session["e_code"].ToString());
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Academic Details Already Entered');$('.modal-backdrop').remove();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Academic Details Entered successfully','success');$('.modal-backdrop').remove();", true);
            cmd = new SqlCommand("SP_DisplayExperience", conn);
            cmd.Parameters.AddWithValue("@E_Code", HdnE_Code.Value);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();
            Gvexperience.DataSource = dt;
            Gvexperience.DataBind();
        }
    }

    protected void btnUpdateBooksPublished_Click(object sender, EventArgs e)
    {
        UpdateBookData1();
    }

    protected void btnUpdatePaperPublished_Click(object sender, EventArgs e)
    {
        UpdatePaperData1();
    }
}