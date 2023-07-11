using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for UserHistory
/// </summary>
public static class UserHistory
{
    public static void Log(string methodName, string user, string message, string IP)
    {
        //execute SQL INSERT
    }
    public static string GetIPAddress()
    {
        string IPAddress = string.Empty;
        IPHostEntry Host = default(IPHostEntry);
        string Hostname = null;
        Hostname = System.Environment.MachineName;
        Host = Dns.GetHostEntry(Hostname);
        foreach (IPAddress IP in Host.AddressList)
        {
            if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                IPAddress = Convert.ToString(IP);
            }
           
        }
        return IPAddress;
    }
}