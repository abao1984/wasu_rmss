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

public partial class Web_sysUser_UserInsert : System.Web.UI.Page
{
    public string strWhere = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;
    classUser user = new classUser();
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            RadioButtonList_IsUse.SelectedValue = "1";
            RadioButtonList_IsVisible.SelectedValue = "1";

            ZLTextBox_UserName.Focus();

            DropBranch1.DataCode = Session["BranchCode"].ToString().Trim();
        }

    }

    protected void Button_Insert_Click(object sender, EventArgs e)
    {
        string UserName = ZLTextBox_UserName.Text.ToString().Trim();
        if(DataFunction.HasRecord(string.Format("select USERNAME from T_SYS_USER where USERNAME='{0}'",UserName)))
        {
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('该用户名已经存在，请重新输入！');</script>");
            return;
        }
        string UserRealName = ZLTextBox_UserRealName.Text.ToString().Trim();
        string UserId = ZLTextBox_UserId.Text.ToString().Trim();
        string UserPass = ZLTextBox_UserPass.Text.ToString().Trim();
        string BranchCode = DropBranch1.SelectedValue.ToString().Trim();

        string DisplayOrder = UserName;

        string UserPhone = ZLTextBox_UserPhone.Text.ToString().Trim();
        string UserRegIp = publ.GetClientIP();
        
        string IsUse = RadioButtonList_IsUse.SelectedValue.ToString().Trim();
        string IsVisible = RadioButtonList_IsVisible.SelectedValue.ToString().Trim();

        UserPass = publ.MD5(UserPass);

        n = user.Insert(UserName, UserRealName, UserId, UserPass, BranchCode, UserRegIp, UserPhone, DisplayOrder, IsUse, IsVisible, out strMsg);
        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "增加用户";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }

        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('操作成功！');</script>");
    }
}
