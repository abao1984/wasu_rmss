using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Announcement_delete : System.Web.UI.Page
{
    public int id;
    public string title = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        id = int.Parse(Request.QueryString["id"]);
        string sql = String.Format("select * from ANNOUNCEMENTS where id={0}",id);
        DataRow row = DataFunction.GetSingleRow(sql);
        title = row["post_title"].ToString();
    }
    protected void buttonOK_Click(object sender, EventArgs e)
    {
        string sql = String.Format("delete from ANNOUNCEMENTS where id={0}",id);
        int result = DataFunction.ExecuteNonQuery(sql);
        Response.Redirect("Announcement_list.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Announcement_list.aspx");
    }
}
