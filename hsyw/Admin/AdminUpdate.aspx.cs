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

public partial class Admin_AdminUpdate : System.Web.UI.Page
{
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
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
        Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "编辑";
        Session["PageNavigator"] += "<a href='AdminList.aspx' target='_self'>管理员设置</a>";
        Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        //导航栏=================================================================================

        strSessCode = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());

        if (!IsPostBack)
        {
            DropBranch1.DataCode = Session["ClientCode"].ToString().Trim();

            strWhere = " and a.UserName = '" + strSessCode + "' ";

            DataRow dr = DataFunction.GetSingleRow(admin.GetQueryStr(strWhere));

            if (dr != null)
            {

                ZLTextBox_AdminName.Text = dr["UserName"].ToString().Trim();
                ZLTextBox_AdminName.Enabled = false;

                ZLTextBox_AdminRealName.Text = dr["UserRealName"].ToString().Trim();
                ZLTextBox_AdminGroup.Text = dr["UserGroup"].ToString().Trim();
                ZLTextBox_AdminGroup.Enabled = false;

                DropBranch1.Sel = dr["BranchCode"].ToString().Trim();

                ZLTextBox_DisplayOrder.Text = dr["DisplayOrder"].ToString().Trim();

                RadioButtonList_IsUse.SelectedValue = dr["IsUse"].ToString().Trim();
                RadioButtonList_IsVisible.SelectedValue = dr["IsVisible"].ToString().Trim();
            }
            else
            {
                strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("参数传递错误！");
                Response.Redirect(strLink, false);
            }

            ZLTextBox_AdminRealName.Focus();


        }

    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        strLink = "AdminList.aspx";
        Response.Redirect(strLink, false);
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        string UserName = ZLTextBox_AdminName.Text.ToString().Trim();
        string UserRealName = ZLTextBox_AdminRealName.Text.ToString().Trim();
        string UserGroup = ZLTextBox_AdminGroup.Text.ToString().Trim();

        string BranchCode = DropBranch1.SelectedValue.ToString().Trim();

        string DisplayOrder = ZLTextBox_DisplayOrder.Text.ToString().Trim(); ;

        string IsUse = RadioButtonList_IsUse.SelectedValue.ToString().Trim();
        string IsVisible = RadioButtonList_IsVisible.SelectedValue.ToString().Trim();

        n = admin.Update(UserName, UserRealName, UserGroup, BranchCode, DisplayOrder, IsUse, IsVisible, out strMsg);
        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "超级管理员编辑其它管理员";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }

        Session["Msg"] = "<script>alert('" + strMsg + "');location.replace('AdminList.aspx');</script>";
        return;
    }
    
}
