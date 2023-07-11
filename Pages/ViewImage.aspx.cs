using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ViewImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Code = Request.QueryString[0].ToString();
        string Type = Request.QueryString[1].ToString();
        Image1.ImageUrl = "ImageHandler.ashx?e_code=" + Code + "&type=" + Type;
    }
}