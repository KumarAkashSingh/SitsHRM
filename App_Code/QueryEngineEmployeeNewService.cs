using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Web.Script.Services;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Collections;
using System.Net.Mail;
using System.IO;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for QueryEngineEmployeeNewService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]

public class QueryEngineEmployeeNewService : System.Web.Services.WebService
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;

    public QueryEngineEmployeeNewService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetAllEmployee(string MaxSCode)
    {
        cmd = new SqlCommand("SP_QueryEngineEmployeeNew", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        cmd.Parameters.AddWithValue("@MaxSCode", MaxSCode);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SendMessage(string RegistrationNumbers, string ToWhom, string textmsg)
    {
        string[] AllRegistrationNumbers = RegistrationNumbers.Split(',');
        string Criteria = "";

        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 16);
        cmd.Parameters.AddWithValue("@E_Code", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();

        if (int.Parse(dt.Rows[0][0].ToString()) < AllRegistrationNumbers.Length)
        {
            return JsonConvert.SerializeObject("You Have Exceeded Your SMS Sending Limit");
        }

        string RegistrationNumber = "(";
        foreach (string str in AllRegistrationNumbers)
        {
            RegistrationNumber += ",'" + str + "'";
        }
        if (RegistrationNumber.Substring(0, 1).Contains(","))
        {
            RegistrationNumber = RegistrationNumber.Substring(1, RegistrationNumber.Length - 1);
        }
        RegistrationNumber += ")";
        RegistrationNumber = RegistrationNumber.Replace("(,", "(");

        //Retrieve All mobile numbers
        string Sql = "select Contact from EmployeeMaster where EmployeeCode in " + RegistrationNumber + " and (Contact is not null or Contact!='')";

        da = new SqlDataAdapter(Sql, conn);
        dt = new DataTable();
        da.Fill(dt);

        //Putting all mobile numbers in list
        ArrayList lstmob = new ArrayList();
        int cols = dt.Columns.Count;
        foreach (DataRow row in dt.Rows)
        {
            if (row["Contact"].ToString().Length >= 10)
            {
                lstmob.Add(row["Contact"].ToString());
            }
        }

        string mobile = String.Join(",", lstmob.ToArray());

        SMSSettings();

        if (mobile != "")
        {
            string Message = "Dear \n\n" + textmsg + "\n\nSunder Deep Group of Institutions";
            SendSMS obj = new SendSMS();
            obj.SMSSend(mobile, Message, Session["MSGUSERID"].ToString(),
                Session["MSGPASSWORD"].ToString(), Session["MSGSENDERID"].ToString(),
                Session["UserCode"].ToString(), Session["userid"].ToString(),
                Session["Name"].ToString(), Session["e_code"].ToString());
        }

        return JsonConvert.SerializeObject("SMS Sent Successfully");
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SendMail()
    {

        string[] AllRegistrationNumbers = HttpContext.Current.Request["E_codes"].ToString().Split(',');

        string RegistrationNumber = "(";
        foreach (string str in AllRegistrationNumbers)
        {
            RegistrationNumber += ",'" + str + "'";
        }
        if (RegistrationNumber.Substring(0, 1).Contains(","))
        {
            RegistrationNumber = RegistrationNumber.Substring(1, RegistrationNumber.Length - 1);
        }
        RegistrationNumber += ")";
        RegistrationNumber = RegistrationNumber.Replace("(,", "(");

        //Retrieve All Emails
        string Sql = "select Email from EmployeeMaster where EmployeeCode in " + RegistrationNumber + " and (Email is not null or Email!='')";
        da = new SqlDataAdapter(Sql, conn);
        dt = new DataTable();
        da.Fill(dt);

        //Putting all emails in list
        ArrayList lstmob = new ArrayList();
        int cols = dt.Columns.Count;

        Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
         RegexOptions.CultureInvariant | RegexOptions.Singleline);

        foreach (DataRow row in dt.Rows)
        {
            bool isValidEmail = regex.IsMatch(row["Email"].ToString());
            if (isValidEmail)
            {
                lstmob.Add(row["Email"].ToString());
            }
        }

        string email = String.Join(",", lstmob.ToArray());

        if (email != "")
        {
            cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
            cmd.Parameters.AddWithValue("@calltype", 6);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt1 = new DataTable();
            da.Fill(dt1);
            cmd.Dispose();
            da.Dispose();

            try
            {
                MailMessage Mail = new System.Net.Mail.MailMessage();
                Mail.Subject = HttpContext.Current.Request["Subject"].ToString();
                Mail.To.Add(email);
                Mail.From = new MailAddress(dt1.Rows[0]["Email"].ToString());
                string MBody = null;
                MBody = HttpContext.Current.Request["Body"].ToString();
                Mail.Body = MBody;
                HttpFileCollection SelectedFiles = HttpContext.Current.Request.Files;
                for (int i = 0; i < SelectedFiles.Count; ++i)
                {
                    HttpPostedFile PostedFile = SelectedFiles[i];
                    string fileName = Path.GetFileName(PostedFile.FileName);
                    Mail.Attachments.Add(new Attachment(PostedFile.InputStream, fileName));
                }
                SmtpClient SMTP = new SmtpClient(dt1.Rows[0]["emailhost"].ToString());
                SMTP.EnableSsl = true;
                SMTP.Credentials = new System.Net.NetworkCredential(dt1.Rows[0]["Email"].ToString(), dt1.Rows[0]["Emailpassword"].ToString());
                SMTP.Port = int.Parse(dt1.Rows[0]["emailport"].ToString());
                SMTP.Send(Mail);

                conn.Open();
                cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
                cmd.Parameters.AddWithValue("@calltype", 161);
                cmd.Parameters.AddWithValue("@WUC", Session["UserCode"].ToString());
                cmd.Parameters.AddWithValue("@loginname", Session["userid"].ToString());
                cmd.Parameters.AddWithValue("@employeename", Session["Name"].ToString());
                cmd.Parameters.AddWithValue("@empCode", Session["e_code"].ToString());
                cmd.Parameters.AddWithValue("@computername", System.Net.Dns.GetHostName());
                cmd.Parameters.AddWithValue("@ipaddress", System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString());
                cmd.Parameters.AddWithValue("@sentto", email);
                cmd.Parameters.AddWithValue("@msgbody", HttpContext.Current.Request["Body"].ToString());
                cmd.Parameters.AddWithValue("@subjects", HttpContext.Current.Request["Subject"].ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }
        return JsonConvert.SerializeObject("Email Sent Successfully");
    }

    protected void SMSSettings()
    {
        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 6);
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt1 = new DataTable();
        da.Fill(dt1);
        cmd.Dispose();
        da.Dispose();

        Session["bodysms"] = dt1.Rows[0]["bodysmsstudent"].ToString();
        Session["bodymail"] = dt1.Rows[0]["bodymailstudent"].ToString();
        Session["MSGUSERID"] = dt1.Rows[0]["msgid"].ToString();
        Session["MSGPASSWORD"] = dt1.Rows[0]["msgpassword"].ToString();
        Session["MSGSENDERID"] = dt1.Rows[0]["msgsenderId"].ToString();
    }

}
