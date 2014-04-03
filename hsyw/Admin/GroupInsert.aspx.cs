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

public partial class Web_sysGroup_GroupInsert : System.Web.UI.Page
{
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;
    
    classGroup group = new classGroup();
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        ////导航栏=================================================================================
        //if (Session["UserGroup"].ToString().Trim() != "0")
        //{
        //    strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("不允许你操作本模块！");
        //    Response.Redirect(strLink, false);
        //} 
        
        //Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "增加";
        //Session["PageNavigator"] += "<a href='GroupList.aspx' target='_self'>角色维护</a>";
        //Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        ////导航栏=================================================================================

        strSessCode = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());

        if (!IsPostBack)
        {

            RadioButtonList_IsUse.SelectedValue = "1";
            RadioButtonList_IsVisible.SelectedValue = "1";

            ZLTextBox_GroupCode.Text = group.GetGroupCode(strSessCode);
            ZLTextBox_GroupCode.Enabled = false;
        }

    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        strLink = "GroupList.aspx";
        Response.Redirect(strLink, false);
    }

    protected void Button_Insert_Click(object sender, EventArgs e)
    {
        string GroupCode = ZLTextBox_GroupCode.Text.ToString().Trim();
        if(DataFunction.HasRecord(string.Format("select * from T_SYS_GROUP where GROUPCODE = '{0}'",GroupCode)))
        {
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('此代码已存在，请重新填写！');</script>");
            ZLTextBox_GroupCode.Text = string.Empty;
            return;
        }
        string GroupName = ZLTextBox_GroupName.Text.ToString().Trim();
        string PGroupCode = strSessCode.ToString().Trim();

        string DisplayOrder = GroupCode;

        string IsUse = RadioButtonList_IsUse.SelectedValue.ToString().Trim();
        string IsVisible = RadioButtonList_IsVisible.SelectedValue.ToString().Trim();


        if (GroupCode == "0")
        {
            Session["Msg"] = "<script>alert('保留代码，请重新输入！');</script>";
            ZLTextBox_GroupCode.Focus();
            return;
        }

        n = group.Insert(GroupCode, GroupName, PGroupCode, DisplayOrder, IsUse, IsVisible,GROUPMS.Text, out strMsg);
        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "增加系统角色";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }

        Session["Msg"] = "<script>alert('" + strMsg + "');location.replace('GroupList.aspx');</script>";
        return;
    }
}
