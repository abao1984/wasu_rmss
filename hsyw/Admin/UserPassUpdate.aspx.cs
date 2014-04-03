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

public partial class Web_sysUser_UserPassUpdate : System.Web.UI.Page
{
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;
    classLog log = new classLog();
    classUser user = new classUser();

    protected void Page_Load(object sender, EventArgs e)
    {
       strSessCode = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());

        if (!IsPostBack)
        {

            strWhere = " and u.UserName = '" + strSessCode + "' ";

            DataRow dr = DataFunction.GetSingleRow(user.GetQueryStr(strWhere));

            if (dr != null)
            {

                ZLTextBox_UserName.Text = dr["UserName"].ToString().Trim();
                ZLTextBox_UserName.Enabled = false;

                ZLTextBox_UserRealName.Text = dr["UserRealName"].ToString().Trim();
                ZLTextBox_UserRealName.Enabled = false;
            }
            else
            {
                strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("参数传递错误！");
                Response.Redirect(strLink, false);
            }

            ZLTextBox_UserPass.Focus();


        }

    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        string UserName = ZLTextBox_UserName.Text.ToString().Trim();
        string UserPass = ZLTextBox_UserPass.Text.ToString().Trim();

        UserPass = publ.MD5(UserPass);

        n = user.UpdatePass(UserName, UserPass, out strMsg);
        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "管理员修改用户密码";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('操作成功！');</script>");
    }

}
