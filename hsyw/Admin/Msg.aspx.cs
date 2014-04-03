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

public partial class Web_Msg : System.Web.UI.Page
{
    public string strLink = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        //导航栏=================================================================================
        Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "提示信息";
        Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        //导航栏=================================================================================
        string strMsg = publ.GetUrlToReceive(Request.QueryString["msg"].ToString());
        string strFlg = publ.GetUrlToReceive(Request.QueryString["code"].ToString());

        if (!IsPostBack)
        {
            if (strFlg == "0")                  //成功
            {
                Image_Msg.ImageUrl = "Images/MessageIcon/MessageOk.gif";
            }
            else if (strFlg == "1")             //出错
            {
                Image_Msg.ImageUrl = "Images/MessageIcon/MessageError.gif";
            }
            else if (strFlg == "2")             //警告
            {
                Image_Msg.ImageUrl = "Images/MessageIcon/MessageAlert.gif";
            }
            else                                //其它情况
            {
                Image_Msg.ImageUrl = "Images/MessageIcon/MessageAlert.gif";
            }

            Label_MSG.Text = strMsg;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        strLink = "Default.aspx";
        Response.Redirect(strLink, false);
    }
}
