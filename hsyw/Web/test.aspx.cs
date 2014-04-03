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

public partial class Web_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //导航栏=================================================================================
        Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "测试页面111";
        Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        //导航栏=================================================================================

    }
}
