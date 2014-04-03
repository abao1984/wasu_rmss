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

public partial class Web_sysData_DataUpdate : System.Web.UI.Page
{
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;
    classData data = new classData();
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        ////导航栏=================================================================================
        //if (Session["UserGroup"].ToString().Trim() != "0")
        //{
        //    strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("不允许你操作本模块！");
        //    Response.Redirect(strLink, false);
        //} 
        
        //Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "编辑";
        //Session["PageNavigator"] += "<a href='DataList.aspx' target='_self'>数据字典</a>";
        //Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        ////导航栏=================================================================================

        strSessCode = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());

        if (!IsPostBack)
        {
            MENU.Attributes.Add("readonly", "true");

            try
            {
                strWhere = " and d.ID = '" + strSessCode + "' ";

                DataRow dr = DataFunction.GetSingleRow(data.GetQueryStr(strWhere));

                if (dr != null)
                {

                    ZLTextBox_DataCode.Text = dr["DataCode"].ToString().Trim();
                    ZLTextBox_DataCode.Enabled = false;
                    ZLTextBox_DataName.Text = dr["DataName"].ToString().Trim();
                    ZLTextBox_DataName.Enabled = false;

                    ZLTextBox_DataDm.Text = dr["DataDm"].ToString().Trim();
                    ZLTextBox_DataDm.Enabled = false;
                    
                    ZLTextBox_DataMc.Text = dr["DataMc"].ToString().Trim();

                    ZLTextBox_DisplayOrder.Text = dr["DisplayOrder"].ToString().Trim();

                    RadioButtonList_IsUse.SelectedValue = dr["IsUse"].ToString().Trim();
                    RadioButtonList_IsVisible.SelectedValue = dr["IsVisible"].ToString().Trim();
                    if (dr["MENU"] != DBNull.Value)
                        MENU.Text = dr["MENU"].ToString();
                    if (dr["ZDZ"] != DBNull.Value)
                        ZDZ.Text = dr["ZDZ"].ToString();
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



            ZLTextBox_DataMc.Focus();


        }

    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        strLink = "DataSubList.aspx?code=" + publ.GetUrlToSend(ZLTextBox_DataCode.Text.ToString().Trim());
        Response.Redirect(strLink, false);
    }
    protected void Button_Update_Click(object sender, EventArgs e)
    {
        string ID = strSessCode;
        string DataCode = ZLTextBox_DataCode.Text.ToString().Trim();
        string DataName = ZLTextBox_DataName.Text.ToString().Trim();
        string DataDm = ZLTextBox_DataDm.Text.ToString().Trim();
        string DataMc = ZLTextBox_DataMc.Text.ToString().Trim();
        string menu = MENU.Text;

        string DisplayOrder = ZLTextBox_DisplayOrder.Text.ToString().Trim(); ;

        string IsUse = RadioButtonList_IsUse.SelectedValue.ToString().Trim();
        string IsVisible = RadioButtonList_IsVisible.SelectedValue.ToString().Trim();

        n = data.Update(ID, DataMc, DisplayOrder, IsUse, IsVisible, menu,ZDZ.Text, out strMsg);
        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "编辑数据字典字段";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }

        Session["Msg"] = "<script>alert('" + strMsg + "');location.replace('DataSubList.aspx?code=" + publ.GetUrlToSend(ZLTextBox_DataCode.Text.ToString().Trim()) + "');</script>";
        return;
    }
}
