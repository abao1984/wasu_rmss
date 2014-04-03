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
////using System.Xml.Linq;

using System.Data.Common;
//using yueue.ADOKeycap;

public partial class Admin_AdminPassSelf : System.Web.UI.Page
{
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;

    classAdmin admin = new classAdmin();
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        //导航栏=================================================================================
        Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "更改密码";
        Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        //导航栏=================================================================================

        
        if (!IsPostBack)
        {

            strWhere = " and a.UserName = '" + Session["UserName"].ToString().Trim() + "' ";

            DataRow dr = DataFunction.GetSingleRow(admin.GetQueryStr(strWhere));

            if (dr != null)
            {

                ZLTextBox_AdminName.Text = dr["UserName"].ToString().Trim();
                ZLTextBox_AdminName.Enabled = false;

                ZLTextBox_AdminRealName.Text = dr["UserRealName"].ToString().Trim();
                ZLTextBox_AdminRealName.Enabled = false;
            }
            else
            {
                strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("参数传递错误！");
                Response.Redirect(strLink, false);
            }

            ZLTextBox_AdminPassOld.Focus();


        }

    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        string UserName = ZLTextBox_AdminName.Text.ToString().Trim();
        string AdminPass = ZLTextBox_AdminPass.Text.ToString().Trim();
        string AdminPassOld = ZLTextBox_AdminPassOld.Text.ToString().Trim();

        AdminPass = publ.MD5(AdminPass);
        AdminPassOld = publ.MD5(AdminPassOld);

        n = admin.UpdatePassSelf(UserName, AdminPass, AdminPassOld, out strMsg);
        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "管理员修改自己密码";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }
        Session["Msg"] = "<script>alert('" + strMsg + "');</script>";


        ZLTextBox_AdminPassOld.Focus();

        return;
    }

}
