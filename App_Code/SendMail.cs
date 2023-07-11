using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using System.Text;
/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendMail
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt1 = null;
	public SendMail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //public bool SendMails(DataTable dt)
    public bool SendMails(string Subject,string Body,string Email)
    {
        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 72);
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
            Mail.Subject = Subject;
            Mail.Bcc.Add(Email);
            Mail.From = new MailAddress(dt1.Rows[0]["Email"].ToString());
            string MBody = null;
            MBody = Body;
            Mail.Body = MBody;
            Mail.IsBodyHtml = true;
            SmtpClient SMTP = new SmtpClient(dt1.Rows[0]["emailhost"].ToString());

            SMTP.EnableSsl = true;

            SMTP.Credentials = new System.Net.NetworkCredential(dt1.Rows[0]["Email"].ToString(), dt1.Rows[0]["Emailpassword"].ToString());
            SMTP.Port = int.Parse(dt1.Rows[0]["emailport"].ToString());
            SMTP.Send(Mail);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public bool SendEmailer(string Email, string UserCode, string userid, string Name, string empCode,string LeadID)
    {
        string EmailFrom = "noreply@sunderdeep.ac.in";
        string Password = "ashishtayal";
        string host = "smtp.gmail.com";
        string port = "587";
        string Subject = "Sunderdeep Group of Institutions";
        try
        {
            MailMessage Mail = new System.Net.Mail.MailMessage();
            Mail.Subject = Subject;
            Mail.Bcc.Add(Email);
            Mail.From = new MailAddress(EmailFrom);
            string MBody = null;
            string Path = System.Web.Hosting.HostingEnvironment.MapPath("~/Files/PushEmailer/Emailer/Emailer.txt");
            //string Path = HttpContext.Current.Server.MapPath("~/Files/PushEmailer/Emailer/Emailer.txt");
            var fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                MBody = streamReader.ReadToEnd();
            }
            //MBody = Body;
            Mail.Body = MBody;
            Mail.IsBodyHtml = true;
       
            SmtpClient SMTP = new SmtpClient(host);

            SMTP.EnableSsl = true;

            SMTP.Credentials = new System.Net.NetworkCredential(EmailFrom, Password);
            SMTP.Port = int.Parse(port);
            SMTP.Send(Mail);
            
            conn.Open();
            cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
            cmd.Parameters.AddWithValue("@calltype", 161);
            cmd.Parameters.AddWithValue("@WUC", UserCode);
            cmd.Parameters.AddWithValue("@loginname", userid);
            cmd.Parameters.AddWithValue("@employeename", Name);
            cmd.Parameters.AddWithValue("@empCode", empCode);
            cmd.Parameters.AddWithValue("@computername", System.Net.Dns.GetHostName());
            cmd.Parameters.AddWithValue("@ipaddress", System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString());
            cmd.Parameters.AddWithValue("@sentto", Email);
            cmd.Parameters.AddWithValue("@msgbody", "Emailer from File to "+ LeadID);
            cmd.Parameters.AddWithValue("@subjects", Subject);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool ScheduleEmailer(string Email, string UserCode, string userid, string Name, string empCode, string LeadID,string Course)
    {
        //Email = "ashishtayalk@gmail.com";
        string EmailerPath = "";
        string Sql = "Select EmailerPath from PushEmailers where Course='" + Course+"' and cast(PushDate as date)='"+DateTime.Now.Date+"'";
        da = new SqlDataAdapter(Sql, conn);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            EmailerPath = dt.Rows[0]["EmailerPath"].ToString();
        }
        

        string EmailFrom = "noreply@sunderdeep.ac.in";
        string Password = "ashishtayal";
        string host = "smtp.gmail.com";
        string port = "587";
        string Subject = "Sunderdeep Group of Institutions";
        try
        {
            MailMessage Mail = new System.Net.Mail.MailMessage();
            Mail.Subject = Subject;
            Mail.Bcc.Add(Email);
            Mail.From = new MailAddress(EmailFrom);
            string MBody = null;
            string Path;
            //string Path = HttpContext.Current.Server.MapPath("~/Files/PushEmailer/Emailer/Emailer.txt");
            if (EmailerPath == "")
            {
                Path = System.Web.Hosting.HostingEnvironment.MapPath("~/Files/PushEmailer/Emailer/Emailer.txt");
            }
            else
            {
                Path = System.Web.Hosting.HostingEnvironment.MapPath("~/" + EmailerPath);
            }

            var fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                MBody = streamReader.ReadToEnd();
            }
            //MBody = Body;
            Mail.Body = MBody;
            Mail.IsBodyHtml = true;

            SmtpClient SMTP = new SmtpClient(host);

            SMTP.EnableSsl = true;

            SMTP.Credentials = new System.Net.NetworkCredential(EmailFrom, Password);
            SMTP.Port = int.Parse(port);
            SMTP.Send(Mail);

            conn.Open();
            cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
            cmd.Parameters.AddWithValue("@calltype", 161);
            cmd.Parameters.AddWithValue("@WUC", UserCode);
            cmd.Parameters.AddWithValue("@loginname", userid);
            cmd.Parameters.AddWithValue("@employeename", Name);
            cmd.Parameters.AddWithValue("@empCode", empCode);
            cmd.Parameters.AddWithValue("@computername", System.Net.Dns.GetHostName());
            cmd.Parameters.AddWithValue("@ipaddress", System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString());
            cmd.Parameters.AddWithValue("@sentto", Email);
            cmd.Parameters.AddWithValue("@msgbody", "Emailer from File to " + LeadID);
            cmd.Parameters.AddWithValue("@subjects", Subject);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SendGeneralMails(string Subject, string Body, string Email, string UserCode, string userid, string Name, string empCode)
    {
        cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
        cmd.Parameters.AddWithValue("@calltype", 72);
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
            Mail.Subject = Subject;
            Mail.Bcc.Add(Email);
            Mail.From = new MailAddress(dt1.Rows[0]["Email"].ToString());
            string MBody = null;
            MBody = Body;
            Mail.Body = MBody;
            Mail.IsBodyHtml = true;
            SmtpClient SMTP = new SmtpClient(dt1.Rows[0]["emailhost"].ToString());

            SMTP.EnableSsl = true;

            SMTP.Credentials = new System.Net.NetworkCredential(dt1.Rows[0]["Email"].ToString(), dt1.Rows[0]["Emailpassword"].ToString());
            SMTP.Port = int.Parse(dt1.Rows[0]["emailport"].ToString());
            SMTP.Send(Mail);

            conn.Open();
            cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
            cmd.Parameters.AddWithValue("@calltype", 161);
            cmd.Parameters.AddWithValue("@WUC", UserCode);
            cmd.Parameters.AddWithValue("@loginname", userid);
            cmd.Parameters.AddWithValue("@employeename", Name);
            cmd.Parameters.AddWithValue("@empCode", empCode);
            cmd.Parameters.AddWithValue("@computername", System.Net.Dns.GetHostName());
            cmd.Parameters.AddWithValue("@ipaddress", System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString());
            cmd.Parameters.AddWithValue("@sentto", Email);
            cmd.Parameters.AddWithValue("@msgbody", Body);
            cmd.Parameters.AddWithValue("@subjects", Subject);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
}