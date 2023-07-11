using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Mail;
using Newtonsoft.Json;

public partial class Pages_QueryEngineEmployeeNew : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["Flag"] = 0;
        if (!Page.IsPostBack)
        {
            CheckSMSCount();
            //MakeSelectCriteria();
        }
    }

    protected void MakeSelectCriteria()
    {
        cmd = new SqlCommand("SP_RegistrarDeskDashboard", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        //cmd.Parameters.AddWithValue("@Type", "Student");
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        //hdTableData.Value = JsonConvert.SerializeObject(dt);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "getAllValue();", true);
    }
   
    protected void CheckSMSCount()
    {
        //Check SMS Count
        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 12);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 13);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt1 = new DataTable();
        da.Fill(dt1);
        cmd.Dispose();
        da.Dispose();

        if (dt.Rows[0][0].ToString() != "0")
        {
            //btnSendSMS.Visible = true;
            smsbal.Text = "&nbsp;[ SMS Balance : " + dt1.Rows[0][0].ToString() + " ]";
            smsbal.Visible = true;
        }
        else
        {
            //btnSendSMS.Visible = false;
            smsbal.Visible = false;
        }
    }
}