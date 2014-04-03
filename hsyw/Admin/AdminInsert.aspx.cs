using System;
using System.Collections;
using System.Configuration;
using System.Data;
////using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
////using System.Xml.Linq;

using System.Data.Common;
//using yueue.ADOKeycap;

public partial class Admin_AdminInsert : System.Web.UI.Page
{
    public string strWhere = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;
    classLog log = new classLog();
    classAdmin admin = new classAdmin();

    protected void Page_Load(object sender, EventArgs e)
    {
        //导航栏=================================================================================
        if (Session["UserGroup"].ToString().Trim() != "0")
        {
            strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("不允许你操作本模块！");
            Response.Redirect(strLink, false);
        } 
        Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "增加";
        Session["PageNavigator"] += "<a href='AdminList.aspx' target='_self'>管理员设置</a>";
        Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        //导航栏=================================================================================

        if (!IsPostBack)
        {

            RadioButtonList_IsUse.SelectedValue = "1";
            RadioButtonList_IsVisible.SelectedValue = "1";

            ZLTextBox_AdminName.Focus();

            DropBranch1.DataCode = "3213";
        }

    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        strLink = "AdminList.aspx";
        Response.Redirect(strLink, false);
    }
    protected void Button_Insert_Click(object sender, EventArgs e)
    {
        string UserName = ZLTextBox_AdminName.Text.ToString().Trim();
        if (DataFunction.HasRecord(string.Format("select * from T_SYS_ADMIN where UserName = '{0}'", UserName)))
        {
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('此用户名已存在，请重新填写！');</script>");
            ZLTextBox_AdminName.Text = string.Empty;
            return;
        }
        string UserRealName = ZLTextBox_AdminRealName.Text.ToString().Trim();
        string AdminPass = ZLTextBox_AdminPass.Text.ToString().Trim();
        string UserGroup = ZLTextBox_AdminGroup.Text.ToString().Trim();
        string BranchCode = DropBranch1.SelectedValue.ToString().Trim();

        string DisplayOrder = UserName;

        string IsUse = RadioButtonList_IsUse.SelectedValue.ToString().Trim();
        string IsVisible = RadioButtonList_IsVisible.SelectedValue.ToString().Trim();

        AdminPass = publ.MD5(AdminPass);

        n = admin.Insert(UserName, UserRealName, AdminPass, UserGroup, BranchCode, DisplayOrder, IsUse, IsVisible, out strMsg);
        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "增加管理员";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }

        Session["Msg"] = "<script>alert('" + strMsg + "');location.replace('AdminList.aspx');</script>";
        return;
    }
}
