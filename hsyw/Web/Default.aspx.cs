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

public partial class Web_Default : System.Web.UI.Page
{
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    classUserOnline uo = new classUserOnline();

    protected void Page_Load(object sender, EventArgs e)
    {
        //导航栏=================================================================================
        Session["Msg"] = ""; Session["PageNavigator"] = ""; 
        //导航栏=================================================================================

        //设置当前在线位置=======================================================================
        uo.Update(Session["UserName"].ToString().Trim(), Session["PageNavigator"].ToString().Trim(), Session["PageSubTite"].ToString().Trim(), out strMsg);
        //=======================================================================================
        Response.Write(this.Title.ToString().Trim());
    }
}
