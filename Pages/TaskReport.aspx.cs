using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
public partial class Pages_TaskReport : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConnString.GetConnString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataTable dt = null;
    int count = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }
    }

    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeader = (CheckBox)GVDetails.HeaderRow.FindControl("chkHeader");

        foreach (GridViewRow row in GVDetails.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("chkCtrl");
            if (chkHeader.Checked)
            {
                cbx.Checked = true;
            }
            else
            {
                cbx.Checked = false;
            }
        }
    }

    protected DataTable FillGrid()
    {
        cmd = new SqlCommand("SP_TaskReport", conn);
        cmd.Parameters.AddWithValue("@calltype", 1);
        //cmd.Parameters.AddWithValue("@EmpCode", Session["e_code"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        da.Dispose();
        return dt;
    }

    protected void GVDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string ID = "";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            ID = GVDetails.Rows[RowIndex].Cells[1].Text;
            ViewState["ID"] = ID;
            GVDetails.DataSource = FillGrid();
            GVDetails.DataBind();
        }

    }   
}