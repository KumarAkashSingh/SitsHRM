using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Net;
using System.Web.SessionState;
using System.Reflection;

public partial class Login : System.Web.UI.Page
{
    Dictionary<string, string> Parameters = null;
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;
    DataTable dt2 = null;
    DataTable dt3 = null;
    string[] Colours = { "#ffb752", "#d15b47", "#5c6bc0", "#d6487e", "#9585bf", "#87b87f", "#6fb3e0" };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                var sessionobject = new List<string>();
                foreach (string key in Session.Keys)
                    sessionobject.Add(key);
                foreach (var key in sessionobject)
                    Session.Remove(key);
            }
            catch (Exception ex)
            {

            }
            Form.DefaultButton = btnLogin.UniqueID;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtLoginID.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Username');", true);
            return;
        }
        else if (txtPassword.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Please Enter Password');", true);
            return;
        }
        if (txtLoginID.Text != "" && txtPassword.Text != "")
        {
            cmd = new SqlCommand("SP_Web_Login", conn);
            cmd.Parameters.AddWithValue("@calltype", 1);
            cmd.Parameters.AddWithValue("@loginName", txtLoginID.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();

            if (dt.Rows.Count > 0)
            {
                //string ResponseURL = "../Pages/Default.aspx";
                Session["userName"] = dt.Rows[0]["loginName"];
                Session["userpwd"] = dt.Rows[0]["Password"];
                Session["LoginType"] = dt.Rows[0]["LoginType"];
                Session["e_code"] = dt.Rows[0]["e_code"];
                Session["UserCode"] = dt.Rows[0]["WebUserCode"];
                Session["flag"] = 0;
                Session["AlertShown"] = null;

                cmd = new SqlCommand("SP_Web_Login", conn);
                cmd.Parameters.AddWithValue("@calltype", 2);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt1 = new DataTable();
                da.Fill(dt1);
                cmd.Dispose();

                Session["LeaveSessionFrom"] = DateTime.Parse(dt1.Rows[0]["LeavesessionFrom"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                Session["LeaveSessionTo"] = DateTime.Parse(dt1.Rows[0]["LeavesessionTo"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                Session["FYFROM"] = DateTime.Parse(dt1.Rows[0]["FYFROM"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                Session["FYTO"] = DateTime.Parse(dt1.Rows[0]["FYTO"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                Session["SetTimeTableToDate"] = DateTime.Parse(dt1.Rows[0]["SetTimeTableToDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                Session["AttendanceType"] = dt1.Rows[0]["AttendanceType"].ToString();
                Session["CN"] = dt1.Rows[0]["companyname"].ToString();
                Session["En_code"] = 0;
                Session["AddFlag"] = true;
                Session["FirstLogin"] = "-1";
                Session["SendSMSEngagement"] = dt1.Rows[0]["SendSMSForEngagement"].ToString();
                Session["EngagementResponse"] = dt1.Rows[0]["EngagementResponse"].ToString();

                Response.Redirect("Pages/Default.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$.notify('Invalid Credentials');", true);
            }
        }
    }
}