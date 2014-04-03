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

public partial class Web_MainPassSelf : System.Web.UI.Page
{
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;
    classUser user = new classUser();
    classLog log = new classLog();
    classUserOnline uo = new classUserOnline();

    protected void Page_Load(object sender, EventArgs e)
    {
        //导航栏=================================================================================
        Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "更改密码";
        Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        //导航栏=================================================================================


        //设置当前在线位置=======================================================================
        //uo.Update(Session["UserName"].ToString().Trim(), "2", this.Title.ToString().Trim(), out strMsg);
        //uo.Update(Session["UserName"].ToString().Trim(), Session["PageNavigator"].ToString().Trim(), Session["PageSubTite"].ToString().Trim(), out strMsg);
        //=======================================================================================

       


        if (!IsPostBack)
        {
            try
            {
                strWhere = " and u.UserName = '" + Session["UserName"].ToString().Trim() + "' ";

                DataRow dr = DataFunction.GetSingleRow(user.GetQueryStr(strWhere));

                if (dr!=null)
                {

                    ZLTextBox_UserName.Text = dr["UserName"].ToString().Trim();
                    ZLTextBox_UserName.Enabled = false;

                    ZLTextBox_UserRealName.Text = dr["UserRealName"].ToString().Trim();
                    ZLTextBox_UserRealName.Enabled = false;
                    strLink = "";
                }
                else
                {

                    strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("参数传递错误！");

                }
            }
            catch (Exception errorMsg)
            {
                strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend(publ.GetCatchMsg(errorMsg.ToString().Trim()));
            }
            finally
            {
                if (strLink.Length > 0)
                {
                    Response.Redirect(strLink, false);
                }
            }


            ZLTextBox_UserPassOld.Focus();


        }

    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        string UserName = ZLTextBox_UserName.Text.ToString().Trim();
        string UserPass = ZLTextBox_UserPass.Text.ToString().Trim();
        string UserPassOld = ZLTextBox_UserPassOld.Text.ToString().Trim();

        UserPass = publ.MD5(UserPass);
        UserPassOld = publ.MD5(UserPassOld);

        n = user.UpdatePassSelf(UserName, UserPass, UserPassOld, out strMsg);

        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "修改自身密码";
                string LogMemo = "修改自身密码";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);
            }
            //写日志文件结束====================================================================
        }

        Session["Msg"] = "<script>alert('" + strMsg + "');</script>";


        ZLTextBox_UserPassOld.Focus();

        return;
    }

}
