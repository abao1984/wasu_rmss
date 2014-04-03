using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

public partial class Web_About : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //导航栏=================================================================================
        Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "关于我们";
        Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        //导航栏=================================================================================


        Label1.Text = "软件名称：" + Session["PageTitle"].ToString().Trim();
        Label2.Text = "使用单位：" + Session["ClientName"].ToString().Trim();
        Label3.Text = "软件版本：" + Session["PageVersion"].ToString().Trim();

        Label4.Text = Session["CopyRightCompany"].ToString().Trim();
        Label5.Text = "制 作 人：" + Session["CopyRightAuthor"].ToString().Trim();
        Label6.Text = "联系电话：" + Session["CopyRightPhone"].ToString().Trim();
        Label7.Text = "联系QQ：" + Session["CopyRightQQ"].ToString().Trim();
        
    }
}
