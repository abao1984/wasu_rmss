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

using System.Data.Common;
//using yueue.ADOKeycap;

public partial class Admin_LogPart : System.Web.UI.Page
{

    public string strWhere = "", strSql = "", strLink = "", strMsg = "", strSessCode = "";
    public int n = 0;
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        strSessCode = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());
        if (!IsPostBack)
        {
            strWhere = " and l.ID = '" + strSessCode + "' ";
            DataRow dr = DataFunction.GetSingleRow(log.GetQueryStr(strWhere));
            if (dr!= null)
            {
                Label_Title.Text = dr["Title"].ToString().Trim();
                Label_Ip.Text = dr["Ip"].ToString().Trim();
                Label_UserName.Text = dr["UserName"].ToString().Trim();
                Label_UserDateTime.Text = dr["UserDateTime"].ToString().Trim();
                TextBox_Memo.Text = dr["Memo"].ToString().Trim();
            }
            else
            {
                strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("参数传递错误！");
                Response.Redirect(strLink, false);
            }


        }
    }
    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        strLink = "LogList.aspx";
        Response.Redirect(strLink, false);
    }
}
