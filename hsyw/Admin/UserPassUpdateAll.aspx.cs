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

public partial class Web_sysUser_UserPassUpdateAll : System.Web.UI.Page
{
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;
    classLog log = new classLog();
    classUser user = new classUser();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            ZLTextBox_UserPass.Focus();


        }

    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        //在上个页面中已被赋值
        string UserName = publ.GetUrlToReceive(Request.QueryString["code"].Trim());
        string UserPass = ZLTextBox_UserPass.Text.ToString().Trim();
        
        UserPass = publ.MD5(UserPass);

        n = user.UpdatePassAll(UserName, UserPass, out strMsg);
        //写日志文件开始====================================================================
        if (Session["BoolLog"].ToString().Trim() == "1")
        {
            string LogStrMsg = "";
            //LogUserName-人员 LogTitle-标题  LogMemo-内容
            string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
            LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
            string LogTitle = "批量设置用户密码";
            string LogMemo = "";
            log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

        }
        //写日志文件结束====================================================================
        Session["Msg"] = "<script>alert('" + strMsg + "');location.replace('UserList.aspx');</script>";
        return;
    }

}
