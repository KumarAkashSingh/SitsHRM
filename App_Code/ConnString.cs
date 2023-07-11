using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConnString
/// </summary>
public class ConnString
{
	public ConnString()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string GetConnString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }
}