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

public partial class Web_sysMenu_MenuInsert : System.Web.UI.Page
{
    public string BY_WHERE = "", str_sess_code = "", str_sql = "", str_link = "", strMsg = "";
    public int n = 0;
    classLog log = new classLog();
    classMenu menu = new classMenu();

    protected void Page_Load(object sender, EventArgs e)
    {
      
        str_sess_code = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());

        if (!IsPostBack)
        {

            RadioButtonList_IsExpand.SelectedValue = "1";
            RadioButtonList_IsUse.SelectedValue = "1";
            RadioButtonList_IsVisible.SelectedValue = "1";

            ZLTextBox_MenuCode.Text = menu.GetMenuCode(str_sess_code);
        }

    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        str_link = "MenuList.aspx?SJGGG="+Session["SJGGG"];
        Response.Redirect(str_link, false);
    }

    protected void Button_Insert_Click(object sender, EventArgs e)
    {
        string MenuCode = ZLTextBox_MenuCode.Text.ToString().Trim();
        string MenuName = ZLTextBox_MenuName.Text.ToString().Trim();
        string PMenuCode = str_sess_code.ToString().Trim();
        string FileName = ZLTextBox_FileName.Text.ToString().Trim();
        string Ico = ZLTextBox_Ico.Text.ToString().Trim();
        string DisplayOrder = MenuCode;

        string IsExpand = RadioButtonList_IsExpand.SelectedValue.ToString().Trim();
        string IsUse = RadioButtonList_IsUse.SelectedValue.ToString().Trim();
        string IsVisible = RadioButtonList_IsVisible.SelectedValue.ToString().Trim();

        n = menu.Insert(MenuCode, MenuName, PMenuCode, FileName, Ico, DisplayOrder, IsExpand, IsUse, IsVisible, out  strMsg);
        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "增加菜单";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }

        Session["Msg"] = "<script>alert('" + strMsg + "');location.replace('MenuList.aspx?SJGGG="+Session["SJGGG"]+"');</script>";
        return;
    }

}
