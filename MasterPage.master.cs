using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class MasterPage : System.Web.UI.MasterPage
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    DataTable dt1 = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null /*|| Page.Request.UrlReferrer == null*/)
        {
            //Response.Redirect("Login.aspx");
        }
        if (!Page.IsPostBack)
        {
            GetMenu();
            GetPageLink();
        }

    }

    private void GetMenu()
    {
        //FillMenu();
        //litName.Text = Session["userName"].ToString();
        if (Session["TMobileMenu"] == null || Session["TDesktopMenu"] == null)
        {
            FillTopMenu();
        }
        else
        {
            //litMobileMenu.Text = Session["TMobileMenu"].ToString();
            //litDesktopMenu.Text = Session["TDesktopMenu"].ToString();
        }
        if (Session["MenuSession_RegistrarDesk"] == null)
        {
            FillMenu();
        }
        else
        {
            litMenu.Text = Session["MenuSession_RegistrarDesk"].ToString();
        }
    }
    //=================================//
    private void FillTopMenu()
    {
        string MobileMenu = "";
        string DesktopMenu = "";
        cmd = new SqlCommand("SP_GetUserMenu ", conn);
        cmd.Parameters.AddWithValue("@Calltype", 1);
        cmd.Parameters.AddWithValue("@UserCode", Session["UserCode"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        int i = 0;
        int Colorflag = 0;
        foreach (DataRow r in dt.Rows)
        {
            //MobileMenu += "<li style='background-color:" + Colours[Colorflag % 7].ToString() + "'><a class='' href='" + r["ModuleURL"].ToString() + "'>" + r["ModuleName"].ToString() + "</a></li>";
            if (i == 0)
            {
                DesktopMenu += "<tr>";
            }
            //DesktopMenu += "<td><div class='menutab' style='background-color:" + Colours[Colorflag % 7].ToString() + "'><a href=" + r["ModuleURL"].ToString() + "><i class='" + r["ModuleIcon"].ToString() + "'></i><h5>" + r["ModuleName"].ToString() + "</h5></a></div></td>";
            Colorflag++;
            if (i == 2)
            {
                DesktopMenu += "</tr>";
                i = -1;
            }
            i++;
        }
        if (i != 0)
        {
            DesktopMenu += "</tr>";
        }
        Session["TMobileMenu"] = MobileMenu;
        Session["TDesktopMenu"] = DesktopMenu;
        //litMobileMenu.Text = Session["TMobileMenu"].ToString();
        //litDesktopMenu.Text = Session["TDesktopMenu"].ToString();
    }

    //===================================//
    void FillMenu()
    {
        string MenuText = "";
        cmd = new SqlCommand("SP_GetUserMenu ", conn);
        cmd.Parameters.AddWithValue("@Calltype", 2);
        //cmd.Parameters.AddWithValue("@UserCode",1);//Session["e_code"].ToString()
        cmd.Parameters.AddWithValue("@UserCode", Session["e_code"].ToString());
        cmd.Parameters.AddWithValue("@ParentModule", 1);
        cmd.Parameters.AddWithValue("@Wmtype", "M");
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        int Colorflag = 0;
        foreach (DataRow r in dt.Rows)
        {
            cmd = new SqlCommand("SP_GetUserMenu ", conn);
            cmd.Parameters.AddWithValue("@Calltype", 2);
            cmd.Parameters.AddWithValue("@UserCode", Session["e_code"].ToString());
            cmd.Parameters.AddWithValue("@ParentModule", r["ModuleCode"].ToString());
            cmd.Parameters.AddWithValue("@Wmtype", "S");
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt1 = new DataTable();
            da.Fill(dt1);
            cmd.Dispose();
            da.Dispose();
            if (dt1.Rows.Count > 0)
            {
                MenuText += " <li id='ModuleCode_" + r["ModuleCode"].ToString() + "'><a href='" + r["ModuleURL"].ToString() + "'><i class='" + r["ModuleIcon"].ToString() + "'></i>" + r["ModuleName"].ToString() + "</a><ul class='nav child_menu'>";
                foreach (DataRow r1 in dt1.Rows)
                {
                    MenuText += "<li id='ModuleCode_" + r["ModuleCode"].ToString() + "'><a href='" + r1["ModuleURL"].ToString() + "'>" + r1["ModuleName"].ToString() + "</a></li>";
                }
                MenuText += "</ul></li>";
            }
            else
            {
                MenuText += " <li id='ModuleCode_" + r["ModuleCode"].ToString() + "'><a href='" + r["ModuleURL"].ToString() + "'><i class='" + r["ModuleIcon"].ToString() + "'></i>" + r["ModuleName"].ToString() + "</a></li>";
            }
            Colorflag++;
        }
        Session["MenuSession_HR"] = MenuText;
        litMenu.Text = MenuText;
    }

    private void GetPageLink()
    {
        string MenuURL = "";
        MenuURL = Request.ServerVariables[42].ToString();
        MenuURL = MenuURL.Substring(MenuURL.LastIndexOf('/') + 1, MenuURL.Length - MenuURL.LastIndexOf('/') - 1);
        if (!MenuURL.Contains(".aspx"))
        {
            MenuURL = MenuURL + ".aspx";
        }
        cmd = new SqlCommand("SP_GetUserMenu ", conn);
        cmd.Parameters.AddWithValue("@Calltype", 3);
        cmd.Parameters.AddWithValue("@MenuURL", MenuURL);
        //cmd.Parameters.AddWithValue("@UserCode", 1); //Session["UserCode"].ToString()
        cmd.Parameters.AddWithValue("@UserCode", Session["UserCode"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        if (dt.Rows.Count > 0)
        {
            litCurrentPath.Text = dt.Rows[0][0].ToString();
            //lblCurrentPageName.Text = dt.Rows[0][1].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "ActiveMenu('" + dt.Rows[0][2].ToString() + "');", true);
        }
    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        //LogOutAudit();
        try
        {
            var sessionobject = new List<string>();
            foreach (string key in Session.Keys)
                sessionobject.Add(key);
            foreach (var key in sessionobject)
                Session.Remove(key);
            Response.Redirect("../Login.aspx");
        }
        catch
        {
            Response.Redirect("../Login.aspx");
        }
    }
}
