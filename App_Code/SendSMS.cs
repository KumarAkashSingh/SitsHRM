using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for SendSMS
/// </summary>
public class SendSMS
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;
	public SendSMS()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void SMSSend(string MobNo, string Message1, string MSGUSERID, string MSGPASSWORD, string MSGSENDERID, string UserID, string UserName, string EmployeeName, string EmpCode)
    {
        //
        string url="";
        
        if (MSGSENDERID == "SDGIIN")
        {
            //url = "http://182.18.144.217/api/mt/SendSMS?APIKEY=" + MSGPASSWORD + "&senderid=" + MSGSENDERID + "&channel=Trans&DCS=0&flashsms=0&number=" + MobNo + "&text=" + Message1;
            url = "https://nstechindia.co.in/api/mt/SendSMS?user=" + MSGUSERID + "&password=" + MSGPASSWORD + "&senderid=" + MSGSENDERID + "&channel=Trans&DCS=0&flashsms=0&number=" + MobNo + "&text=" + Message1;
        }

        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        WebResponse response = null;
        //' set up request
        try
        {
            response = req.GetResponse();
            conn.Open();
            cmd = new SqlCommand("SP_Web_GetUserInfo", conn);
            cmd.Parameters.AddWithValue("@calltype", 77);
            cmd.Parameters.AddWithValue("@WUC", UserID);
            cmd.Parameters.AddWithValue("@loginname", UserName);
            cmd.Parameters.AddWithValue("@employeename", EmployeeName);
            cmd.Parameters.AddWithValue("@empCode", EmpCode);
            cmd.Parameters.AddWithValue("@computername", System.Net.Dns.GetHostName());
            cmd.Parameters.AddWithValue("@ipaddress", System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString());
            cmd.Parameters.AddWithValue("@sentto", MobNo);
            cmd.Parameters.AddWithValue("@msgbody", Message1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        catch (WebException ex)
        {
            Console.WriteLine(ex.Status);
        }
    }
}