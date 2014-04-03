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

public partial class Web_sysMenu_MenuUpdate : System.Web.UI.Page
{
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;
    classLog log = new classLog();
    classMenu menu = new classMenu();

    protected void Page_Load(object sender, EventArgs e)
    {
        ////导航栏=================================================================================
        //if (Session["UserGroup"].ToString().Trim() != "0")
        //{
        //    strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("不允许你操作本模块！");
        //    Response.Redirect(strLink, false);
        //}
        //Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "编辑";
        //Session["PageNavigator"] += "<a href='MenuList.aspx?SJGGG="+Session["SJGGG"]+"' target='_self'>菜单维护</a>";
        //Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        ////导航栏=================================================================================

        strSessCode = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());

        if (!IsPostBack)
        {

            strWhere = " and m.MenuCode = '" + strSessCode + "' ";
            try
            {
                DataRow dr = DataFunction.GetSingleRow(menu.GetQueryStr(strWhere));

                if (dr != null)
                {

                    ZLTextBox_MenuCode.Text = dr["MenuCode"].ToString().Trim();
                    ZLTextBox_MenuCode.Enabled = false;

                    ZLTextBox_MenuName.Text = dr["MenuName"].ToString().Trim();
                    ZLTextBox_FileName.Text = dr["FileName"].ToString().Trim();
                    ZLTextBox_Ico.Text = dr["Ico"].ToString().Trim();
                    ZLTextBox_DisplayOrder.Text = dr["DisplayOrder"].ToString().Trim();

                    RadioButtonList_IsExpand.SelectedValue = dr["IsExpand"].ToString().Trim();
                    RadioButtonList_IsUse.SelectedValue = dr["IsUse"].ToString().Trim();
                    RadioButtonList_IsVisible.SelectedValue = dr["IsVisible"].ToString().Trim();

                    Session["TEMP1"] = dr["PMenuCode"].ToString().Trim();

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

            ZLTextBox_MenuName.Focus();

            
        }

    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        strLink = "MenuList.aspx?SJGGG="+Session["SJGGG"];
        Response.Redirect(strLink, false);
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        string MenuCode = ZLTextBox_MenuCode.Text.ToString().Trim();
        string MenuName = ZLTextBox_MenuName.Text.ToString().Trim();
        string PMenuCode = Session["TEMP1"].ToString().Trim();
        string FileName = ZLTextBox_FileName.Text.ToString().Trim();
        string Ico = ZLTextBox_Ico.Text.ToString().Trim();
        string DisplayOrder = ZLTextBox_DisplayOrder.Text.ToString().Trim(); ;

        string IsExpand = RadioButtonList_IsExpand.SelectedValue.ToString().Trim();
        string IsUse = RadioButtonList_IsUse.SelectedValue.ToString().Trim();
        string IsVisible = RadioButtonList_IsVisible.SelectedValue.ToString().Trim();

        n = menu.Update(MenuCode, MenuName, PMenuCode, FileName, Ico, DisplayOrder, IsExpand, IsUse, IsVisible, out  strMsg);
        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "编辑菜单";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }
        Session["Msg"] = "<script>alert('" + strMsg + "');location.replace('MenuList.aspx?SJGGG="+Session["SJGGG"]+"');</script>";
        return;
    }
}
